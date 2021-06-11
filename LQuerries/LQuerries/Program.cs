using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LQuerries
{
    class Program
    {
        //Yield trials
        static IEnumerator<int> GetInts()
        {
            Console.WriteLine("first");
            yield return 1;

            Console.WriteLine("second");
            yield return 2;
        }

        //Yield break
        static IEnumerator<int> GenerateMultiplicationTable(int maxValue)
        {
            for (int i = 2; i <= 10; i++)
            {
                for (int j = 2; j <= 10; j++)
                {
                    int result = i * j;

                    if (result > maxValue)
                    {

                        yield break;

                    }
                    else
                    {
                        Console.WriteLine($"{result} is greater than {maxValue}");
                        yield return result;
                    }

                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("LinqQuerries\n");
            //****LINQ Query to Array****

            // Data source
            string[] names = { "Bill", "Steve", "James", "Mohan" };

            // LINQ Query 
            var myLinqQuery = from name in names
                              where name.Contains('a')
                              select name;

            // Query execution
            foreach (var name in myLinqQuery)
                Console.WriteLine(name + " ");
            Console.WriteLine("\n");
            //LINQ LAMBDA EXPRESSION
            Student[] studentArray = {
                    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                    new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                    new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 } ,
                    new Student() { StudentID = 6, StudentName = "Chris",  Age = 17 } ,
                    new Student() { StudentID = 7, StudentName = "Rob",Age = 19  } ,
                    new Student() { StudentID = 8, StudentName = "Bill",  Age = 26 } ,
                };

            // Use LINQ to find teenager students
            Student[] teenAgerStudents = studentArray.Where(s => s.Age > 12 && s.Age < 20).ToArray();
            //teenAgerStudents.ToList();
            foreach (var teen in teenAgerStudents) { Console.WriteLine(teen); }
            Console.WriteLine("\n");

            //Extracting Age as a List and iterating it with ForEach Method
            var Ago = (from b in teenAgerStudents select b.Age).ToList<int>();
            Ago.ForEach(i => { Console.WriteLine(i); });
            Console.WriteLine("\n");

            //Extracting Age,StudentName,StudentID from  Array as a List<teenAger>() and iterating through
            var infoteen = (from c in teenAgerStudents select new { c.Age, c.StudentID, c.StudentName });
            foreach (var ab in infoteen)
            {
                Console.WriteLine(ab.StudentName + "\t" + ab.StudentID + "\t" + ab.Age);
            }
            Console.WriteLine("\n");

            // Use LINQ to find first student whose name is Bill 
            Student bill = studentArray.Where(s => s.StudentName == "Bill").FirstOrDefault();
            Console.WriteLine(bill.Age + "\t" + bill.StudentID + "\t" + bill.StudentName);
            Console.WriteLine("\n");

            // Use LINQ to find student whose StudentID is 5 FirstOrDefault Method
            Student student5 = studentArray.Where(s => s.StudentID == 5).FirstOrDefault();
            Console.WriteLine(student5.Age + "\t" + student5.StudentName);
            Console.WriteLine("\n");

            //LastOrDefault Method
            var std6 = studentArray.Where(s => s.StudentName == "Bill").LastOrDefault();
            Console.WriteLine(std6.Age + "\t" + std6.StudentID);
            Console.WriteLine("\n");


            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13} ,
                new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };

            var filteredResult = studentList.Where((s, i) => {
                if (i % 2 == 0) // if it is even element
                    return true;

                return false;
            });

            foreach (var std in filteredResult)
                Console.WriteLine(std.StudentName);

            //Calling  Yield return

            IEnumerator<int> intsEnumerator = GetInts(); // print nothing
            Console.WriteLine("...");                    // print "..."

            intsEnumerator.MoveNext();                   // print "first"
            Console.WriteLine(intsEnumerator.Current);   // print 1

            intsEnumerator.MoveNext();
            Console.WriteLine(intsEnumerator.Current);

            //Calling Yield break
            var gener = GenerateMultiplicationTable(1);
            Console.WriteLine(gener.MoveNext());

            //Non Generic List=>ArrayList //****ofType() Linq
            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(3.12m);
            mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });

            var stringResult = from s in mixedList.OfType<string>()
                               select s;

            var intResult = from s in mixedList.OfType<int>()
                            select s;
            var decResult = from s in mixedList.OfType<Decimal>()
                            select s;

            var stdResult = from s in mixedList.OfType<Student>()
                            select s;

            foreach (var str in stringResult)
                Console.WriteLine(str);

            foreach (var integer in intResult)
                Console.WriteLine(integer);

            foreach (var deci in decResult)
                Console.WriteLine(deci);

            foreach (var std in stdResult)
                Console.WriteLine(std.StudentName);
            Console.WriteLine("\n");

            //Thenby Both Ascending and Descending. Multiple Sort
            var thenByResult = studentList.OrderBy(s => s.StudentName).ThenBy(s => s.Age);

            var thenByDescResult = studentList.OrderBy(s => s.StudentName).ThenByDescending(s => s.Age);

            Console.WriteLine("ThenBy:");

            foreach (var std in thenByResult)
                Console.WriteLine("Name: {0}, Age:{1}", std.StudentName, std.Age);
            Console.WriteLine("\n");

            Console.WriteLine("ThenByDescending:");

            foreach (var std in thenByDescResult)
                Console.WriteLine("Name: {0}, Age:{1}", std.StudentName, std.Age);
            Console.WriteLine("\n");

            //****Group By METHOD*****
            var groupedResult = from s in studentList
                                group s by s.Age;

            //iterate each group        
            foreach (var ageGroup in groupedResult)
            {
                Console.WriteLine("Age Group: {0}", ageGroup.Key); //Each group has a key 

                foreach (Student s in ageGroup) // Each group has inner collection
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }
            Console.WriteLine("\n");

            //TO LOOKUP()
            var lookupResult = studentList.ToLookup(s => s.Age);

            foreach (var group in lookupResult)
            {
                Console.WriteLine("Age Group: {0}", group.Key);  //Each group has a key 

                foreach (Student s in group)  //Each group has a inner collection  
                    Console.WriteLine("Student Name: {0}", s.StudentName);
            }

            //Range from 1 to 7
            List<int> Rangedvalues = Range(1, 7);
            Console.WriteLine("FROM 1 to 7:");
            Rangedvalues.ForEach(i => Console.WriteLine(i));

            //Range from 4 to 20
            Console.WriteLine("\n\nFROM 4 to 20:");
            Range(4, 20).ForEach(n => Console.WriteLine(n));

            //Getting Odd and Even Numbers from Linq
            var even = Range(4, 40).Where(c => c % 2 == 0);
            Console.WriteLine("\n\nEVEN NUMBERS ARE:");
            foreach (var evener in even)
            {
                Console.Write(evener + ",");
            }

            //Getting Odd and Even Numbers from Linq
            var odd = Range(4, 40).Where(c => c % 2 != 0);
            Console.WriteLine("\n\nODD NUMBERS ARE:");
            foreach (var ods in odd)
            {
                Console.Write(ods + ",");
            }
            Console.ReadLine();


        }
        //Writting the Range METHOD
        static List<int> Range(int start, int end)
        {
            List<int> valuable = new List<int>();
            for (int i = start; i <= end; i++)
            {
                valuable.Add(i);
            }
            return valuable;
        }
    }
}
