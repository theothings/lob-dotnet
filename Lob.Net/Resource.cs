using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lob.Net.Resouces
{
    abstract public class Resource : IResource
    {
        private Lob lob;
        const string BaseUrl = "https://api.lob.com";
        protected abstract string ResourcePathName { get; }
        protected HttpClient client = new HttpClient();

        public Resource(Lob lob)
        {
            this.lob = lob;
            this.client.BaseAddress = new Uri( BaseUrl );

            // Set default request headers
            this.client.DefaultRequestHeaders.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", this.lob.ApiKey, "")));
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            this.client.DefaultRequestHeaders.UserAgent.ParseAdd("Lob/v1 C#Bindings/" + this.lob.ClientVersion);
            this.client.DefaultRequestHeaders.Add("Lob-Version", this.lob.Version);
        }

        public dynamic All(dynamic options = null, bool includeMetaData = false)
        {
            var all = SendRequest(
                    "GET",
                    this.ResourcePathName,
                    options
                );

            if (includeMetaData)
                return all;
            else
                return all["data"];
        }

        public dynamic Get(string id)
        {
            return this.SendRequest(
                    "GET",
                    this.ResourcePathName + "/" + id
                );
        }

        public dynamic Create(dynamic data)
        {
            return this.SendRequest(
                    "POST",
                    this.ResourcePathName,
                    null,
                    data
                );
        }

        public dynamic Delete(string id)
        {
            return this.SendRequest(
                    "DELETE",
                    this.ResourcePathName + "/" + id
                );
        }

        protected dynamic SendRequest(string method, string path, dynamic query = null, dynamic body = null)
        {
            // Get the full path from the path and url parameters
            string fullPath = this.GetPath(path, query);

            // Set the http headers

            HttpResponseMessage response;
            // Get the response
            if (method == "GET")
                response = client.GetAsync(fullPath).Result;
            else if (method == "POST")
                response = client.PostAsJsonAsync(fullPath, (object)body).Result;
            else if (method == "DELETE")
                response = client.DeleteAsync(fullPath).Result;
            else
                response = client.GetAsync(fullPath).Result;

            // If not a succesful response than process throw the appropriate exception based on response code
            if (!response.IsSuccessStatusCode)
            {
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException();
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }
                if (response.StatusCode == HttpStatusCode.BadRequest || (int)response.StatusCode == 422)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    throw new BadRequestException();
                }
                if ((int)response.StatusCode == 429)
                {
                    throw new TooManyRequestsException();
                }
                if((int)response.StatusCode >= 500)
                {
                    throw new ServerErrorException();
                }

                throw new Exception("An unexpected error occured");
            }

            var resultText = response.Content.ReadAsStringAsync().Result;
            // Return JSON as a dynamic object
            return JObject.Parse( resultText );
        }

        protected string GetPath(string path, ExpandoObject query)
        {
            path = "/v1/" + path;
            if (query == null)
                return path;

            path = path += "?";
            foreach (KeyValuePair<string, object> pair in query)
            {
                path += pair.Key + "=" + pair.Value + "&";
            }

            return path;
        }
    }
}
