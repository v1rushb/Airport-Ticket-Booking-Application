using Airport.Models;

namespace Airport.Interfaces {
    public interface ICSVFlightSaver {
        void SaveFlights(string path, List<Flight> flights); 
    }
}