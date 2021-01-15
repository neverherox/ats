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
            IPhone phone4 = new Phone { PhoneNumber = "444444444444444" };

            station.RegisterPhone(phone1);
            station.RegisterPhone(phone2);
            station.RegisterPhone(phone3);
            station.RegisterPhone(phone4);

            IAbonent caller1 = new Abonent { Name = "A", Phone = phone1 };
            IAbonent caller2 = new Abonent { Name = "B", Phone = phone2 };
            IAbonent caller3 = new Abonent { Name = "C", Phone = phone3 };
            IAbonent caller4 = new Abonent { Name = "D", Phone = phone4 };
            billingSystem.RegisterAbonent(caller1);
            billingSystem.RegisterAbonent(caller2);
            billingSystem.RegisterAbonent(caller3);
            billingSystem.RegisterAbonent(caller4);


            caller1.Phone.Call(caller3.Phone.PhoneNumber);
            caller3.Phone.AnswerCall();
            caller2.Phone.Call(caller1.Phone.PhoneNumber);
            Thread.Sleep(1000);
            caller2.Phone.DropCall();

            caller2.Phone.Call(caller4.Phone.PhoneNumber);
            caller4.Phone.AnswerCall();
            Thread.Sleep(1500);

            caller1.Phone.DropCall();
            caller2.Phone.DropCall();
            caller3.Phone.DropCall();
            caller4.Phone.DropCall();

            caller1.Phone.Call(caller2.Phone.PhoneNumber);
            caller2.Phone.AnswerCall();
            Thread.Sleep(2000);

            caller3.Phone.Call(caller4.Phone.PhoneNumber);
            caller4.Phone.AnswerCall();
            Thread.Sleep(3000);

            caller1.Phone.DropCall();
            caller2.Phone.DropCall();
            caller3.Phone.DropCall();
            caller4.Phone.DropCall();

            caller2.Phone.Call(caller3.Phone.PhoneNumber);
            Thread.Sleep(1000);
            caller2.Phone.DropCall();

            caller2.Phone.Call(caller3.Phone.PhoneNumber);
            Thread.Sleep(1000);
            caller3.Phone.DropCall();
            caller2.Phone.DropCall();


            IReport report1 = billingSystem.CreateReport(caller1);
            IReport report2 = billingSystem.CreateReport(caller2);
            IReport report3 = billingSystem.CreateReport(caller3);
            IReport report4 = billingSystem.CreateReport(caller4);

            Console.WriteLine();
            Console.WriteLine(report1.ToString());
            Console.WriteLine(report2.ToString());
            Console.WriteLine(report3.ToString());
            Console.WriteLine(report4.ToString());

            Console.ReadKey(); 
        }
    }
}
