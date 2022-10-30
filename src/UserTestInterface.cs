public class UserTestInterface
{
    protected static int origCol;
    protected static int origRow;

    public static void WriteAt(string s, int x, int y)
    {
        try
        {
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.Write(s);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }
    }

    public void start()
    {
        ConsoleKeyInfo input;

        Console.WriteLine("Press a key, together with Alt, Ctrl, or Shift");
        Console.WriteLine("Press ESC to exit.");

        origCol = Console.CursorTop;
        origRow = Console.CursorLeft;
        int indexY = 0;
        int indexX = 0;
        Console.Clear();

        do
        {
            input = Console.ReadKey(true);
            // Console.WriteLine();
            // Console.WriteLine(">");
            // Console.WriteLine("Key pressed {0}", input.Key.ToString());
            // Console.WriteLine("Key pressed {0}", input.Key);
            if (input.Key.ToString() == "DownArrow")
            {
                indexY++;
                WriteAt("", indexX, indexY);
            }

            // if (input.Key.ToString() == "RightArrow")
            // {
            //     indexX++;
            //     WriteAt("", indexX, indexY);
            // }

            if (input.Key.ToString() == "UpArrow")
            {
                indexY--;
                WriteAt("", indexX, indexY);
            }

            if (input.Key.ToString() == "C")
            {
                Console.Clear();
                indexX = 0;
                indexY = 0;
            }
        } while (input.Key != ConsoleKey.Escape);
    }

}
