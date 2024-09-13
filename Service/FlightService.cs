using Airport.Utilties;
using Airport.Models;
using Airport.Enums;

namespace Airport.Service {
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
            } // TODO: add DepartedAirport and ArrivedAirport
        }

        public void LoadFlights(string filePath) {
            var flights = LoadFlightsFromCSV.LoadFlights(filePath);
            _flights.AddRange(flights);

            Console.WriteLine($"Loaded {flights.Count} into current flights.");
        }

        public void SaveFlights(string filePath) {
            SaveFlightsIntoCSV.SaveFlights(filePath, _flights);

            Console.WriteLine($"Saved {_flights.Count} into: {filePath}");
        }

        //TODO: implement the LookUpFlights() -> List<Flights> 
        public List<Flight> LookUpFlights(string? departedCountry = null, string? destinatedCountry= null , DateTime? departureTime = null,
                                            FlightClass? classType = null, string? departedAirport = null, string? arrivalAirport = null) { // method for a tpyical user.
                                            return _flights.Where(el => (departedCountry== null || el.DepartedCountry.Equals(departedCountry, StringComparison.OrdinalIgnoreCase)) &&
                                                                    (destinatedCountry == null || el.DestinatedCountry.Equals(destinatedCountry, StringComparison.OrdinalIgnoreCase)) &&
                                                                    (departureTime == null || el.DepartureTime.Date == departureTime.Value.Date) &&
                                                                    (classType == null || el.Class == classType)
                                                                    && (departedAirport== null || el.DepartedAirport.Equals(departedAirport, StringComparison.OrdinalIgnoreCase)) 
                                                                    && (arrivalAirport== null || el.ArrivalAirport.Equals(arrivalAirport, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        //TODO make a flights have their own cap, making some unavailable.
    }
}