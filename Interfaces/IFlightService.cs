using Airport.Models;

public interface IFlightService {
    IEnumerable<Flight> LookUpFlights(SearchCriteria criteria);
    void LoadFlights(IEnumerable<Flight> flights);
    //TODO: Add some validtions for the results or smth.
}