using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.Models
{
    public class Vehicle
    {
        //[Required]
        public int Id { get; set; }
        [Range(1950, 2050)]
        public int Year { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
    }
}