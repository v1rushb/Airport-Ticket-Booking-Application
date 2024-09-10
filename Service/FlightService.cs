using System;

namespace Airport {
    public class FlightService {
        private readonly List<Flight> _flights = new List<Flight>();

        public void AddFlight(Flight flight) {
            _flights.Add(flight);
            System.Console.WriteLine("Flight's been added."); //remove later
        }

        public List<Flight> GetAllFlights() {
            return _flights;
        }

        public Flight? GetFlightByID(string ID) {
            return _flights.FirstOrDefault(el => el.FlightID == ID);
        }

        public void DisplayFlightInfo() {
            foreach (var flight in _flights) {
                System.Console.WriteLine($"Flight ID: {flight.FlightID}, From: {flight.DepartedCountry} to {flight.DestinatedCountry}, Class: {flight.Class}, Price: {flight.Cost}");
            }
        }

        //TODO: implement the LookUpFlights() -> List<Flights>
        //TODO make a flights have their own cap, making some unavailable.
    }
}