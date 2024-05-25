using WebApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class PersonDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName { get { return LastName + " " + FirstName; } }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public int Age { get { return DateTime.Now.Year - DateOfBirth.Year; } }

        public string Birthplace { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsGraduated { get; set; }

        public override string ToString()
        {
            return $"Name: {FullName}, Gender: {Gender}, Date of Birth: {DateOfBirth}, Birthplace: {Birthplace}, Phone number: {PhoneNumber}, Graduated: {IsGraduated}";
        }
    }
}