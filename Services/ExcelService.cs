public class ExcelService
{
    public void GenerateExcel(string jsonData)
    {
        var baseDirectory = Directory.GetCurrentDirectory();
        var relativeScriptPath = Path.Combine("Utils", "Python", "ExcelConverter.py");
        var scriptPath = Path.Combine(baseDirectory, relativeScriptPath);
        var culturePath = scriptPath.Replace("\\", "/");
        PythonRunner.RunScript(culturePath, jsonData);
    }
}
