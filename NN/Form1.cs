using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NN
{
    public partial class Form1 : Form
    {
        private DataTable dataTable = new DataTable();
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Database.mdb;");
        private OleDbDataAdapter adapter;
        private DataView dataView;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                connection.Open();
                adapter = new OleDbDataAdapter("SELECT * FROM services", connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.Fill(dataTable);

                dataView = new DataView(dataTable);
                dataGridView1.DataSource = dataView;

                // Скрываем все столбцы
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.Visible = false;
                }

                // Делаем видимыми только нужные столбцы
                if (dataGridView1.Columns.Contains("Название услуги"))
                {
                    dataGridView1.Columns["Название услуги"].Visible = true;
                }
                else
                {
                    MessageBox.Show("Столбец 'Название услуги' не найден в таблице.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (dataGridView1.Columns.Contains("Цена"))
                {
                    dataGridView1.Columns["Цена"].Visible = true;
                }
                else
                {
                    MessageBox.Show("Столбец 'Цена' не найден в таблице.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем индекс выбранной строки
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

                if (dataGridView1.Columns.Contains("Название услуги") && dataGridView1.Columns.Contains("Цена")
                    && dataGridView1.Columns.Contains("Описание") && dataGridView1.Columns.Contains("Длительность") && dataGridView1.Columns.Contains("Код"))
                {
                    textBox1.Text = dataGridView1.Rows[selectedRowIndex].Cells["Название услуги"].Value?.ToString() ?? "";
                    textBox2.Text = dataGridView1.Rows[selectedRowIndex].Cells["Цена"].Value?.ToString() ?? "";
                    textBox3.Text = dataGridView1.Rows[selectedRowIndex].Cells["Описание"].Value?.ToString() ?? "";
                    textBox4.Text = dataGridView1.Rows[selectedRowIndex].Cells["Длительность"].Value?.ToString() ?? "";
                }
                else
                {
                    MessageBox.Show("Один или несколько столбцов не найдены в таблице.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку в DataGridView.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Проверка на заполненность всех текстбоксов
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, Выберите услугу", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Прерываем выполнение метода, если поля не заполнены
            }

            // Создаем экземпляр Form2 и передаем значения в свойства.
            Form2 form2 = new Form2();

            form2.TextBox1Value = textBox1.Text;
            form2.TextBox2Value = textBox2.Text;
            form2.TextBox4Value = textBox4.Text;

            // Отображаем Form2
            form2.Show();

            // Скрываем текущую форму (BDform0).  Не закрываем, чтобы не потерять данные.
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
