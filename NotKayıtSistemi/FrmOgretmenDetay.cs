using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotKayıtSistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GLCB4AC;Initial Catalog=DBNOTKAYIT;Integrated Security=True;");
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBNOTKAYITDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dBNOTKAYITDataSet.TBLDERS);
        
        }

        private void btnOgrencikaydet_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLDERS(OGRAD,OGSOYAD,OGRNUMARA) values(@p1,@p2,@p3)",conn);
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskNumara.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("öğrenci kaydedildi");
            this.tBLDERSTableAdapter.Fill(this.dBNOTKAYITDataSet.TBLDERS);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int secilen=dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtSınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            
            
        }

        private void btnOgrencıGuncelle_Click(object sender, EventArgs e)
        {
            double ortalama,s1,s2,s3;
            string durum;
            s1 = Convert.ToDouble(txtSınav1.Text);
            s2=Convert.ToDouble(txtSınav2.Text);
            s3=Convert.ToDouble(txtSınav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblortalama2.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum="True";
            }
            else
            {
                durum = "False";
            }

            conn.Open();
            SqlCommand cmd = new SqlCommand("update TBLDERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", conn);
            cmd.Parameters.AddWithValue("@P1", txtSınav1.Text);
            cmd.Parameters.AddWithValue("@P2", txtSınav2.Text);
            cmd.Parameters.AddWithValue("@P3", txtSınav3.Text);
            cmd.Parameters.AddWithValue("@P4", decimal.Parse(lblortalama2.Text));
            cmd.Parameters.AddWithValue("@P5", durum);
            cmd.Parameters.AddWithValue("@P6", mskNumara.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            this.tBLDERSTableAdapter.Fill(this.dBNOTKAYITDataSet.TBLDERS);

            conn.Open();
            SqlCommand cmd2 = new SqlCommand("select count(Durum) from TBLDERS where DURUM=1",conn);
            int gecenSay = Convert.ToInt32(cmd2.ExecuteScalar());
            SqlCommand cmd3 = new SqlCommand("select count(*) from TBLDERS",conn);
            int tumOgr = Convert.ToInt32(cmd3.ExecuteScalar());
            lblgecensayisi2.Text = gecenSay.ToString();
            lblkalansayisi2.Text = (tumOgr - gecenSay).ToString();
            conn.Close();

        }
    }
}
