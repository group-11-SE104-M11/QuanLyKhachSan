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

namespace QUANLYKHACHSAN
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void btn_dang_nhap_Click(object sender, EventArgs e)
        {
            NHANVIEN nhanvien = new NHANVIEN();

            NGUOIDUNG login_info = new NGUOIDUNG();

            login_info.TAIKHOAN = txb_taikhoan.Text.Trim();
            login_info.MATKHAU = txb_matkhau.Text.Trim();

            if (login_info.TAIKHOAN == "" || login_info.MATKHAU == "")
            {
                MessageBox.Show("Vui lòng NHẬP đầy đủ thông tin!");
                return;
            }


            NGUOIDUNG login_user = getinfouser(login_info.TAIKHOAN, login_info.MATKHAU);
            if (login_user == null)
            {
                MessageBox.Show("Nhập sai tài khoản hoặc mật khẩu");
                return;
            }
            else
            {
                nhanvien = getinfoNHANVIEN(login_user.IDNHANVIEN);
            }




            this.Hide();
            Form1 f1 = new Form1(nhanvien);
            f1.ShowDialog();

        }

        private NGUOIDUNG getinfouser(string TAIKHOAN, string MATKHAU)
        {
            NGUOIDUNG login_user = new NGUOIDUNG();

            string query = "Select IDNHANVIEN, TAIKHOAN, MATKHAU from NGUOIDUNG where TAIKHOAN = '" + TAIKHOAN + "' and MATKHAU = '" + MATKHAU + "'";
            DataTable dt = DAO.DataProvider.Instance.ExecuteQuery(query);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                login_user.IDNHANVIEN = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                login_user.TAIKHOAN = TAIKHOAN;
                login_user.MATKHAU = MATKHAU;
                return login_user;
            }    
            return null;
        }
        private NHANVIEN getinfoNHANVIEN(int ID)
        {
            NHANVIEN login_user = new NHANVIEN();
            

            string query = "Select HOTEN, SDT, CMND, GIOITINH from NHANVIEN where ID = '" + ID + "'";
            DataTable dt = DAO.DataProvider.Instance.ExecuteQuery(query);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                login_user.HOTEN = dt.Rows[i].ItemArray[0].ToString();
                login_user.SDT = dt.Rows[i].ItemArray[1].ToString();
                login_user.CMND = dt.Rows[i].ItemArray[2].ToString();
                login_user.GIOITINH = dt.Rows[i].ItemArray[3].ToString();
                login_user.ID = ID;
                return login_user;
            }
            return null;
        }
    }
}
