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

        [Option('l', "login", Required = false, HelpText = "Logs in user")]
        public string? Login { get; set; }

        [Option('p', "password", Required = false, HelpText = "Password for user")]
        public string? Password { get; set; }

        [Option('g', "logout", Required = false, HelpText = "Logout user")]
        public bool Logout { get; set; }

        [Option('e', "encrypt", Required = false, HelpText = "Encrypt existing files")]
        public string? Encrypt { get; set; }

        [Option('n', "no-move", Required = false, HelpText = "Do not move files")]
        public bool NoMove { get; set; }

        [Option('d', "decrypt", Required = false, HelpText = "Decrypt existing files")]
        public string? Decrypt { get; set; }

        [Option('u', "list-users", Required = false, HelpText = "List all users")]
        public bool ListUsers { get; set; }

        [Option('t', "list", Required = false, HelpText = "List files in user folder")]
        public bool List { get; set; }
    }

    public static void RunOptions(Options opts)
    {
        Console.WriteLine(@"
    ___    __            _____            _________ 
   /   |  / /___  ____ _/ ___/___  _____ / ____/ (_)
  / /| | / / __ \/ __ `/\__ \/ _ \/ ___// /   / / / 
 / ___ |/ / /_/ / /_/ /___/ /  __/ /___/ /___/ / /  
/_/  |_/_/ .___/\__,_//____/\___/\___(_)____/_/_/   
        /_/                                         
");
        if (opts.AddUser != null && opts.Password != null)
        {
            User user = Auth.GetLoggedUser();

            if (user is null || !user.IsAdministrator)
            {
                Console.WriteLine(">> You are not logged in as admin");
                return;
            }

            User newUser = new User(opts.AddUser, opts.Password, false);
            newUser.Save();
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

            if (!opts.NoMove)
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

        else if (opts.ListUsers)
        {
            Auth.ListUsers();
        }

        else if (opts.List)
        {
            User user = Auth.GetLoggedUser();

            if (user == null)
            {
                Console.WriteLine(">> You must be logged in to perform this action");
                return;
            }

            user.ListFiles();
        }
        
        else {
            Console.WriteLine(">> No arguments passed or the command is incomplete");
        }
    }
    
    public static void Main(string[] args)
    {
        PrepareEnvironment.run();

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions);
    }
}

