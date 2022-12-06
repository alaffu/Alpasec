public class AdministratorOptionsHandler
{
    private Program.Options options;

    private Administrator admin;

    private Boolean IsAddingUser()
    {
        if (options.AddUser != null && options.Password != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private Boolean IsChangingUser()
    {
        if (options.ChangeUser != null && options.NewName != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean isDeletingUser()
    {
        if (options.Test != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AddUser()
    {
        User newUser = new User(options.AddUser, options.Password, false);
        admin.Save(newUser);
    }

    private void ChangeUser()
    {
        admin.ChangeUser(options.ChangeUser, options.NewName);
        Console.WriteLine(options.ChangeUser);
        Console.WriteLine(options.NewName);

    }

    private void DeleteUser()
    {
        admin.deleteUser(options.Test);
    }

    public AdministratorOptionsHandler(Program.Options opts, Administrator admin, User user)
    {
        options = opts;
        this.admin = admin;

        if (IsAddingUser())
        {
            AddUser();
        }
        else if (IsChangingUser())
        {
            ChangeUser();
        }
        else if (isDeletingUser())
        {
            DeleteUser();
        }

        new UserOptionsHandler(opts, user);
    }

}
