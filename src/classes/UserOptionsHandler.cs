public class UserOptionsHandler
{
    private Program.Options options;

    private Boolean IsTryingLogin()
    {
        if (options.Login != null && options.Password != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean IsTryingEncrypt()
    {
        if (options.Encrypt != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean IsTryingDecrypt()
    {

        if (options.Decrypt != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Boolean IsTryingLogout()
    {
        if (options.Logout)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private Boolean IsTryingListFiles()
    {
        if (options.List)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EncryptFile(User user)
    {
        string result = Encrypt.RunPython("encrypt", options.Encrypt, user.Password);

        if (!options.NoMove)
        {
            user.MoveFile(result, options.Encrypt);
        }
    }

    private int LoggedUserOptionsHandler(User user)
    {
        int run = 0;
        if (IsTryingEncrypt())
        {
            EncryptFile(user);
            run++;
        }

        else if (IsTryingDecrypt())
        {
            Encrypt.RunPython("decrypt", options.Decrypt, user.Password);
            run++;
        }
        else if (IsTryingLogout())
        {
            Auth.Logout();
            run++;
        }
        else if (IsTryingListFiles())
        {
            user.ListFiles();
            run++;
        }
        return run;

    }
    private int NoPermissionsOptionsHandler()
    {
        int status = 0;
        if (IsTryingLogin())
        {
            Auth.Login(options.Login, options.Password);
            status++;
        } else if (options.ListUsers)
        {
            Auth.ListUsers();
            status++;
        }

        return status;
    }
    public static UserOptionsHandler Start(Program.Options opts, User user, int statusHowManyOptionsRun=0)
    {
        return new UserOptionsHandler(opts, user, statusHowManyOptionsRun);
    }

    public UserOptionsHandler(Program.Options opts, User user, int statusHowManyOptionsRun = 0)
    {
        options = opts;

        if (user != null && user.IsLoggedIn)
        {
            statusHowManyOptionsRun += LoggedUserOptionsHandler(user);
        }

        statusHowManyOptionsRun += NoPermissionsOptionsHandler();

        if (statusHowManyOptionsRun == 0)
        {
            Console.WriteLine(">> No arguments passed or you are not logged in");
        }
    }
}
