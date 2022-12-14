using System.Diagnostics;
using System.Runtime.InteropServices;

static class Encrypt
{
    public static string GetPythonPath()
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return "/usr/bin/python3";

        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return "python.exe";

        else
            throw new Exception("Operational system not yet supported");
    }

    public static string RunPython(string action, string fileName, string key)
    {
        try
        {
            string PYTHON_PATH = GetPythonPath();

            string SCRIPT_RELATIVE_PATH = Path.Combine(Program.rootPath, "scripts/encrypt.py");
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = $"{SCRIPT_RELATIVE_PATH} {action} {fileName} {key}";
            start.FileName = PYTHON_PATH;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;

            using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd();
                    string result = reader.ReadToEnd();

                    if (stderr != "")
                        Console.WriteLine($">> {stderr}");
                    if (result != "")
                        Console.WriteLine(result);

                    return result;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($">> {e.Message}");
            return "";
        }
    }
}
