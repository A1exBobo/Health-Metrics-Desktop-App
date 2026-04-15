using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Metrics_Desktop_App.Classes
{
    internal class Person
    {
        public string FirstName;
        public string LastName;
        public string FatherInitials;
        public int Age;
        public double Height;
        public double Mass;

        public Person(string firstname,string lastname,string fatherinitials,int age,double height,double mass) 
        {
            FirstName = firstname;
            LastName = lastname;
            FatherInitials = fatherinitials;
            Age = age;
            Height = height;
            Mass = mass;
        }
    }
}
