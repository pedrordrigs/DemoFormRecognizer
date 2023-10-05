using System.Diagnostics;
using System.Text.RegularExpressions;  // Adicione este using para Regex

public class PythonRunner
{
    public static void RunScript(string scriptPath, string arguments)
    {
        // Desescapa o argumento JSON
        string unescapedArguments = Regex.Unescape(arguments);

        // Grava o argumento JSON em um arquivo temporário
        string tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, unescapedArguments);

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{scriptPath}\" \"{tempFilePath}\"", // Passa o caminho do arquivo como argumento
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.Start();
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        string error = process.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(error))
        {
            Console.WriteLine("Error: " + error);
        }
        Console.WriteLine(result);

        // Remove o arquivo temporário
        File.Delete(tempFilePath);
    }
}
