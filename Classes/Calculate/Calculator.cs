using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Metrics_Desktop_App.Classes.Calculate
{
    internal class Calculator
    {
        //idealweight weightcategory 


        protected float[] BMI_ranges = new float[] { 18.5f, 24.9f, 29.9f, 34.9f, 39.9f };
        protected float[] PI_ranges = new float[] { 11f, 14f, 17f };
        public float Inaltime;
        public float Masa;

        public Calculator(float inaltime, float masa)
        {

            Inaltime = inaltime;
            Masa = masa;
        }

        public virtual float GetWeightDiffrence()
        {
            return 0;
        }

        public virtual string WeightCategory()
        {
            return "Nu s-a implementat";
        }

        public virtual float Calculate()
        {
            return 0;

        }
    }
}
