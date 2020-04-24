using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace microweather.Models
{
    [Table("MicroWeather")]
    public class Observation
    {
        public DateTime timestamp { get; set; }

        public int altitude { get; set; }

        public float temperature { get; set; }

        public int humidity { get; set; }

        public int pressure { get; set; }

        public float wind_speed { get; set; }

        public string wind_direction { get; set; }

        public int soil_moisture { get; set; }

        public float soil_temp { get; set; }

        public float rain_rate { get; set; }

        public string image { get; set; }

    }
}
