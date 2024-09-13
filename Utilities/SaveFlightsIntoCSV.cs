using System;

namespace Airport.Utilties {
    public static class SaveFlightsIntoCSV {
        public static void SaveFlights(string csvPath, List<Flight> flights) {
            try {
                var writer = new StreamWriter(csvPath);
                foreach(var flight in flights) {
                    writer.WriteLine($"{flight.FlightID},{flight.DepartedCountry},{flight.DestinatedCountry}," + 
                                        $"{flight.DepartureTime},{flight.Cost},{flight.Class},{flight.ArrivalAirport},{flight.DepartedAirport}");
                }
                Console.WriteLine($"Successfully saved {flights.Count} flights to {csvPath}");
            } catch (Exception ex) {
                Console.WriteLine($"Something's up with file: {csvPath}");
            }
        }
    }   
}