using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleAPI.Models
{
    public class VehicleRepository : IVehicleRepository
    {
        private List<Vehicle> vehicles = new List<Vehicle>();
        private int _nextId = 1;

        public VehicleRepository()
        {
            // seed the repository with some data
            Add(new Vehicle { Year = 2018, Make = "Dodge", Model = "Charger" });
            Add(new Vehicle { Year = 2018, Make = "Ford", Model = "Mustang" });
            Add(new Vehicle { Year = 2018, Make = "Chevrolet", Model = "Camaro" });
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return vehicles;
        }

        public Vehicle Get(int id)
        {
            return vehicles.Find(p => p.Id == id);
        }

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

        public void Remove(int id)
        {
            vehicles.RemoveAll(p => p.Id == id);
        }

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