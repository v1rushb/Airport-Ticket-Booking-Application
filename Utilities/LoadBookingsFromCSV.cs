using Airport.Interfaces;
using Airport.Models;
using Airport.Repositories;

namespace Airport.Utilties {
    public class LoadBookingsFromCSV : ICSVBookingLoader
    {
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Passenger> _passengerRepository;

        public LoadBookingsFromCSV(IRepository<Flight> flightRepository, IRepository<Passenger> passengerRepository) {
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
        }
        public List<Booking> LoadBookings(string targetCSVFilePath) {
            var bookings = new List<Booking>();
            try {
                var lines = File.ReadLines(targetCSVFilePath);
                var lineNumPtr = 0;

                foreach(var line in lines) {
                    lineNumPtr++;
                    var data = line.Split(",");

                    if(data.Length != 6) {
                        throw new FormatException($"Invalid data format in line {lineNumPtr}: {line}");
                    }

                    if(!DateTime.TryParse(data[3], out DateTime bookingDate)) {
                        throw new FormatException($"Invalid date format in line {lineNumPtr}: {data[3]}");
                    }

                    var flight = _flightRepository.GetByID(data[2]);
                    var passenger = _passengerRepository.GetByID(data[1]);

                    if(passenger == null) {
                        throw new Exception($"Passenger with ID {data[1]} not found.");
                    }

                    if (flight == null) {
                        throw new Exception($"Flight with ID {data[2]} not found.");
                    }

                    if(!decimal.TryParse(data[4], out decimal price)) {
                        throw new FormatException($"Invalid price format in line {lineNumPtr}: {data[4]}");
                    }

                    bookings.Add(new Booking {
                        BID = data[0],
                        passenger = passenger,
                        flight = flight,
                        BookingDate = bookingDate,
                        Price = price
                    });   
                }
            } catch (IOException ex) {
                throw new ApplicationException("An error occurred while reading the CSV file.", ex);
            } catch (Exception ex) {
                Console.WriteLine($"Error while reading CSV file. Error: {ex.Message}");
            }
            return bookings;
        }
    }
}