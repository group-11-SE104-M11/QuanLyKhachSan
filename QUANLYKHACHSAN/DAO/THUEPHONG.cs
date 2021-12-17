using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYKHACHSAN.DAO
{
    public class THUEPHONG
    {
        public THUEPHONG() { }
        public int ID { get; set; }
        public int IDPhong { get; set; }
        public int IDKhachHang { get; set; }
        public decimal SoNguoi { get; set; }
        public DateTime NgayDatPhong { get; set; }
        public DateTime NgayCheckIn { get; set; }
        public DateTime NgayCheckOut { get; set; }
        public int IDTrangThai { get; set; }
        public int IDTinhTrangPhong { get; set; }

        public int LapPhieuThuePhong()
        {
            string cmd = "insert into BANGTHUEPHONG values('" + this.IDPhong + "','" + this.IDKhachHang + "','" + this.SoNguoi + "','" + this.NgayDatPhong +
                 "','" + this.NgayCheckIn + "','" + this.NgayCheckOut + "','" + this.IDTrangThai + "')";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);

            cmd = "update PHONG set IDTINHTRANG='" + this.IDTinhTrangPhong + "' WHERE ID ='" + this.IDPhong + "'";
            kq = DataProvider.Instance.ExecuteNonQuery(cmd);

            return kq;
        }

        public int SuaPhieuThuePhong()
        {
            string cmd = "update BANGTHUEPHONG set SoNguoi='" + this.SoNguoi + "',NgayDatPhong='" + this.NgayDatPhong.ToString() + "',NgayCheckin='" + this.NgayCheckIn.ToString() + "'" +
               ",NgayCheckout = '" + this.NgayCheckOut.ToString() + "',IDTrangThai = '" + this.IDTrangThai + "' WHERE ID ='" + this.ID + "'";
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);

            cmd = "update PHONG set IDTINHTRANG='" + this.IDTinhTrangPhong + "' WHERE ID ='" + this.IDPhong + "'";
            kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }

    }
}
