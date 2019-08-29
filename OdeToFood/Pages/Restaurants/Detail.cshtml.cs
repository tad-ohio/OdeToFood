using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            IActionResult result = null;

            this.Restaurant = this.restaurantData.GetById(restaurantId);
            if (Restaurant == null)
            {
                result = RedirectToPage("./NotFound");
            }
            else
            {
                result = Page();
            }

            return result;
        }
    }
}