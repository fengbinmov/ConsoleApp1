using System;
using MServer.Client;
using System.Threading;

namespace ConsoleApp1
{

    public class MyApp
    {
        public static bool ISRUN;

        public static string IP = "192.168.123.84";
        public static int PROT = 2525;
        static My myIntnet;



        static void Main(string[] args)
        {
            ISRUN = true;
            //Console.WriteLine("IP:");
            //IP = Console.ReadLine();
            //Console.WriteLine("PROT:");
            //PROT = int.Parse(Console.ReadLine());

            myIntnet = new My();

        START: Console.WriteLine("" +
           "启动方式:\n" +
           "1.Client");
            switch (Console.ReadLine())
            {
                case "1":
                    myIntnet = new MyClient();
                    break;
                default:
                    goto START;
                    break;
            }
            Thread thread = new Thread(myIntnet.OnEnable);
            thread.Start();
            Input input = new Input();

            while (ISRUN)
            {
                Input.text = Console.ReadLine();
            }
            myIntnet.enable = false;
            
            Console.WriteLine("Close App");
            Console.ReadKey();
        }
    }
}
