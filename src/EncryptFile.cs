using System.Diagnostics;

public static class EncryptFile
{
    public static string RunPython()
    {
        const string PYTHON_PATH = "python.exe";
        const string SCRIPT_RELATIVE_PATH = "/src/scripts/script_encrypt.py";

        ProcessStartInfo start = new ProcessStartInfo();
        start.Arguments = $"{Path.Join(Environment.CurrentDirectory, SCRIPT_RELATIVE_PATH)} text.txt";
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
