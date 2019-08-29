using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            IActionResult result = null;

            this.Cuisines = this.htmlHelper?.GetEnumSelectList<CuisineType>();

            if (restaurantId.HasValue)
            {
                this.Restaurant = this.restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                this.Restaurant = new Restaurant();
            }

            if (this.Restaurant == null)
            {
                result = RedirectToPage("./NotFound");
            }
            else
            {
                result = Page();
            }

            return result;
        }

        public IActionResult OnPost()
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                if (this.Restaurant.Id <= 0)
                {
                    this.Restaurant = restaurantData.Add(this.Restaurant);
                }
                else
                {
                   this.Restaurant = restaurantData.Update(this.Restaurant);
                }
                this.restaurantData.Commit();

                TempData["Message"] = "Restaurant saved!"; //Displays on the re-direct page.

                //When an update is done from a Post it is best to redirect off of the edit page to prevent 
                //the user from refreshing the page which re-sends the update.
                result = RedirectToPage("./Detail", new { restaurantId = this.Restaurant.Id });
            }
            else
            {
                result = Page();
            }

            this.Cuisines = this.htmlHelper?.GetEnumSelectList<CuisineType>();

            return result;
        }
    }
}