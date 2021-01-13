# Bibliotekariens Gränssnitt

## Kom igång

- Ladda ned repot från github.
- Öppna solution-filen i visual studio 2019.
- [Konfigurera anslutningssträngar](#Konfigurering-av-applikation)
- Se till att API:t är igång och kontaktbart.
- Kör programmet.

## Komponenter

- Programmet är skrivet i språket C# och i ASP.NET CORE 3.1. 
- prgrammet har följande paket tillagda:
    - Microsoft.Extensions.Http.Polly v5.0.1
    - newtonsoft.json v12.0.3

- programmet är en del av en större arkitektur. Detta är klient-delen.

##  För en applikation i produktion

### Driftmiljöer för denna applikation

Detta program är inte klart för produktionssättning. Det bör, tills det är klarskrivet __*endast*__ användas i test-/utvecklingsmiljö! 

### Konfigurering av applikation

Konfigurering för att koppla upp mot API görs med denna kod i **Startup.cs** i metoden ConfigureServices(IServiceCollection services):
```
            services.AddHttpClient("API Client", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44303/api");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                { TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)}));
```

