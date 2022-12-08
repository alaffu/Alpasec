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
        if (options.ChangeUser != null && (options.NewName != null || options.NewPassword != null))
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
        if (options.DeleteUser != null)
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
        User newUser = new User(options.AddUser, options.Password, Guid.NewGuid().ToString(), false);
        admin.Save(newUser);
    }

    private void ChangeUser()
    {
        admin.ChangeUser(options.ChangeUser, options.NewName, options.NewPassword);
    }

    private void DeleteUser()
    {
        admin.deleteUser(options.DeleteUser);
    }

    public static AdministratorOptionsHandler Start(Program.Options opts, Administrator admin, User user)
    {
        return new AdministratorOptionsHandler(opts, admin, user);
    }

    public AdministratorOptionsHandler(Program.Options opts, Administrator admin, User user)
    {
        options = opts;
        this.admin = admin;
        int statusHowManyOptionRun = 0;

        if (IsAddingUser())
        {
            AddUser();
            statusHowManyOptionRun++;
        }
        else if (IsChangingUser())
        {
            ChangeUser();
            statusHowManyOptionRun++;
        }
        else if (isDeletingUser())
        {
            DeleteUser();
            statusHowManyOptionRun++;
        }

        UserOptionsHandler.Start(opts, user, statusHowManyOptionRun);
    }
}
