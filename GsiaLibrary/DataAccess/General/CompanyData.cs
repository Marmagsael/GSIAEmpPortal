using Microsoft.Extensions.Configuration;

namespace GsiaLibrary.DataAccess.General;

public class CompanyData : ICompanyData
{

    private readonly IConfiguration _config;

    public CompanyData(IConfiguration config)
    {
        _config = config;
    }

    public string GetCompanyInfo()
    {
        return _config.GetSection("CompanyInfo:CompanyName").Value;
    }
}
