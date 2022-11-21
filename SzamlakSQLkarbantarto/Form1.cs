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
using MySql.Data.MySqlClient;

namespace SzamlakSQLkarbantarto
{
    public partial class Form1 : Form
    {

        MySqlConnection conn;
        MySqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "szamlak";
            conn = new MySqlConnection(builder.ConnectionString);

            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "A program leáll!");
                Environment.Exit(0);
            }
            finally
            {
                conn.Close();
            }

            lista_update();

        }

        private void lista_update()
        {
            listBox.Items.Clear();
            cmd.CommandText = "SELECT `id`,`tulajdonosNeve`,`egyenleg`,`nyitasdatum` FROM `szamlak` WHERE 1";
            conn.Open();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Szamla uj = new Szamla(dr.GetInt32("id"), dr.GetString("tulajdonosNeve"), dr.GetInt32("egyenleg"), dr.GetDateTime("nyitasdatum"));
                    listBox.Items.Add(uj);
                }

            }

            conn.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "INSERT INTO `szamlak` (`id`, `tulajdonosNeve`, `egyenleg`, `nyitasdatum`) VALUES (NULL, @tulajdonosneve, @egyenleg, @nyitasdatum);";
            cmd.Parameters.AddWithValue("@tulajdonosneve",textBoxTulajdonosneve.Text);
            cmd.Parameters.AddWithValue("@egyenleg", textBoxEgyenleg.Text);
            cmd.Parameters.AddWithValue("@nyitasdatum", dateTimePicker1.Value);
            conn.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Sikeres rogzites!");
                conn.Close();
            }
            if (conn.State==ConnectionState.Open) 
            {
                conn.Close();
            }

            lista_update();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex<0)
            {
                MessageBox.Show("Nincs kijelolve semmi!");  
            }

            cmd.CommandText = "UPDATE `szamlak` SET `id`=@id,`tulajdonosNeve`=@tulajdonosneve,`egyenleg`=@egyenleg,`nyitasdatum`=@nyitasdatum WHERE `id`=@id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", textBoxId.Text);
            cmd.Parameters.AddWithValue("@tulajdonosneve", textBoxTulajdonosneve.Text);
            cmd.Parameters.AddWithValue("@egyenleg", textBoxEgyenleg.Text);
            cmd.Parameters.AddWithValue("@nyitasdatum", dateTimePicker1.Value);
            conn.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Sikeres modositas!");
                
            }
            conn.Close();
   
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
            {
                return;
            }
            Szamla kivalasztottszamla = (Szamla)listBox.SelectedItem;
            textBoxId.Text = kivalasztottszamla.Id.ToString();
            textBoxTulajdonosneve.Text = kivalasztottszamla.Tulajdonosneve.ToString();
            textBoxEgyenleg.Text = kivalasztottszamla.Egyenleg.ToString();
            dateTimePicker1.Value = kivalasztottszamla.Nyitasdatum;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (listBox.SelectedIndex < 0)
            {
                return;
            }

            cmd.CommandText = "DELETE FROM `szamlak` WHERE `id`=@id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", textBoxId.Text);
            conn.Open();

            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Sikeres torles!");
            }
            conn.Close();
            lista_update();
        }
    }
}
