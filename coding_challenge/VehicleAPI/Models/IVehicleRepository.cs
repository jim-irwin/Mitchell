using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleAPI.Models
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetAll();
        Vehicle Get(int id);
        Vehicle Add(Vehicle item);
        void Remove(int id);
        bool Update(Vehicle item);
    }
}
