using core.DTOs.SimpleFinDTOs;
using Newtonsoft.Json;

namespace core.Gateways;

public class SimpleFinBridgeGateway
{
    private readonly HttpClient _http;
    private readonly DateUtility _dateUtility;
    public SimpleFinBridgeGateway(HttpClient http, DateUtility dateUtility)
    {
        _http = http;
        _dateUtility = dateUtility;
    }

    public async Task<string> GetAccessUrl(string simpleFinToken)
    {

        var request = new HttpRequestMessage(
            HttpMethod.Post, 
            DecodeToken(simpleFinToken)
        );
        HttpResponseMessage response = await _http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
        string accessUrl = await response.Content.ReadAsStringAsync();
        return accessUrl;
    }
    public async Task<AccountSetDto?> GetAccountSet(string? userAccessUrl, CancellationToken ct)
    {
        if (userAccessUrl == null)
        {
            throw new Exception("Access url can not be null");
        }

        var uri = new Uri(userAccessUrl);
        var credentials = uri.UserInfo; // "username:password"
        var base64Credentials = Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(credentials)
        );

        var lastMonth = DateTime.Now.AddDays(-30);
        var timestamp = _dateUtility.ConvertDateToTimestamp(lastMonth);
        var cleanUrl = $"{uri.Scheme}://{uri.Host}{uri.AbsolutePath}/accounts?version=2&start-date={timestamp}";

        var request = new HttpRequestMessage(HttpMethod.Get, cleanUrl);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Basic", base64Credentials
        );
        var response = await _http.SendAsync(request, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
        string json = await response.Content.ReadAsStringAsync(ct);
        AccountSetDto? accountSet = JsonConvert.DeserializeObject<AccountSetDto>(json);
        return accountSet;
    }
    private string DecodeToken(string base64Token)
    {
        byte[] data = Convert.FromBase64String(base64Token);
        string decodedString = System.Text.Encoding.UTF8.GetString(data);
        return decodedString;
    }
}