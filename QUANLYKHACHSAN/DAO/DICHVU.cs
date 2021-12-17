using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYKHACHSAN.DAO
{
    public class DICHVU
    {
        public DICHVU() { }
        public int ID { get; set; }
        public string TenDichVu { get; set; }
        public decimal Gia { get; set; }
        public string ThoiGianPhucVu { get; set; }

        public void TimDichVu(string tendvcheck)
        {

            string query = "select ID from DICHVU where (TENDICHVU=N'" + tendvcheck.Trim() + "')";
            string id = DataProvider.Instance.ExecuteScalar(query).ToString();

            query = "update DICHVU set TENDICHVU = N'" + this.TenDichVu + "'" +
            ",GIA='" + this.Gia + "',THOIGIANPHUCVU='" + this.ThoiGianPhucVu + "'WHERE ID = '" + id + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
            return;
        }

        public int ThemDichVu()
        {
            string query = "SELECT COUNT(*) FROM DICHVU WHERE DICHVU.TENDICHVU = N'" + this.TenDichVu + "'";
            int k = int.Parse(DataProvider.Instance.ExecuteScalar(query).ToString());
            if (k == 0)
            {
                query = "INSERT[dbo].[DICHVU]([TENDICHVU], [GIA], [THOIGIANPHUCVU]) VALUES(N'" + this.TenDichVu + "'," + this.Gia + ", N'" + this.ThoiGianPhucVu + "')";
                DataProvider.Instance.ExecuteQuery(query);
                return 1;
            }
            return 0;
        }

        public void SuaDichVu(string tendvcheck)
        {

            string query = "select ID from DICHVU where (TENDICHVU=N'" + tendvcheck.Trim() + "')";
            string id = DataProvider.Instance.ExecuteScalar(query).ToString();

            query = "update DICHVU set TENDICHVU = N'" + this.TenDichVu + "'" +
            ",GIA='" + this.Gia + "',THOIGIANPHUCVU='" + this.ThoiGianPhucVu + "'WHERE ID = '" + id + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
            return;
        }

        public void XoaDichVu(string tendv)
        {

            string query = "select ID from DICHVU where (TENDICHVU=N'" + tendv + "')";
            string id = DataProvider.Instance.ExecuteScalar(query).ToString();

            query = "DELETE FROM DICHVU WHERE ID = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
            return;
        }


    }
}
