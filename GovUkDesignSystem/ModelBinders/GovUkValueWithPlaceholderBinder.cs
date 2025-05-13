using System;
using System.Linq;
using System.Threading.Tasks;
using GovUkDesignSystem.Attributes.DataBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GovUkDesignSystem.ModelBinders;

/// <summary>
/// Binder used to specify a placeholder value on a view model.
/// When combined with <see cref="GovUkDataBindingExpectedPlaceholderAttribute"/> it will bind the value as `null` if the specified placeholder is submitted.
/// Useful for placeholders in select inputs in conjunction with requirement checks, null will be interpreted as not submitted in validators.
/// </summary>
public class GovUkValueWithPlaceholderBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var placeholderAttribute = bindingContext.ModelMetadata.ValidatorMetadata
            .OfType<GovUkDataBindingExpectedPlaceholderAttribute>().SingleOrDefault();
        if (placeholderAttribute == null)
        {
            throw new Exception(
                "When using the GovUkValueWithPlaceholderBinder you must also provide a GovUkDataBindingExpectedPlaceholderAttribute attribute and ensure that you register GovUkDataBindingExpectedPlaceholderProvider in your application's Startup.ConfigureServices method.");
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        bindingContext.Result = ModelBindingResult.Success(value == placeholderAttribute.Placeholder ? null : value);

        return Task.CompletedTask;
    }
}