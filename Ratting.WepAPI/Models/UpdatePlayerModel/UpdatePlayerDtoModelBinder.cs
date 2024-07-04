using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Ratting.WepAPI.Models.UpdatePlayerModel;

public class UpdatePlayerDtoModelBinder: IModelBinder
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
            var model = JsonConvert.DeserializeObject<UpdatePlayerDto>(body);

            if (model == null)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Unable to bind UpdatePlayerDto");
                return;
            }

            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}