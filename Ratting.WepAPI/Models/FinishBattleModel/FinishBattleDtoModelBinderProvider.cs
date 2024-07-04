using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Ratting.WepAPI.Models.FinishBattleModel;

public class FinishBattleDtoModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(FinishBattleDto))
        {
            return new BinderTypeModelBinder(typeof(FinishBattleDtoModelBinder));
        }

        return null;
    }
}