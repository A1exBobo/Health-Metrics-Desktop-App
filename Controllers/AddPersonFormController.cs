using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Health_Metrics_Desktop_App.Controllers
{
    internal class AddPersonFormController
    {
        //here comes the validation of input 
        public string Firstname;
        public string Lastname;
        public string FatherInitials;
        public int  Age;



        public AddPersonFormController(string firstname, string lastname, string fatherInitials, int age)
        {
            Firstname = firstname;
            Lastname = lastname;
            FatherInitials = fatherInitials;
            Age = age;
        }

        private void Message(string message)
        {
            //dispalys a message 
            MessageBox.Show(message); //should customize 
        }


        private void FixSpelling(string name)
        {
            name = new string(name.Where(c => !char.IsDigit(c)).ToArray());

            // trim spaces
            name = name.Trim();



            // capitalize first letter
            name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }


        public void ValidateName()
        {
            //function checks (name>2 name name < 30) 
            //checks spelling mistakes 
            //not allowed numbers 
            //for every error it dispalys a message 
            if (string.IsNullOrWhiteSpace(Firstname))
            {
                Message("You have to provide a FirstName.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Lastname))
            {
                Message("You have to provide a LastName.");
                return;
            }

            FixSpelling(Firstname); FixSpelling(Lastname);

        } 


        public void ValidateInitials()
        {
            //function checkes (name<2)
            //doesn't allow numbers 
            if (string.IsNullOrWhiteSpace(Firstname))
            {
                Message("You have to provide fathers initials.");
                return;
            }


        }

        public void  ValidateAge()
        {
            //age must be positive (between 18 and 65 for adults)
            if (Age < 18 || Age > 65)
            {
                Message("Your age is out of the adult range.");
                return;
            }

        }



    }
}
