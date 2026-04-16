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


        // CONTROLLER
        public void SaveButton(int personId, float height, float mass)
        {
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
        }


    }
}
