using Flurl.Http;

// See Properties/launchSettings.json
var host = Environment.GetEnvironmentVariable("KC_HOST"); // e.g. "https://localhost:8080"
var realm = Environment.GetEnvironmentVariable("KC_REALM"); // e.g. "master"
var clientId = Environment.GetEnvironmentVariable("KC_CLIENT_ID");
var clientSecret = Environment.GetEnvironmentVariable("KC_CLIENT_SECRET");

if (host == null || realm == null || clientId == null || clientSecret == null)
    throw new InvalidOperationException("Please specify the required environment variables.");

var tokenResult = await $"{host}/auth/realms/{realm}/protocol/openid-connect/token"
    .PostUrlEncodedAsync(new {
        client_id = clientId,
        client_secret = clientSecret,
        grant_type = "client_credentials"
    })
    .ReceiveJson<TokenResult>();

var groups = await $"{host}/auth/admin/realms/{realm}/groups"
    .WithOAuthBearerToken(tokenResult.AccessToken)
    .GetJsonAsync<GroupResult[]>();

Console.WriteLine("Keycloak Groups");
foreach (var group in groups) {
    Console.WriteLine($" + {group.Name}");

    var members = await $"{host}/auth/admin/realms/{realm}/groups/{group.Id}/members"
        .WithOAuthBearerToken(tokenResult.AccessToken)
        .GetJsonAsync<MemberResult[]>();
    
    foreach (var member in members)
        Console.WriteLine($"   - {member}");
}