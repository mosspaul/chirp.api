using core.DTOs.SimpleFinDTOs;
using Newtonsoft.Json;

namespace core.Gateways;

public class SimpleFinBridgeGateway
{
    private readonly HttpClient _http;
    public SimpleFinBridgeGateway(HttpClient http)
    {
        _http = http;
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
    public async Task<AccountSet?> GetAccountSet(string? userAccessUrl)
    {
        if (userAccessUrl == null)
        {
            throw new Exception("Access url can not be null");
        }
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            userAccessUrl
        );
        var response = await _http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
        string json = await response.Content.ReadAsStringAsync();
        AccountSet? accountSet = JsonConvert.DeserializeObject<AccountSet>(json);
        return accountSet;
    }
    private string DecodeToken(string base64Token)
    {
        byte[] data = Convert.FromBase64String(base64Token);
        string decodedString = System.Text.Encoding.UTF8.GetString(data);
        return decodedString;
    }
}