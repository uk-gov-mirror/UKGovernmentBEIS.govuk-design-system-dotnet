using System;
using System.Linq;
using System.Threading.Tasks;
using GovUkDesignSystem.Attributes.DataBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GovUkDesignSystem.ModelBinders;

/// <summary>
/// It's not expected to need to use this as a model binder, it is added implicitly when adding a <see cref="GovUkDataBindingExpectPlaceholderAttribute"/> attribute.
/// Add that attribute instead.
/// </summary>
public class GovUkValueWithPlaceholderBinder : IModelBinder
{
    public const string DefaultPlaceholder = "choose";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var placeholderAttribute = bindingContext.ModelMetadata.ValidatorMetadata
            .OfType<GovUkDataBindingExpectPlaceholderAttribute>().SingleOrDefault();
        var placeHolder = placeholderAttribute?.Placeholder ?? DefaultPlaceholder;

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        bindingContext.Result = ModelBindingResult.Success(value == placeHolder ? null : value);

        return Task.CompletedTask;
    }
}