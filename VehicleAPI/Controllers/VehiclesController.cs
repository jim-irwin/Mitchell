using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VehicleAPI.Models;

namespace VehicleAPI.Controllers
{
    public class VehiclesController : ApiController
    {
        static readonly IVehicleRepository _repository = new VehicleRepository();

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _repository.GetAll();
        }

        public Vehicle GetVehicle(int id)
        {
            Vehicle item = _repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public HttpResponseMessage PostVehicle(Vehicle item)
        {
            var response = Request.CreateResponse<Vehicle>(HttpStatusCode.Created, item);
            if (ModelState.IsValid)
            {
                item = _repository.Add(item);
                string uri = Url.Link("DefaultApi", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
        }

        public void PutVehicle(int id, Vehicle vehicle)
        {
            vehicle.Id = id;
            if (ModelState.IsValid)
            {
                if (!_repository.Update(vehicle))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            else
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                throw new HttpResponseException(response);
            }
        }

        public void DeleteVehicle(int id)
        {
            _repository.Remove(id);
        }
    }
}
