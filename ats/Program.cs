using ats.ats.Contracts;
using ats.BillingSys;
using System;
using System.Threading;

namespace ats
{
    class Program
    {
        static void Main(string[] args)
        {
            IStation station = new Station();
            BillingSystem billingSystem = new BillingSystem();
            billingSystem.RegisterStationEventHandlers(station);

            Phone phone1 = new Phone { PhoneNumber = "111111111111111" };
            Phone phone2 = new Phone { PhoneNumber = "222222222222222" };
            Phone phone3 = new Phone { PhoneNumber = "333333333333333" };
            station.RegisterPhone(phone1);
            station.RegisterPhone(phone2);
            station.RegisterPhone(phone3);


            Client caller1 = new Client { Name = "A", Phone = phone1 };
            Client caller2 = new Client { Name = "B", Phone = phone2 };
            Client answerer = new Client { Name = "C", Phone = phone3 };
            billingSystem.RegisterClient(caller1);
            billingSystem.RegisterClient(caller2);

            billingSystem.RegisterClient(answerer);


            caller1.Phone.Call(answerer.Phone.PhoneNumber);
            answerer.Phone.AnswerCall();
            Thread.Sleep(1000);
            caller1.Phone.DropCall();
            answerer.Phone.DropCall();


            caller2.Phone.Call(answerer.Phone.PhoneNumber);
            Thread.Sleep(1000);
            caller2.Phone.DropCall();

            Report caller1Report = billingSystem.CreateReport(caller1);
            Report caller2Report = billingSystem.CreateReport(caller2);
            Report answererReport = billingSystem.CreateReport(answerer);

            Console.ReadKey(); 
        }
    }
}
