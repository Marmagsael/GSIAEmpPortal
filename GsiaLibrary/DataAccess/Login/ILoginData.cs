using GsiaLibrary.Models;
using GsiaLibrary.Models.UI.Login;

namespace GsiaLibrary.DataAccess.Login
{
    public interface ILoginData
    {
        QueryResponseModel _10000_ValidateEmployeeByLoginNameAndPassword(LoginInputModel? input);
        QueryResponseModel _20000_ValidateEmployeeByEmail(string email);
        Task<QueryResponseModel> _3000_RegisterAccount(RegisterInputModel input);
    }
}