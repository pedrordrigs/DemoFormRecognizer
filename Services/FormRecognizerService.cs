using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

public class FormRecognizerService
{
    private readonly FormRecognizerClient _client;

    public FormRecognizerService(IConfiguration configuration)
    {
        var endpoint = Environment.GetEnvironmentVariable("FormRecognizerEndpoint");
        var apiKey = Environment.GetEnvironmentVariable("FormRecognizerApiKey");

        var credential = new AzureKeyCredential(apiKey);
        _client = new FormRecognizerClient(new Uri(endpoint), credential);
    }

    public async Task<string> AnalyzeFormAsync(Stream formStream)
    {
        var operation = await _client.StartRecognizeContentAsync(formStream);
        var operationResponse = await operation.WaitForCompletionAsync();
        return JsonConvert.SerializeObject(operationResponse.Value);
    }
}
