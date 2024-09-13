namespace Airport.Models {
    public abstract class Person {
        public required string ID { get; set; }
        public required string Name { get; set; }

        public required string Phone { get; set; }

        public required int Age { get; set; }

        public required PersonType type { get; set; }

        public required string Email { get; set; }
    }

    public enum PersonType {
        Passenger,
        Admin
    }
}