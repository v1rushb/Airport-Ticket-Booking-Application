namespace Airport.Utilties {
    public class ValidationResult {
        public bool IsValid { get; set; }
        public IList<System.ComponentModel.DataAnnotations.ValidationResult> Errors { get; set; }
    }
}