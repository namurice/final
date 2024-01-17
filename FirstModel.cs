using FinalsProject.Validations;

namespace FinalsProject.Models
{
    public class FirstModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
