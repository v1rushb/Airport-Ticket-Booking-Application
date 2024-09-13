using Airport.Service;
namespace Airport.Models {
    public class Admin : Person {

        private readonly FlightService _flightService;
        public Admin(string id, string name, string phone, int age, FlightService flightService) {
            ID = id;
            Name = name;
            Phone = phone;
            Age = age;
            type = PersonType.Admin;
            _flightService = flightService;
        }

        public AddFlight() {
            
        }


    }
}