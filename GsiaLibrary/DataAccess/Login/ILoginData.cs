using GsiaLibrary.Models;
using GsiaLibrary.Models.UI.Login;

namespace GsiaLibrary.DataAccess.Login
{
    public interface ILoginData
    {
        string GetCompanyInfo();
        QueryResponseModel ValidateEmployeeByAddedCredentials(VerifyAccountInputModel? input);
        QueryResponseModel ValidateEmployeeByEmail(string email);
        QueryResponseModel ValidateEmployeeByLoginNameAndPassword(LoginInputModel? input);
    }
}