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

        [Option('e', "change-user", Required = false, HelpText = "Change the user needs to specify username")]
        public string? ChangeUser { get; set; }

        [Option('e', "new-name", Required = false, HelpText = "Set new username")]
        public string? NewName { get; set; }

        [Option('x', "test", Required = false, HelpText = "test module shouldnt be used in production")]
        public string? Test { get; set; }
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

        var user = Auth.GetLoggedUser();

        if (user is Administrator)
        {
            Administrator loggedAdmin = new Administrator(user.Name, user.Password);
            // new AdministratorOptionsHandler(opts, loggedAdmin, user);
            AdministratorOptionsHandler.Start(opts, loggedAdmin, user);

        }
        else if (user is not Administrator)
        {
            // new UserOptionsHandler(opts, user);
            UserOptionsHandler.Start(opts, user);
            return;
        }

    }

    public static void Main(string[] args)
    {
        PrepareEnvironment.run();

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions);
    }
}

