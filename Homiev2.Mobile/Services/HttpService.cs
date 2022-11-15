namespace Homiev2.Mobile.Services
{
    public class HttpService
    {
        public HttpClient Client { get; private set; }
        public HttpService()
        {
            HttpClient Client = new();

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            Client = new HttpClient(insecureHandler);
#else
    HttpClient Client = new HttpClient();
#endif

            Client.BaseAddress = new Uri("https://10.0.2.2:7074");
        }

        private HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

    }
}
