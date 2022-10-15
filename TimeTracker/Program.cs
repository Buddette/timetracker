using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TimeTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //OldProgram();
            Application.Run(new yeah());
        }

        public static void OldProgram()
        {
            // code that nobody really understands just leave it alone
            [DllImport("kernel32.dll")]
            static extern bool FreeConsole();
            [DllImport("kernel32.dll")]
            static extern int AllocConsole();
            [DllImport("kernel32.dll")]
            static extern IntPtr GetStdHandle(int nStdHandle);
            const int STD_OUTPUT_HANDLE = -11; // funny number
            #region ShowConsole() stuff
            static void ShowConsole() //basically makes new function, don't question it
            {
                _ = AllocConsole(); // tell windows to allocate a new console for the program
                IntPtr stdout = GetStdHandle(STD_OUTPUT_HANDLE); // get a handle to the output of the new console window
                SafeFileHandle sfh = new SafeFileHandle(stdout, true); // create a safer version of the handle because pointers are stinky and unsafe
                FileStream fs = new FileStream(sfh, FileAccess.Write); // create a file stream which basically turns the handle to a file so we can write to it like a text document
                StreamWriter stdout_sw = new(fs, Encoding.ASCII); // create a stream writer that lets us write to that fake file, and tell it to encode in ASCII (what the console windows use, if you use unicode youll get weird spaces because it wont be able to interpret the data properly)
                stdout_sw.AutoFlush = true; // "flushing" the stream writer just means to write all the data to the file, this just makes it do it automatically whenever we call WriteLine
                Console.SetOut(stdout_sw); // set the output of our program to the new console's output
            }
            #endregion

            #region notes
            //Console.WriteLine("Hello, World!");
            //string path = @"c:\Users\me\Documents\Time Tracker\timetracker.txt";
            //Process.MainModule;
            //ProcessModule.ModuleName;
            //Process.OnExited;
            //Process.GetProcesses;
            //File.WriteAllText
            //File.ReadAllText
            //.ToString()
            //Int32.Parse()
            #endregion

            //TODO
            //figure out what to do
            //maybe ui
            //maybe more features
            //test github - done

            AllocConsole();

            if (Process.GetProcessesByName("TimeTracker").Length > 2)
            {
                Console.WriteLine("Time Tracker is already running.\n");
                goto skip;
            }

            string path = Directory.GetCurrentDirectory() + @"\timetracker.txt";

            if (File.Exists(path) == false)
            {
                //dont ask
                FileStream stream = File.Create(path);
                //this closes the file so other things can use it - tilly
                stream.Dispose();
                Console.WriteLine("Enter process name: ");
                string process_name = Console.ReadLine();
                File.WriteAllText(path, "0\n" + process_name);
            }
            string data = File.ReadAllText(path);
            //testing[0] testing [1]
            string[] testing = data.Split('\n');
            string program = testing[1];

            //long.Parse(data) also works (change everything from int to long)
            int previous = Int32.Parse(testing[0]);

            Console.WriteLine("Current usage time: " + previous / 1000 + "s\n");
            Thread.Sleep(3000);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            //using (Process VS = new Process())
            //ProcessStartInfo startinfo = new ProcessStartInfo("ServiceHub.TestWindowStoreHost.exe");
            if (Process.GetProcessesByName(program).Any())
            {
                FreeConsole();

                // waits for first one to close
                //Process process = Process.GetProcessesByName("notepad")[0];
                //process.WaitForExit();

                // gets a list of all processes called notepad
                Process[] processes = Process.GetProcessesByName(program);

                // goes through each process in the list and waits for it to close
                foreach (Process process in processes)
                {
                    process.WaitForExit();
                }

                ShowConsole();

                Console.WriteLine("The application has exited.");
            }
            else
            {
                Console.WriteLine("The application is not running.");
                goto skip;
            }
            timer.Stop();

            //idk why it works it just does
            int current = (int)timer.ElapsedMilliseconds;

            Console.Clear();
            Console.WriteLine("Time this session: " + current / 1000 + "s\n");

            int contents = current + previous;

            Console.WriteLine("Current usage time: " + contents / 1000 + "s\n");

            string time = contents.ToString();

            File.WriteAllText(path, time + "\n" + testing[1]);

            skip:
            Console.WriteLine("\nPress any button to exit.");
            //readline but one key
            Console.ReadKey();
        }
    }
}
