using System;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
   public class Qyery
    {
         OleDbConnection connection; 
         OleDbCommand command;
         OleDbDataAdapter dataAdapter;
        DataTable bufferTable;
        public Qyery(string Conn)
        {
            connection= new OleDbConnection(Conn);
            bufferTable = new DataTable();
        }

        public DataTable updatetDB()
        {
            connection.Open ();
            dataAdapter = new OleDbDataAdapter("Select * from tDB", connection);
            bufferTable.Clear ();
            dataAdapter.Fill(bufferTable);
            connection.Close ();
            return bufferTable;    
        } 

        public void DeleteID(string inputID)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM tDB WHERE ID={inputID}" , connection);
            command.ExecuteNonQuery ();
            connection.Close();
        }

        public  void Add ()
        {
            MessageBox.Show("Нажмите ОК и выбери файл csv");
            var csvnew = JobItem.LoadFile(@OpenCsvFile());
            connection.Open();

            MessageBox.Show("Закройте это окно, и дождитесь надписи что все занесено в таблицу!");
            foreach (var i in csvnew)
            {
                command = new OleDbCommand($"INSERT INTO tDB(text_,created_date,rubrics) VALUES(@text_,created_date,@rubrics)", connection);
                command.Parameters.AddWithValue("text", i.text);
                command.Parameters.AddWithValue("created_at", DateTime.Parse(i.created_date));
                command.Parameters.AddWithValue("updated_at", i.rubrics);
                command.ExecuteNonQuery();    
            }
            MessageBox.Show("все занесено в таблицу tDB");
            connection.Close ();
        }
        public string OpenCsvFile()
        {
            string textOPENfile="";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textOPENfile = ofd.FileName;
            return textOPENfile;    
        }   
      }
 }

