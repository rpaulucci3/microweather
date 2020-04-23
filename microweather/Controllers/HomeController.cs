using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using microweather.Models;
using Microsoft.AspNetCore.Hosting;

namespace microweather.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            configuration = config;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Observation> obs = GetObservations();
            return View(obs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IEnumerable<Observation> GetObservations()
        {
            string conString = ConfigurationExtensions.GetConnectionString(configuration, "Development");
            IEnumerable<Observation> myList = new List<Observation>();

            using (IDbConnection db = new SqlConnection(conString))
            {
                myList = db.Query<Observation>("Select * From MicroWeather ORDER BY [timestamp] DESC").ToList();
            }
            return myList;
        }

        public string GetLatestImage() 
        {
            Utils u = new Utils(configuration);
            return u.GetLatestImage();
        }

        [HttpPost]
        public bool AddObservation([FromBody] Observation obsInput)
        {
            obsInput.timestamp = DateTime.UtcNow;
            string conString = ConfigurationExtensions.GetConnectionString(configuration, "Development");
            using (IDbConnection db = new SqlConnection(conString))
            {
                db.Open();
                var transaction = db.BeginTransaction();
                db.Insert<Observation>(obsInput, transaction);
                transaction.Commit();
            }
            return true;
        }

    }
}
