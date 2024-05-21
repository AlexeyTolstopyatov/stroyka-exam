using strc.sqlserver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace strc
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            string alltables = "SELECT TABLE_NAME FROM strc.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            string[] vec = await QueryManager.ExecuteVectorAsync(alltables);
            foreach (string s in vec) 
            {
                tablesList.Items.Add(s);
            }
        }

        private async void tablesList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tableView.Controls.Clear();

            // selected item
            DataTable table = 
                await QueryManager.ExecuteMapAsync($"SELECT * FROM [{tablesList.SelectedItem}]");

            tableView.DataSource = table;
        }
    }
}
