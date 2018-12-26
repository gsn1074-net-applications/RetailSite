using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RetailSite.Products.Api.Mapping.ModelBinders
{
	public class ArrayModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			//only process IEnumerables
			if(!bindingContext.ModelMetadata.IsEnumerableType)
			{
				bindingContext.Result = ModelBindingResult.Failed();
				return Task.CompletedTask;
			}

			//get input value
			var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

			//return null for null or whitespace values
			if(string.IsNullOrWhiteSpace(value))
			{
				bindingContext.Result = ModelBindingResult.Success(null);
				return Task.CompletedTask;
			}

			//get the type of the enumerable and a converter
			var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
			var converter = TypeDescriptor.GetConverter(elementType);

			var values = value.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => converter.ConvertFromString(x.Trim())).ToArray();

			//create an array of the appropriate type and set it as the model value
			var typedValues = Array.CreateInstance(elementType, values.Length);
			values.CopyTo(typedValues, 0);
			bindingContext.Model = typedValues;

			//return success passing in the model
			bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
			return Task.CompletedTask;
		}
	}
}
