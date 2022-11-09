using Newtonsoft.Json;

class User
{
    public string Name { get; set; }
    public string Password { get; set; }
    private string Path = Path.Combine(Program.usersPath, Name);

    private bool UserAlreadyExists()
    {
        string json = File.ReadAllText(Program.usersJsonPath);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        foreach (User user in usersList)
        {
            if (user.Name == this.Name)
                return true;
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
        } else {
            Console.WriteLine(">> User already exists");
        }
    }

    public void Delete()
    {

    }

    public User(string name, string password)
    {
        Name = name;
        Password = password;
    }
}
