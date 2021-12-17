using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYKHACHSAN.DAO
{
    public class KHACHHANG
    {
        public KHACHHANG() { }
        public int ID { get; set; }
        public string TenKH { get; set; }
        public string CMND { get; set; }
        public string SDT { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        public int ThemKH ()
        {
            string query = "insert into KHACHHANG values(N'" + this.TenKH + "','" + this.CMND + "','" + this.SDT + "','" + this.NgaySinh + "',N'" + this.GioiTinh + "')";
            int kq = DataProvider.Instance.ExecuteNonQuery(query);
            return kq;
        }
        public int SuaKH()
        {
            string query = "update KHACHHANG set HOTEN = N'" + this.TenKH + "'" +
            ",CMND='" + this.CMND + "',SDT='" + this.SDT + "',NGAYSINH='" + this.NgaySinh.ToString() +
            "',GIOITINH=N'" + this.GioiTinh + "'WHERE ID = '" + this.ID + "'";
            int kq = DataProvider.Instance.ExecuteNonQuery(query);
            return kq;
        }
    }
}
