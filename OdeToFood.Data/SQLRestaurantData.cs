using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SQLRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext dbContext;

        public SQLRestaurantData(OdeToFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            this.dbContext.Add(newRestaurant);

            return newRestaurant;
        }

        public int Commit()
        {
            return this.dbContext.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = this.GetById(id);
            if (restaurant != null)
            {
                this.dbContext.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return this.dbContext.Restaurants.Find(id);
        }

        public int GetCountOfRestaurants()
        {
            return this.dbContext.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return this.dbContext.Restaurants.Where(r => r.Name.StartsWith(name) || string.IsNullOrWhiteSpace(name))
                                             .OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            EntityEntry result = this.dbContext.Restaurants.Attach(updatedRestaurant);
            result.State = EntityState.Modified;

            return updatedRestaurant;
        }
    }
}