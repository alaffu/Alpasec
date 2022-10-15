// See https://aka.ms/new-console-template for more information
using System;
using System.Text;
using System.Diagnostics;

public class ColorTerminal
{
    public static void ColorMain()
    {
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        ConsoleColor currentBackground = Console.BackgroundColor;
        ConsoleColor currentForeground = Console.ForegroundColor;

        Console.WriteLine("all the foreground color except {0}, the background color: ",
        currentBackground);

        foreach (var color in colors)
        {
            if (color == currentBackground) continue;
            Console.ForegroundColor = color;
            Console.WriteLine(" The foregroung color is {0}.", color);
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("this should be a green text");
    }
}

public class ConsoleKeyExample

{
    public static void consoleKeyMain()
    {
        ConsoleKeyInfo input;
        do
        {
            Console.WriteLine("Press a key, together with Alt, Ctrl, or Shift");
            Console.WriteLine("Press ESC to exit.");
            input = Console.ReadKey(true);

            StringBuilder output = new StringBuilder(
                String.Format("You pressed {0}", input.Key.ToString()));

            bool modifiers = false;

            if ((input.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt)
            {
                output.Append(", together with " + ConsoleModifiers.Alt.ToString());
                modifiers = true;
            }

            if ((input.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
            {
                if (modifiers)
                {
                    output.Append(" and ");
                }
                else
                {
                    output.Append(", together with ");
                }
                output.Append(ConsoleModifiers.Control.ToString());
            }

            if ((input.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift)
            {
                if (modifiers)
                {
                    output.Append(" and ");
                }
                else
                {
                    output.Append(", together with");
                    modifiers = true;
                }
                output.Append(ConsoleModifiers.Shift.ToString());
            }
            output.Append(".");
            Console.WriteLine(output.ToString());
            Console.WriteLine();

        } while (input.Key != ConsoleKey.Escape);

    }
}

public class Program
{
    public static string RunPython()
    {
        const string PYTHON_PATH = "/usr/bin/python3";
        const string SCRIPT_RELATIVE_PATH = "/scripts/script_encrypt.py";


        ProcessStartInfo start = new ProcessStartInfo();
        start.Arguments = Path.Join(Directory.GetCurrentDirectory(), SCRIPT_RELATIVE_PATH);
        start.FileName = PYTHON_PATH;
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        

        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string stderr = process.StandardError.ReadToEnd();
                string result = reader.ReadToEnd();
                Console.WriteLine(stderr);
                Console.WriteLine(result);

                return result;
            }
        }
    }
    public static void Main()
    {
        RunPython();
        // Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        // Console.WriteLine(Directory.GetCurrentDirectory());
        // ConsoleKeyInfo input;

        // do{

        //     Console.WriteLine("Press a key, together with Alt, Ctrl, or Shift");
        //     Console.WriteLine("Press ESC to exit.");
        //     input = Console.ReadKey(true);
        //     Console.WriteLine();
        //     Console.WriteLine(">");
        //     Console.WriteLine("Key pressed {0}", input.Key.ToString());
        //     Console.WriteLine("Key pressed {0}", input.Key);
        //     if (input.Key.ToString() == "DownArrow")
        //     {
        //         Console.WriteLine("you did it");
        //     }
        // }while (input.Key != ConsoleKey.Escape);
    }
}
