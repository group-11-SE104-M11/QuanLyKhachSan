using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYKHACHSAN.DAO
{
    public class LOAIPHONG
    {
        public LOAIPHONG() { }
        public int ID { get; set; }
        public string TenLoaiPhong { get; set; }
        public decimal Gia { get; set; }
        public int SoNguoiToiDa { get; set; }
        public int SoGiuong { get; set; }
        public decimal PhuThu { get; set; }

        public int ThemLoaiPhong()
        {
            string cmd = "insert into LOAIPHONG values ('" + this.TenLoaiPhong + "','" + this.Gia + "','" + this.SoNguoiToiDa + "','" + this.SoGiuong + "','" + this.PhuThu + "')";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }

        public int SuaLoaiPhong()
        {
            string cmd = "update LOAIPHONG set TENLOAIPHONG = N'" + this.TenLoaiPhong + "', GIA = '" + this.Gia + "', SONGUOITOIDA = '" + this.SoNguoiToiDa + "', SOGIUONG = '" + this.SoGiuong + "', PHUTHU = '" + this.PhuThu + "' where ID = '" + this.ID + "'";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }

        public int XoaLoaiPhong()
        {
            string cmd = "delete from LOAIPHONG where TENLOAIPHONG='" + this.TenLoaiPhong + "'";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }


    }
}
