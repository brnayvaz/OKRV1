using Newtonsoft.Json;
using System.Net;


namespace Okr.Ocelot.Gateway.MockData
{
    public class FakeHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
                        
            //do stuff and optionally call the base handler..

            if (request.RequestUri.ToString() == "https://localhost:44359/api/mockData/httpClientMiddleWare")
            {
                /*var myContent = JsonConvert.SerializeObject(new { Message = "Mock Data Dönüldü" });
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.Content = byteContent;*/

                // Create the response.
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { Message = "Mock Data Dönüldü" }))
                };

                // Note: TaskCompletionSource creates a task that does not contain a delegate.
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);   // Also sets the task state to "RanToCompletion"
                return tsc.Task;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
