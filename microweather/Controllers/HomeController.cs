using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using microweather.Models;
using static System.Net.Mime.MediaTypeNames;
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
                myList = db.Query<Observation>("Select * From MicroWeather").ToList();
            }
            return myList;
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

        public bool AddImage(Object files)
        {
            var _files = HttpContext.Request.Form.Files;
            if (_files == null) { return false; }
            var file = _files.First();
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "image.jpg");

            using (var bits = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(bits);
            }

            return true;
        }
    }
}
