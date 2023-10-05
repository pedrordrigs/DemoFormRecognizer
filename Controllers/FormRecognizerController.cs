using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using Newtonsoft.Json.Linq;

[Route("api/[controller]")]
[ApiController]
public class FormRecognizerController : ControllerBase
{
    private readonly FormRecognizerService _formRecognizerService;
    private readonly ExcelService _excelService;

    public FormRecognizerController(FormRecognizerService formRecognizerService, ExcelService excelService)
    {
        _formRecognizerService = formRecognizerService;
        _excelService = excelService;
    }

    [HttpPost("analyze-form")]
    public async Task<IActionResult> AnalyzeForm(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            return BadRequest("Invalid file.");
        }

        using var stream = formFile.OpenReadStream();
        var formResult = await _formRecognizerService.AnalyzeFormAsync(stream);
        _excelService.GenerateExcel(formResult);
        return Ok("Excel generated successfully.");
    }

    [HttpPost("unescape-json")]
    public async Task<IActionResult> UnescapeJson(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            return BadRequest("Invalid file.");
        }

        using var stream = formFile.OpenReadStream();
        var escapedJson = await _formRecognizerService.AnalyzeFormAsync(stream);

        // Desescapar o JSON
        var unescapedJson = HttpUtility.HtmlDecode(escapedJson);

        // Formatando o JSON para torná-lo legível
        JToken jToken = JToken.Parse(unescapedJson);
        string formattedJson = jToken.ToString(Newtonsoft.Json.Formatting.Indented);

        // Gerar um nome de arquivo único baseado em timestamp
        string uniqueFileName = $"local_file_{DateTime.Now.Ticks}.json";

        // Salvar localmente como arquivo na pasta Outputs
        System.IO.File.WriteAllText($"Outputs/{uniqueFileName}", formattedJson);

        // Retornar o JSON formatado diretamente
        return Content(formattedJson, "application/json");
    }
}
