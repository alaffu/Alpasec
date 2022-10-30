using System.Diagnostics;

public class EncryptFile
{
    public static string RunPython()
    {
        const string PYTHON_PATH = "/usr/bin/python3";
        const string SCRIPT_RELATIVE_PATH = "/scripts/script_encrypt.py";


        ProcessStartInfo start = new ProcessStartInfo();
        start.Arguments = Path.Join(Directory.GetCurrentDirectory(), SCRIPT_RELATIVE_PATH);
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
