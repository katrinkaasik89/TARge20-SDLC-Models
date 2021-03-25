using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FridgeDB
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;

        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["FridgeDB.Properties.Settings.FridgeConnectionString"].ConnectionString;
        }
        private void PopulateFoodTable()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM FoodType", connection))
            {
                DataTable foodTable = new DataTable();
                adapter.Fill(foodTable);

                listFood.DisplayMember = "FoodTypeName";
                listFood.ValueMember = "Id";
                listFood.DataSource = foodTable;
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFoodName();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateFoodTable();
        }

        private void Food(object sender, EventArgs e)
        {

        }
        private void PopulateFoodName()
        {
            string query = "SELECT Food.Name FROM FoodType INNER JOIN Food On Food.TypeId = FoodType.Id WHERE FoodType.Id=@TypeId";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@TypeId", listFood.SelectedValue);
                DataTable foodTable = new DataTable();
                adapter.Fill(foodTable);
                listFoodNames.DisplayMember = "Name";
                listFoodNames.ValueMember = "Id";
                listFoodNames.DataSource = foodTable;
            }
        }
    }
}
