using AuthenticationDemo.Data;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace AuthenticationDemo.Services
{
    public class StudentService : IStudentService
    {
        private readonly DemoDbContext _context;
        //private readonly UserManager<IdentityUser> _userManager;
        IHttpContextAccessor _httpContextAccessor;
        public StudentService(  DemoDbContext context,
                                //UserManager<IdentityUser> userManager,
                                IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            //_userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Student> CreateStudent(StudentCreateOrUpdate student)
        {
            if (student.Id != 0)
                return _context.Students.FirstOrDefault(p => p.Id == student.Id);

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var entity = new Student
            {
                FirstMidName= student.FirstMidName,
                LastName= student.LastName,
                EnrollmentDate= student.EnrollmentDate,
                UserId = student.UserId ?? userId
            };
            if (string.IsNullOrEmpty(student.UserId))
            {
                student.UserId = userId;
            }
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _context.Students.FirstOrDefault(p => p.UserId == userId);
        }
    }
}