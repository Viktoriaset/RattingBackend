using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Ratting.WepAPI.Models.FindBattleModel;

public class FindBattleDtoModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(FindBattleDto))
        {
            return new BinderTypeModelBinder(typeof(FindBattleDtoModelBinder));
        }

        return null;
    }
}