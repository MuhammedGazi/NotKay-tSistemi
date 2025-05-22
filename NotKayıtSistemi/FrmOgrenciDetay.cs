using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace NotKayıtSistemi
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        public string numara;
        //Data Source = DESKTOP - GLCB4AC; Initial Catalog = DBNOTKAYIT; Integrated Security = True; Trust Server Certificate=True
        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GLCB4AC;Initial Catalog=DBNOTKAYIT;Integrated Security=True;");
            lblNumara2.Text = numara;

            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from TBLDERS where OGRNUMARA=@p1", conn);
            cmd.Parameters.AddWithValue("@p1",numara);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[2].ToString()+" "+dr[3].ToString();
                lblsınav1.Text = dr[4].ToString();
                lblsınav2.Text = dr[5].ToString();
                lblsınav3.Text = dr[6].ToString();
                lblortalama2.Text = dr[7].ToString();
                lbldurum2.Text = dr[8].ToString();
            }
            conn.Close();
        }
    }
}
