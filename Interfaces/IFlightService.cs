using Airport.Models;
using Airport.Utilties;

public interface IFlightService {
    IEnumerable<Flight> LookUpFlights(SearchCriteria criteria);
    void LoadFlights(string targetCSVFilePath);
    void SaveFlights(string targetCSVFilePath);
    //TODO: Add some validtions for the results or smth.

    ValidationResult ValidateFlight(Flight flight);
    Dictionary<string, string> GetValidatedRules();
}