using System.Diagnostics.Contracts;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Airport.Interfaces;
using Airport.Models;

namespace Airport.Repositories {
    public class FlightRepository : IRepository<Flight> {
        private readonly string targetCSVFilePath = "Date/Flights.csv"; // TODO: add a csv file validator later or just throw an excption
        private List<Flight> _flights = [];

        public IEnumerable<Flight> GetAll() {
            return _flights;
        }

        public Flight? GetByID(string ID) {
            return _flights.FirstOrDefault(el => el.FlightID.Equals(ID));
        }

        public void Add(Flight flight) { // TODO: do some validation here.
            _flights.Add(flight);
            System.Console.WriteLine($"Flight's been added! {flight}");
        }

        public void Update(Flight flight) { // TODO: probs add some auto sync for addition and update and deletion to csv file.
            var currentFlight = GetByID(flight.FlightID);
            if(currentFlight != null) {
                _flights.Remove(flight);
                _flights.Add(flight);
                System.Console.WriteLine($"Flight's Been updated! {flight}");
            }
        }

        public void Delete(Flight flight) {
            var currentFlight = GetByID(flight.FlightID);
            if(currentFlight != null) {
                _flights.Remove(currentFlight);
                System.Console.WriteLine($"Flight: {flight}\n has been deleted."); // TODO: check correct format if needed.
            }
        }

    }
}