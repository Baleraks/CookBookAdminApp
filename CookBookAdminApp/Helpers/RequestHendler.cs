using RestSharp;
using System.Net;
using System.Text.Json;

namespace CookBookAdminApp.Helpers
{
    public static class RequestHandler
    {
        public static async Task<ResponseValue<T>> HandlerAsync<T>(ResponseValue<T> responseValue, RestResponse restResponse)
        {
            switch (restResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        responseValue.Exception = restResponse.Content;
                        responseValue.Status = ResponseStatus.Error;
                        break;
                    }
                case HttpStatusCode.Unauthorized:
                    {
                        var res = await AuthHendler.RefreshAsync();
                        if (res)
                        {
                            var client = ClientFactory.CreateClient(restResponse.Request.Resource);
                            var request = restResponse.Request;
                            var response = await client.ExecuteAsync(request, new CancellationTokenSource(5000).Token);
                            responseValue = await HandlerAsync(responseValue, response);
                        }
                        else
                        {
                            responseValue.Exception = restResponse.Content;
                            responseValue.Status = ResponseStatus.Error;
                        }
                        break;
                    }
                case HttpStatusCode.InternalServerError:
                    {
                        responseValue.Exception = nameof(HttpStatusCode.InternalServerError);
                        responseValue.Status = ResponseStatus.Error;
                        break;
                    }
                case HttpStatusCode.NotFound:
                    {
                        responseValue.Exception = nameof(HttpStatusCode.NotFound);
                        responseValue.Status = ResponseStatus.Error;
                        break;
                    }
                case HttpStatusCode.NoContent:
                    {
                        responseValue.Status = ResponseStatus.Completed;
                        break;
                    }
                default:
                    {
                        responseValue.Value = JsonSerializer.Deserialize<T>(restResponse.Content);
                        responseValue.Status = ResponseStatus.Completed;
                        break;
                    }
            }
            return responseValue;
        }
    }
}
