using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Ratting.WepAPI.Models.FinishBattleModel;

public class FinishBattleDtoModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
        {
            var body = await reader.ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<FinishBattleDto>(body);

            if (model == null)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Unable to bind FinishBattleDto");
                return;
            }

            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}