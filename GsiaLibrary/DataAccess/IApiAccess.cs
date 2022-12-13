using GsiaLibrary.Models;

namespace GsiaLibrary.DataAccess
{
    public interface IApiAccess
    {
        QueryResponseModel ExecuteDataFromApi(string ApiUrl, HttpContent content);
        QueryResponseModel FetchDataFromApi(string ApiUrl);
    }
}