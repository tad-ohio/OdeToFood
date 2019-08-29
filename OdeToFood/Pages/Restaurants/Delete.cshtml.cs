using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            IActionResult result = null;

            this.Restaurant = this.restaurantData.GetById(restaurantId);
            if (this.Restaurant != null)
            {
                result = this.Page();
            }
            else
            {
                result = this.RedirectToPage("./NotFound");
            }

            return result;
        }

        public IActionResult OnPost(int restaurantId)
        {
            IActionResult result = null;

            Restaurant restaurant = this.restaurantData.Delete(restaurantId);
            this.restaurantData.Commit();

            if (restaurant == null)
            {
                result = this.RedirectToPage("./NotFound");
            }
            else
            {
                this.TempData["Message"] = $"{restaurant.Name} deleted";

                result = this.RedirectToPage("./List");
            }

            return result;
        }
    }
}