using Airport.Models;
using Airport.Enums;
using Airport.Interfaces;
using System.ComponentModel;

namespace Airport.Utilties {

    public class LoadFlightsFromCSV : ICSVFlightLoader {
        public List<Flight> LoadFlights(string path) {
            var flights = new List<Flight>();

            try {
                var lines = File.ReadAllLines(path);
                int lineNumPtr = 0;
                foreach(var line in lines) {
                    lineNumPtr++;
                    var data = line.Split(',');
                    if(data.Length != 8) {
                        throw new  FormatException($"Invalid data format in line {lineNumPtr}: {line}");
                    }
                    
                    if (!DateTime.TryParse(data[3], out DateTime departureTime)) {
                        throw new FormatException($"Invalid date format in line {lineNumPtr}: {data[3]}");
                    }

                    if (!decimal.TryParse(data[4], out decimal cost)) {
                        throw new FormatException($"Invalid price format in line {lineNumPtr}: {data[4]}");
                    }

                    if (!Enum.TryParse(data[5], out FlightClass flightClass)) {
                        throw new FormatException($"Invalid class in line {lineNumPtr}: {data[5]}");
                    } // do some general centralized customerror handler.


                    flights.Add(new Flight{
                        FlightID = data[0],
                        DepartedCountry = data[1],
                        DestinatedCountry = data[2],
                        DepartureTime = departureTime,
                        Cost = cost,
                        Class = flightClass,
                        ArrivalAirport = data[6],
                        DepartedAirport = data[7]
                    });
                } 
            } catch (IOException ex) {
                throw new ApplicationException("An error occurred while reading the CSV file.", ex);
            } catch (Exception ex) {
                Console.WriteLine($"Error while reading CSV File. \n Error: {ex.Message}");
            }
            System.Console.WriteLine("Flights loaded successfully from CSV.");
            return flights;
        }
    }
}