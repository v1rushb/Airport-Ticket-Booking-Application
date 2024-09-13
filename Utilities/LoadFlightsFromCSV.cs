using System;
using Airport.Models;
using Airport.Enums;

namespace Airport.Utilties {
    public static class LoadFlightsFromCSV { //after gym: do dep inj.
        public static List<Flight> LoadFlights(string path) {
            var flights = new List<Flight>();
            try {
                var lines = File.ReadAllLines(path);
                foreach(var line in lines) {
                    var data = line.Split(',');
                    if(data.Length != 8) {
                        // Console.WriteLine($"Invalid data format in line {line}");
                        throw new Exception($"Invalid data format in line {line}");
                        // continue;
                    }

                    flights.Add(new Flight{
                        FlightID = data[0],
                        DepartedCountry = data[1],
                        DestinatedCountry = data[2],
                        DepartureTime = DateTime.Parse(data[3]), //TODO : use TryParse
                        Cost = int.Parse(data[4]),
                        Class = (FlightClass)Enum.Parse(typeof(FlightClass), data[5]), // look for an alternative way.
                        ArrivalAirport = data[6],
                        DepartedAirport = data[7]
                    });
                }
            } catch(Exception ex) {
                Console.WriteLine($"Error while reading CSV File.");
            }
            return flights;
        }        
    }
}