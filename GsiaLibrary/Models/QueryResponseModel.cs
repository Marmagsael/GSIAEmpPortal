﻿
namespace GsiaLibrary.Models;

public class QueryResponseModel 
{
    public string? Reponse { get; set; }
    public string? Code { get; set; } = "";
    public string? Description { get; set; }
    public string? ErrorField { get; set; }
    public string? QueryResult { get; set; } = null;

}
