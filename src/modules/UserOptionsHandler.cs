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

        if (options.Logout != null)
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
        int status = 0;
        if (IsTryingEncrypt())
        {
            EncryptFile(user);
            status++;
        }

        else if (IsTryingDecrypt())
        {
            Encrypt.RunPython("decrypt", options.Decrypt, user.Password);
            status++;
        }
        else if (IsTryingLogout())
        {
            Auth.Logout();
            status++;
        }
        else if (IsTryingListFiles())
        {
            user.ListFiles();
            status++;
        }
        return status;

    }
    private int NoPermissionsOptionsHandler()
    {
        int status = 0;
        if (IsTryingLogin())
        {
            Auth.Login(options.Login, options.Password);
            status++;
        }

        if (options.ListUsers)
        {
            Auth.ListUsers();
            status++;
        }

        return status;
    }

    public UserOptionsHandler(Program.Options opts, User? user = null)
    {
        options = opts;
        int statusHowManyOptionsRun = 0;

        if (user != null)
        {
            if (user.IsLoggedIn)
            {
                statusHowManyOptionsRun = LoggedUserOptionsHandler(user);
            }
        }

        statusHowManyOptionsRun += NoPermissionsOptionsHandler();
        if (statusHowManyOptionsRun == 0)
        {
            Console.WriteLine(">> Must be logged in to perform this action");
        }

    }
}
