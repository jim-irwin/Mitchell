using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleAPI.Models
{
    /// <summary>
    /// Store vehicle collection in local memory and implement IVehicleRepository interface
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        private int _nextId = 1;

        public VehicleRepository()
        {
            // seed the repository with some initial data
            Add(new Vehicle { Year = 2018, Make = "Dodge", Model = "Charger" });
            Add(new Vehicle { Year = 2018, Make = "Ford", Model = "Mustang" });
            Add(new Vehicle { Year = 2018, Make = "Chevrolet", Model = "Camaro" });
        }

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns>list of Vehicles</returns>
        public IEnumerable<Vehicle> GetAll()
        {
            return vehicles;
        }

        /// <summary>
        /// Get vehicle by id
        /// </summary>
        /// <param name="id">vehicle id</param>
        /// <returns>Vehicle with matching id</returns>
        public Vehicle Get(int id)
        {
            return vehicles.Find(p => p.Id == id);
        }

        /// <summary>
        /// Add new vehicle
        /// </summary>
        /// <param name="item">vehicle to add</param>
        /// <returns>added Vehicle</returns>
        public Vehicle Add(Vehicle item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            vehicles.Add(item);
            return item;
        }

        /// <summary>
        /// Remove a vehicle by id
        /// </summary>
        /// <param name="id">id of vehicle to remove</param>
        public void Remove(int id)
        {
            vehicles.RemoveAll(p => p.Id == id);
        }

        /// <summary>
        /// Update existing Vehicle
        /// </summary>
        /// <param name="item">Vehicle to update</param>
        /// <returns>bool</returns>
        public bool Update(Vehicle item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = vehicles.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            vehicles.RemoveAt(index);
            vehicles.Add(item);
            return true;
        }
    }
}