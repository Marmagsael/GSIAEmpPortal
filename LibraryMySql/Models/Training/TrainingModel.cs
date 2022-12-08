
namespace LibraryMySql.Models.Training; 

public class TrainingModel
{
    public int Id { get; set; }
    public string? Program { get; set; } = string.Empty;
    public string? Venue { get; set; } = string.Empty;
    public string? Trainor { get; set; } = string.Empty;
    public DateTime? DateTaken { get; set; } = DateTime.UtcNow;
    public string? Type { get; set; } = string.Empty;


}
