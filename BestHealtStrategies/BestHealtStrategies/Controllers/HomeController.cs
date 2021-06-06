using BestHealtStrategies.Data;
using BestHealtStrategies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BestHealtStrategies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var t = User.Identity.Name;
            if(t != null )
            {
                User user = _context.Users.ToList().Find(u => u.UserName == t);
                List<DailyMealPlan> plans = _context.DailyMealPlan.ToList().FindAll(p => p.UserId == user.Id);
                foreach (DailyMealPlan plan in plans)
                {
                    List<Meal> meals = _context.Meal.ToList().FindAll(m => m.MealPlanID == plan.Id);
                    plan.Meals = meals;
                    Nutrient n = _context.Nutrient.ToList().Find(n => n.DailyMealPlanId == plan.Id);
                    plan.Nutrient = n;
                }
                //var users = _context.Users.ToList();
                return View(user);
            }
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
    }
}
