using Newtonsoft.Json;

public class Auth
{
    private static bool LoggedUserExits()
    {
        string json = File.ReadAllText(Program.usersJsonPath);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        if (usersList != null)
        {
            foreach (User user in usersList)
            {
                if (user.IsLoggedIn)
                    return true;
            }
        }

        return false;
    }

    public static User GetLoggedUser()
    {
        if (!LoggedUserExits()) return null;

        string json = File.ReadAllText(Program.usersJsonPath);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        if (usersList != null)
        {
            foreach (User user in usersList)
            {
                if (user.IsLoggedIn)
                    return user;
            }
        }

        return null;
    }

    public static void Login(string username, string password)
    {
        if (LoggedUserExits())
        {
            Console.WriteLine(">> A user is already logged in");
            return;
        }

        string json = File.ReadAllText(Program.usersJsonPath);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        if (usersList != null) {
            foreach (User user in usersList)
            {
                if (user.Name == username && user.Password == password)
                {
                    user.IsLoggedIn = true;
                    File.WriteAllText(Program.usersJsonPath, JsonConvert.SerializeObject(usersList));

                    Console.WriteLine(">> Logged in");
                    return;
                }
            }
        }

        Console.WriteLine(">> Invalid credentials");
    }

    public static void Logout()
    {
        if (!LoggedUserExits())
        {
            Console.WriteLine(">> You are not logged in");
            return;
        }

        string json = File.ReadAllText(Program.usersJsonPath);
        List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);

        if (usersList != null)
        {
            foreach (User user in usersList)
            {
                if (user.IsLoggedIn)
                {
                    user.IsLoggedIn = false;
                    File.WriteAllText(Program.usersJsonPath, JsonConvert.SerializeObject(usersList));

                    Console.WriteLine(">> Logged out");
                    return;
                }
            }
        }
    }
}