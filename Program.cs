using System;
using System.Threading;

namespace MouseJiggler
{
    internal class Program
    {
        public const int WAIT_TIME = 60000; //900000
        public const int IDLE_TIMEOUT = 30000;

        static void Main(string[] args)
        {

            uint timestamp = (uint)Environment.TickCount;

            while (true)
            {
                // check if we having waited as long as we should
                if((uint)Environment.TickCount - timestamp > WAIT_TIME)
                {
                    timestamp = (uint)Environment.TickCount;
                    uint idle = UserInput.IdleTime();
                    Console.WriteLine("Idle time is {0:D}", idle);
                    if (idle > IDLE_TIMEOUT)
                    {
                        Console.WriteLine("Been idle for more than threshold {0:D}", IDLE_TIMEOUT/1000);
                        InteractWithSystem.ResetTimer();
                    }
                }

                // wait for loop to go round again
                Thread.Sleep(5000);
            }
        }
    }
}
