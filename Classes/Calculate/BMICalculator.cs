using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Metrics_Desktop_App.Classes.Calculate
{
    internal class BMICalculator:Calculator
    {
        public BMICalculator(float inaltime, float masa): base(inaltime, masa)
        {
        }

        public float GetBMI()
        {
            return Masa / (Inaltime * Inaltime);
        }


        public override float Calculate()
        {
            return GetBMI();
        }


        public override string WeightCategory()
        {
            float bmi = GetBMI();

            if (bmi < BMI_ranges[0]) return "Underweight";
            if (bmi < BMI_ranges[1]) return "Normal";
            if (bmi < BMI_ranges[2]) return "Overweight";
            if (bmi < BMI_ranges[3]) return "Obese Class 1";
            if (bmi < BMI_ranges[4]) return "Obese Class 2";

            return "Obese Class 3";
        }

        public override float GetWeightDiffrence()
        {

            // BMI normal range midpoint
            float normalBMI = (BMI_ranges[0] + BMI_ranges[1]) / 2f;

            // Calcul greutate ideală
            float idealWeight = normalBMI * (Inaltime * Inaltime);

            // Diferența între greutatea ideală și cea curentă
            float diference = idealWeight - Masa;

            return diference;


        }

    }
}
