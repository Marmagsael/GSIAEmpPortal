namespace GSIA.Models
{
    public class ValidationInputModel
    {
        public string Schema { get; set; } = String.Empty;
        public string VEmpNumber { get; set; } = String.Empty;
        public string Position_ { get; set; } = String.Empty;
        public string MovNumber { get; set; } = String.Empty;
        public string SecLicense { get; set; } = String.Empty;
        public DateTime? DateHired { get; set; }
        public string VPassword { get; set; } = String.Empty;

    }
}
