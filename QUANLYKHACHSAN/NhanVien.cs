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
        public int ID;
        public string HOTEN { get; set; }
        public string SDT { get; set; }

        public string CMND { get; set; }

        public string GIOITINH { get; set; }


    }
    public class NGUOIDUNG
    {
        public int IDNHANVIEN;
        public string TAIKHOAN;
        public string MATKHAU;
    }
    #endregion
}
