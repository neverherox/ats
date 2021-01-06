using ats.ats.Contracts;
using ats.BillingSys;
using ats.BillingSys.Contracts;
using System;
using System.Threading;

namespace ats
{
    class Program
    {
        static void Main(string[] args)
        {
            IStation station = new Station();
            IBillingSystem billingSystem = new BillingSystem();
            billingSystem.RegisterStationEventHandlers(station);

            IPhone phone1 = new Phone { PhoneNumber = "111111111111111" };
            IPhone phone2 = new Phone { PhoneNumber = "222222222222222" };
            IPhone phone3 = new Phone { PhoneNumber = "333333333333333" };
            station.RegisterPhone(phone1);
            station.RegisterPhone(phone2);
            station.RegisterPhone(phone3);


            IClient caller1 = new Client { Name = "A", Phone = phone1 };
            IClient caller2 = new Client { Name = "B", Phone = phone2 };
            IClient answerer = new Client { Name = "C", Phone = phone3 };
            billingSystem.RegisterClient(caller1);
            billingSystem.RegisterClient(caller2);
            billingSystem.RegisterClient(answerer);


            caller1.Phone.Call(answerer.Phone.PhoneNumber);
            answerer.Phone.AnswerCall();
            Thread.Sleep(1000);
            caller1.Phone.DropCall();
            answerer.Phone.DropCall();


            caller1.Phone.Call(answerer.Phone.PhoneNumber);
            answerer.Phone.AnswerCall();
            Thread.Sleep(1500);
            caller1.Phone.DropCall();
            answerer.Phone.DropCall();

            answerer.Phone.Call(caller1.Phone.PhoneNumber);
            caller1.Phone.AnswerCall();
            Thread.Sleep(2000);
            caller1.Phone.DropCall();
            answerer.Phone.DropCall();

            caller2.Phone.Call(answerer.Phone.PhoneNumber);
            answerer.Phone.AnswerCall();
            Thread.Sleep(3000);
            caller2.Phone.DropCall();
            answerer.Phone.DropCall();

            caller2.Phone.Call(answerer.Phone.PhoneNumber);
            Thread.Sleep(500);
            caller2.Phone.DropCall();

            caller2.Phone.Call(answerer.Phone.PhoneNumber);
            Thread.Sleep(500);
            answerer.Phone.DropCall();

            IReport caller1Report = billingSystem.CreateReport(caller1);
            IReport caller2Report = billingSystem.CreateReport(caller2);
            IReport answererReport = billingSystem.CreateReport(answerer);

            billingSystem.SortCallsByDate(caller1Report);
            billingSystem.SortCallsByDate(caller2Report);
            billingSystem.SortCallsByDate(answererReport);


            Console.ReadKey(); 
        }
    }
}
