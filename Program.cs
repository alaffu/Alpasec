using CommandLine;

public class Program
{
    public static string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Alpasec");

    public class Options
    {
        [Option('a', "add-user", Required = false, HelpText = "Adds new user")]
        public string AddUser { get; set; }

        [Option('p', "set-password", Required = false, HelpText = "Sets user password")]
        public string Password { get; set; }

        [Option('e', "encrypt", Required = false, HelpText = "Encrypt existing files")]
        public string Encrypt { get; set; }

        [Option('d', "decrypt", Required = false, HelpText = "Decrypt existing files")]
        public string Decrypt { get; set; }

        [Option('l', "logout", Required = false, HelpText = "Logout user")]
        public string Logout { get; set; }
    }

    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions);
    }

    public static void RunOptions(Options opts)
    {
        if (opts.AddUser != null)
        {
            Console.WriteLine("Adding user");
        }
        else if (opts.Password != null)
        {
            Console.WriteLine("Enter new password");
        }
        else if (opts.Encrypt != null)
        {
            Console.WriteLine("Encrypting...");
            Encrypt.RunPython("encrypt", opts.Encrypt);
        }
        else if (opts.Decrypt != null)
        {
            Console.WriteLine("Decrypting...");
            Encrypt.RunPython("decrypt", opts.Decrypt);
        }
        else if (opts.Logout != null)
        {
            Console.WriteLine("Logging out...");
        } else {
            Console.WriteLine("Alpasec.Cli 1.0.0");
        }
    }
}

