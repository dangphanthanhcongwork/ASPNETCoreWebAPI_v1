namespace WebApplication.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public interface IPerson
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; }
        Gender Gender { get; set; }
        DateTime DateOfBirth { get; set; }
        string? BirthPlace { get; set; }
        string? PhoneNumber { get; set; }
        bool IsGraduated { get; set; }
    }
}
