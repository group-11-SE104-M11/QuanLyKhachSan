using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUANLYKHACHSAN.DAO
{
    public class HOADON
    {
        public HOADON() { }
        public int ID { get; set; }
        public int IDKhachHang { get; set; }
        public int IDThuePhong { get; set; }
        public int IDNhanVien { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayLapHD { get; set; }

        public int TaoHoaDon()
        {

            string cmd_TaoHD = "INSERT INTO DBO.HOADON values ('" + this.IDKhachHang + "','" + this.IDThuePhong + "','" + this.IDNhanVien + "','" + this.TongTien + "','" + this.NgayLapHD + "')";
            int KQ = DataProvider.Instance.ExecuteNonQuery(cmd_TaoHD);

            return KQ;
        }

    }
}