using JeffPaulin.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace JeffPaulin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            var c = Assembly.GetExecutingAssembly();
            var cl = c.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new { Controller = x.DeclaringType.Name, Area = x.DeclaringType.Namespace.Split('.').Reverse().Skip(1).First() }).Distinct()
                .OrderBy(x => x.Controller).ToList();
            cl = cl.Where(x => x.Area == "Admin" && x.Controller != "DefaultController").Distinct().ToList();

            List<AdminCenterViewModel> vm = new List<AdminCenterViewModel>();
            foreach (var i in cl)
            {
                string cName = i.Controller.Replace("Controller", "");
                cName = Regex.Replace(cName, "([a-z])_?([A-Z])", "$1 $2");
                AdminCenterViewModel a = new AdminCenterViewModel() { Area = i.Area, Controller = cName };
                vm.Add(a);
            }
            return View(vm);
        }
    }
}
