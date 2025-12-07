using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace NN
{
    public partial class Form2 : Form
    {
        private DataTable dataTable = new DataTable();
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Database.mdb;");
        private OleDbDataAdapter adapter;
        private DataView dataView;

        public string TextBox1Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string TextBox2Value
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string TextBox4Value
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public Form2()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                connection.Open();
                adapter = new OleDbDataAdapter("SELECT * FROM bd", connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.Fill(dataTable);

                dataView = new DataView(dataTable);
                dataGridView1.DataSource = dataView;

                // Делаем видимыми только нужные столбцы
                if (dataGridView1.Columns.Contains("Код"))
                {
                    dataGridView1.Columns["Код"].Visible = false;
                }

            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                textBox4.Text = dataGridView1.SelectedCells[0].Value?.ToString();
            }
            else
            {
                MessageBox.Show("Выберите ячейку в таблице.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox4.Text) ||
              string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            // Добавлена проверка номера телефона
            else if (!textBox6.Text.StartsWith("+7") || textBox6.Text.Length != 12)
            {
                MessageBox.Show("Номер телефона должен начинаться с +7 и содержать 12 символов вместе с +7.");
                return;
            }

            else
            {

                // Сохраняем данные в БД
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Database.mdb;";
                string query = "INSERT INTO bron (Услуга, Цена, Время, Имя, Номер) " +
                               "VALUES (@usluga, @cena, @vremya, @imya, @nomer)";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usluga", textBox1.Text);
                    command.Parameters.AddWithValue("@cena", textBox2.Text);
                    command.Parameters.AddWithValue("@vremya", textBox4.Text); // Дату и время
                    command.Parameters.AddWithValue("@imya", textBox5.Text);
                    command.Parameters.AddWithValue("@nomer", textBox6.Text);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные сохранены!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при сохранении: " + ex.Message);
                    }
                    Form3 form3 = new Form3();
                    form3.TextBox1Value = textBox1.Text;
                    form3.TextBox4Value = textBox4.Text;
                    form3.TextBox5Value = textBox5.Text;
                    form3.Show();
                    this.Hide();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Экранирование одинарных кавычек
            string filterValue = textBox1.Text.Replace("'", "''");

            // Задаем фильтр для DataView  (с учетом регистра)
            dataView.RowFilter = $"Услуга LIKE '%{filterValue}%'";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
