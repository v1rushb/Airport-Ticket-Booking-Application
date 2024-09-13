using Airport.Utilties;
using Airport.Models;
using Airport.Enums;
using Airport.Interfaces;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;
using System.Buffers;

namespace Airport.Service {
    // public class FlightService {
    //     private readonly List<Flight> _flights = new List<Flight>();

    //     public void AddFlight(Flight flight) {
    //         _flights.Add(flight);
    //         System.Console.WriteLine("Flight's been added."); //remove later
    //     }

    //     public List<Flight> GetAllFlights() {
    //         return _flights;
    //     }

    //     public Flight? GetFlightByID(string ID) {
    //         return _flights.FirstOrDefault(el => el.FlightID == ID);
    //     }

    //     public void DisplayFlightInfo() {
    //         foreach (var flight in _flights) {
    //             System.Console.WriteLine($"Flight ID: {flight.FlightID}, From: {flight.DepartedCountry} to {flight.DestinatedCountry}, Class: {flight.Class}, Price: {flight.Cost}");
    //         } // TODO: add DepartedAirport and ArrivedAirport
    //     }

    //     public void LoadFlights(string filePath) {
    //         var flights = LoadFlightsFromCSV.LoadFlights(filePath);
    //         _flights.AddRange(flights);

    //         Console.WriteLine($"Loaded {flights.Count} into current flights.");
    //     }

    //     public void SaveFlights(string filePath) {
    //         SaveFlightsIntoCSV.SaveFlights(filePath, _flights);

    //         Console.WriteLine($"Saved {_flights.Count} into: {filePath}");
    //     }

    //     //TODO: implement the LookUpFlights() -> List<Flights> 
    //     public List<Flight> LookUpFlights(string? departedCountry = null, string? destinatedCountry= null , DateTime? departureTime = null,
    //                                         FlightClass? classType = null, string? departedAirport = null, string? arrivalAirport = null) { // method for a tpyical user.
    //                                         return _flights.Where(el => (departedCountry== null || el.DepartedCountry.Equals(departedCountry, StringComparison.OrdinalIgnoreCase)) &&
    //                                                                 (destinatedCountry == null || el.DestinatedCountry.Equals(destinatedCountry, StringComparison.OrdinalIgnoreCase)) &&
    //                                                                 (departureTime == null || el.DepartureTime.Date == departureTime.Value.Date) &&
    //                                                                 (classType == null || el.Class == classType)
    //                                                                 && (departedAirport== null || el.DepartedAirport.Equals(departedAirport, StringComparison.OrdinalIgnoreCase)) 
    //                                                                 && (arrivalAirport== null || el.ArrivalAirport.Equals(arrivalAirport, StringComparison.OrdinalIgnoreCase))).ToList();
    //     }

    //     //TODO make a flights have their own cap, making some unavailable.
    // }

    public class FlightService : IFlightService {
        private readonly IRepository<Flight> _flightRepository;
        private readonly ICSVFlightLoader _csvFlightLoader;
        private readonly ICSVFlightSaver _csvFlightSaver;



        public FlightService(IRepository<Flight> flightRepository, ICSVFlightLoader csvFlightLoader, ICSVFlightSaver csvFlightSaver) {
            _flightRepository = flightRepository;
            _csvFlightLoader = csvFlightLoader;
            _csvFlightSaver = csvFlightSaver;
        }

        public IEnumerable<Flight> LookUpFlights(SearchCriteria criteria) {
            var flights = _flightRepository.GetAll(); // idk maybe reformate it to look a bit simplier to read?
            return flights.Where(el => (criteria.DepartedCountry == null || el.DepartedCountry.Equals(criteria.DepartedCountry, StringComparison.OrdinalIgnoreCase)) &&
                        (criteria.DestinatedCountry == null || el.DestinatedCountry.Equals(criteria.DestinatedCountry, StringComparison.OrdinalIgnoreCase)) &&
                        (criteria.DepartureTime == null || el.DepartureTime.Date == criteria.DepartureTime.Value.Date) &&
                        (criteria.ClassType == null || el.Class == criteria.ClassType)
                        && (criteria.DepartedAirport == null || el.DepartedAirport.Equals(criteria.DepartedAirport, StringComparison.OrdinalIgnoreCase)) 
                        && (criteria.ArrivalAirport== null || el.ArrivalAirport.Equals(criteria.ArrivalAirport, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
        }
        
        public void LoadFlights(string targetCSVFilePath) {
            var flights = _csvFlightLoader.LoadFlights(targetCSVFilePath);

            foreach(var flight in flights) // Huge TODO: do a prof. validation.
                _flightRepository.Add(flight);
        }

        public void SaveFlights(string targetCSVFilePath) {
            var flights = _flightRepository.GetAll().ToList();
            _csvFlightSaver.SaveFlights(targetCSVFilePath, flights);
        }

    }
}