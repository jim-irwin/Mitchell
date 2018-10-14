using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleAPI;
using VehicleAPI.Models;
using VehicleAPI.Controllers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace VehicleAPI.Tests
{
    [TestClass]
    public class VehiclesControllerTest
    {
        static readonly string EndPoint = "http://localhost:49597/";

        [TestMethod]
        public void Get()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();

            // Act
            IEnumerable<Vehicle> result = controller.GetAllVehicles();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            VehiclesController controller = new VehiclesController();

            // Act & Assert
            try
            {
                // Test invalid id
                Vehicle result = controller.GetVehicle(0);
                Assert.IsNull(result);
            }
            catch (HttpResponseException)
            {
                // Test valid id
                Vehicle result = controller.GetVehicle(1);
                Assert.AreEqual(1, result.Id);
            }            
        }

        [TestMethod]
        public void PostAndDelete()
        {
            using (WebClient client = new WebClient())
            {
                // Arrange
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                Vehicle item = new Vehicle
                {
                    Year = 1994,
                    Make = "Audi",
                    Model = "RS2"
                };
                client.UploadString(EndPoint + "vehicles", JsonConvert.SerializeObject(item));
                string RepoStr = client.DownloadString(EndPoint + "vehicles");
                List<Vehicle> RepoList = JsonConvert.DeserializeObject<List<Vehicle>>(RepoStr);
                int ItemId = RepoList[RepoList.Count - 1].Id;
                string NewItem = client.DownloadString(EndPoint + "vehicles/" + ItemId);

                // Assert
                Assert.AreEqual(RepoList[RepoList.Count - 1].Model, "RS2");

                // Test Delete
                bool IsRemoved = false;
                client.UploadValues(EndPoint + "vehicles/" + ItemId, "DELETE", new NameValueCollection());
                // Delete again to test with id that does not exist
                client.UploadValues(EndPoint + "vehicles/" + ItemId, "DELETE", new NameValueCollection());
                // Check item was removed
                try
                {
                    string DelItem = client.DownloadString(EndPoint + "vehicles/" + ItemId);
                }
                catch (WebException)
                {
                    IsRemoved = true;
                }
                Assert.IsTrue(IsRemoved);
            }
        }

        [TestMethod]
        public void InvalidModelState()
        {
            using (WebClient client = new WebClient())
            {
                bool failed = true;
                // Missing Required Attribute
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                Vehicle item = new Vehicle
                {
                    Year = 1910,
                    Make = "Frisbie"
                };
                try
                {
                    client.UploadString(EndPoint + "vehicles", JsonConvert.SerializeObject(item));
                }
                catch (WebException)
                {
                    failed = false;
                }
                Assert.IsFalse(failed);
                failed = true;

                // Invalid Year
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                item = new Vehicle
                {
                    Year = 1900,
                    Make = "Fiat",
                    Model = "6"
                };
                // POST
                try
                {
                    client.UploadString(EndPoint + "vehicles", JsonConvert.SerializeObject(item));
                }
                catch (WebException)
                {
                    failed = false;
                }
                Assert.IsFalse(failed);
                failed = true;
                //PUT
                try
                {
                    client.UploadString(EndPoint + "vehicles/1", "PUT", JsonConvert.SerializeObject(item));
                }
                catch (WebException)
                {
                    failed = false;
                }
                Assert.IsFalse(failed);
            }
        }

        [TestMethod]
        public void Put()
        {
            using (WebClient client = new WebClient())
            {
                // Test Update
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string OldItem = client.DownloadString(EndPoint + "vehicles/1");
                Vehicle OldModel = JsonConvert.DeserializeObject<Vehicle>(OldItem);
                int NewYear = OldModel.Year - 1;
                Vehicle item = new Vehicle
                {
                    Year = NewYear,
                    Make = "Dodge",
                    Model = "Charger"
                };
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.UploadString(EndPoint + "vehicles/1", "PUT", JsonConvert.SerializeObject(item));
                string NewItem = client.DownloadString(EndPoint + "vehicles/1");
                Vehicle NewModel = JsonConvert.DeserializeObject<Vehicle>(NewItem);
                // Assert
                Assert.AreNotEqual(OldModel, NewModel);
                Assert.AreEqual(NewModel.Year, NewYear);

                // Test Update Item Not Found
                bool failed = true;
                try
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    // put invalid item
                    client.UploadString(EndPoint + "vehicles/0", "PUT", JsonConvert.SerializeObject(item));
                }
                catch (WebException)
                {
                    // make sure we got exception
                    failed = false;
                }
                // Assert
                Assert.IsFalse(failed);
            }
        }
    }
}
