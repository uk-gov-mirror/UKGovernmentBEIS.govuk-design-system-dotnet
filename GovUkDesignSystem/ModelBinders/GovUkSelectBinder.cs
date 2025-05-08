using System.Threading.Tasks;
using GovUkDesignSystem.GovUkDesignSystemComponents;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GovUkDesignSystem.ModelBinders;

public class GovUkSelectBinder : IModelBinder
{
    
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;
        
        bindingContext.Result = ModelBindingResult.Success(value switch
        {
            SelectViewModel.SELECT_PLACEHOLDER_VALUE => null,
            _ => value
        });
        
        return Task.CompletedTask;
    }
}