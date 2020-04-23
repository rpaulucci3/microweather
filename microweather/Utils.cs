using Dapper;
using Microsoft.Extensions.Configuration;
using microweather.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace microweather
{
    public class Utils
    {
        private readonly IConfiguration configuration;

        public Utils(IConfiguration config)
        {
            configuration = config;
        }

        public string GetLatestImage()
        {
            string conString = ConfigurationExtensions.GetConnectionString(configuration, "Development");
            Observation obs = new Observation();
            using (IDbConnection db = new SqlConnection(conString))
            {
                obs = db.QuerySingleOrDefault<Observation>("SELECT TOP 1 [image] FROM [MicroWeather] ORDER BY [image] DESC");
            }
            return obs.image;
        }
    }
}

