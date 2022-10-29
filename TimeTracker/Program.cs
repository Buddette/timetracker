using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Shapes;

//TO DO
//Fix initial path setting prompt, use input from GUI
//Fix skip location, not sure what to do but make it work
//other stuff

namespace TimeTracker
{
    public class Program
    {
        public static string path = Directory.GetCurrentDirectory() + @"\timetracker.txt";
        public static string process_name = "notepad.exe";
        public static Stopwatch timer = new Stopwatch();
        public static bool stop;
        public static yeah window = new yeah();

        public static void Main(string[] args)
        {
            if (File.Exists(path) == false)
            {
                //dont ask
                FileStream stream = File.Create(path);
                //this closes the file so other things can use it - tilly
                stream.Dispose();
                File.WriteAllText(path, "0\n" + process_name);
            }

            Application.Run(window);
        }

        public static void NewProgramStart()
        {
            stop = false;

            if (File.Exists(path) == false)
            {
                FileStream stream = File.Create(path);
                stream.Dispose();
                //Console.WriteLine("Enter process name: ");
                //string process_name = Console.ReadLine();
                File.WriteAllText(path, "0\n" + process_name);
            }

            if (Process.GetProcessesByName("TimeTracker").Length > 2)
            {
                //Console.WriteLine("Time Tracker is already running.\n");
                return;
            }
            
            string data = File.ReadAllText(path);
            //testing[0] testing [1]
            string[] testing = data.Split('\n');
            string program = testing[1];

            int previous = Int32.Parse(testing[0]);

            //Console.WriteLine("Current usage time: " + previous / 1000 + "s\n");
            
            timer.Start();
            if (Process.GetProcessesByName(program).Any())
            {
                Process[] processes = Process.GetProcessesByName(program);

                foreach (Process process in processes)
                {
                    //process.WaitForExit();
                }

                //Console.WriteLine("The application has exited.");
            }
            else
            {
                //Console.WriteLine("The application is not running.");
                //return;
            }

            while(stop is false)
            {
            }

            timer.Stop();

            int current = (int)timer.ElapsedMilliseconds;

            int contents = current + previous;

            string time = contents.ToString();
            window.updatenumberthing("current time: " + time);

            File.WriteAllText(path, time + "\n" + testing[1]);

            timer.Reset();
        }
    }
};
