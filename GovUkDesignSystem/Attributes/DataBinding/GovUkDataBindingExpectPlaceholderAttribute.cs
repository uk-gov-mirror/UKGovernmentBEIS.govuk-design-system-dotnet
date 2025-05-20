using System;
using GovUkDesignSystem.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace GovUkDesignSystem.Attributes.DataBinding;

/// <summary>
/// Attribute used to specify a placeholder value on a view model.
/// When the bound value equals the placeholder, the value will be instead bound as <c>null</c>. This can be interpreted as not present by validators.
/// See <see cref="GovUkValueWithPlaceholderBinder.DefaultPlaceholder"/> for the default placeholder value this binder is expecting if one is not passed here.
/// <br/>
/// If specifying a custom placeholder you must also register a provider for this attribute, for instance in <c>Startup.cs</c>
/// <code>
/// services.AddControllersWithViews(options =>
/// {
///     options.ModelMetadataDetailsProviders.Add(new GovUkDataBindingExpectPlaceholderProvider());
///     ...
/// })
/// </code>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class GovUkDataBindingExpectPlaceholderAttribute(string placeholder = null) : ModelBinderAttribute(typeof(GovUkValueWithPlaceholderBinder))
{
    public readonly string Placeholder = placeholder;
}