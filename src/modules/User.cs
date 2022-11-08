// Import JsonConvert
using Newtonsoft.Json;

class User
{
    public string? Name { get; set; }

    public string? Password { get; set; }

    private bool UserAlreadyExists()
    {
        string users = Path.Combine(Program.rootPath, "users.json");

        if (!File.Exists(users))
        {
            File.Create(users).Close();
            return false;
        }

        string json = File.ReadAllText(users);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        foreach (User user in usersList)
        {
            if (user.Name == this.Name)
            {
                return true;
            }
        }
        return false;
    }

    public void Save()
    {
        if (!UserAlreadyExists())
        {
            string users = Path.Combine(Program.rootPath, "users.json");
            List<User> usersList = new List<User>();
            
            if (!File.Exists(users))
            {
                File.Create(users).Close();
            }

            string json = File.ReadAllText(users);

            if (json != "") {
                usersList = JsonConvert.DeserializeObject<List<User>>(json);
            }
            
            usersList.Add(this);
            File.WriteAllText(users, JsonConvert.SerializeObject(usersList));
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
