using SSLCertificateChecker.Interfaces;
namespace SSLCertificateChecker.Models
{
    public class CertificateChecker : ICertificateChecker
    {
        private readonly HttpClient _httpClient;
        private readonly CertificateHttpClientHandler _httpClientHandler;

        public CertificateChecker(HttpClient httpClient, CertificateHttpClientHandler clientHandler)
        {
            _httpClient = httpClient;
            _httpClientHandler = clientHandler;
        }

        public async Task Check(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpResponseMessage responseMessage = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
                responseMessage.EnsureSuccessStatusCode();

                X509Certificate2? certificate = _httpClientHandler.GetCertificate(uri);
                if (certificate == null) Console.WriteLine($"Certificate wasn't found for site: {url}");

                Console.WriteLine($"Expiration date: {certificate!.GetExpirationDateString()}");
                Console.WriteLine($"Issuer: {certificate.Issuer}");
                Console.WriteLine($"Subject: {certificate.Subject}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Message: {e.Message}");
            }
        }
    }
}
