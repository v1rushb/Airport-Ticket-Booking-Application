using Airport.Models;

namespace Airport.Interfaces {
    public interface ICSVPassengerLoader {
        List<Passenger> LoadPassengers(string targetCSVFilePath);
    }
}