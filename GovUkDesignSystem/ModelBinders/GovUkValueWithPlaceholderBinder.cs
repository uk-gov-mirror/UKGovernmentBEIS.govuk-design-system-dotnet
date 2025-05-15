using System;
using System.Linq;
using System.Threading.Tasks;
using GovUkDesignSystem.Attributes.DataBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GovUkDesignSystem.ModelBinders;

/// <summary>
/// Binder used to specify a placeholder value on a view model.
/// When the binded value equals the placeholder, the value will be instead bound as <c>null</c>. This can be interpreted as not present by validators.
/// See <see cref="DefaultPlaceholder"/> for the default placeholder value this binder is expecting.
/// <br/>
/// If desired for the page markup, there is the option to specify a custom placeholder value. As we cannot pass arguments to the binder, an additional attribute must be added to the view model property. For example:
/// <code>
/// [ModelBinder(typeof(GovUkValueWithPlaceholderBinder))]
/// [GovUkDataBindingExpectedPlaceholder("expected placeholder value")]
/// public string SelectedAddressIndex { get; set; }
/// </code>
/// You must also register a provider for this attribute in the startup of your app if using a custom placeholder, for instance in <c>Startup.cs</c>
/// <code>
/// services.AddControllersWithViews(options =>
/// {
///     options.ModelMetadataDetailsProviders.Add(new GovUkDataBindingExpectedPlaceholderProvider());
///     ...
/// })
/// </code>
/// </summary>
public class GovUkValueWithPlaceholderBinder : IModelBinder
{
    public const string DefaultPlaceholder = "choose";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var placeholderAttribute = bindingContext.ModelMetadata.ValidatorMetadata
            .OfType<GovUkDataBindingExpectedPlaceholderAttribute>().SingleOrDefault();
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