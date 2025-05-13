using System;
using GovUkDesignSystem.ModelBinders;

namespace GovUkDesignSystem.Attributes.DataBinding;

/// <summary>
/// Attribute used to specify a placeholder value on a view model.
/// When combined with <see cref="GovUkValueWithPlaceholderBinder"/> it will bind the value as `null` if the value specified here is submitted.
/// </summary>
/// <param name="placeholder">Placeholder value that will be interpreted as null</param>
[AttributeUsage(AttributeTargets.Property)]
public class GovUkDataBindingExpectedPlaceholderAttribute(string placeholder) : Attribute
{
    public readonly string Placeholder = placeholder;
}