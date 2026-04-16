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
            var controller = new IndexFormController();

            controller.SaveButton(
                (int)comboBox1.SelectedValue,
                (float)numericUpDown1.Value,
                (float)numericUpDown2.Value
            );
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
        }
    }
}
