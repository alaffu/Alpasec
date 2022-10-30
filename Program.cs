// See https://aka.ms/new-console-template for more information
// using System;
//
public class Program
{
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
        foreach (var arg in args)
        {
            Console.WriteLine(arg);
        }

    }
}
