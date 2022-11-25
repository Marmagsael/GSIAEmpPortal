namespace GSIA.Models
{
    public class AuthenticatedDataModel
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
