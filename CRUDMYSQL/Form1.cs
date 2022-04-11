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

namespace CRUDMYSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=LAB-RPL1-PC23\\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
        con.Open();
            string status = "";
            if (radioButton1.Checked == true)
            {
                status = radioButton1.Text;
            }
            else
            {
                status = radioButton2.Text;
            }
            SqlCommand com = new SqlCommand("exec dbo.SP_Product_Insert '"+int.Parse(textBox1.Text)+"', '"+textBox2.Text+ "','"+comboBox1.Text+"', '"+ status +"', '"+ dateTimePicker1.Value.Date.ToString("MM/dd/yyyy") +"'", con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Saved");
            LoadAllRecord();
        }

        void LoadAllRecord()
        {
            SqlCommand com = new SqlCommand("exec dbo.SP_Product_View", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllRecord();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string status = "";
            if (radioButton1.Checked == true)
            {
                status = radioButton1.Text;
            }
            else
            {
                status = radioButton2.Text;
            }
            SqlCommand com = new SqlCommand("exec dbo.SP_Product_Update '" + int.Parse(textBox1.Text) + "', '" + textBox2.Text + "','" + comboBox1.Text + "', '" + status + "', '" + DateTime.Parse(dateTimePicker1.Text) + "'", con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Update");
            LoadAllRecord();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("Are you confirm to delete?", "Delete", MessageBoxButtons.YesNo)== DialogResult.Yes)
            {
            con.Open();
            string status = "";
            SqlCommand com = new SqlCommand("exec dbo.SP_Product_Delete '" + int.Parse(textBox1.Text) + "'", con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Delete");
            LoadAllRecord();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.SP_Product_Search '" + int.Parse(textBox1.Text) + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
