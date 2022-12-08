using Newtonsoft.Json;

public class Administrator : User
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

    public void ChangeUser(string username, string newUsername, string newPassword)
    {
        void changeUserJsonFile(string name, string newName, string newPass)
        {
            var userToChangeIndex = User.SearchJsonIndexUser(name);

            string userJsonFile = File.ReadAllText(Program.usersJsonPath);
            dynamic userJson = JsonConvert.DeserializeObject(userJsonFile);

            if (userToChangeIndex != null)
            {
                if (newPass != null)
                    userJson[userToChangeIndex].Password = newPass;
                if (newName != null)
                    userJson[userToChangeIndex].Name = newName;

                string output = JsonConvert.SerializeObject(userJson);
                File.WriteAllText(Program.usersJsonPath, output);

                Console.WriteLine(">> User changed");
            }
        }

        void renameUserDirectory(string name, string newName)
        {
            string pathUserDirectory = Program.usersPath + "/" + name;
            string newPathUserDirectory = Program.usersPath + "/" + newName;

            if (Directory.Exists(pathUserDirectory))
            {
                Directory.Move(pathUserDirectory, newPathUserDirectory);
            }
        }

        User? user = User.Search(username);

        try
        {
            changeUserJsonFile(username, newUsername, newPassword);
        }
        catch
        {
            Console.WriteLine(">> User does not exist");

        }


        if (user?.Name != null && newUsername != null && user?.Name != newUsername)
        {
            renameUserDirectory(user?.Name, newUsername);
        }

    }

    private void deleteUserDirectory(User user)
    {
        string pathUserDirectory = Program.usersPath + "/" + user.Name;

        if (Directory.Exists(pathUserDirectory))
        {
            Boolean deleteEvenIfNotEmpty = true;
            Directory.Delete(pathUserDirectory, deleteEvenIfNotEmpty);
        }
    }

    public void deleteUser(string username)
    {

        void deleteUserFromJson(User user)
        {
            var userToDeleteIndex = user.SearchJsonIndex();

            string userJsonFile = File.ReadAllText(Program.usersJsonPath);
            dynamic userJson = JsonConvert.DeserializeObject(userJsonFile);

            if (userToDeleteIndex != null)
            {
                userJson[userToDeleteIndex].Remove();
                string output = JsonConvert.SerializeObject(userJson);
                File.WriteAllText(Program.usersJsonPath, output);
            }
        }

        User? user = User.Search(username);
        if (user != null && !user.IsAdministrator)
        {
            deleteUserFromJson(user);
            deleteUserDirectory(user);
            Console.WriteLine(">> User deleted");
        }
        else if (user.IsAdministrator)
        {
            Console.WriteLine(">> An administrator cannot be deleted");
        }
        else
        {
            Console.WriteLine(">> Missing or Incorrect username");
        }
    }

    public Administrator(string name, string password, string key) : base(name, password, key, true)
    {
        Name = name;
        Password = password;
        Key = key;
    }
}
