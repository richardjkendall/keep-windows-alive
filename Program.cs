using System;
using System.Reflection;
using System.Threading;
using CommandLine;

namespace MouseJiggler
{
    internal class Program
    {

        public const int DEFAULT_WAIT_TIME = 60;
        public const int DEFAULT_IDLE_TIMEOUT = 30;

        public class Options
        {
            [Option('w', "waittime", Required = false, HelpText = "How long should the tool wait before it checks to see if the computer is idle", Default = DEFAULT_WAIT_TIME)]
            public int WaitTime { get; set; } 

            [Option('i', "idletime", Required = false, HelpText = "How long should the system be idle for before the idle time is reset", Default = DEFAULT_IDLE_TIMEOUT)]
            public int IdleTimeout { get; set; }
        }

        
        static void Main(string[] args)
        {
            int WAIT_TIME = 0;
            int IDLE_TIMEOUT = 0;

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine($"keep-windows-alive {version}");
            Console.WriteLine("(C) 2022 Richard Kendall.");
            Console.WriteLine("");

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    WAIT_TIME = o.WaitTime * 1000;
                    IDLE_TIMEOUT = o.IdleTimeout * 1000;
                })
                .WithNotParsed(e =>
                {
                    Console.WriteLine("Exit due to command line error");
                    Environment.Exit(1);
                });

            Console.WriteLine($"Wait time between loops: {WAIT_TIME / 1000} seconds");
            Console.WriteLine($"Idle time before reset happens: {IDLE_TIMEOUT / 1000} seconds");
            Console.WriteLine("");

            uint timestamp = (uint)Environment.TickCount;

            while (true)
            {
                // check if we having waited as long as we should
                if((uint)Environment.TickCount - timestamp > WAIT_TIME)
                {
                    timestamp = (uint)Environment.TickCount;
                    uint idle = UserInput.IdleTime();
                    Console.WriteLine($"Idle time is {idle / 1000} seconds");
                    if (idle > IDLE_TIMEOUT)
                    {
                        Console.WriteLine($"\tThe idle time is beyond the expected threshold, resetting.");
                        InteractWithSystem.ResetTimer();
                    }
                }

                // wait for loop to go round again
                // just wait for a second
                Thread.Sleep(1000);
            }
        }
    }
}
