using Newtonsoft.Json;

class Administrator : User
{

    public void Save(User newUser)
    {
        if (!newUser.AlreadyExists())
        {
            List<User> usersList = new List<User>();

            string json = File.ReadAllText(Program.usersJsonPath);

            if (json != "")
                usersList = JsonConvert.DeserializeObject<List<User>>(json);

            usersList?.Add(newUser);

            File.WriteAllText(Program.usersJsonPath, JsonConvert.SerializeObject(usersList));
            Console.WriteLine(">> User added");
        }
        else
        {
            Console.WriteLine(">> User already exists");
        }
    }

    public Administrator(string name, string password) : base(name, password, true)
    {
        Name = name;
        Password = password;
    }
}
