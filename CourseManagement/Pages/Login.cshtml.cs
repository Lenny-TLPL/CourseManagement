using CourseManagementRepository.IRepository;
using CourseManagementRepository.IRepository.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseManagement.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserBasicRepository _repository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public LoginModel(IUserBasicRepository repository, ITeacherRepository teacherRepository, IStudentRepository studentRepository)
        {
            _repository = repository;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
           
        }

        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Page();
        }
        public IActionResult OnPost()
        {
            var account = _repository.Login(Email, Password);
            if (account != null)
            {
                //0: moderator
                //1: teacher
                //2: student
                HttpContext.Session.SetInt32("Role", (int)account.Role);
                
                switch (account.Role)
                {
                    case 0:
                        HttpContext.Session.SetInt32("UserId", account.Id);
                        ViewData["Message"] = "Mod";
                        return Page();
                        //return RedirectToPage("/Moderator/Index");
                    case 1:
                        var teacher = teacherRepository.GetTeacherByUserBasicId(account.Id);
                        HttpContext.Session.SetInt32("UserId", teacher.Id);
                        ViewData["Message"] = "Teacher";
                        //return Page();
                        return RedirectToPage("/Teacher/Schedule");

                    case 2:
                        var student = studentRepository.GetStudentByUserBasicId(account.Id);
                        HttpContext.Session.SetInt32("UserId", student.Id);
                        ViewData["Message"] = "Student";
                        //return Page();
                        return RedirectToPage("/Student/Schedule");

                }

            }
            ViewData["Message"] = "Wrong email or password!";
            return Page();
        }

        
    }
}
