using Health_Metrics_Desktop_App.Classes;
using Health_Metrics_Desktop_App.Classes.Calculate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Health_Metrics_Desktop_App.Handlers
{
    internal class IndexFormController
    {
        


        public IndexFormController() { }


        private void Message(string message)
        {
            //dispalys a message 
            MessageBox.Show(message); //should customize 
        }
        private void ValidateHeight(float height)
        {
            if (height < 1.2f || height > 2.3f)
                throw new ArgumentException("Height must be between 1.2m and 2.3m.");
        }

        private void ValidateMass(float mass)
        {
            if (mass < 30f || mass > 300f)
                throw new ArgumentException("Mass must be between 30kg and 300kg.");
        }
        // CONTROLLER
        public void SaveButton(int personId, float height, float mass)
        {
            try
            {
                ValidateHeight(height);
                ValidateMass(mass);

                var bmiCalc = new BMICalculator(height, mass);
                var piCalc = new PICalculator(height, mass);

                float bmi = bmiCalc.Calculate();
                float pi = piCalc.Calculate();

                var executor = new QueryExecutor();

                executor.ExecuteNonQueryFromResource(
                    QueryExecutor.SqlFiles.InsertMeasurements,
                    new SqlParameter("@PersonId", personId),
                    new SqlParameter("@Height", height),
                    new SqlParameter("@Mass", mass),
                    new SqlParameter("@BMI", bmi),
                    new SqlParameter("@PI", pi)
                );

                Message("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        public (float bmi, float pi) Calculate(float height, float mass)
        {
            ValidateHeight(height);
            ValidateMass(mass);

            var bmiCalc = new BMICalculator(height, mass);
            var piCalc = new PICalculator(height, mass);

            return (bmiCalc.Calculate(), piCalc.Calculate());
        }


        public void Calculatebutton(int personId,float height, float mass)
        {
            ValidateHeight(height);
            ValidateMass(mass);

            var bmiCalc = new BMICalculator(height, mass);  //textBox.value 
            var piCalc = new PICalculator(height, mass);    //TextBox.value

            float bmi = bmiCalc.Calculate(); //textBox.Value
            float pi = piCalc.Calculate();  //textBox.Value

        }


    }
}
