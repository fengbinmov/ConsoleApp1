using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    public class Input
    {
        Thread thread = null;

        public static string input = "";

        public static bool IsText(string ms)
        {

            if (ms == input)
            {
                input = "";
                return true;
            }
            return false;
        }
        public static string ICDLine()
        {
            string[] list = input.Split(' ');
            if (list[0] == "s")
            {
                input = "";
                return list[1];
            }
            return "";
        }

        public static string IDownLine()
        {
            string[] list = input.Split(' ');
            if (list[0] == "down")
            {
                input = "";
                string temp = "";
                for (int i = 1; i < list.Length; i++)
                {
                    if (i == list.Length - 1)
                        temp += list[i];
                    else
                        temp += list[i] + " ";
                }
                return temp;
            }
            return "";
        }
        public void InputEventStart()
        {

            thread = new Thread(InputEvent);
            thread.Start();
        }
        public void InputEvent()
        {
            while (MyApp.ISRUN)
            {

                input = Console.ReadLine();
            }
        }
    }
}
