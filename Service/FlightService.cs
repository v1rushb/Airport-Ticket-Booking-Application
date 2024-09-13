using Airport.Utilties;
using Airport.Models;
using Airport.Enums;
using Airport.Interfaces;

namespace Airport.Service {
    public class FlightService : IFlightService {
        private readonly IRepository<Flight> _flightRepository;
        private readonly ICSVFlightLoader _csvFlightLoader;
        private readonly ICSVFlightSaver _csvFlightSaver;

        private readonly IValidationService _validationService;



        public FlightService(IRepository<Flight> flightRepository, ICSVFlightLoader csvFlightLoader, ICSVFlightSaver csvFlightSaver, IValidationService validationService) {
            _flightRepository = flightRepository;
            _csvFlightLoader = csvFlightLoader;
            _csvFlightSaver = csvFlightSaver;
            _validationService = validationService;
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
            var validFlights = new List<Flight>();
            var errors = new Dictionary<Flight, List<string>>();

            foreach(var flight in flights) {
                var validationResult = ValidateFlight(flight);
                if(validationResult.IsValid) {
                    validFlights.Add(flight);
                } else {
                    errors.Add(flight, validationResult.Errors.Select(el => el.ErrorMessage).ToList());
                }
            }
            foreach(var flight in validFlights)
                _flightRepository.Add(flight);

            if(errors.Any()) {
                foreach(var error in errors) {
                    System.Console.WriteLine($"Errors for flight {error.Key.FlightID}: ");
                    foreach(var msg in error.Value) {
                        System.Console.WriteLine($"- {msg}");
                    }
                }
            }
        }

        public void SaveFlights(string targetCSVFilePath) {
            var flights = _flightRepository.GetAll().ToList();
            _csvFlightSaver.SaveFlights(targetCSVFilePath, flights);
        }

        public ValidationResult ValidateFlight(Flight flight) {
            return _validationService.Validate(flight);
        }

        public Dictionary<string, string> GetValidatedRules() {
            return _validationService.GetValidationRules<Flight>();
        }
    }
}