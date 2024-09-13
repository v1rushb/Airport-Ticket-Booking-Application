using System.Data;
using Airport.Interfaces;
using Airport.Models;

namespace Airport.Repositories {
    public class FlightRepository : IRepository<Flight> {
        private readonly string targetCSVFilePath = @"Data/Flights.csv"; // TODO: add a csv file validator later or just throw an excption
        private List<Flight> _flights = new List<Flight>();

        public IEnumerable<Flight> GetAll() {
            return _flights ?? new List<Flight>();
        }


        public Flight? GetByID(string ID) {
            try {
                return _flights.SingleOrDefault(el => el.FlightID.Equals(ID));
            } catch(Exception ex) {
                System.Console.WriteLine($"Error While fetching a booking with the ID: {ID} \n more  info: {ex}"); // idk handle using CEH?
                throw new ApplicationException("Error While fetching a booking with the ID: {ID}", ex);
            }
        }

        public void Add(Flight flight) { // TODO: do some validation here.
        System.Console.WriteLine("YOink!");
            var currentFlight = GetByID(flight.FlightID);
            if(currentFlight == null) {
                _flights.Add(flight);
                System.Console.WriteLine($"Flight's been added! {flight}");
                return;
            }
            throw new DuplicateNameException("Flight already exists.");

        }

        public void Update(Flight flight) { // TODO: probs add some auto sync for addition and update and deletion to csv file.
            var currentFlight = GetByID(flight.FlightID);
            if(currentFlight != null) {
                _flights.Remove(flight);
                _flights.Add(flight);
                System.Console.WriteLine($"Flight's Been updated! {flight}");
            }
        }

        public void Delete(string ID) {
            var currentFlight = GetByID(ID);
            if(currentFlight != null) {
                _flights.Remove(currentFlight);
                // System.Console.WriteLine($"Flight: {flight}\n has been deleted."); // TODO: check correct format if needed.
            }
        }

    }
}