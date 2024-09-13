using System.Data;
using Airport.Interfaces;
using Airport.Models;

namespace Airport.Repositories {
    public class PassengerRepository : IRepository<Passenger> {
        private List<Passenger> _passengers = new List<Passenger>();
        private readonly ICSVPassengerLoader _loadPassengersFromCSV;
        private readonly string targetCSVFilePath = @"Data/Passengers.csv";

        public PassengerRepository(ICSVPassengerLoader loadPassengersFromCSV) {
            _loadPassengersFromCSV = loadPassengersFromCSV;
        }

        public IEnumerable<Passenger> GetAll() {
            return _passengers ?? new List<Passenger>();
        }


        public Passenger? GetByID(string ID) {
            try {
                return _passengers.SingleOrDefault(el => el.ID.Equals(ID));
            } catch(Exception ex) {
                System.Console.WriteLine($"Error While fetching a passenger with the ID: {ID} \n more  info: {ex}"); // idk handle using CEH?
                throw new ApplicationException("Error While fetching a passenger with the ID: {ID}", ex);
            }
        }

        public void Add(Passenger passenger) { // TODO: do some validation here.
        // System.Console.WriteLine("YOink!");
            var currentFlight = GetByID(passenger.ID);
            if(currentFlight == null) {
                _passengers.Add(passenger);
                System.Console.WriteLine($"Passenger's been added! {passenger}");
                return;
            }
            throw new DuplicateNameException("passenger already exists.");

        }

        public void Update(Passenger passenger) { // TODO: probs add some auto sync for addition and update and deletion to csv file.
            var currentFlight = GetByID(passenger.ID);
            if(currentFlight != null) {
                _passengers.Remove(passenger);
                _passengers.Add(passenger);
                System.Console.WriteLine($"Passenger's Been updated! {passenger}");
            }
        }

        public void Delete(string ID) {
            var currentPassenger = GetByID(ID);
            if(currentPassenger != null) {
                _passengers.Remove(currentPassenger);
                // System.Console.WriteLine($"Flight: {flight}\n has been deleted."); // TODO: check correct format if needed.
            }
        }

        public void LoadAllPassengers() {
            _passengers = _loadPassengersFromCSV.LoadPassengers(targetCSVFilePath);
        }

    }
}