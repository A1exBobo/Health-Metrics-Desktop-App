using Health_Metrics_Desktop_App.Classes;
using Health_Metrics_Desktop_App.Forms;
using Health_Metrics_Desktop_App.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            float height = (float)numericUpDown1.Value;
            float mass = (float)numericUpDown2.Value;

            int personId = (int)comboBox1.SelectedValue;

            try
            {
                var controller = new IndexFormController();
                controller.SaveButton(personId, height, mass);

                textBox1.Clear(); 
                textBox3.Clear();

                numericUpDown1.Value = numericUpDown1.Minimum;
                numericUpDown2.Value = numericUpDown2.Minimum;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //takes me to AddpersonForm
            AddPers AddPers = new AddPers();
            AddPers.Show();
        }

        private void IndexForm_Load(object sender, EventArgs e)
        {
            var executor = new QueryExecutor();

            comboBox1.DataSource = executor.ExecuteQueryFromResource(QueryExecutor.SqlFiles.SelectPerson);
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "PersonId";
            comboBox1.SelectedIndex = 0;

            Savebutton.Visible = false; //hides save button on load 
        }

        //calculate pi and bmi button 
        private void button1_Click_1(object sender, EventArgs e)
        {
            float height = (float)numericUpDown1.Value;
            float mass = (float)numericUpDown2.Value;

            try
            {
                var controller = new IndexFormController();
                var result = controller.Calculate(height, mass);

                textBox1.Text = result.bmi.ToString("0.00");
                textBox3.Text = result.pi.ToString("0.00");

                Savebutton.Visible = true; // show Save button

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
