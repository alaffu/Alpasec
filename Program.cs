using CommandLine;

public class Program
{
    public class Options
    {
        [Option('a', "add-user", Required = false, HelpText = "Adds new user")]
        public string? AddUser { get; set; }

        [Option('p', "set-password", Required = false, HelpText = "Sets user password")]
        public string? Password { get; set; }

        [Option('e', "encrypt", Required = false, HelpText = "Encrypt existing files and folders")]
        public string? Encrypt { get; set; }

        [Option('l', "logout", Required = false, HelpText = "Logout user")]
        public string? Logout { get; set; }
    }

    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(
            opt =>
            {
                if (opt.Encrypt.Length != 0)
                {
                    //Console.WriteLine("Encrypting...");
                    //Console.WriteLine(Directory.GetCurrentDirectory());
                    
                    // EncryptFile.RunPython();
                }
            }
        );

    }
}

