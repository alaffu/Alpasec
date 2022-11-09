using CommandLine;

public class Program
{
    public static string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Alpasec");
    public static string usersPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Alpasec");
    public static string usersJsonPath = Path.Combine(Program.rootPath, "users.json");

    public class Options
    {
        [Option('a', "add-user", Required = false, HelpText = "Adds new user")]
        public string? AddUser { get; set; }

        [Option('s', "set-password", Required = false, HelpText = "Sets user password")]
        public string? SetPassword { get; set; }

        [Option('l', "login", Required = false, HelpText = "Username for login")]
        public string? Login { get; set; }

        [Option('p', "password", Required = false, HelpText = "Password for login")]
        public string? Password { get; set; }

        [Option('e', "encrypt", Required = false, HelpText = "Encrypt existing files")]
        public string? Encrypt { get; set; }

        [Option('d', "decrypt", Required = false, HelpText = "Decrypt existing files")]
        public string? Decrypt { get; set; }

        [Option('l', "logout", Required = false, HelpText = "Logout user")]
        public bool Logout { get; set; }
    }

    public static void PrepareEnvironment()
    {
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        if (!Directory.Exists(usersPath))
            Directory.CreateDirectory(usersPath);

        if (!File.Exists(usersJsonPath))
            File.Create(usersJsonPath).Close();
    }

    public static void RunOptions(Options opts)
    {
        if (opts.AddUser != null && opts.SetPassword != null)
        {
            User user = new User(opts.AddUser, opts.SetPassword);
            user.Save();
        }

        else if (opts.Login != null && opts.Password != null)
        {
            Auth.Login(opts.Login, opts.Password);
        }

        else if (opts.Encrypt != null)
        {
            User user = Auth.GetLoggedUser();

            if (user is null)
            {
                Console.WriteLine(">> You must be logged in to perform this action");
                return;
            }

            string result = Encrypt.RunPython("encrypt", opts.Encrypt, user.Password);
            user.MoveFile(result, opts.Encrypt);
        }

        else if (opts.Decrypt != null)
        {
            User user = Auth.GetLoggedUser();

            if (user == null)
            {
                Console.WriteLine(">> You must be logged in to perform this action");
                return;
            }

            Encrypt.RunPython("decrypt", opts.Decrypt, user.Password);
        }

        else if (opts.Logout)
        {
            Auth.Logout();
        }
        
        else {
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

