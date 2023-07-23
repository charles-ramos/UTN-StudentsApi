using Students.Common.Models;

namespace Students.Api.ViewModels
{
    public class StudentVM : Entity
    {
        public int StudentId { get; set; } = 0;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int DNI { get; set; } = 0;
        public string Address { get; set; } = null!;
    }
}