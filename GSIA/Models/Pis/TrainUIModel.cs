using System.ComponentModel.DataAnnotations;

namespace GSIA.Models.Pis;

public class TrainUiModel
{
    [Required]
    [Display(Name = "Empnumber")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Empnumber { get; set; }

    [Required]
    [Display(Name = "Program")]
    [StringLength(60, ErrorMessage = "This field must be 60 long.")]
    public string? Program { get; set; }

    [Required]
    [Display(Name = "Taken")]
    [StringLength(20, ErrorMessage = "This field must be 20 long.")]
    public string? Taken { get; set; }

    [Required]
    [Display(Name = "School")]
    [StringLength(50, ErrorMessage = "This field must be 50 long.")]
    public string? School { get; set; }

    [Required]
    [Display(Name = "Trainor")]
    [StringLength(30, ErrorMessage = "This field must be 30 long.")]
    public string? Trainor { get; set; }

    [Required]
    [Display(Name = "Type")]
    [StringLength(9, ErrorMessage = "This field must be 9 long.")]
    public string? Type { get; set; }

    [Required]
    [Display(Name = "Idtrainhdr")]
    public string? Idtrainhdr { get; set; }
}
