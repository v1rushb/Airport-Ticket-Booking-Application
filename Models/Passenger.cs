
namespace Airport.Models {
    public class Passenger : Person {
        public void DisplayPassengerInfo()
        {
            Console.WriteLine($"Passenger: ID: {ID}, Name: {Name}, Age: {Age}, Phone: {Phone}, Email: {Email}");
        }
    }
}