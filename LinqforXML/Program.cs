using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace LinqforXML
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load From XML
            List<Plant> Plants = (from e in XDocument.Load("plant_catalog.xml").Root.Elements("PLANT")
                                  select new Plant
                                  {
                                      Common = e.Element("COMMON").Value,
                                      Botanical = e.Element("BOTANICAL").Value,
                                      Zone = e.Element("ZONE").Value,
                                      Light = e.Element("LIGHT").Value,
                                      Price = e.Element("PRICE").Value.Replace("$", string.Empty),
                                      Availability = e.Element("AVAILABILITY").Value
                                  }).ToList();
            foreach (Plant p in Plants)
            {
                Console.WriteLine("PLANT : ");
                Console.WriteLine("Common:{0},Botanical:{1},Zone:{2},Light:{3},Price:{4},Availability:{5}\n", p.Common, p.Botanical, p.Zone, p.Light, p.Price, p.Availability);
            }

            Console.WriteLine(" ==================== ");

            // Load XML
            var Employees = (from e in XDocument.Load("employees.xml").Root.Elements("Employee")
                             select new Employee
                             {
                                 Name = e.Element("Name").Value,
                                 Sex = e.Element("Sex").Value,
                                 Age = Convert.ToInt32(e.Element("Age").Value),
                                 Emails = (from mail in e.Element("Emails").Elements("Email")
                                           select new Email
                                           {
                                               Address = mail.Value
                                           }).ToArray(),
                                 Phones = (from p in e.Element("Phones").Elements("Phone")
                                           select new Phone
                                           {
                                               PhoneNumber = p.Value
                                           }).ToArray(),
                                 // XContainer.Element()可回傳null，非引發例外
                                 Note = e.Element("Note") == null ? string.Empty : e.Element("Note").Value
                             }).ToList();
            foreach (Employee e in Employees)
            {
                Console.WriteLine("Name:{0}\tSex:{1}\tAge:{2}", e.Name, e.Sex, e.Age);
                foreach (Email mail in e.Emails)
                    Console.WriteLine("Email:{0}", mail.Address);
                foreach (Phone phone in e.Phones)
                    Console.WriteLine("Phone:{0}", phone.PhoneNumber);
                if (!string.IsNullOrEmpty(e.Note))
                    Console.WriteLine("Note:{0}", e.Note);
                Console.WriteLine("\n");
            }

            Console.ReadLine();
        }
    }

    // 對應 XML 文件之物件
    class Plant
    {
        public string Common { get; set; }
        public string Botanical { get; set; }
        public string Zone { get; set; }
        public string Light { get; set; }
        public string Price { get; set; }
        public string Availability { get; set; }
    }

    class Email
    {
        public string Address { get; set; }
    }

    class Phone
    {
        public string PhoneNumber { get; set; }
    }

    // 對應 XML 文件之物件
    class Employee
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public Email[] Emails { get; set; }
        public Phone[] Phones { get; set; }
        public string Note { get; set; }
    }
}
