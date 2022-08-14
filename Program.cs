using SSLCertificateChecker.Interfaces;
using SSLCertificateChecker.Issuers;
using SSLCertificateChecker.Models;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<CertificateHttpClientHandler>();
        services.AddHttpClient<ICertificateChecker, CertificateChecker>()
            .ConfigurePrimaryHttpMessageHandler(handler 
                => handler.GetRequiredService<CertificateHttpClientHandler>());
        services.AddHostedService<Issuer>();
    }).Build();
    
await host.RunAsync();