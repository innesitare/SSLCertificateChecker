using SSLCertificateChecker.Interfaces;

namespace SSLCertificateChecker.Issuers;

public class Issuer : BackgroundService
{
    private readonly ILogger<Issuer> _logger;
    private readonly ICertificateChecker _certificateChecker;

    public Issuer(ILogger<Issuer> logger, ICertificateChecker certificateChecker)
    {
        _logger = logger;
        _certificateChecker = certificateChecker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _certificateChecker.Check("https://youtube.com/");
    }
}