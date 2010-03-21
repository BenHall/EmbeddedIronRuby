using System;

namespace EmbeddedIronRuby
{
   class Program
   {
      [STAThread]
      static void Main()
      {
         OutputHeader();
         DlrHost host = new DlrHost();
         host.Run();
      }

      private static void OutputHeader()
      {
         Console.WriteLine("Welcome to Embedded IronRuby Sample");
         Console.WriteLine("Type ir to enter IronRuby");
         Console.WriteLine("Type exit to quit the application");
      }
   }
}
