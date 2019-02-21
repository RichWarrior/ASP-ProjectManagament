using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Debugger.IsAttached)
            {
                Console.Title = "PM Task Scheduler Service";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("[!]{0}-Servis Başlatıldı!",DateTime.Now);
                Startup.OnStart();
                Console.ReadKey();
            }
            else
            {
                ServiceBase.Run(new ServiceBase[] { new PasswordNotification(),new WorkDefNotification()});
            }
        }
    }
}
