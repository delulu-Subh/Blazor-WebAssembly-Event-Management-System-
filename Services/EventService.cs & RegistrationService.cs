using EventManagementSystem.Models;

namespace EventManagementSystem.Services
{
    // Service to manage events and their state
    public class EventService
    {
        private List<Event> _events = new();
        private int _nextId = 1;

        // Event to notify components when events are updated
        public event Action? OnEventsChanged;

        public EventService()
        {
            // Initialize with sample data
            _events = new List<Event>
            {
                new Event { Id = _nextId++, Name = "Tech Conference 2026", Date = new DateTime(2026, 3, 15), Location = "Convention Center", AvailableSeats = 50, TotalSeats = 50 },
                new Event { Id = _nextId++, Name = "Workshop: Blazor Basics", Date = new DateTime(2026, 2, 20), Location = "University Hall", AvailableSeats = 30, TotalSeats = 30 },
                new Event { Id = _nextId++, Name = "Networking Mixer", Date = new DateTime(2026, 4, 10), Location = "Business Plaza", AvailableSeats = 100, TotalSeats = 100 },
                new Event { Id = _nextId++, Name = "AI & Machine Learning Seminar", Date = new DateTime(2026, 5, 5), Location = "Tech Park Auditorium", AvailableSeats = 75, TotalSeats = 75 }
            };
        }

        public List<Event> GetAllEvents() => _events;

        public Event? GetEventById(int id) => _events.FirstOrDefault(e => e.Id == id);

        // Updates available seats and notifies listeners
        public bool UpdateEventSeats(int eventId, int seatsToReduce)
        {
            var evt = GetEventById(eventId);
            if (evt != null && evt.AvailableSeats >= seatsToReduce)
            {
                evt.AvailableSeats -= seatsToReduce;
                OnEventsChanged?.Invoke(); // Notify all subscribers
                return true;
            }
            return false;
        }

        public void AddEvent(Event evt)
        {
            evt.Id = _nextId++;
            _events.Add(evt);
            OnEventsChanged?.Invoke();
        }
    }

    // Service to manage registrations
    public class RegistrationService
    {
        private List<Registration> _registrations = new();
        private int _nextId = 1;
        private readonly EventService _eventService;

        public event Action? OnRegistrationsChanged;

        public RegistrationService(EventService eventService)
        {
            _eventService = eventService;
        }

        public List<Registration> GetAllRegistrations() => _registrations;

        public List<Registration> GetRegistrationsByEvent(int eventId) 
            => _registrations.Where(r => r.EventId == eventId).ToList();

        // Registers a user for an event
        public bool RegisterForEvent(Registration registration)
        {
            // Check if event has available seats
            if (_eventService.UpdateEventSeats(registration.EventId, 1))
            {
                registration.Id = _nextId++;
                registration.RegistrationDate = DateTime.Now;
                _registrations.Add(registration);
                OnRegistrationsChanged?.Invoke();
                return true;
            }
            return false;
        }

        // Check if user is already registered for an event
        public bool IsUserRegistered(string email, int eventId)
        {
            return _registrations.Any(r => r.Email.Equals(email, StringComparison.OrdinalIgnoreCase) 
                                          && r.EventId == eventId);
        }
    }
}
