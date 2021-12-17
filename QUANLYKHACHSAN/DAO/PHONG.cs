using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYKHACHSAN.DAO
{
    public class PHONG
    {
        public PHONG() { }
        public int ID { get; set; }
        public string TenPhong { get; set; }
        public int IDLoaiPhong { get; set; }
        public int IDTinhTrangPhong { get; set; }

        public int ThemPhong()
        {
            string cmd = "insert into PHONG values ('" + this.TenPhong + "','" + this.IDLoaiPhong + "','" + this.IDTinhTrangPhong + "')";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }

        public int SuaPhong()
        {
            string cmd = "update PHONG set TENPHONG = N'" + this.TenPhong + "', IDLOAIPHONG = '" + this.IDLoaiPhong + "', IDTINHTRANG = '" + this.IDTinhTrangPhong + "' where ID = '" + this.ID + "'";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }

        public int XoaPhong()
        {
            string cmd = "delete from PHONG where TENPHONG = N'" + this.TenPhong + "'";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }
    }
}
