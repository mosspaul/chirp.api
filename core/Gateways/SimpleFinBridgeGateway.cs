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
    private string DecodeToken(string base64Token)
    {
        byte[] data = Convert.FromBase64String(base64Token);
        string decodedString = System.Text.Encoding.UTF8.GetString(data);
        return decodedString;
    }
}