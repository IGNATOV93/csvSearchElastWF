using System;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        Qyery controller;
        public Form1()
        {
            InitializeComponent();
            controller = new Qyery(ConnectionString.ConnStr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = controller.updatetDB();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            controller.Add();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            string textOPENfile;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                 textOPENfile = ofd.FileName;   
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            string TextInputSearch;
            TextInputSearch = textBox2.Text;
            dataGridView2.DataSource = Elastic.SearchELast(TextInputSearch);
            ;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox4.Text = "CloudID";//подсказка
            textBox4.ForeColor = Color.Gray;
            textBox3.Text = "UserName";//подсказка
            textBox3.ForeColor = Color.Gray;
            textBox1.Text = "Password";//подсказка
            textBox1.ForeColor = Color.Gray;
            richTextBox1.Text = @"Вначале авторизуйтесь в эластике,прежде чем выбирать csv файл !
Название файла вашей бд должно быть Database4.mdb и файл закинуть по пути С:\intel
Название файла вашего csv должно быть posts.csv и выбрать через кнопку INSERT
Поля в вашей таблице mdb должны быть :ID,text_,created_date,rubrics .
Готовая таблица в mdb будет приложена файлом с программой.
";
            richTextBox2.Text = "ID";
            richTextBox3.Text = "ID";
        }
        private void button2_Click(object sender, EventArgs e)
        { 
            string text4 = textBox4.Text;
            string text3 = textBox3.Text;
            string text1 = textBox1.Text;
            textBox4.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;
            textBox1.ForeColor = Color.Black;
            Elastic.Autorization(text4,text3,text1); 
        }
        private void button5_Click(object sender, EventArgs e)
        {
            controller.DeleteID(richTextBox2.Text);
            MessageBox.Show("Запись удалена из БД");
            controller.updatetDB();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Elastic.DeleteELast_ID(richTextBox3.Text);
            MessageBox.Show("Запись удалена");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Elastic.DeleteELast_INDEX();
            MessageBox.Show("Индекс удален");
        }
    }
}
