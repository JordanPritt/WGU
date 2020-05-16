using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using TeacherAssistant.Models.Students;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TeacherAssistant.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(Student student)
        {
            // add the teacher add server side to avoid security hole.
            student.TeacherId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;

            ModelState.Clear();
            TryUpdateModelAsync(student);

            if (ModelState.IsValid)
            {
                // TODO: call service to create valid student.
                ModelState.Clear();
                return View(new Student());
            }
            else
                return View();
        }
    }
}