using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningSystem.Web.Models;
using LearningSystem.Service;

namespace LearningSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public readonly ICourseService courses;

        public HomeController(ICourseService courses)
        {
            this.courses = courses;
        }

        public async Task<IActionResult> Index()
            => View(await this.courses.Active());

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
