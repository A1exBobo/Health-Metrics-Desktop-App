using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Metrics_Desktop_App.Classes.Calculate
{
    internal class PICalculator:Calculator
    {
        public PICalculator(float inaltime, float masa): base(inaltime, masa)
        {
        }

        public float GetPI()
        {
            return Masa / (Inaltime * Inaltime * Inaltime);
        }

        public override float Calculate()
        {
            return GetPI();
        }

        public override string WeightCategory()
        {
            /*
              BMI_ranges = { 18.5f, 24.9f, 29.9f, 34.9f, 39.9f };
              PI_ranges = { 11f, 14f, 17f };
             */

            float pi = GetPI();

            if (pi < PI_ranges[0]) return "Underweight";
            if (pi < PI_ranges[1]) return "Normal";
            if (pi < PI_ranges[2]) return "Overweight";


            return "Obese Class 1,2 or 3";
        }

        public override float GetWeightDiffrence()
        {
            float normalPI = (PI_ranges[0] + PI_ranges[1]) / 2f;

            float idealWeight = normalPI * (Inaltime * Inaltime * Inaltime);
            float diference = idealWeight - Masa;

            return diference;
        }

    }
}
