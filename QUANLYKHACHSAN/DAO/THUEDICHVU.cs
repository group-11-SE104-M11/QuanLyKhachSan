using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYKHACHSAN.DAO
{
    public class THUEDICHVU
    {
        public THUEDICHVU() { }
        public int ID { get; set; }
        public int IDDichVu { get; set; }
        public int IDThuePhong { get; set; }
        public decimal SoLuong { get; set; }

        public int LapPhieu()
        {
            string query = "insert into BANGTHUEDICHVU values('" + this.IDDichVu + "','" + this.IDThuePhong + "','" + this.SoLuong + "')";
            int kq = DataProvider.Instance.ExecuteNonQuery(query);
            return kq;
        }

        public int SuaPhieuTDV()
        {
            string cmd = "update BANGTHUEDICHVU set SOLUONG = " + this.SoLuong + " where ID = " + this.ID;
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }

        public int XoaPhieuTDV()
        {
            string cmd = "delete from BANGTHUEDICHVU" + " where ID = " + this.ID;
            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            return kq;
        }

    }
}
