using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registraciya
{
    public partial class Form1 : Form
    {
        Class1 db = new Class1();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.MaxLength = 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var pwd = textBox2.Text;

            string com = $"INSERT INTO Login (User_name, Password) VALUES ('{login}', '{pwd}')";

            SqlCommand cmd = new SqlCommand(com, db.getConnection());

            db.openConnection();
            if (CheckUser())
            {
                return;
            }
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан", "Респект");
            }
            else
            {
                MessageBox.Show("Не получилось");
            }
            db.closeConnection();
        }

        private Boolean CheckUser()
        {
            var login = textBox1.Text;
            var pwd = textBox2.Text;

            string check = $"SELECT * FROM Login WHERE User_name = '{login}' and Password = '{pwd}'";

            SqlCommand checkCmd = new SqlCommand(check, db.getConnection());
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            dataAdapter.SelectCommand = checkCmd;
            dataAdapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Не повезло, не фартануло");
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == 64 || e.KeyChar == 33)
            {
                e.Handled = false;
            }
            else 
            { 
                e.Handled = true; 
            }
        }
    }
}
