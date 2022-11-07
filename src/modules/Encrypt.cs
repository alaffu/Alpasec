using System.Diagnostics;

public static class Encrypt
{
    public static string RunPython(string action, string fileName)
    {
        const string PYTHON_PATH = "python.exe";
        string SCRIPT_RELATIVE_PATH = Path.Combine(Program.rootPath, "scripts/encrypt.py");

        ProcessStartInfo start = new ProcessStartInfo();
        start.Arguments = $"{SCRIPT_RELATIVE_PATH} {action} {fileName}";
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
                Console.WriteLine(stderr);
                Console.WriteLine(result);

                return result;
            }
        }

    }

}
