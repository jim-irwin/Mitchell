using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VehicleAPI.Models;

namespace VehicleAPI.Controllers
{
    /// <summary>
    /// This Controller will handle all HTTP requests
    /// </summary>
    public class VehiclesController : ApiController
    {
        static readonly IVehicleRepository _repository = new VehicleRepository();

        /// <summary>
        /// Get a list of all vehicles
        /// </summary>
        /// <returns>IEnumerable<Vehicle></returns>
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Get a Vehicle by id
        /// </summary>
        /// <param name="id">id of Vehicle</param>
        /// <returns>Vehicle with matching id</returns>
        public Vehicle GetVehicle(int id)
        {
            Vehicle item = _repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        /// <summary>
        /// Create a new Vehicle
        /// </summary>
        /// <param name="item">item to create</param>
        /// <returns>HttpResponseMessage</returns>
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

        /// <summary>
        /// Update a Vehicle by Id
        /// </summary>
        /// <param name="id">id of Vehicle to update</param>
        /// <param name="vehicle">updated Vehicle</param>
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

        /// <summary>
        /// Delete a Vehicle by Id
        /// </summary>
        /// <param name="id">Id of Vehicle to Delete</param>
        public void DeleteVehicle(int id)
        {
            _repository.Remove(id);
        }
    }
}
