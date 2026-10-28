
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

string? clientId = Environment.GetEnvironmentVariable("GRAPH_CLIENT_ID");
string tenantId = Environment.GetEnvironmentVariable("GRAPH_TENANT_ID") ?? "common";

if (string.IsNullOrWhiteSpace(clientId))
{
    Console.WriteLine("Environment variable GRAPH_CLIENT_ID is missing. Set it to your App (client) ID.");
    return;
}

var scopes = new[] { "Calendars.ReadWrite" };

var deviceCodeCredential = new DeviceCodeCredential(new DeviceCodeCredentialOptions
{
    ClientId = clientId,
    TenantId = tenantId,
    DeviceCodeCallback = (code, ct) =>
    {
        Console.WriteLine(code.Message);
        return Task.CompletedTask;
    }
});

var graph = new GraphServiceClient(deviceCodeCredential, scopes);

string subject = args.Length > 0 ? args[0] : "Testitapahtuma (Graph .NET)";
DateTime startUtc = DateTime.UtcNow.AddMinutes(5);
DateTime endUtc   = startUtc.AddMinutes(30);
if (args.Length > 1 && DateTime.TryParse(args[1], out var parsed))
{
    startUtc = DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
    endUtc = startUtc.AddMinutes(30);
}

var newEvent = new Event
{
    Subject = subject,
    Body = new ItemBody { ContentType = BodyType.Text, Content = "Luotu Microsoft Graph .NET SDK:llä" },
    Start = new DateTimeTimeZone { DateTime = startUtc.ToString("yyyy-MM-ddTHH:mm:ss"), TimeZone = "UTC" },
    End   = new DateTimeTimeZone { DateTime = endUtc.ToString("yyyy-MM-ddTHH:mm:ss"), TimeZone = "UTC" },
    Location = new Location { DisplayName = "Online" },
    IsOnlineMeeting = true,
    OnlineMeetingProvider = OnlineMeetingProviderType.TeamsForBusiness,
};

Console.WriteLine("Luodaan tapahtumaa...");
var created = await graph.Me.Events.PostAsync(newEvent);

if (created is null)
{
    Console.WriteLine("Tapahtuman luonti epäonnistui.");
    return;
}

Console.WriteLine($"OK. ID: {created.Id}");
if (created.OnlineMeeting != null)
{
    Console.WriteLine($"Join URL: {created.OnlineMeeting.JoinUrl}");
}
else if (!string.IsNullOrWhiteSpace(created.OnlineMeetingUrl))
{
    Console.WriteLine($"Join URL: {created.OnlineMeetingUrl}");
}
