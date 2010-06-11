using System;

namespace EmbeddedIronRuby
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            OutputHeader();
            DLRHost host = new DLRHost();
            host.Run();
        }

        private static void OutputHeader()
        {
            Console.WriteLine("Welcome to Embedded IronRuby Sample");
            Console.WriteLine("Type ir to enter IronRuby");
            Console.WriteLine("Type exit to quit the application");
        }

    }

    public class DLRHost
    {
        public DLRHost()
        {

        }

        public void Run()
        {
            string line = GetLine("C#");

            while (!line.Equals("exit") && !line.Equals(string.Empty))
            {
                if (line.Equals("ir"))
                    EnterIronRuby();
                else
                    Console.WriteLine("You typed in: " + line);

                line = GetLine("C#");
            }
        }

        private string GetLine(string lang)
        {
            Console.Write(lang + "> ");
            string line = Console.ReadLine();
            return line;
        }

        private void EnterIronRuby()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            Console.ResetColor();
            Console.Clear();
        }


    }
}