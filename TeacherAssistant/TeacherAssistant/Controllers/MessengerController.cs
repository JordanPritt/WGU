using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace TeacherAssistant.Controllers
{
    [Authorize]
    public class MessengerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}