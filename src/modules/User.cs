using Newtonsoft.Json;

public class User
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsLoggedIn { get; set; }
    public bool IsAdministrator { get; set; }
    private string UserPath { get; set; }

    public bool AlreadyExists()
    {
        string json = File.ReadAllText(Program.usersJsonPath);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        if (usersList != null)
        {
            foreach (User user in usersList)
            {
                if (user.Name == this.Name)
                    return true;
            }
        }

        return false;
    }

    public void MoveFile(string result, string file)
    {
        if (!result.StartsWith($">> File {file} encrypted"))
            return;

        if (!Directory.Exists(UserPath))
            Directory.CreateDirectory(UserPath);

        string filePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), file);
        string newFilePath = Path.Combine(UserPath, file);

        try
        {
            System.IO.File.Move(filePath, newFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(">> A file with this name already exists in your user folder");
        }
    }

    public void ListFiles()
    {
        if (!Directory.Exists(UserPath))
        {
            Console.WriteLine(">> No files found");
            return;
        }

        string[] fileEntries = Directory.GetFiles(UserPath);

        if (fileEntries.Length == 0)
        {
            Console.WriteLine(">> No files found");
            return;
        }

        Console.WriteLine($">> Files stored in the {Name} folder:");
        foreach (string fileName in fileEntries)
            Console.WriteLine($"   {Path.GetFileName(fileName)}");
    }

    public User(string name, string password, bool isAdministrator)
    {
        Name = name;
        Password = password;
        IsLoggedIn = false;
        IsAdministrator = isAdministrator;
        UserPath = Path.Combine(Program.usersPath, Name);
    }
}
