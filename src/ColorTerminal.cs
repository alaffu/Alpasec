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

