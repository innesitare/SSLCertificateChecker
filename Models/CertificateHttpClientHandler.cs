namespace SSLCertificateChecker.Models
{
    public class CertificateHttpClientHandler : HttpClientHandler
    {
        private readonly ConcurrentDictionary<Uri, X509Certificate2> _certificates = new();

        public CertificateHttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (requestMessage, certificate, _, sslErrors) =>
            {
                if (certificate == null || requestMessage.RequestUri == null)
                    return false;
                    
                _certificates.AddOrUpdate(requestMessage.RequestUri, _ => certificate, (_, _) => certificate);
                Console.WriteLine($"Requested URI: {requestMessage.RequestUri}");
                Console.WriteLine($"Errors: {sslErrors}");

                return sslErrors == SslPolicyErrors.None;
            };
        }

        public X509Certificate2? GetCertificate(Uri uri) => _certificates.GetValueOrDefault(uri);
    }
}
