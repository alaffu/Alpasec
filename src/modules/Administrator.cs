using Newtonsoft.Json;
class Administrator : User
{
    new public void Save()
    {
        if (!UserAlreadyExists())
        {
            List<User> usersList = new List<User>();

            string json = File.ReadAllText(Program.usersJsonPath);

            if (json != "")
                usersList = JsonConvert.DeserializeObject<List<User>>(json);

            usersList?.Add(this);

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
