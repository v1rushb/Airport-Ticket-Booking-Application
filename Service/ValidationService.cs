using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Airport.Interfaces;

namespace Airport.Service {
    public class ValidationService : IValidationService
    {
        public Utilties.ValidationResult Validate<T>(T obj) {
            var context = new ValidationContext(obj);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, context, results, true);
            
            return new Utilties.ValidationResult {
                IsValid = isValid,
                Errors = results,
            };

        }

        public Dictionary<string, string> GetValidatedRules<T>() {
            var rules = new Dictionary<string, string>();
            var properties = typeof(T).GetProperties();
            foreach(var property in properties) {
                var attributes = property.GetCustomAttributes<ValidationAttribute>();
                var constraints = string.Join(", ", attributes.Select(el => el.GetType().Name.Replace("Attribute", "")));
                rules.Add(property.Name, constraints);
            }
            return rules;
        }
    }
}