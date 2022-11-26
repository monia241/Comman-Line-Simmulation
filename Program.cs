using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace OS_FILE_
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    Console.Write(Directory.GetCurrentDirectory() + ">");
                    string command = Console.ReadLine();
                    command = command.ToLower();
                    if (!string.IsNullOrEmpty(command)) { Parsing(command); }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (true);
        }
        private static void Parsing(string command)
        {
            // split command and argument
            List<string> Parsing_commands = command.Split(' ').ToList();

            // new list to remove empty command or argument
            List<string> commands = new List<string>();

           foreach (String c in Parsing_commands)
             {
                // remove empty command or argument
                if (!string.IsNullOrEmpty(c))
                {
                    commands.Add(c);
                }
             }
            Cmd.Instance.CheckCommand(commands);
        }     
    }
}
