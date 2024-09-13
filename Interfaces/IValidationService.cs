using Airport.Utilties;

namespace Airport.Interfaces {
    public interface IValidationService {
        ValidationResult Validate<T>(T obj);
        Dictionary<string, string> GetValidationRules<T>();
    }
}