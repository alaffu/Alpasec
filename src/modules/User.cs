using Newtonsoft.Json;

public class User
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsLoggedIn { get; set; }
    private string UserPath { get; set; }

    private bool UserAlreadyExists()
    {
        string json = File.ReadAllText(Program.usersJsonPath);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        if (usersList != null)
        {
            foreach (User user in usersList) {
                if (user.Name == this.Name)
                    return true;
            }
        }

        return false;
    }

    public void Save()
    {
        if (!UserAlreadyExists())
        {
            List<User> usersList = new List<User>();

            string json = File.ReadAllText(Program.usersJsonPath);

            if (json != "")
                usersList = JsonConvert.DeserializeObject<List<User>>(json);
            
            usersList.Add(this);
            File.WriteAllText(Program.usersJsonPath, JsonConvert.SerializeObject(usersList));
            Console.WriteLine(">> User added");
        } else {
            Console.WriteLine(">> User already exists");
        }
    }

    public void MoveFile(string result, string file)
    {
        if (!result.StartsWith($">> File {file} encrypted"))
            return;

        if (!Directory.Exists(UserPath))
            Directory.CreateDirectory(UserPath);

        string filePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), file);
        string newFilePath = Path.Combine(UserPath, file);

        try {
            System.IO.File.Move(filePath, newFilePath);
        } catch (Exception e) {
            Console.WriteLine(">> A file with this name already exists in your user folder");
        }
    }

    public User(string name, string password)
    {
        Name = name;
        Password = password;
        IsLoggedIn = false;
        UserPath = Path.Combine(Program.usersPath, Name);
    }
}
