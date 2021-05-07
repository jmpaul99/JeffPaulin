using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using JeffPaulin.Models;
using System.Threading.Tasks;
using PagedList;

namespace JeffPaulin.Controllers
{
    public class SessionsController : Controller
    {
        jpContext db = new jpContext();
        public IActionResult Index(int? page)
        {
            var sessions = db.Sessions.ToList();
            int pageSize = 10;
            int pageNumber = page ?? 1;
            return View(sessions.ToPagedList(pageNumber, pageSize));
        }
    }
}
