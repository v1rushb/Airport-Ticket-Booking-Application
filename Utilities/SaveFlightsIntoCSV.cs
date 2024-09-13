using Airport.Models;
using Airport.Interfaces;

namespace Airport.Utilties {
    public class SaveFlightsIntoCSV : ICSVFlightSaver {
        public void SaveFlights(string path, List<Flight> flights) {
            try {
                var writer = new StreamWriter(path);
                    foreach(var flight in flights) {
                    writer.WriteLine($"{flight.FlightID},{flight.DepartedCountry},{flight.DestinatedCountry}," +
                                        $"{flight.DepartureTime},{flight.Cost},{flight.Class},{flight.ArrivalAirport},{flight.DepartedAirport}");
                    }
            } catch(IOException ex) {
                throw new ApplicationException($"An error occurred while writing to the file: {path}", ex);
            } catch(Exception ex) {
                Console.WriteLine($"Something's up with file: {path}");
                Console.WriteLine($"More info: {ex}");
                throw;
            }
        }
    }
}