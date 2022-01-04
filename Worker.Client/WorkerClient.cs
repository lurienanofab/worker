using LNF.Models.Service;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Configuration;

namespace Worker.Client
{
    public class WorkerClient
    {
        private IRestClient _client;

        public WorkerClient()
        {
            var baseUrl = ConfigurationManager.AppSettings["WorkerAPI"];
            var username = ConfigurationManager.AppSettings["BasicAuthUsername"];
            var password = ConfigurationManager.AppSettings["BasicAuthPassword"];

            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("Missing required AppSetting: WorkerAPI");

            if (string.IsNullOrEmpty(username))
                throw new Exception("Missing required AppSetting: BasicAuthUsername");

            if (string.IsNullOrEmpty(password))
                throw new Exception("Missing required AppSetting: BasicAuthPassword");

            _client = new RestClient(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }

        public string Execute(string command, string[] args)
        {
            IRestRequest req = new RestRequest("execute");
            WorkerRequest wr = new WorkerRequest() { Command = command, Args = args };
            req.AddJsonBody(wr);
            var resp = _client.Post(req);

            var content = JsonConvert.DeserializeObject<string>(resp.Content);

            if (resp.IsSuccessful)
                return content;

            throw new Exception(content);
        }
    }
}
