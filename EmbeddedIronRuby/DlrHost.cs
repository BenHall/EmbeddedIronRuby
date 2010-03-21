using System;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace EmbeddedIronRuby
{
    public class DlrHost
    {
        public void Run()
        {
            string line;
            do
            {
                line = GetLine("C#");

                if (line.Equals("ir"))
                    EnterIronRuby();
                else
                    Console.WriteLine("You typed: " + line);

            } while (line != "quit");
        }

        private void EnterIronRuby()
        {
            SetConsoleColour();

            ScriptEngine engine = IronRuby.Ruby.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            SetVariables(scope);

            string line;
            do
            {
                line = GetLine("ir");
                ExecuteRuby(line, engine, scope);

            } while (line != "quit");

            ResetConsole();
        }

        private void SetVariables(ScriptScope scope)
        {
            scope.SetVariable("started", DateTime.Now);
        }

        private void ExecuteRuby(string line, ScriptEngine engine, ScriptScope scope)
        {
            object result = null;

            try
            {
                ScriptSource s = engine.CreateScriptSourceFromString(line, SourceCodeKind.AutoDetect);
                result = s.Execute(scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
  
            if (result != null) 
                Console.WriteLine("Result from IronRuby: " + result);
            else
                Console.WriteLine("Method returned nil");
        }

        private void SetConsoleColour()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
        }

        private void ResetConsole()
        {
            Console.ResetColor();
            Console.Clear();
        }

        private string GetLine(string lang)
        {
            Console.Write(lang + "> ");
            string line = Console.ReadLine();
            return line;
        }
    }
}