using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVCDemoLab.Models
{
    public class ProductModelBinder : IModelBinder
    {
        //ModelBinder --- >HttpContext   Request Body 
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            string ProductId = bindingContext.HttpContext.Request.Form["ProductId"];
            string Name = bindingContext.HttpContext.Request.Form["Name"];
            string Price = bindingContext.HttpContext.Request.Form["Price"];
            string Description = bindingContext.HttpContext.Request.Form["Description"];
            string ImagePath = bindingContext.HttpContext.Request.Form["ImagePath"];
            string CategotyId = bindingContext.HttpContext.Request.Form["CategotyId"];

            //Check the Categoty
            if (int.Parse(CategotyId) == 0)
            {
                bindingContext.ModelState.AddModelError("CategotyId", "Must Select the Category");
            }
            //create New Price 
            decimal newPrice = Convert.ToDecimal(Price) + 100m;
            int Id;
            int.TryParse(ProductId, out Id);
            Product newProduct = new Product
            {
                ProductId = Id,
                Name = Name,
                Price = newPrice,
                Description = Description,
                ImagePath = ImagePath ?? string.Empty,
                CategotyId = int.Parse(CategotyId)
            };
            bindingContext.Result = ModelBindingResult.Success(newProduct);
            return Task.CompletedTask;
        }
    }
}
