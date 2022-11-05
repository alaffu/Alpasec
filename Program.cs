// See https://aka.ms/new-console-template for more information
// using System;
//
using CommandLine;

public class Program
{

    private string key = "";

    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose message")]
        public bool Verbose { get; set; }

        [Option('a', "add-user", Required = false, HelpText = "Adds new user")]
        public string? AddUser { get; set; }

        [Option('f', "password", Required = false, HelpText = "Sets user password")]
        public string? Password { get; set; }

        [Option("files")]
        public IEnumerable<string>? Files { get; set; }

    }
    // private IDictionary<string, string> arguments = new Dictionary<string, string>();
    //
    // public void ArgumentHandler()
    // {
    // }

    public static void Main(string[] args)
    {
        /* Eu separei cada modulo desse projeto dentro da pasta src/
        Cada arquivo tem uma classe só, que vai ser responsável de fazer algo.

        Os modulos são bem simples ainda, então é possível testar cada um individualmente
        para ter uma idea do que eles fazem.

        Basta descomentar o módulo que vc deseja testar para ter uma demo :).
        */

        // EncryptFile.RunPython();

        // ColorTerminal.ColorMain();

        // UserTestInterface userTest = new UserTestInterface();
        // userTest.start();

        // Código abaixo daqui não faz mais parte dos módulos
        // foreach (var arg in args)
        // {
        //     Console.WriteLine(arg);
        // }
        //
        // for (int i = 0; i <= args.Length; i++)
        // {
        //
        // }

        Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(
            opt =>
            {
                User newUser = new User();

                if (opt.AddUser.Length != 0)
                {
                    newUser.Name = opt.AddUser;
                }
                if (opt.Password.Length != 0)
                {
                    newUser.Password = opt.Password;
                    Console.WriteLine(newUser);
                    Console.WriteLine(newUser.Name);
                    Console.WriteLine(newUser.Password);
                }
            }
        );

    }
}

