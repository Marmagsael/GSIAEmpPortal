using GsiaLibrary.Models;

namespace GsiaLibrary.DataAccess
{
    public interface IApiAccess
    {
        QueryResponseModel FetchDataFromApi(string ApiUrl);
    }
}