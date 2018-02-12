using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialization
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Company Company { get; set; }

        public Person()
        { }

        public Person(string name, int age, Company comp)
        {
            Name = name;
            Age = age;
            Company = comp;
        }
    }

    [Serializable]
    public class Company
    {
        public string Name { get; set; }

        // стандартный конструктор без параметров
        public Company() { }

        public Company(string name)
        {
            Name = name;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Person person1 = new Person("Tom", 29, new Company("Microsoft"));
            //Person person2 = new Person("Bill", 25, new Company("Apple"));
            //Person[] people = new Person[] {  };

            XmlSerializer formatter = new XmlSerializer(typeof(NewDataSet));

            //using (FileStream fs = new FileStream(@"C:\1\people.xml", FileMode.OpenOrCreate))
            //{
            //    formatter.Serialize(fs, people);
            //}

            using (FileStream fs = new FileStream(@"C:\1\www.xml", FileMode.Open))
            {
                NewDataSet newpeople = (NewDataSet)formatter.Deserialize(fs);

                foreach (var p in newpeople.Table1)
                {
                    
                    
                    Console.WriteLine(p.D126, p.D39, p.D83, p.N313);
                }
            }
            Console.ReadLine();
        }
    }
}