using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using DotNetEnv;

public class CallsPlugin : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromPhone;

    public CallsPlugin()
    {
        Env.Load();

        _accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID") 
            ?? throw new InvalidOperationException("TWILIO_ACCOUNT_SID environment variable is required");
        _authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN") 
            ?? throw new InvalidOperationException("TWILIO_AUTH_TOKEN environment variable is required");
        _fromPhone = Environment.GetEnvironmentVariable("TWILIO_FROM_PHONE") 
            ?? throw new InvalidOperationException("TWILIO_FROM_PHONE environment variable is required");

        _httpClient = new HttpClient();
        var byteArray = Encoding.ASCII.GetBytes($"{_accountSid}:{_authToken}");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }

    [KernelFunction, Description("Make a voice call using the Twilio API.")]
    public async Task<string> MakeCallAsync(string toPhone, string voiceUrl = "http://demo.twilio.com/docs/voice.xml")
    {
        try
        {
            var url = $"https://api.twilio.com/2010-04-01/Accounts/{_accountSid}/Calls.json";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("To", toPhone),
                new KeyValuePair<string, string>("From", _fromPhone),
                new KeyValuePair<string, string>("Url", voiceUrl),
            });

            var response = await _httpClient.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? $"Call initiated to {toPhone}."
                : $"Failed to make call: {response.StatusCode} - {responseText}";
        }
        catch (Exception ex)
        {
            return $"Error during call: {ex.Message}";
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}