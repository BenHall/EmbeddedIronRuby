using System;

namespace EmbeddedIronRuby
{
    public class ConsoleWrapper
    {
        public ConsoleColor bg { set { Console.BackgroundColor = value; } }
        public ConsoleColor fg { set { Console.ForegroundColor = value; } }
        public string title { set { Console.Title = value; } }
    }
}