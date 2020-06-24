using AuthenticationDemo.Models;
using System.Threading.Tasks;

namespace AuthenticationDemo.Services
{
    public interface IStudentService
    {
        Task<Student> CreateStudent(StudentCreateOrUpdate student);
    }
}