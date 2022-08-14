namespace SSLCertificateChecker.Interfaces
{
    public interface ICertificateChecker
    {
        Task Check(string url);
    }
}
