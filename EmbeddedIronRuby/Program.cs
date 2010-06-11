using System;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;

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
        ScriptEngine Engine;
        ScriptScope Scope;
        public DLRHost()
        {
            Engine = IronRuby.Ruby.CreateEngine();
            Scope = Engine.CreateScope();
            //ExecuteWPF();
        }

        private void ExecuteWPF()
        {
            Console.WriteLine("Referencing WPF");
            Execute("require 'mscorlib'");
            Execute("require 'PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'");
            Execute("require 'PresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'");
            
            Execute("include System");
            Execute("include System::Windows");
            Execute("include System::Windows::Controls");
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

            Scope.SetVariable("c", new ConsoleWrapper());
            Scope.SetVariable("ui", new UpdateUI());

            string code = GetLine("ir");

            while (!code.Equals("exit") && !code.Equals(string.Empty))
            {
                Execute(code);
                code = GetLine("ir");
            }

            Console.ResetColor();
            Console.Clear();
        }

        private void Execute(string code)
        {
            try
            {
                ScriptSource source = Engine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
                object result = source.Execute(Scope);
                if (result != null)
                    Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
    }

    public class UpdateUI
    {
        public delegate void DataChangedEventHandler(object sender, DataChangedEventArgs e);
        public event DataChangedEventHandler DataChanged;

        public UpdateUI()
        {
            DataChanged += Updated;
        }

        void Updated(object sender, DataChangedEventArgs e)
        {
            Console.WriteLine("I've been called (This is C#) " + e.DataFromRuby);
        }

        public virtual void Updated(string data)
        {
            if (DataChanged != null)
            {
                var args = new DataChangedEventArgs();
                args.DataFromRuby = data;
                DataChanged(this, args);
            }
        }
    }

    public class DataChangedEventArgs : EventArgs
    {
        public string DataFromRuby { get; set; }
    }
}