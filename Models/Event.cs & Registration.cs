using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    // Event model representing an event in the system
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public int AvailableSeats { get; set; }
        public int TotalSeats { get; set; }
        
        // Computed property for attendee count
        public int AttendeeCount => TotalSeats - AvailableSeats;
    }

    // Registration model with validation attributes
    public class Registration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select an event")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid event")]
        public int EventId { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
