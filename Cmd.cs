using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace OS_FILE_
{
   public sealed class Cmd
    {
        
        string[] CmdS = System.IO.File.ReadAllLines(@"E:\Cmd(Short).txt");
        string[] CmdD = System.IO.File.ReadAllLines(@"E:\Cmd(Des).txt");
        string[] CmdA = System.IO.File.ReadAllLines(@"E:\Cmd(All).txt");

        List<string> Mcommand=new List<string>();

        // singleton design pattern
        private static Cmd instance = null;
        public static Cmd Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Cmd();
                }
                return instance;
            }
        }
        
        public void CheckCommand(List<string> commands)
        {
            // use to stop search in file 
            bool flag = false;

            // put command in Mcommand to use it another Functions
            Mcommand = commands;

            // CmdS file >> contain name of commands
            foreach (string line in CmdS)
                {
                    if(line.Equals(commands[0]))
                    {
                        ExecuteCommand(commands[0]);
                        flag = true;
                        break;
                    }                             
                }
                if (!flag) { Console.WriteLine("'{0}' is not recognized as an internal or external command", commands[0]); }
                      
        }

        private void ExecuteCommand(string v)
        {
            // v ->> name of command 
            switch (v)
            {
                case "clr":
                    Console.Clear();
                    break;

                case "quit":
                    Environment.Exit(0);  
                    break;

                case "dir":
                    Dir();
                    break;

                case "help":
                    Help();
                    break;

                case "cd":
                    Cd(); 
                    break;

                case "copy":
                    Console.WriteLine("Next Task");
                    break;

                case "del":
                    Console.WriteLine("Next Task");
                    break;

                case "md":
                    Md();
                    break;

                case "rd":
                    Rd();
                    break;

                case "rename":
                    Rename();
                    break;

                case "type":
                    Type();
                    break;

                case "import":
                    Console.WriteLine("Next Task");
                    break;

                case "export":
                    Console.WriteLine("Next Task");
                    break;

                default:
                    Console.WriteLine("{0} is not recognized as an internal or external command", v);
                    break;
            }
        }

        private void Type()
        {
            if (Mcommand.Count == 1)
            {
                Console.WriteLine("The syntax of command is incorrect");
            }
            else if (Mcommand.Count == 2)
            {
                //   check if a Path is a File or a Directory ? true > directory
                bool result = File.GetAttributes(Mcommand[1]).HasFlag(FileAttributes.Directory);

                if (result)
                {
                    Console.WriteLine("Access is denied");
                }
                else
                {
                    // case 3 TODO
                    if (File.Exists(Mcommand[1]))
                    {
                        string[] lines = File.ReadAllLines(Mcommand[1]);

                        foreach (string line in lines)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the file specified ");
                    }

                }
            }
            else if (Mcommand.Count > 2)
            {
                for (int i = 1; i < Mcommand.Count; i++)
                {
                    if (File.Exists(Mcommand[i]))
                    {
                        string[] lines = File.ReadAllLines(Mcommand[i]);

                        foreach (string line in lines)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the file specified "+ Mcommand[i]);
                    }
                }
            }
        }
        private void Rename()
        {
            if (Mcommand.Count == 1)
            {
                Console.WriteLine("The syntax of command is incorrect");
            }
            else if (Mcommand.Count == 2)
            {
                string[] myFiles = Directory.GetDirectories(Directory.GetCurrentDirectory());

                foreach (var myFile in myFiles)
                {

                    if (Path.GetFileName(myFile) == Mcommand[1])
                    {
                        Console.WriteLine("The syntax of the command is incorrect");
                        break;
                    }

                }
            }
            else if (Mcommand.Count == 3)
            {
                // check if a Path is a File or a Directory ? 
                bool result = File.GetAttributes(Mcommand[1]).HasFlag(FileAttributes.Directory);

                if (result)
                {

                    string[] myFiles = Directory.GetDirectories(Directory.GetCurrentDirectory());

                    foreach (var myFile in myFiles)
                    {

                        if (Path.GetFileName(myFile) == Mcommand[1])
                        {
                            Directory.Move(Mcommand[1], Mcommand[2]);
                            break;
                        }

                    }
                }
                else
                {
                    bool con = true;
                    string[] Files = Directory.GetFiles(Directory.GetCurrentDirectory());

                    foreach (var file in Files)
                    {

                        if (Path.GetFileName(file) == Mcommand[2])
                        {
                            con = false;
                            break;
                        }

                    }

                    if (con)
                    {
                        foreach (var file in Files)
                        {

                            if (Path.GetFileName(file) == Mcommand[1] && con)
                            {
                                File.Move(Path.GetFullPath(Mcommand[1]), Path.GetFullPath(Mcommand[2]));
                                break;
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("A duplicate file name exists, or the file cannot be found");
                    }
                }
            }
        }

        private void Rd()
        {
            if (Mcommand.Count == 1)
            {
                Console.WriteLine("The syntax of command is incorrect");
            }
            else if (Mcommand.Count == 2)
            {
                if (Directory.Exists(Mcommand[1]))
                {
                    Directory.Delete(Mcommand[1]);
                }
                else
                {
                    // rd then file name
                    int No_of_files = 0;
                    string[] myFiles = Directory.GetFiles(Directory.GetCurrentDirectory());
                    foreach (var myFile in myFiles)
                    {
                        if (Path.GetFileName(myFile.ToLower()) == Mcommand[1].ToLower())
                        {
                            No_of_files++;
                        }
                    }
                    if (No_of_files == 1)
                    {
                        Console.WriteLine("The directory name is invalid");
                    }
                    else
                    {
                        Console.WriteLine("The system cannot find the file specified");
                    }

                }
            }
            else
            {
                // rd then multiple file name
                for (int i = Mcommand.Count; i > 1; i--)
                {
                    if (Directory.Exists(Mcommand[i - 1]))
                    {
                        Directory.Delete(Mcommand[i - 1]);
                    }
                }
            }
        }

        private void Md()
        {
            if (Mcommand.Count == 1)
            {
                Console.WriteLine("The syntax of command is incorrect");
                
            }
            else
            {
                // creates one directory or more than one 
                for (int i= Mcommand.Count; i>1 ;i--)
                {
                    // check name of directory that already exists or not
                    if (!Directory.Exists(Mcommand[i-1]))
                    {
                        // creates a directory
                        Directory.CreateDirectory(Mcommand[i-1]);
                    }
                    else
                    {
                        //  directory already exists    
                        Console.WriteLine("A Subdirectory or file " + Mcommand[i-1] + " already exists");

                    }
                }
               
            }
            
        }

        private void Cd()
        {
            // display current directory
            if (Mcommand.Count == 1)
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
            }

            else if(Mcommand.Count == 2)
            {
                // check path vaild or not true >> if NOT valid false >> if it valid

                /*   bool valid = ValidPath(Mcommand[1]);

                   // valid path
                   if (!valid)
                   {
                       // compare root directory of Mcommand[1] and Current directory and check that path in dirve correct
                       if (Directory.GetDirectoryRoot(Mcommand[1]).ToLower().Equals(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()).ToLower())
                           && Directory.Exists(Mcommand[1]))
                       {
                           // change current directory to the specified path
                            Directory.SetCurrentDirectory(Mcommand[1]);

                       }
                       //  root directory of Mcommand[1] and Current directory have the same drive but path not correct
                       else if (Directory.GetDirectoryRoot(Mcommand[1]).ToLower().Equals(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()).ToLower()))
                       {
                           Console.WriteLine("The System Cannot find the path specified");
                       }
                       else
                       {
                           // valid path but not in the same drive of current directory
                           // display current directory 
                           Console.WriteLine(Directory.GetCurrentDirectory());
                       }
                   }*/
                // invalid path
                /* else
                 {
                     Console.WriteLine("The System Cannot find the path specified");

                 }*/
                try
                {
                    // compare root directory of Mcommand[1] and Current directory and check that path in dirve correct
                    if (Directory.Exists(Mcommand[1])&& 
                        Directory.GetDirectoryRoot(Mcommand[1]).ToLower().Equals(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()).ToLower()))
                    {
                        Directory.SetCurrentDirectory(Mcommand[1]);
                    }
                    else
                    {
                        Console.WriteLine("The System Cannot find the path specified");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The System Cannot find the path specified");
                }

            }

          
        }

        private bool ValidPath(string v)
        {
            bool valid = true;
            List<string> Pattern = new List<string> { "^", "<", ">", ";", "|", "'", "/", ",", "\\", ":", "=", "?", "\"", "*" };
            for (int i = 0; i < Pattern.Count; i++)
            {
                if (v.Contains(Pattern[i]))
                {
                    valid = false;
                    break;
                }
            }
            return valid;
        }

        private void Dir()
        {
            // dir only >> list of files and subdirectories in a current directory
            if (Mcommand.Count == 1)
            {
                list_current_directory();
                
            }

            if (Mcommand.Count == 2)
            {
                int count = 0;
                String path = Directory.GetCurrentDirectory();
                string[] myFiles = Directory.GetFiles(path);
                foreach (var myFile in myFiles)
                {
                    if(Path.GetFileName(myFile.ToLower())==Mcommand[1].ToLower())
                    {
                        count++;
                    }
                }
                DriveInfo myDrive = new DriveInfo(Directory.GetDirectoryRoot(path).Substring(0, 1));

                Console.WriteLine("Volume in drive " + Directory.GetDirectoryRoot(path).Substring(0, 1) + " is " + myDrive.VolumeLabel);

                //print Volume serial number
                Console.WriteLine();
                Console.WriteLine("Directory of " + Directory.GetCurrentDirectory());
                Console.WriteLine();

                if (count==1)
                {
                    FileInfo f = new FileInfo(Mcommand[1]);
                  
                    DateTime dt = Directory.GetCreationTime(Mcommand[1]);
                    Console.Write(dt + "\t");
                    Console.WriteLine(Path.GetFileName(Mcommand[1]));
                    Console.WriteLine("\t"+"\t"+ f.Length.ToString() + " bytes");
                }
                else
                {
                    Console.WriteLine("File Not Found");
                }
            }
            else
            {


            }
        }

        private void list_current_directory()
        {
            int NOfiles=0; // number of files 
            int Nodirs=0; // number of dirctories
            long FilesSizes = 0; // total of file sizes 

            String path = Directory.GetCurrentDirectory();
            string[] myFiles = Directory.GetFiles(path);
            DriveInfo myDrive = new DriveInfo(Directory.GetDirectoryRoot(path).Substring(0, 1));
            
            Console.WriteLine("Volume in drive " + Directory.GetDirectoryRoot(path).Substring(0, 1) + " is " + myDrive.VolumeLabel);

            //print Volume serial number

            Console.WriteLine();
            Console.WriteLine("Directory of " + Directory.GetCurrentDirectory());
            Console.WriteLine();
            foreach (var myFile in myFiles)
            {
                NOfiles++;
                DateTime dt = Directory.GetCreationTime(myFile);
                // var lastAccessTime = Directory.GetLastAccessTime(myFile);
                // var lastWriteTime = Directory.GetLastWriteTime(myFile);
                FilesSizes += myFile.Length;
                Console.WriteLine(dt + "\t" + "\t"+ Path.GetFileName(myFile));
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(Directory.GetCurrentDirectory());
            foreach (string subdirectory in subdirectoryEntries)
            {
                DateTime dt = Directory.GetCreationTime(subdirectory);
                //var lastAccessTime = Directory.GetLastAccessTime(subdirectory);
                // var lastWriteTime = Directory.GetLastWriteTime(subdirectory);

                Console.WriteLine(dt + "\t" +"<DIR>"+ "\t" +Path.GetFileName(subdirectory));
                Nodirs++;
            }

            Console.WriteLine("\t"+ "\t" + NOfiles + " File(s)"+"\t"+String.Format("{0:n0}" , FilesSizes)+" bytes");
            Console.WriteLine("\t"+ "\t" + Nodirs +" dir(s)" + "\t" +String.Format("{0:n0}", myDrive.AvailableFreeSpace)+" bytes free");
          
            Console.WriteLine();
        }

        private void Help()
        {
            int counterCmdS = 0; // store number of line in command file
            int counterCmdD = 0;  // store number of line in command descrption
            bool exist = false;
            
            // help without any argument 
            if (Mcommand.Count == 1)
            {
                // print all file of command and descrptions 
                foreach (string line in CmdA)
                {
                    Console.WriteLine(line);
                }
            }

            // help command with one argument
            else if (Mcommand.Count == 2)
            {
                // check if argument exist or not
                foreach (string line in CmdS)
                {
                    counterCmdS++; //  to get number of line in command file
                    if (line.Equals(Mcommand[1]))
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist) { Console.WriteLine("This command is not supported by the help utility.Try "+@""""+"{0}  /?" + @"""", Mcommand[1]); }

                // argument exist
                else
                {          
                    foreach (string line in CmdD)
                    {
                        counterCmdD++;
                        if (counterCmdS == counterCmdD)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            // help command with multi argument
            else
            {
                Console.WriteLine("HELP [command] ... Provides help information for Windows commands.");
            }
        }
    }
}