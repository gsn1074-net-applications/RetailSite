using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RetailSite.Products.Api.DTO.Backend;
using RetailSite.Products.Api.DAL.Contexts;
using RetailSite.Products.Api.DAL.Entities;

namespace RetailSite.Products.Api.DAL.Repositories
{
	public class ProductsRepository : IProductsRepository, IDisposable
	{
		private ProductsContext _context;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<ProductsRepository> _logger;
		private CancellationTokenSource _cancellationTokenSource;

		public ProductsRepository(ProductsContext context, IHttpClientFactory httpClientFactory, ILogger<ProductsRepository> logger)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		//read

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			//TODO: remove - used for load testing
			await _context.Database.ExecuteSqlCommandAsync("WAITFOR DELAY '00:00:02';");
			return await _context.Products.Include(p => p.Category).ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<int> ids)
		{
			return await _context.Products.Where(p => ids.Contains(p.Id)).Include(p => p.Category).ToListAsync();
		}

		public async Task<Product> GetProductAsync(int id)
		{
			return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<ProductImage> GetProductImageAsync(string imageId)
		{
			var httpClient = _httpClientFactory.CreateClient();

			var response = await httpClient.GetAsync($"https://localhost:44339/api/productimages/{imageId}");  //TODO: move path to config

			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<ProductImage>(await response.Content.ReadAsStringAsync());
			}

			return null;
		}

		public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_cancellationTokenSource = new CancellationTokenSource();
			
			var productImageUrls = new [] //TODO: get path from config
			{
				$"https://localhost:44339/api/productimages/{productId}-large-main",
				$"https://localhost:44339/api/productimages/{productId}-large-thumbnail1",
				//$"https://localhost:44339/api/productimages/{productId}-large-thumbnail2?returnFault=true",
				$"https://localhost:44339/api/productimages/{productId}-large-thumbnail3",
				$"https://localhost:44339/api/productimages/{productId}-large-thumbnail4",
			};

			var downloadProductImageTasksQuery = from productImageUrl in productImageUrls select DownloadProductImageAsync(httpClient, productImageUrl, _cancellationTokenSource.Token);

			var downloadTasks = downloadProductImageTasksQuery.ToList();

			try
			{
				return await Task.WhenAll(downloadTasks);
			}
			catch(OperationCanceledException ex)
			{
				_logger.LogInformation($"{ex.Message}");

				foreach(var task in downloadTasks)
				{
					_logger.LogInformation($"Task {task.Id} has status {task.Status}");
				}

				return new List<ProductImage>();
			}
			catch(Exception ex)
			{
				_logger.LogError($"{ex.Message}");
				throw;
			}
		}

		private async Task<ProductImage> DownloadProductImageAsync(HttpClient httpClient, string url, CancellationToken cancellationToken)
		{
			var response = await httpClient.GetAsync(url, cancellationToken);

			if(response.IsSuccessStatusCode)
			{
				var image = JsonConvert.DeserializeObject<ProductImage>(await response.Content.ReadAsStringAsync());
				return image;
			}

			_cancellationTokenSource.Cancel();

			return null;
		}

		//create - no async

		public void AddProduct(Product product) 
		{
			if(product == null)
			{
				throw new ArgumentNullException(nameof(product));
			}

			_context.Add(product);
		}

		//delete - no async

		public void DeleteProduct(Product product) 
		{
			if(product == null)
			{
				throw new ArgumentNullException(nameof(product));
			}

			_context.Products.Remove(product);
		}

		//save
		public async Task<bool> SaveChangesAsync() 
		{
			return (await _context.SaveChangesAsync() > 0);
		}

		//dispose

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(_context != null)
				{
					_context.Dispose();
					_context = null;
				}
				if(_cancellationTokenSource != null)
				{
					_cancellationTokenSource.Dispose();
					_cancellationTokenSource = null;
				}
			}
		}
	}
}
