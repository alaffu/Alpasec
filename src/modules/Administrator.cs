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

    public void ChangeUser(string username, string newUsername)
    {
        // remember to check if there is a user logged
        var loggedUser = Auth.GetLoggedUser();

        void changeNameJsonFile(string name, string newName, string passwordFuture = "")
        {
            var userToChangeIndex = User.SearchJsonIndexUser(name);

            string userJsonFile = File.ReadAllText(Program.usersJsonPath);
            dynamic userJson = JsonConvert.DeserializeObject(userJsonFile);

            // string name = "changed_name";
            // string password = "changed_password";

            // raise error if there the program dont find the user
            if (userToChangeIndex != null)
            {
                userJson[userToChangeIndex].Name = newName;
                string output = JsonConvert.SerializeObject(userJson);
                File.WriteAllText(Program.usersJsonPath, output);
            }
        }

        void renameUserDirectory(string name, string newName)
        {
            string pathUserDirectory = Program.usersPath + "/" + name;
            string newPathUserDirectory = Program.usersPath + "/" + newName;

            // search file and check if it exists
            // string[] fileEntries = Directory.GetFiles(Program.usersPath + "/" + user?.Name);
            Directory.Move(pathUserDirectory, newPathUserDirectory);
        }

        User? user = User.Search(username);

        changeNameJsonFile(username, newUsername);

        if (user?.Name != null)
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
        // else
        // {
        //     Console.WriteLine($">> The directory: {pathUserDirectory}\n does not exist");
        // }
    }

    public void deleteUser(string username)
    {

        void deleteUserFromJson(User user)
        {
            var userToDeleteIndex = user.SearchJsonIndex();

            string userJsonFile = File.ReadAllText(Program.usersJsonPath);
            dynamic userJson = JsonConvert.DeserializeObject(userJsonFile);
            Console.WriteLine(userJson.GetType());

            if (userToDeleteIndex != null)
            {
                userJson[userToDeleteIndex].Remove();
                string output = JsonConvert.SerializeObject(userJson);
                File.WriteAllText(Program.usersJsonPath, output);
            }
        }

        User? user = User.Search(username);
        if (user != null)
        {
            deleteUserFromJson(user);
            deleteUserDirectory(user);
            Console.WriteLine("Usuário deletado com sucesso");
        }
        else
        {
            Console.WriteLine(">> Nome do usuário incorreto ou inexistente");
        }
    }

    public Administrator(string name, string password) : base(name, password, true)
    {
        Name = name;
        Password = password;
    }
}
