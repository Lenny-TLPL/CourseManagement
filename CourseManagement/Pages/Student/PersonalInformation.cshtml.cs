using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CourseManangementModels.Models;
using CourseManagementRepository.IRepository;

namespace CourseManagement.Pages.Student
{
    public class PersonalInformationModel : PageModel
    {
        private readonly IStudentRepository studentRepository;

        public PersonalInformationModel(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

      public CourseManangementModels.Models.Student Student { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            

            var student = studentRepository.GetById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                Student = student;
            }
            return Page();
        }
    }
}
