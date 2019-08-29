using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            this.restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Chi Chi's", Location = "Sandusky", Cuisine = CuisineType.Mexican },
                new Restaurant { Id = 3, Name = "Indian", Location = "India", Cuisine = CuisineType.Indian },
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            this.restaurants.Add(newRestaurant);
            newRestaurant.Id = this.restaurants.Max(r => r.Id) + 1;

            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = this.GetById(id);
            if (restaurant != null)
            {
                this.restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return this.restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return this.restaurants
                   .Where(r => string.IsNullOrEmpty(name) || r.Name.StartsWith(name))
                   .OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            Restaurant selectedRestaurant = this.restaurants.Where(r => r.Id == updatedRestaurant.Id).SingleOrDefault();
            if (selectedRestaurant != null)
            {
                selectedRestaurant.Name = updatedRestaurant.Name;
                selectedRestaurant.Location = updatedRestaurant.Location;
                selectedRestaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return selectedRestaurant;
        }
    }
}