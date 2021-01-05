using ats.ats.Contracts;
using System;


namespace ats
{
    class Program
    {
        static void Main(string[] args)
        {
            IStation station = new Station();
            IPhone caller = station.CreatePhone("11111111111111");
            IPhone caller1 = station.CreatePhone("2222222222222");
            IPhone answerer = station.CreatePhone("33333333333333");

            caller.Call(answerer.PhoneNumber);
            answerer.AnswerCall();
            answerer.DropCall();
            caller1.Call(answerer.PhoneNumber);
            answerer.DropCall();
           
            Console.ReadKey(); 
        }
    }
}
