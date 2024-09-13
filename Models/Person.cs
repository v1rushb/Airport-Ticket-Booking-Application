
using System.ComponentModel.DataAnnotations;

namespace Airport.Models {
    public abstract class Person {
        [Required]
        public string ID { get; set; }
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Phone { get; set; } // TODO: add some palestinian phone validation here. probs regex? look up.

        [Required]
        public required int Age { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }
    }
}