using Newtonsoft.Json;

static class PrepareEnvironment
{
  public static void run()
    {
        if (!Directory.Exists(Program.rootPath))
            Directory.CreateDirectory(Program.rootPath);

        if (!Directory.Exists(Program.usersPath))
            Directory.CreateDirectory(Program.usersPath);

        if (!File.Exists(Program.usersJsonPath)) {
            File.Create(Program.usersJsonPath).Close();

            List<User> usersList = new List<User>();
            usersList.Add(new User("admin", "admin"));
            File.WriteAllText(Program.usersJsonPath, JsonConvert.SerializeObject(usersList));
        }
    }
}