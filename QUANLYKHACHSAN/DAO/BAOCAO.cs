using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUANLYKHACHSAN.DAO
{
    public class BAOCAO
    {
        public BAOCAO() { }
        public int ID { get; set; }
        public string TenBaoCao { get; set; }
        public int ThangBaoCao { get; set; }
        public int NamBaoCao { get; set; }
        public int IDNhanVien { get; set; }
        public DateTime NgayLapBC { get; set; }
        public string DoanhThu { get; set; }

        public int ThemBaoCao()
        {
            string cmd_TaoBC = "INSERT INTO DBO.BAOCAO values ('" + this.TenBaoCao + "','" + this.ThangBaoCao + "','" + this.NamBaoCao + "','" + this.IDNhanVien + "','" + this.NgayLapBC + "','" + this.DoanhThu + "')";

            int KQ = DataProvider.Instance.ExecuteNonQuery(cmd_TaoBC);
            return KQ;
        }

    }
}