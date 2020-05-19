using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TeacherAssistant.Models.Students;
using TeacherAssistant.Services;

namespace TeacherAssistant.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly StudentsService _studentService;

        public StudentsController(StudentsService studentsService)
        {
            _studentService = studentsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string teacherId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;

            StudentsViewModel viewModel = new StudentsViewModel()
            {
                StudentsTable = _studentService.GetStudentTable(teacherId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(StudentsViewModel student)
        {
            // add the teacher add server side to avoid security hole.
            student.TeacherId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;

            ModelState.Clear();
            await TryUpdateModelAsync((Student)student);

            if (ModelState.IsValid)
            {
                var create = await _studentService.CreateStudent((Student)student);

                if (create == false)
                    ModelState.AddModelError("Email", "Sorry, email must be unique.");

                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    student.StudentsTable = _studentService.GetStudentTable(student.TeacherId);
                    return View((StudentsViewModel)student);
                }
            }
            else
                return RedirectToAction("Index");
        }
    }
}
