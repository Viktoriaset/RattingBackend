using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Ratting.WepAPI.Models.UpdatePlayerModel;

public class UpdatePlayerDtoModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(UpdatePlayerDto))
        {
            return new BinderTypeModelBinder(typeof(UpdatePlayerDtoModelBinder));
        }

        return null;
    }
}