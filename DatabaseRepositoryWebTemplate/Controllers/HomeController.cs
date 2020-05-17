using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DatabaseRepositoryWebTemplate.Models;
using DBLib.Models;
using DBLib.Repositories.Interfaces;

namespace DatabaseRepositoryWebTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAlbumRepository _albumRepository;
        private IEmployeeRepository _employeeRepository;

        public HomeController(ILogger<HomeController> logger, IAlbumRepository albumRepository, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _albumRepository = albumRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult TestAPI()
        {
            List<Album> list = _albumRepository.Query();
            return new JsonResult(list);
        }

        public JsonResult TestE()
        {
            List<Employees> list = _employeeRepository.Query();
            return new JsonResult(list);
        }
    }
}
