namespace GsiaLibrary.Models.UI._001Main;

public class MenuOutputModel
{
    public int Odr { get; set; }
    public int Id { get; set; }
    public int IdParent { get; set; }
    public int Indent { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Icon1 { get; set; } = string.Empty;
    public string Icon2 { get; set; } = string.Empty;
    public string DispText { get; set; } = string.Empty;
    public int IsWithChild { get; set; }
    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;

}
