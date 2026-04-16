using Health_Metrics_Desktop_App.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Health_Metrics_Desktop_App
{
    public partial class IndexForm : Form
    {
        public IndexForm()
        {
            InitializeComponent();
        }


        //save button
        private void button1_Click(object sender, EventArgs e)
        {
            //execute query(insertMeasurement)

            //display some content in the texboxes 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //takes me to AddpersonForm
            AddPers AddPers = new AddPers();
            AddPers.Show();
        }
    }
}
