using GsiaLibrary.Models;

namespace GsiaLibrary.DataAccess
{
    public interface IApiAccess
    {
        Task<QueryResponseModel> ExecuteDataFromApi<T>(string ApiUrl, T content);
        QueryResponseModel FetchDataFromApi(string ApiUrl);
    }
}