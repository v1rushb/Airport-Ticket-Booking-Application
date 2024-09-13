using Airport.Models;

namespace Airport.Interfaces {
    public interface ICSVFlightLoader { // probs do some generics?
        List<Flight> LoadFlights(string path);
    }
}