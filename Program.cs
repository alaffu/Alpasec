using CommandLine;

public class Program
{
    public static string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Alpasec");
    public static string usersPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Alpasec");
    public static string usersJsonPath = Path.Combine(Program.rootPath, "users.json");

    public class Options
    {
        [Option('a', "add-user", Required = false, HelpText = "Adds new user")]
        public string AddUser { get; set; }

        [Option('p', "set-password", Required = false, HelpText = "Sets user password")]
        public string SetPassword { get; set; }

        [Option('a', "user", Required = false, HelpText = "Username for login")]
        public string User { get; set; }

        [Option('p', "password", Required = false, HelpText = "Password for login")]
        public string Password { get; set; }

        [Option('e', "encrypt", Required = false, HelpText = "Encrypt existing files")]
        public string Encrypt { get; set; }

        [Option('d', "decrypt", Required = false, HelpText = "Decrypt existing files")]
        public string Decrypt { get; set; }

        [Option('l', "logout", Required = false, HelpText = "Logout user")]
        public string Logout { get; set; }
    }

    public static void PrepareEnvironment()
    {
        if (!File.Exists(usersJsonPath))
            File.Create(users).Close();

        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        if (!Directory.Exists(usersPath))
            Directory.CreateDirectory(usersPath);
    }

    public static void RunOptions(Options opts)
    {
        if (opts.AddUser != null && opts.SetPassword != null)
        {
            User user = new User(opts.AddUser, opts.SetPassword);
            user.Save();
        }
        else if (opts.User != null && opts.Password != null)
        {
            Console.WriteLine("Logging in");
        }
        else if (opts.Encrypt != null)
        {
            Encrypt.RunPython("encrypt", opts.Encrypt);
        }
        else if (opts.Decrypt != null)
        {
            Encrypt.RunPython("decrypt", opts.Decrypt);
        }
        else if (opts.Logout != null)
        {
            Console.WriteLine("Logging out...");
        } else {
            Console.WriteLine(">> No arguments passed or incomplete arguments");
        }
    }

    public static void Main(string[] args)
    {
        PrepareEnvironment();

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions);
    }
}

