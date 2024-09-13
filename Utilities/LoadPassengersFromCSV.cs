using Airport.Interfaces;
using Airport.Models;

namespace Airport.Utilties {
    public class LoadPassengersFromCSV : ICSVPassengerLoader
    {
        public List<Passenger> LoadPassengers(string targetCSVFilePath) {
            var passengers = new List<Passenger>();
            try {
                var lines = File.ReadAllLines(targetCSVFilePath);
                foreach (var line in lines) {
                    var data = line.Split(',');

                    if (data.Length != 5) {
                        throw new FormatException($"Invalid data format: {line}");
                    }

                    passengers.Add(new Passenger {
                        ID = data[0],
                        Name = data[1],
                        Email = data[2],
                        Age = int.Parse(data[3]),
                        Phone = data[4]
                    });

                }
                Console.WriteLine("Passengers loaded successfully from CSV.");
            } catch (IOException ex) {
                Console.WriteLine($"Error reading the CSV file: {ex.Message}");
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred while reading the CSV: {ex.Message}");
            }
            return passengers;
        }
    }
}