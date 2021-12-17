using QUANLYKHACHSAN.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QUANLYKHACHSAN
{
    #region Minh
    public class NHANVIEN
    {
        public NHANVIEN() { }
        public int ID { get; set; }
        public string HOTEN { get; set; }
        public string GIOITINH { get; set; }
        public string SDT { get; set; }
        public string CMND { get; set; }
        public string DIACHI { get; set; }
        public decimal LUONGCB { get; set; }
        public int IDCHUCVU { get; set; }


        public int ThemNhanVien()
        {
            string query = "INSERT[dbo].[NHANVIEN]([HOTEN], [GIOITINH], [SDT], [CMND], [DIACHI], [LUONGCOBAN], " +
                "[IDCHUCVU]) VALUES(N'" + this.HOTEN + "', N'" + this.GIOITINH + "', N'" + this.SDT + "', N'" + this.CMND + "', N'" + this.DIACHI + "', " + this.LUONGCB + "," + this.IDCHUCVU + ")";
            int kq = DataProvider.Instance.ExecuteNonQuery(query);
            return kq;
        }

        public void SuaNhanVien()
        {
            string query = "update NHANVIEN set HOTEN = N'" + this.HOTEN + "',GIOITINH = N'" + this.GIOITINH + "',SDT = N'" + this.SDT + "', CMND = N'" + this.CMND + "',DIACHI = N'" + this.DIACHI + "',LUONGCOBAN = " + this.LUONGCB + ",IDCHUCVU = " + this.IDCHUCVU + "WHERE ID =" + this.ID;
            int kq = DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void XoaNhanVien()
        {
            string query1 = "DELETE FROM NGUOIDUNG WHERE NGUOIDUNG.IDNHANVIEN =" + this.ID;
            string query2 = "DELETE FROM NHANVIEN WHERE ID = " + this.ID;
            DataProvider.Instance.ExecuteNonQuery(query1);
            DataProvider.Instance.ExecuteNonQuery(query2);
        }
    

    }
    public class NGUOIDUNG
    {
        public int IDNHANVIEN;
        public string TAIKHOAN;
        public string MATKHAU;
    }
    #endregion
}