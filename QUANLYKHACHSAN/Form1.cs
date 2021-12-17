using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QUANLYKHACHSAN.DAO;

namespace QUANLYKHACHSAN
{
    public partial class Form1 : Form
    {

        string tendvcheck = "";
        int STT = 0;

        // Biến toàn cục
        string IDNhanVien_SignedIn = "";
        string TenNhanVien_SignedIn = "";

        // Menu toolscipts
        #region Menu
        private void btn_khachhang_Click(object sender, EventArgs e)
        {
            phong_control.Visible = false;
            khachhang_control.Visible = true;
            nhanvien_control.Visible = false;
            dichvu_control.Visible = false;
            hoadon_control.Visible = false;
            baocao_control.Visible = false;
            DoiMatKhau_control.Visible = false;
        }

        private void btn_phong_Click(object sender, EventArgs e)
        {
            QLP_Load_Phong(sender, e);
            phong_control.Visible = true;
            khachhang_control.Visible = false;
            nhanvien_control.Visible = false;
            dichvu_control.Visible = false;
            hoadon_control.Visible = false;
            baocao_control.Visible = false;
            DoiMatKhau_control.Visible = false;
        }
        //Tab control 
        private void btn_nhanvien_Click(object sender, EventArgs e)
        {
            nhanvien_control.Visible = true;
            phong_control.Visible = false;
            khachhang_control.Visible = false;
            dichvu_control.Visible = false;
            hoadon_control.Visible = false;
            baocao_control.Visible = false;

            btn_NV_suaNV.Enabled = false;
            btn_NV_xoaNV.Enabled = false;

            DoiMatKhau_control.Visible = false;
            btn_NV_RefreshNV_Click(sender, e);
        }
        private void btn_dichvu_Click(object sender, EventArgs e)
        {
            dichvu_control.Visible = true;
            nhanvien_control.Visible = false;
            phong_control.Visible = false;
            khachhang_control.Visible = false;
            hoadon_control.Visible = false;
            baocao_control.Visible = false;
            DoiMatKhau_control.Visible = false;

            btn_DV_SuaDV.Enabled = false;
            btn_DV_XoaDV.Enabled = false;
            LoadThuePhong(list_TDV_TP);
            LoadDichVu(list_TDV_DV);
            LoadThueDV(list_ThueDV_DST);
            DV_loadDichVu();
        }
        private void btn_hoadon_Click(object sender, EventArgs e)
        {
            HD_List_HD();
            hoadon_control.Visible = true;
            dichvu_control.Visible = false;
            nhanvien_control.Visible = false;
            phong_control.Visible = false;
            khachhang_control.Visible = false;
            baocao_control.Visible = false;
            DoiMatKhau_control.Visible = false;

        }
        private void btn_baocao_click(object sender, EventArgs e)
        {
            BC_Load_BaoCao();
            baocao_control.Visible = true;
            hoadon_control.Visible = false;
            dichvu_control.Visible = false;
            nhanvien_control.Visible = false;
            phong_control.Visible = false;
            khachhang_control.Visible = false;
            DoiMatKhau_control.Visible = false;
        }
        private void dangxuat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn đăng xuất", "Cảnh báo", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                this.Hide();
                Login f1 = new Login();
                f1.ShowDialog();
            }
        }

        private void DoiMatKhau_Click(object sender, EventArgs e)
        {
            DoiMatKhau_control.Visible = true;
            baocao_control.Visible = false;
            hoadon_control.Visible = false;
            dichvu_control.Visible = false;
            nhanvien_control.Visible = false;
            phong_control.Visible = false;
            khachhang_control.Visible = false;
        }
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        void PhanQuyen(object sender, EventArgs e)
        {
            string query = "SELECT IDCHUCVU FROM NHANVIEN WHERE ID = " + IDNhanVien_SignedIn;
            //DataTable getIDChucVu = DataProvider.Instance.ExecuteQuery(query);
            //string IDChucVu = getIDChucVu.Rows[0].ItemArray[0].ToString().Trim();
            string IDChucVu = DataProvider.Instance.ExecuteScalar(query).ToString();

            string query1 = "SELECT TENCHUCVU FROM CHUCVU WHERE ID = " + IDChucVu;
            //DataTable getChucVu = excuteQuery(query1);
            //string ChucVu = getChucVu.Rows[0].ItemArray[0].ToString().Trim();
            string ChucVu = DataProvider.Instance.ExecuteScalar(query1).ToString();

            switch (ChucVu)
            {
                case "Quản lý":
                    //
                    break;
                case "Lễ tân":
                    btn_QLP_Them.Visible = false;
                    btn_QLP_Xoa.Visible = false;
                    btn_QLP_Sua.Visible = false;
                    btnThem.Visible = false;
                    btnSua.Visible = false;
                    btnXoa.Visible = false;

                    btn_nhanvien.Visible = false;

                    btn_DV_ThemDichVu.Visible = false;
                    btn_DV_SuaDV.Visible = false;
                    btn_DV_XoaDV.Visible = false;

                    btn_baocao.Visible = false;
                    break;
                case "Kế toán":
                    btn_baocao_click(sender, e);

                    btn_phong.Visible = false;
                    btn_khachhang.Visible = false;
                    btn_nhanvien.Visible = false;
                    btn_dichvu.Visible = false;
                    btn_hoadon.Visible = false;
                    break;
                case "Nhân viên dịch vụ":
                    btn_dichvu_Click(sender, e);

                    btn_phong.Visible = false;
                    btn_khachhang.Visible = false;
                    btn_nhanvien.Visible = false;
                    btn_hoadon.Visible = false;
                    btn_baocao.Visible = false;

                    btn_DV_ThemDichVu.Visible = false;
                    btn_DV_XoaDV.Visible = false;
                    btn_DV_SuaDV.Visible = false;
                    btn_Sua_DST.Visible = false;
                    btn_Xoa_DST.Visible = false;
                    btn_TDV_LapPhieu.Visible = false;
                    break;
            };
        }
        #endregion

        #region KhachHang
        private void listView_KhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_KhachHang.SelectedItems.Count == 0) return;

            // Lay phan tu duoc chon tren listview
            ListViewItem lvi = listView_KhachHang.SelectedItems[0];


            // Hien thi thong tin tu listview sang cac textbox
            tb_KH_HoTen.Text = lvi.SubItems[1].Text;
            tb_KH_CMND.Text = lvi.SubItems[2].Text;
            tb_KH_SDT.Text = lvi.SubItems[3].Text;
            date_KH_NgaySinh.Text = lvi.SubItems[4].Text;
            string gender = lvi.SubItems[5].Text;
            if (gender == "Nữ")
            {
                rbtn_KH_GTNu.Checked = true;
            }
            else
            {
                rbtn_KH_GTNam.Checked = true;
            }
        }

        private void btn_KH_TimKiem_Click(object sender, EventArgs e)
        {
            string cmd_tk = "select * from KHACHHANG where ID > 0";
            if (tb_KH_TimHoTen.Text.Trim() != "")
                cmd_tk += " and HOTEN=N'" + tb_KH_TimHoTen.Text.Trim() + "'";
            if (tb_KH_TimCMND.Text.Trim() != "")
                cmd_tk += " and CMND='" + tb_KH_TimCMND.Text.Trim() + "'";
            if (tb_KH_TimSDT.Text.Trim() != "")
                cmd_tk += " and SDT='" + tb_KH_TimSDT.Text.Trim() + "'";
            QLKH_Load_ThongTinKH(cmd_tk);
        }

        //Tu Them
        private void btn_KH_Them_Click(object sender, EventArgs e)
        {
            KHACHHANG kh = new KHACHHANG();

            if (QLKH_CheckThongTin() == false)
                return;

            kh.TenKH = tb_KH_HoTen.Text.Trim();
            kh.CMND = tb_KH_CMND.Text.Trim();
            kh.SDT = tb_KH_SDT.Text.Trim();
            kh.NgaySinh = date_KH_NgaySinh.Value;
            if (rbtn_KH_GTNam.Checked == true)
            {
                kh.GioiTinh = "Nam";
            }
            else
            {
                kh.GioiTinh = "Nữ";
            }

            int kq = 0;
            if (DV_Xacnhan("Bạn có đồng ý thêm khách hàng?") == true)
            {
                kq = kh.ThemKH();
            }
            else
            {
                return;
            }

            cmd = "select * from KHACHHANG";
            if (kq > 0)
            {
                MessageBox.Show("Thêm khách hàng thành công");
                QLKH_Load_ThongTinKH(cmd);
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại");
                QLKH_Load_ThongTinKH(cmd);
            }
        }

        //Tu sua kh
        private void btn_KH_Sua_Click(object sender, EventArgs e)
        {
            KHACHHANG kh = new KHACHHANG();

            if (QLKH_CheckThongTin() == false)
                return;

            if (rbtn_KH_GTNam.Checked == true)
            {
                kh.GioiTinh = "Nam";
            }
            else
            {
                kh.GioiTinh = "Nữ";
            }

            kh.ID = Convert.ToInt32(listView_KhachHang.SelectedItems[0].SubItems[0].Text.Trim());
            kh.TenKH = tb_KH_HoTen.Text.Trim();
            kh.CMND = tb_KH_CMND.Text.Trim();
            kh.SDT = tb_KH_SDT.Text.Trim();
            kh.NgaySinh = date_KH_NgaySinh.Value;

            int kq = 0;
            if (DV_Xacnhan("Bạn có đồng ý sửa thông tin khách hàng?") == true)
            {
                kq = kh.SuaKH();
            }
            else
            {
                return;
            }

            cmd = "select * from KHACHHANG";
            if (kq > 0)
            {
                MessageBox.Show("Chỉnh sửa thông tin thành công!");

                QLKH_Load_ThongTinKH(cmd);
                clear_data();
            }
            else
            {
                MessageBox.Show("Chỉnh sửa thông tin thất bại!");
                QLKH_Load_ThongTinKH(cmd);
                clear_data();
            }
        }

        //Kiem tra nhap thong tin du chua
        bool QLKH_CheckThongTin()
        {
            if (rbtn_KH_GTNam.Checked != true && rbtn_KH_GTNu.Checked != true || tb_KH_HoTen.Text.Trim() == "" || tb_KH_CMND.Text.Trim() == "" || tb_KH_SDT.Text.Trim() == "")
            {
                MessageBox.Show("Thiếu thông tin");
                return false;
            }
            return true;
        }

        #endregion

        #region Phong

        string cmd = "select * from KHACHHANG";
        private void Form1_Load(object sender, EventArgs e)
        {
            PhanQuyen(sender, e);

            btn_phong.CheckState = CheckState.Checked;
            // Tu load listview
            QLKH_Load_ThongTinKH(cmd);
            HienThiThongTin_KH_thue(cmd);

            cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG and TEN = N'Còn trống'";
            HienThiPhong(cmd);

            cmd = "select BTP.ID,P.TENPHONG,KH.HOTEN,KH.CMND,BTP.SoNguoi,BTP.NgayDatPhong,BTP.NgayCheckin,BTP.NgayCheckout,TT.TENTRANGTHAI " +
                "from BANGTHUEPHONG as BTP,PHONG as P,TRANGTHAITHUEPHONG as TT,KHACHHANG as KH " +
                "where BTP.IDPhong = P.ID and BTP.IDKhachHang = KH.ID and BTP.IDTrangThai = TT.ID";
            HienThiThongTin_TraCuu_Thue(cmd);

            cmd = "select TENLOAIPHONG,GIA,SONGUOITOIDA,SOGIUONG,PHUTHU from LOAIPHONG";
            HienThiLoaiPhong(cmd);

            //quoc_them_phan_nay
            cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG";
            HienThiQuanLiPhong(cmd);


        }

        void QLP_Load_Phong(object sender, EventArgs e)
        {
            cmd = "select * from KHACHHANG";
            HienThiThongTin_KH_thue(cmd);

            cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG and TEN = N'Còn trống'";
            HienThiPhong(cmd);

            cmd = "select BTP.ID,P.TENPHONG,KH.HOTEN,KH.CMND,BTP.SoNguoi,BTP.NgayDatPhong,BTP.NgayCheckin,BTP.NgayCheckout,TT.TENTRANGTHAI " +
                "from BANGTHUEPHONG as BTP,PHONG as P,TRANGTHAITHUEPHONG as TT,KHACHHANG as KH " +
                "where BTP.IDPhong = P.ID and BTP.IDKhachHang = KH.ID and BTP.IDTrangThai = TT.ID";
            HienThiThongTin_TraCuu_Thue(cmd);

            cmd = "select TENLOAIPHONG,GIA,SONGUOITOIDA,SOGIUONG,PHUTHU from LOAIPHONG";
            HienThiLoaiPhong(cmd);

            //quoc_them_phan_nay
            cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG";
            HienThiQuanLiPhong(cmd);
        }

        private void HienThiThongTin_TraCuu_Thue(string cmd)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);
            list_TraCuu_Thue.Items.Clear();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list_TraCuu_Thue.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                list_TraCuu_Thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
            }
        }

        private void HienThiPhong(string cmd)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);
            list_Phong.Items.Clear();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list_Phong.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                list_Phong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                list_Phong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                list_Phong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                list_Phong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                list_Phong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                list_Phong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
            }

        }
        private void HienThiLoaiPhong(string cmd)
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);
            list_LoaiPhong.Items.Clear();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list_LoaiPhong.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                list_LoaiPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                list_LoaiPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                list_LoaiPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                list_LoaiPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
            }
        }

        private void HienThiThongTin_KH_thue(string cmd = "select * from KHACHHANG")
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);
            list_KH_thue.Items.Clear();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list_KH_thue.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                list_KH_thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                list_KH_thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                list_KH_thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                list_KH_thue.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
            }
        }

        private void QLKH_Load_ThongTinKH(string cmd)
        {

            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);

            listView_KhachHang.Items.Clear();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                listView_KhachHang.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                listView_KhachHang.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                listView_KhachHang.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                listView_KhachHang.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                listView_KhachHang.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                listView_KhachHang.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
            }

        }

        //clear
        private void clear_data()
        {
            tb_KH_HoTen.Clear();
            tb_KH_CMND.Clear();
            tb_KH_SDT.Clear();
            date_KH_NgaySinh.ResetText();
            rbtn_KH_GTNam.Checked = false;
            rbtn_KH_GTNu.Checked = false;
        }


        int ID_ten_phong = -1;
        private void list_Phong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_Phong.SelectedItems.Count == 0) return;
            // Lay phan tu duoc chon tren listview
            ListViewItem lvi = list_Phong.SelectedItems[0];
            // Hien thi thong tin tu listview sang cac textbox
            ten_phong_thue.Text = lvi.SubItems[0].Text;

            string cmnd_ten_phong = "select P.ID " +
                "from dbo.LOAIPHONG as L,PHONG as P " +
                "where L.ID = P.IDLOAIPHONG and TENPHONG = '" + lvi.SubItems[0].Text + "'";


            ID_ten_phong = int.Parse(DataProvider.Instance.ExecuteScalar(cmnd_ten_phong).ToString());
        }

        int ID_KH = -10000;
        private void list_KH_thue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_KH_thue.SelectedItems.Count == 0) return;
            // Lay phan tu duoc chon tren listview
            ListViewItem lvi = list_KH_thue.SelectedItems[0];
            // Hien thi thong tin tu listview sang cac textbox
            ten_KH_thue.Text = lvi.SubItems[0].Text;
            cmnd_thue.Text = lvi.SubItems[1].Text;
            // Console.WriteLine(lvi.SubItems[1].Text);

            string cmd_kh = "select ID from KHACHHANG where CMND = '" + lvi.SubItems[1].Text + "'";

            ID_KH = int.Parse(DataProvider.Instance.ExecuteScalar(cmd_kh).ToString());

        }

        private void tim_KH_thue_TextChanged(object sender, EventArgs e)
        {
            string cmnd = tim_KH_thue.Text.Trim();
            string cmd_tk = "select * from dbo.KHACHHANG where CMND like '" + cmnd + "%'";
            HienThiThongTin_KH_thue(cmd_tk);
        }

        private void btn_dk_thue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng kí?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (ten_phong_thue.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn phòng thuê!!!", "Thông báo lỗi");
                return;
            }

            if (ten_KH_thue.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng thuê!!!", "Thông báo lỗi");
                return;
            }
            if (so_nguoi_thue.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số người thuê phải lớn hơn 0!!!", "Thông báo lỗi");
                return;
            }

            if (check_in.Value.Date > check_out.Value.Date)
            {
                MessageBox.Show("Ngày check in không thể sau ngày check out!!!", "Thông báo lỗi");
                return;

            }
            if (check_in.Value.Date < dat_phong.Value.Date)
            {
                MessageBox.Show("Ngày đặt phòng không thể sau ngày check in!!!", "Thông báo lỗi");
                return;
            }
            if (tt_phong.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn trạng thái thuê phòng!!!", "Thông báo lỗi");
                return;
            }
            if (tt_phong.Text.Trim() == "Đã check out" || tt_phong.Text.Trim() == "Đã hủy")
            {
                MessageBox.Show("Chọn sai trạng thái thuê phòng!!!", "Thông báo lỗi");
                return;
            }

            string tt_phong_thue = tt_phong.SelectedItem.ToString().Trim();
            string cmd_tt_thue_phong = "select ID " +
                "from TRANGTHAITHUEPHONG where TENTRANGTHAI = N'" + tt_phong_thue + "'";
            int ID_TT_Phong = int.Parse(DataProvider.Instance.ExecuteScalar(cmd_tt_thue_phong).ToString());

            THUEPHONG thuephong = new THUEPHONG();

            //xu li luu data 
            thuephong.IDPhong = ID_ten_phong;
            thuephong.IDKhachHang = ID_KH;
            thuephong.SoNguoi = so_nguoi_thue.Value;
            thuephong.NgayDatPhong = dat_phong.Value;
            thuephong.NgayCheckIn = check_in.Value;
            thuephong.NgayCheckOut = check_out.Value;
            thuephong.IDTrangThai = ID_TT_Phong;

            string temp_TT = "";
            if (tt_phong.Text.Trim() == "Đã check in")
            {
                temp_TT = "Đã thuê";
            }
            else if (tt_phong.Text.Trim() == "Chờ check in")
            {
                temp_TT = "Đã đặt";
            }
            else
            {
                temp_TT = "Còn trống";
            }

            cmd = "select T.ID from TINHTRANGPHONG as T where T.TEN =N'" + temp_TT + "'";

            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);
            thuephong.IDTinhTrangPhong = int.Parse(dt.Rows[0].ItemArray[0].ToString());


            int kq = thuephong.LapPhieuThuePhong();
            if (kq > 0)
            {
                MessageBox.Show("Đăng ký thành công");
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại");
            }

            //  list_TraCuu_Thue.Clear();
            cmd = "select BTP.ID, P.TENPHONG,KH.HOTEN,KH.CMND,BTP.SoNguoi,BTP.NgayDatPhong,BTP.NgayCheckin,BTP.NgayCheckout,TT.TENTRANGTHAI " +
                "from BANGTHUEPHONG as BTP,PHONG as P,TRANGTHAITHUEPHONG as TT,KHACHHANG as KH " +
                "where BTP.IDPhong = P.ID and BTP.IDKhachHang = KH.ID and BTP.IDTrangThai = TT.ID";
            HienThiThongTin_TraCuu_Thue(cmd);

            cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                  "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG and TEN = N'Còn trống'";
            HienThiPhong(cmd);


        }

        string temp_ID = "";
        private void list_TraCuu_Thue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_TraCuu_Thue.SelectedItems.Count == 0) return;

            // Lay phan tu duoc chon tren listview
            ListViewItem lvi = list_TraCuu_Thue.SelectedItems[0];


            // Hien thi thong tin tu listview sang cac textbox
            temp_ID = lvi.SubItems[0].Text;
            ten_phong_thue.Text = lvi.SubItems[1].Text;
            ten_KH_thue.Text = lvi.SubItems[2].Text;
            cmnd_thue.Text = lvi.SubItems[3].Text;
            so_nguoi_thue.Value = Convert.ToInt32(lvi.SubItems[4].Text);
            dat_phong.Value = Convert.ToDateTime(lvi.SubItems[5].Text);
            check_in.Value = Convert.ToDateTime(lvi.SubItems[6].Text);
            check_out.Value = Convert.ToDateTime(lvi.SubItems[7].Text);

            if (lvi.SubItems[8].Text == "Đã check in")
            {
                tt_phong.Text = tt_phong.Items[0].ToString();
            }
            else if (lvi.SubItems[8].Text == "Đã check out")
            {
                tt_phong.Text = tt_phong.Items[1].ToString();
            }
            else if (lvi.SubItems[8].Text == "Đã hủy")
            {
                tt_phong.Text = tt_phong.Items[2].ToString();
            }
            else
            {
                tt_phong.Text = tt_phong.Items[3].ToString();
            }
        }

        private void txt_TraCuu_Phong_TextChanged(object sender, EventArgs e)
        {

            string phong = txt_TraCuu_Phong.Text.Trim();
            string cmd_tk = "select BTP.ID,P.TENPHONG,KH.HOTEN,KH.CMND,BTP.SoNguoi,BTP.NgayDatPhong,BTP.NgayCheckin,BTP.NgayCheckout,TT.TENTRANGTHAI " +
                "from BANGTHUEPHONG as BTP,PHONG as P,TRANGTHAITHUEPHONG as TT,KHACHHANG as KH " +
                "where BTP.IDPhong = P.ID and BTP.IDKhachHang = KH.ID and BTP.IDTrangThai = TT.ID and P.TENPHONG like '" + phong + "%'";
            HienThiThongTin_TraCuu_Thue(cmd_tk);
        }

        private void txt_TraCuu_CMND_TextChanged(object sender, EventArgs e)
        {
            string cmnd = txt_TraCuu_CMND.Text.Trim();
            string cmd_tk = "select BTP.ID,P.TENPHONG,KH.HOTEN,KH.CMND,BTP.SoNguoi,BTP.NgayDatPhong,BTP.NgayCheckin,BTP.NgayCheckout,TT.TENTRANGTHAI " +
                "from BANGTHUEPHONG as BTP,PHONG as P,TRANGTHAITHUEPHONG as TT,KHACHHANG as KH " +
                "where BTP.IDPhong = P.ID and BTP.IDKhachHang = KH.ID and BTP.IDTrangThai = TT.ID and KH.CMND like '" + cmnd + "%'";
            HienThiThongTin_TraCuu_Thue(cmd_tk);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLP_Load_Phong(sender, e);
            if (tabControl1.SelectedIndex == 0)
            {
                btn_sua.Visible = false;
                //btn_xoa.Visible = false;
                btn_dk_thue.Visible = true;
            }
            else
            {
                btn_sua.Visible = true;
                //btn_xoa.Visible = true;
                btn_dk_thue.Visible = false;
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa thông tin thuê phòng?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int trang_thai_phong = -1;
            cmd = " select ID from dbo.TRANGTHAITHUEPHONG where TENTRANGTHAI = N'" + tt_phong.Text.Trim() + "'";
            trang_thai_phong = int.Parse(DataProvider.Instance.ExecuteScalar(cmd).ToString());

            THUEPHONG thuephong = new THUEPHONG();
            thuephong.SoNguoi = so_nguoi_thue.Value;
            thuephong.NgayDatPhong = dat_phong.Value;
            thuephong.NgayCheckIn = check_in.Value;
            thuephong.NgayCheckOut = check_out.Value;
            thuephong.IDTrangThai = trang_thai_phong;
            thuephong.ID = Convert.ToInt32(temp_ID);

            string temp_TT = "";
            if (tt_phong.Text.Trim() == "Đã check in")
            {
                temp_TT = "Đã thuê";
            }
            else if (tt_phong.Text.Trim() == "Chờ check in")
            {
                temp_TT = "Đã đặt";
            }
            else
            {
                temp_TT = "Còn trống";
            }

            cmd = "select P.ID,T.ID from PHONG as P,TINHTRANGPHONG as T where P.TENPHONG =N'" + ten_phong_thue.Text.Trim() + "' and T.TEN =N'" + temp_TT + "'";

            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);
            thuephong.IDPhong = int.Parse(dt.Rows[0].ItemArray[0].ToString());
            thuephong.IDTinhTrangPhong = int.Parse(dt.Rows[0].ItemArray[1].ToString());


            cmd = "select BTP.ID,P.TENPHONG,KH.HOTEN,KH.CMND,BTP.SoNguoi,BTP.NgayDatPhong,BTP.NgayCheckin,BTP.NgayCheckout,TT.TENTRANGTHAI " +
             "from BANGTHUEPHONG as BTP,PHONG as P,TRANGTHAITHUEPHONG as TT,KHACHHANG as KH " +
             "where BTP.IDPhong = P.ID and BTP.IDKhachHang = KH.ID and BTP.IDTrangThai = TT.ID";

            int kq = thuephong.SuaPhieuThuePhong();
            if (kq > 0)
            {
                MessageBox.Show("Chỉnh sửa thông tin thành công!");
                HienThiThongTin_TraCuu_Thue(cmd);
                clear_data();
            }
            else
            {
                MessageBox.Show("Chỉnh sửa thông tin thất bại!");
                HienThiThongTin_TraCuu_Thue(cmd);
                clear_data();
            }

            cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                  "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG and TEN = N'Còn trống'";
            HienThiPhong(cmd);
        }

        private void tim_loai_phong_thue_TextChanged(object sender, EventArgs e)
        {
            string loai_phong_thue = tim_loai_phong_thue.Text.Trim();
            string cmd_tim_phong = "select TENPHONG, TENLOAIPHONG, GIA, SOGIUONG, SONGUOITOIDA, PHUTHU, TEN " +
                "from dbo.LOAIPHONG as L,PHONG as P,TINHTRANGPHONG as T " +
                "where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG and TEN = N'Còn trống' and TENLOAIPHONG like N'" + loai_phong_thue + "%'";

            HienThiPhong(cmd_tim_phong);
        }

        private void tim_loai_phong_TextChanged(object sender, EventArgs e)
        {
            string loai_phong = tim_loai_phong.Text.Trim();
            string cmd_tim_loai_phong = "select TENLOAIPHONG, GIA, SONGUOITOIDA, SOGIUONG, PHUTHU from LOAIPHONG " +
                  "where TENLOAIPHONG LIKE '" + loai_phong + "%'";

            HienThiLoaiPhong(cmd_tim_loai_phong);
        }

        private void list_LoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_LoaiPhong.SelectedItems.Count == 0) return;
            ListViewItem lvi = list_LoaiPhong.SelectedItems[0];
            ten_loai_phong = lvi.SubItems[0].Text.Trim();
            txtTenLoaiPhong.Text = lvi.SubItems[0].Text;
            txtGia.Text = lvi.SubItems[1].Text;
            txtSoNguoiToiDa.Text = lvi.SubItems[2].Text;
            txtSoGiuong.Text = lvi.SubItems[3].Text;
            txtPhuThu.Text = lvi.SubItems[4].Text;
            tb_QLLP_TenLPCheck.Text = lvi.SubItems[0].Text.Trim();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm loại phòng?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (txtTenLoaiPhong.Text.Trim() == "" || txtGia.Text.Trim() == "" || txtSoNguoiToiDa.Text.Trim() == "" || txtSoGiuong.Text.Trim() == "" || txtPhuThu.Text.Trim() == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
                return;
            }

            LOAIPHONG loaiphong = new LOAIPHONG();

            loaiphong.TenLoaiPhong = txtTenLoaiPhong.Text.Trim();
            loaiphong.Gia = decimal.Parse(txtGia.Text.Trim());
            loaiphong.SoNguoiToiDa = int.Parse(txtSoNguoiToiDa.Text.Trim());
            loaiphong.SoGiuong = int.Parse(txtSoGiuong.Text.Trim());
            loaiphong.PhuThu = decimal.Parse(txtPhuThu.Text.Trim());

            string cmd = "select count(ID) from LOAIPHONG where TENLOAIPHONG = N'" + loaiphong.TenLoaiPhong + "'";
            int check = Int32.Parse(DataProvider.Instance.ExecuteScalar(cmd).ToString());

            if (check == 1)
            {
                MessageBox.Show("Trùng tên loại phòng");
                return;
            }

            int kq = loaiphong.ThemLoaiPhong();
            if (kq > 0)
            {
                MessageBox.Show("Thêm loại phòng thành công");
                cmd = "select TENLOAIPHONG,GIA,SONGUOITOIDA,SOGIUONG,PHUTHU from LOAIPHONG";
                HienThiLoaiPhong(cmd);
            }
            else
            {
                MessageBox.Show("Thêm loại phòng thất bại");
            }
        }
        string ten_loai_phong = ""; // lay de xoa

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa loại phòng?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (txtTenLoaiPhong.Text.Trim() == "" || txtGia.Text.Trim() == "" || txtSoNguoiToiDa.Text.Trim() == "" || txtSoGiuong.Text.Trim() == "" || txtPhuThu.Text.Trim() == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
                return;
            }

            LOAIPHONG loaiphong = new LOAIPHONG();

            loaiphong.TenLoaiPhong = txtTenLoaiPhong.Text.Trim();
            loaiphong.Gia = decimal.Parse(txtGia.Text.Trim());
            loaiphong.SoNguoiToiDa = int.Parse(txtSoNguoiToiDa.Text.Trim());
            loaiphong.SoGiuong = int.Parse(txtSoGiuong.Text.Trim());
            loaiphong.PhuThu = decimal.Parse(txtPhuThu.Text.Trim());

            if (loaiphong.TenLoaiPhong != tb_QLLP_TenLPCheck.Text.Trim())
            {
                string cmd = "select count(ID) from LOAIPHONG where TENLOAIPHONG = N'" + loaiphong.TenLoaiPhong + "'";
                int check = Int32.Parse(DataProvider.Instance.ExecuteScalar(cmd).ToString());

                if (check == 1)
                {
                    MessageBox.Show("Trùng tên loại phòng");
                    return;
                }
            }

            string cmd1 = "select ID from LOAIPHONG where TENLOAIPHONG = N'" + tb_QLLP_TenLPCheck.Text.Trim() + "'";
            loaiphong.ID = Int32.Parse(DataProvider.Instance.ExecuteScalar(cmd1).ToString());


            int kq = DataProvider.Instance.ExecuteNonQuery(cmd);
            if (kq > 0)
            {
                MessageBox.Show("Sửa loại phòng thành công");
                cmd = "select TENLOAIPHONG,GIA,SONGUOITOIDA,SOGIUONG,PHUTHU from LOAIPHONG";
                HienThiLoaiPhong(cmd);
            }
            else
            {
                MessageBox.Show("Sửa loại phòng thất bại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTenLoaiPhong.Text.Trim() == "" || txtGia.Text.Trim() == "" || txtSoNguoiToiDa.Text.Trim() == "" || txtSoGiuong.Text.Trim() == "" || txtPhuThu.Text.Trim() == "")
            {
                MessageBox.Show("Nhập thiếu thông tin");
                return;
            }
            string TenLoaiPhong = tb_QLLP_TenLPCheck.Text;
            string query = "SELECT ID FROM LOAIPHONG WHERE TENLOAIPHONG = N'" + TenLoaiPhong + "'";
            string IDLoaiPhong = DataProvider.Instance.ExecuteScalar(query).ToString();
            string cmd = "SELECT COUNT (*) FROM PHONG AS P, BANGTHUEPHONG AS TP WHERE P.IDLoaiPhong = " + IDLoaiPhong + " AND P.ID = TP.IDPhong";
            int dt = int.Parse(DataProvider.Instance.ExecuteScalar(cmd).ToString());
            if (dt != 0)
            {
                MessageBox.Show(dt.ToString());
                MessageBox.Show("Không được phép xóa loại phòng này");
                return;
            }
            DialogResult kq = MessageBox.Show("Do you really want to delete?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (kq == DialogResult.Yes)
            {
                XoaLoaiPhong(ten_loai_phong);
            }
        }

        private void XoaLoaiPhong(string ten_loai_phong)  // xoa quan ly loai phong (2)
        {
            LOAIPHONG loaiphong = new LOAIPHONG();
            loaiphong.TenLoaiPhong = txtTenLoaiPhong.Text.Trim();

            int kq = loaiphong.XoaLoaiPhong();
            if (kq > 0)
            {
                MessageBox.Show("Xóa loại phòng thành công");
                cmd = "select TENLOAIPHONG,GIA,SONGUOITOIDA,SOGIUONG,PHUTHU from LOAIPHONG";
                HienThiLoaiPhong(cmd);
            }
            else
            {
                MessageBox.Show("Xóa loại phòng thất bại");
            }
        }

        // Quoc
        private void listView_QuanLyPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_QuanLyPhong.SelectedItems.Count == 0) return;
            ListViewItem lvi = listView_QuanLyPhong.SelectedItems[0];
            tb_QLP_TenPhong.Text = lvi.SubItems[0].Text;
            cbb_QLP_LoaiPhong.Text = lvi.SubItems[1].Text;
            cbb_QLP_TTPhong.Text = lvi.SubItems[6].Text;
            //LoadIDLoaiPhong
            string Selected_LoaiPhong = lvi.SubItems[1].Text.Trim();
            string cmd_QLP_TimIDPLoaiPhong = "SELECT ID FROM LOAIPHONG WHERE TENLOAIPHONG =N'" + Selected_LoaiPhong + "'";
            tb_QLP_IDLoaiPhong.Text = DataProvider.Instance.ExecuteScalar(cmd_QLP_TimIDPLoaiPhong).ToString().Trim();
            //LoadIDTTPhong
            string Selected_TTPhong = lvi.SubItems[6].Text.Trim();
            string cmd_QLP_TimIDPTTPhong = "SELECT ID FROM TINHTRANGPHONG WHERE TEN =N'" + Selected_TTPhong + "'";
            tb_QLP_IDTinhTrangPhong.Text = DataProvider.Instance.ExecuteScalar(cmd_QLP_TimIDPTTPhong).ToString().Trim();
            //LoadIDPhong
            tb_QLP_TenPhongCheck.Text = lvi.SubItems[0].Text.Trim();
        }

        private void HienThiQuanLiPhong(string cmd)
        {

            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);
            listView_QuanLyPhong.Items.Clear();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                listView_QuanLyPhong.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                listView_QuanLyPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                listView_QuanLyPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                listView_QuanLyPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                listView_QuanLyPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                listView_QuanLyPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                listView_QuanLyPhong.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
            }
        }
        // Tim` Phong`
        private void btn_QLP_Tim_Click(object sender, EventArgs e)
        {
            string QLP_TimTenPhong = tb_QLP_TenPhong.Text.Trim();
            string QLP_TimLoaiPhong = cbb_QLP_LoaiPhong.Text.Trim();
            string QLP_TimTinhTrangPhong = cbb_QLP_TTPhong.Text.Trim();
            string cmd_QLP_TimThongTinPhong = "select TENPHONG, TENLOAIPHONG, GIA, SOGIUONG, SONGUOITOIDA, PHUTHU, TEN " +
                "from dbo.LOAIPHONG as L,PHONG as P,TINHTRANGPHONG as T " +
                "where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG and TENPHONG like N'" + QLP_TimTenPhong + "%'" +
                "and TENLOAIPHONG like N'" + QLP_TimLoaiPhong + "%' and TEN like N'" + QLP_TimTinhTrangPhong + "%' ";
            HienThiQuanLiPhong(cmd_QLP_TimThongTinPhong);
        }
        // Them Phong`
        private void btn_QLP_Them_Click(object sender, EventArgs e)
        {
            PHONG phong = new PHONG();

            if (MessageBox.Show("Bạn có muốn thêm phòng?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (tb_QLP_TenPhong.Text == "")
            {
                MessageBox.Show("Thiếu tên phòng!");
                return;
            }
            else if (tb_QLP_IDLoaiPhong.Text == "")
            {
                MessageBox.Show("Thiếu loại phòng!");
                return;
            }
            else if (tb_QLP_IDTinhTrangPhong.Text == "")
            {
                MessageBox.Show("Thiếu trình trạng phòng!");
                return;
            }
            else
            {

                phong.TenPhong = tb_QLP_TenPhong.Text.Trim();
                phong.IDLoaiPhong = int.Parse(tb_QLP_IDLoaiPhong.Text.Trim());
                phong.IDTinhTrangPhong = int.Parse(tb_QLP_IDTinhTrangPhong.Text.Trim());


                string cmd = "select count(ID) from PHONG where TENPHONG = N'" + phong.TenPhong + "'";
                int check = Int32.Parse(DataProvider.Instance.ExecuteScalar(cmd).ToString());
                if (check > 0)
                {
                    MessageBox.Show("Trùng tên phòng");
                    return;
                }

                int kq = phong.ThemPhong();
                if (kq > 0)
                {
                    MessageBox.Show("Thêm phòng thành công");
                    cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                    "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG";
                    HienThiQuanLiPhong(cmd);
                }
                else
                {
                    MessageBox.Show("Thêm phòng thất bại");
                }
            }
        }
        //Sua Phong`
        private void btn_QLP_Sua_Click(object sender, EventArgs e)
        {
            PHONG phong = new PHONG();

            if (MessageBox.Show("Bạn có muốn sửa phòng?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (tb_QLP_IDLoaiPhong.Text == "")
            {
                MessageBox.Show("Thiếu loại phòng!");
                return;
            }
            else if (tb_QLP_IDTinhTrangPhong.Text == "")
            {
                MessageBox.Show("Thiếu tình trạng phòng!");
                return;
            }
            else
            {
                phong.TenPhong = tb_QLP_TenPhong.Text.Trim();
                phong.IDLoaiPhong = int.Parse(tb_QLP_IDLoaiPhong.Text.Trim());
                phong.IDTinhTrangPhong = int.Parse(tb_QLP_IDTinhTrangPhong.Text.Trim());

                string tenphongcheck = tb_QLP_TenPhongCheck.Text.Trim();

                if (phong.TenPhong != tenphongcheck)
                {
                    string cmd = "select count(ID) from PHONG where TENPHONG = N'" + phong.TenPhong + "'";
                    int check = Int32.Parse(DataProvider.Instance.ExecuteScalar(cmd).ToString().Trim());

                    if (check == 1)
                    {
                        MessageBox.Show("Trùng tên phòng");
                        return;
                    }
                }


                string cmd1 = "select ID from PHONG where TENPHONG = N'" + tenphongcheck + "'";
                int IDPhong = Int32.Parse(DataProvider.Instance.ExecuteScalar(cmd1).ToString().Trim());

                int kq = phong.SuaPhong();
                if (kq > 0)
                {
                    MessageBox.Show("Sửa phòng thành công");
                    cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                    "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG";
                    HienThiQuanLiPhong(cmd);
                }
                else
                {
                    MessageBox.Show("Sửa phòng thất bại");
                }
            }
        }
        // Xoa Phong`
        private void btn_QLP_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult kq1 = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (kq1 == DialogResult.Yes)
            {
                string tenphong = tb_QLP_TenPhong.Text.Trim();

                string cmd = "SELECT COUNT (*) FROM PHONG as P, BANGTHUEPHONG as BTP WHERE P.ID = BTP.IDPhong and P.TENPHONG = N'" + tenphong + "'";
                int kq2 = int.Parse(DataProvider.Instance.ExecuteScalar(cmd).ToString());
                if (kq2 > 0)
                {
                    MessageBox.Show("Xóa thất bại, dữ liệu phòng đang được dùng!!");
                    return;
                }

                else
                {
                    PHONG phong = new PHONG();
                    phong.TenPhong = tb_QLP_TenPhong.Text.Trim();

                    int kq3 = phong.XoaPhong();
                    if (kq3 > 0)
                    {
                        MessageBox.Show("Xóa thành công");
                        cmd = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                        "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG";
                        HienThiQuanLiPhong(cmd);
                    }
                    else
                        MessageBox.Show("Xóa thất bại!!!");

                }
            }
            else
            {
                tb_QLP_TenPhong.Clear();
                cbb_QLP_LoaiPhong.Items.Clear();
                cbb_QLP_TTPhong.Items.Clear();
                return;
            }
        }
        //Clear
        private void btn_QLP_Clear_Click(object sender, EventArgs e)
        {
            tb_QLP_TenPhong.Clear();
            cbb_QLP_LoaiPhong.Text = String.Empty;
            cbb_QLP_TTPhong.Text = String.Empty;

            string cmd_QLP_HienThiLai = "select TENPHONG,TENLOAIPHONG,GIA,SOGIUONG,SONGUOITOIDA,PHUTHU,TEN from dbo.LOAIPHONG as L," +
                "PHONG as P,TINHTRANGPHONG as T where L.ID = P.IDLOAIPHONG and T.ID = P.IDTINHTRANG";
            HienThiQuanLiPhong(cmd_QLP_HienThiLai);
        }

        private void cbb_QLP_LoaiPhong_DropDown(object sender, EventArgs e)
        {
            cbb_QLP_LoaiPhong.Items.Clear();
            string cmd_QLP_ThemLoaiPhong = "SELECT * FROM LOAIPHONG";
            DataTable data_QLP_LoaiPhong = DataProvider.Instance.ExecuteQuery(cmd_QLP_ThemLoaiPhong);

            for (int i = 0; i < data_QLP_LoaiPhong.Rows.Count; i++)
            {
                string LoaiPhong = data_QLP_LoaiPhong.Rows[i].ItemArray[1].ToString();
                cbb_QLP_LoaiPhong.Items.Add(LoaiPhong);
            }
        }

        private void cbb_QLP_TTPhong_DropDown(object sender, EventArgs e)
        {
            cbb_QLP_TTPhong.Items.Clear();
            string cmd_QLP_ThemTinhTrangPhong = "SELECT * FROM TINHTRANGPHONG";
            DataTable data_QLP_TinhTrangPhong = DataProvider.Instance.ExecuteQuery(cmd_QLP_ThemTinhTrangPhong);

            for (int i = 0; i < data_QLP_TinhTrangPhong.Rows.Count; i++)
            {
                string TinhTrangPhong = data_QLP_TinhTrangPhong.Rows[i].ItemArray[1].ToString();
                cbb_QLP_TTPhong.Items.Add(TinhTrangPhong);
            }
        }

        private void cbb_QLP_LoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Selected_LoaiPhong = cbb_QLP_LoaiPhong.SelectedItem.ToString().Trim();
            string cmd_QLP_TimIDPLoaiPhong = "SELECT ID FROM LOAIPHONG WHERE TENLOAIPHONG =N'" + Selected_LoaiPhong + "'";
            tb_QLP_IDLoaiPhong.Text = DataProvider.Instance.ExecuteScalar(cmd_QLP_TimIDPLoaiPhong).ToString().Trim();
        }

        private void cbb_QLP_TTPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Selected_TTPhong = cbb_QLP_TTPhong.SelectedItem.ToString().Trim();
            string cmd_QLP_TimIDPTTPhong = "SELECT ID FROM TINHTRANGPHONG WHERE TEN =N'" + Selected_TTPhong + "'";
            tb_QLP_IDTinhTrangPhong.Text = DataProvider.Instance.ExecuteScalar(cmd_QLP_TimIDPTTPhong).ToString().Trim();
        }


        #endregion

        #region QuanLyDichVu

        //Message box xác nhận
        bool DV_Xacnhan(String message)
        {
            string title = "Xác nhận";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Hiển thị lên listview
        void DV_loadDichVu(string query = "SELECT * FROM DBO.[DICHVU]")
        {
            listView_DichVu.Items.Clear();
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            int i;
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                listView_DichVu.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                listView_DichVu.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                listView_DichVu.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
            }

        }

        // Refresh 
        private void btn_DV_RefreshDv_Click(object sender, EventArgs e)
        {
            listView_DichVu.Items.Clear();
            tb_DV_Tendichvu.Clear();
            tb_DV_Gia.Clear();
            maskedTB_DV_Tgbd.Clear();
            maskedTB_DV_Tgkt.Clear();
            DV_loadDichVu();
        }

        //Binding listview
        private void listView_DichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_DichVu.SelectedItems.Count == 0) return;

            // Lay phan tu duoc chon tren listview
            ListViewItem lvi = listView_DichVu.SelectedItems[0];
            // Hien thi thong tin tu listview sang cac textbox
            tb_DV_Tendichvu.Text = lvi.SubItems[0].Text;
            tb_DV_Gia.Text = lvi.SubItems[1].Text;
            string timee = lvi.SubItems[2].Text;
            maskedTB_DV_Tgbd.Text = timee.Split('-')[0];
            maskedTB_DV_Tgkt.Text = timee.Split('-')[1];
            tendvcheck = lvi.SubItems[0].Text;
            btn_DV_SuaDV.Enabled = true;
            btn_DV_XoaDV.Enabled = true;

        }
        //Tìm dịch vụ
        private void btn_DV_TimDichVu_Click(object sender, EventArgs e)
        {
            listView_DichVu.Items.Clear();
            string startTime = maskedTB_DV_Tgbd.Text.Replace(":", string.Empty).Replace(" ", string.Empty);
            string endTime = maskedTB_DV_Tgkt.Text.Replace(":", string.Empty).Replace(" ", string.Empty); ;
            string checkTime = maskedTB_DV_Tgbd.Text + "-" + maskedTB_DV_Tgkt.Text;
            if (tb_DV_Tendichvu.Text != "" && tb_DV_Gia.Text != "" && startTime != "" && endTime != "")
            {
                //string cmd_tk = "select * from DICHVU where (TENDICHVU=N'" + tb_Tendichvu.Text.Trim() + "') ";
                string cmd_tk = "select * from DICHVU where (TENDICHVU=N'" + tb_DV_Tendichvu.Text.Trim() + "' and GIA='" + tb_DV_Gia.Text.Trim() +
                "' and THOIGIANPHUCVU='" + checkTime.Trim() + "')";
                DV_loadDichVu(cmd_tk);
                return;
            }
            else if (tb_DV_Tendichvu.Text != "" && startTime != "" && endTime != "")
            {
                string cmd_tk = "select * from DICHVU where (TENDICHVU=N'" + tb_DV_Tendichvu.Text.Trim() +
                "' and THOIGIANPHUCVU='" + checkTime.Trim() + "')";
                DV_loadDichVu(cmd_tk);
                return;
            }
            else if (tb_DV_Tendichvu.Text != "" && tb_DV_Gia.Text != "")
            {
                string cmd_tk = "select * from DICHVU where (TENDICHVU=N'" + tb_DV_Tendichvu.Text.Trim() + "' and GIA='" + tb_DV_Gia.Text.Trim() + "')";
                DV_loadDichVu(cmd_tk);
                return;
            }
            else if (tb_DV_Gia.Text != "" && startTime != "" && endTime != "")
            {

                string cmd_tk = "select * from DICHVU where (GIA='" + tb_DV_Gia.Text.Trim() +
                "' and THOIGIANPHUCVU='" + checkTime.Trim() + "')";
                DV_loadDichVu(cmd_tk);
                return;
            }
            else if (tb_DV_Tendichvu.Text != "")
            {
                string cmd_tk = "select * from DICHVU where (TENDICHVU=N'" + tb_DV_Tendichvu.Text.Trim() + "')";
                DV_loadDichVu(cmd_tk);
                return;

            }
            else if (tb_DV_Gia.Text != "")
            {
                string cmd_tk = "select * from DICHVU where (GIA=N'" + decimal.Parse(tb_DV_Gia.Text.Trim()) + "')";
                DV_loadDichVu(cmd_tk);
                return;
            }
            else if (startTime != "")
            {
                string[] arr = new string[4];
                ListViewItem itm;
                DataTable dt = new DataTable();
                string query = "SELECT * FROM DBO.[DICHVU]";
                dt = DataProvider.Instance.ExecuteQuery(query);
                int i;
                if (endTime != "")
                {
                    int timestart = Int32.Parse(startTime);
                    int timeend = Int32.Parse(endTime);
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        int timestart_hh = Int32.Parse(dt.Rows[i].ItemArray[3].ToString().Split('-')[0].Split(':')[0] + dt.Rows[i].ItemArray[3].ToString().Split('-')[0].Split(':')[1]);
                        int timeend_hh = Int32.Parse(dt.Rows[i].ItemArray[3].ToString().Split('-')[1].Split(':')[0] + dt.Rows[i].ItemArray[3].ToString().Split('-')[1].Split(':')[1]);
                        if (timestart_hh <= timestart && timeend <= timeend_hh)
                        {
                            arr[0] = dt.Rows[i].ItemArray[1].ToString();
                            arr[1] = dt.Rows[i].ItemArray[2].ToString();
                            arr[2] = dt.Rows[i].ItemArray[3].ToString();
                            itm = new ListViewItem(arr);
                            listView_DichVu.Items.Add(itm);
                        }
                    }
                    return;
                }
                else
                {
                    int timestart = Int32.Parse(startTime);
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        int timestart_hh = Int32.Parse(dt.Rows[i].ItemArray[3].ToString().Split('-')[0].Split(':')[0] + dt.Rows[i].ItemArray[3].ToString().Split('-')[0].Split(':')[1]);
                        int timeend_hh = Int32.Parse(dt.Rows[i].ItemArray[3].ToString().Split('-')[1].Split(':')[0] + dt.Rows[i].ItemArray[3].ToString().Split('-')[1].Split(':')[1]);

                        if (timestart_hh <= timestart && timestart <= timeend_hh)
                        {
                            arr[0] = dt.Rows[i].ItemArray[1].ToString();
                            arr[1] = dt.Rows[i].ItemArray[2].ToString();
                            arr[2] = dt.Rows[i].ItemArray[3].ToString();
                            itm = new ListViewItem(arr);
                            listView_DichVu.Items.Add(itm);
                        }
                    }
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DV_loadDichVu();

            }
        }

        //Thêm dịch vụ
        private void btn_DV_ThemDichVu_Click(object sender, EventArgs e)
        {

            DICHVU dv = new DICHVU();
            dv.TenDichVu = tb_DV_Tendichvu.Text.Trim();

            string timedb = maskedTB_DV_Tgbd.Text.Trim();
            string timekt = maskedTB_DV_Tgkt.Text.Trim();
            string timetotal = timedb + "-" + timekt;
            dv.ThoiGianPhucVu = timetotal;

            if (dv.TenDichVu == "" || tb_DV_Gia.Text.Trim() == "" || timedb == ":" || timekt == ":")
            {
                MessageBox.Show("Nhập thiếu thông tin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (tb_DV_Gia.Text.Trim() != "")
                {
                    if (DV_ktDulieu(tb_DV_Gia.Text.Trim()) == false)
                    {
                        MessageBox.Show("Nhập sai kiểu dữ liệu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            dv.Gia = decimal.Parse(tb_DV_Gia.Text);

            if (DV_Xacnhan("Bạn có đồng ý thêm dịch vụ?") == true)
            {
                int k = dv.ThemDichVu();
                MessageBox.Show(k.ToString());
                if (k == 0)
                {
                    MessageBox.Show("Dịch vụ đã tồn tại");
                    return;
                }
            }
            else
            {
                return;
            }

            MessageBox.Show("Thêm dịch vụ thành công!");
            DV_loadDichVu();
        }
        //Sửa dịch vụ
        private void btn_DV_SuaDV_Click(object sender, EventArgs e)
        {
            DICHVU dv = new DICHVU();

            dv.TenDichVu = tb_DV_Tendichvu.Text.Trim();
            dv.Gia = 0; ;
            string tgbatdau = maskedTB_DV_Tgbd.Text;
            string tgketthuc = maskedTB_DV_Tgkt.Text;
            string tgphucvu = tgbatdau + "-" + tgketthuc;
            dv.ThoiGianPhucVu = tgphucvu;

            if (tb_DV_Gia.Text.Trim() != "")
            {
                if (DV_ktDulieu(tb_DV_Gia.Text.Trim()) == false)
                {

                    MessageBox.Show("Nhập sai kiểu dữ liệu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    dv.Gia = decimal.Parse(tb_DV_Gia.Text.Trim());
                }
            }


            if (DV_Xacnhan("Bạn có đồng ý sửa dịch vụ?") == true)
            {
                dv.SuaDichVu(tendvcheck);
            }
            else
            {
                return;
            }
            MessageBox.Show("Sửa dịch vụ thành công");
            DV_loadDichVu();
        }
        //Xóa dịch vụ
        private void btn_DV_XoaDV_Click(object sender, EventArgs e)
        {

            string tendv = tb_DV_Tendichvu.Text.Trim();
            DICHVU dv = new DICHVU();
            if (DV_Xacnhan("Bạn có đồng ý xóa dịch vụ?") == true)
            {
                dv.XoaDichVu(tendv);
            }
            else
            {
                return;
            }
            MessageBox.Show("Xóa dịch vụ thành công!");
            DV_loadDichVu();
        }

        #endregion 

        #region QuanLyThueDichVu

        private void dichvu_control_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThuePhong(list_TDV_TP);
            LoadDichVu(list_TDV_DV);
            LoadThueDV(list_ThueDV_DST);
        }

        private void btn_TDV_LapPhieu_Click(object sender, EventArgs e)
        {
            THUEDICHVU thuedv = new THUEDICHVU();
            if (txt_TDV_IDDichVuLP.Text == "" || txt_TDV_IDThuePhongLP.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
                return;
            }

            thuedv.IDDichVu = Int32.Parse(txt_TDV_IDDichVuLP.Text);
            thuedv.IDThuePhong = Int32.Parse(txt_TDV_IDThuePhongLP.Text);
            thuedv.SoLuong = nbr_TDV_SoLuongLP.Value;


            int kq;
            if (DV_Xacnhan("Bạn có đồng ý lập phiếu thuê?") == true)
            {
                kq = thuedv.LapPhieu();
            }
            else
            {
                return;
            }

            if (kq > 0)
            {
                MessageBox.Show("Lập phiếu thành công");
            }
            else
            {
                MessageBox.Show("Lập phiếu thất bại");
            }
        }

        #region thuê phòng
        private void txt_TDV_TenPhong_TextChanged(object sender, EventArgs e)
        {
            string TenPhong = txt_TDV_TenPhong.Text.Trim();
            string NguoiThue = txt_TDV_NguoiThue.Text.Trim();
            string cmd = "select TP.ID, P.TENPHONG, KH.HOTEN " +
                "from PHONG as P, BANGTHUEPHONG as TP, KHACHHANG as KH, TRANGTHAITHUEPHONG as TT "
                + "where TP.IDPhong = P.ID AND TP.IDKhachHang = KH.ID AND TP.IDTrangThai = TT.ID AND TT.TENTRANGTHAI = N'Đã check in'";
            if (TenPhong != "")
                cmd += " AND P.TENPHONG LIKE N'" + TenPhong + "%'";
            if (NguoiThue != "")
                cmd += " AND KH.HOTEN LIKE N'" + NguoiThue + "%'";
            list_TDV_TP.Items.Clear();
            LoadThuePhong(list_TDV_TP, cmd);
        }
        private void txt_TDV_NguoiThue_TextChanged(object sender, EventArgs e)
        {
            string TenPhong = txt_TDV_TenPhong.Text.Trim();
            string NguoiThue = txt_TDV_NguoiThue.Text.Trim();
            string cmd = "select TP.ID, P.TENPHONG, KH.HOTEN " +
                "from PHONG as P, BANGTHUEPHONG as TP, KHACHHANG as KH, TRANGTHAITHUEPHONG as TT "
                + "where TP.IDPhong = P.ID AND TP.IDKhachHang = KH.ID AND TP.IDTrangThai = TT.ID AND TT.TENTRANGTHAI = N'Đã check in'";
            if (TenPhong != "")
                cmd += " AND P.TENPHONG LIKE N'" + TenPhong + "%'";
            if (NguoiThue != "")
                cmd += " AND KH.HOTEN LIKE N'" + NguoiThue + "%'";
            list_TDV_TP.Items.Clear();
            LoadThuePhong(list_TDV_TP, cmd);
        }

        private void list_TDV_TP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_TDV_TP.SelectedItems.Count == 0) return;
            ListViewItem lvi = list_TDV_TP.SelectedItems[0];
            // Hien thi thong tin tu listview sang cac textbox
            txt_TDV_IDThuePhongLP.Text = lvi.SubItems[0].Text;
            txt_TDV_TenPhongLP.Text = lvi.SubItems[1].Text;
            txt_TDV_NguoiThueLP.Text = lvi.SubItems[2].Text;
        }

        void LoadThuePhong(ListView list_TDV_TP, string cmd = "select TP.ID, P.TENPHONG, KH.HOTEN " +
                "from PHONG as P, BANGTHUEPHONG as TP, KHACHHANG as KH, TRANGTHAITHUEPHONG as TT " +
                "where TP.IDPhong = P.ID AND TP.IDKhachHang = KH.ID AND TP.IDTrangThai = TT.ID AND TT.TENTRANGTHAI = N'Đã check in'")
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);

            list_TDV_TP.Items.Clear();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list_TDV_TP.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                list_TDV_TP.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                list_TDV_TP.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
            }

        }

        #endregion

        #region dịch vụ
        private void txt_TDV_TenDichVu_TextChanged(object sender, EventArgs e)
        {
            string TenDichVu = txt_TDV_TenDichVu.Text.Trim();
            cmd = "select DV.ID, DV.TENDICHVU, DV.GIA, DV.THOIGIANPHUCVU " +
                "from DICHVU as DV";
            if (TenDichVu != "")
                cmd += " Where DV.TENDICHVU LIKE N'" + TenDichVu + "%'";
            list_TDV_DV.Items.Clear();
            LoadDichVu(list_TDV_DV, cmd);
        }

        void LoadDichVu(ListView list_TDV_DV, string cmd = "select DV.ID, DV.TENDICHVU, DV.GIA, DV.THOIGIANPHUCVU " +
                "from DICHVU as DV")
        {
            list_TDV_DV.Items.Clear();
            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list_TDV_DV.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                list_TDV_DV.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                list_TDV_DV.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                list_TDV_DV.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
            }
        }
        private void list_TDV_DV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_TDV_DV.SelectedItems.Count == 0) return;
            ListViewItem lvi = list_TDV_DV.SelectedItems[0];
            // Hien thi thong tin tu listview sang cac textbox
            txt_TDV_IDDichVuLP.Text = lvi.SubItems[0].Text;
            txt_TDV_TenDichVuLP.Text = lvi.SubItems[1].Text;
            txt_TDV_GiaLP.Text = lvi.SubItems[2].Text;
        }


        #endregion

        #region thông tin thuê dịch vụ
        void LoadThueDV(ListView list_ThueDV_DST, string cmd = "select TDV.ID, P.TENPHONG, KH.HOTEN, DV.TENDICHVU, DV.GIA, TDV.SOLUONG " +
                "from PHONG as P, BANGTHUEPHONG as TP, KHACHHANG as KH, BANGTHUEDICHVU as TDV, DICHVU as DV, TRANGTHAITHUEPHONG as TT "
                + "where TP.IDPhong = P.ID AND TP.IDKhachHang = KH.ID AND TP.ID = TDV.IDBANGTHUEPHONG AND DV.ID = TDV.IDDICHVU AND TP.IDTrangThai = TT.ID AND TT.TENTRANGTHAI = N'Đã check in'")
        {
            list_ThueDV_DST.Items.Clear();

            DataTable dt = DataProvider.Instance.ExecuteQuery(cmd);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list_ThueDV_DST.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                list_ThueDV_DST.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                list_ThueDV_DST.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                list_ThueDV_DST.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                list_ThueDV_DST.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                list_ThueDV_DST.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
            }
        }

        private void list_ThueDV_DST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_ThueDV_DST.SelectedItems.Count == 0) return;
            ListViewItem lvi = list_ThueDV_DST.SelectedItems[0];
            // Hien thi thong tin tu listview sang cac textbox
            txt_IDThueDV_DST.Text = lvi.SubItems[0].Text;
            txt_TenPhong_DST.Text = lvi.SubItems[1].Text;
            txt_NguoiThue_DST.Text = lvi.SubItems[2].Text;
            txt_DichVu_DST.Text = lvi.SubItems[3].Text;
            txt_Gia_DST.Text = lvi.SubItems[4].Text;
            nbr_SoLuong_DST.Value = Convert.ToDecimal(lvi.SubItems[5].Text);
        }
        private void btn_Sua_DST_Click(object sender, EventArgs e)
        {

            if (txt_IDThueDV_DST.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
                return;
            }
            //xu li luu data
            THUEDICHVU thuedv = new THUEDICHVU();
            thuedv.ID = int.Parse(txt_IDThueDV_DST.Text);
            thuedv.SoLuong = nbr_SoLuong_DST.Value;

            int kq;
            if (DV_Xacnhan("Bạn có đồng ý sửa phiếu thuê?") == true)
            {
                kq = thuedv.SuaPhieuTDV();
            }
            else
            {
                return;
            }

            if (kq > 0)
            {
                MessageBox.Show("Sửa thông tin thành công");
                LoadThueDV(list_ThueDV_DST);
            }
            else
            {
                MessageBox.Show("Sửa thông tin thất bại");
            }
        }

        private void btn_Xoa_DST_Click(object sender, EventArgs e)
        {

            if (txt_IDThueDV_DST.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
                return;
            }
            //xu li luu data
            THUEDICHVU thuedv = new THUEDICHVU();
            thuedv.ID = int.Parse(txt_IDThueDV_DST.Text);
            thuedv.SoLuong = nbr_SoLuong_DST.Value;

            int kq;

            if (DV_Xacnhan("Bạn có đồng ý xóa phiếu thuê?") == true)
            {
                kq = thuedv.XoaPhieuTDV();
            }
            else
            {
                return;
            }

            if (kq > 0)
            {
                MessageBox.Show("Xóa thành công");
                LoadThueDV(list_ThueDV_DST);
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }
        private void txt_TimTT_DST_TextChanged(object sender, EventArgs e)
        {
            string NguoiThue = txt_TimTT_DST.Text.Trim();
            cmd = "select TDV.ID, P.TENPHONG, KH.HOTEN, DV.TENDICHVU, DV.GIA, TDV.SOLUONG " +
                "from PHONG as P, BANGTHUEPHONG as TP, KHACHHANG as KH, BANGTHUEDICHVU as TDV, DICHVU as DV "
                + "where TP.IDPhong = P.ID AND TP.IDKhachHang = KH.ID AND TP.ID = TDV.IDBANGTHUEPHONG AND DV.ID = TDV.IDDICHVU";
            if (NguoiThue != "")
                cmd += " AND KH.HOTEN LIKE N'" + NguoiThue + "%'";
            list_ThueDV_DST.Items.Clear();
            LoadThueDV(list_ThueDV_DST, cmd);
        }
        #endregion

        #endregion

        #region HoaDon
        private void printDocument_HD_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(groupBox_HD_HoaDon.Width, groupBox_HD_HoaDon.Height);
            groupBox_HD_HoaDon.DrawToBitmap(bmp, new Rectangle(0, 0, groupBox_HD_HoaDon.Width, groupBox_HD_HoaDon.Height));
            e.Graphics.DrawImage((Image)bmp, x, y);

        }

        private void button_HD_XuatHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xuất hóa đơn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            printPreviewDialog_HD.Document = printDocument_HD;
            printPreviewDialog_HD.ShowDialog();
        }
        void HD_Check_KhachHang(string query = "SELECT * FROM DBO.KHACHHANG")
        {
            DataTable Data_KhachHang = DataProvider.Instance.ExecuteQuery(query);

            string TenKH = textBox_HD_TenKH.Text.Trim();
            string CMND = textBox_HD_CMND.Text.Trim();
            string SDT = textBox_HD_SDT.Text.Trim();

            if (Data_KhachHang.Rows.Count != 0)
            {
                textBox_HD_TenKH.Clear();
                textBox_HD_SDT.Clear();
                textBox_HD_CMND.Clear();

                textBox_HD_IDKH.Text = Data_KhachHang.Rows[0].ItemArray[0].ToString();
                textBox_HD_TenKH.Text = Data_KhachHang.Rows[0].ItemArray[1].ToString();
                textBox_HD_CMND.Text = Data_KhachHang.Rows[0].ItemArray[2].ToString();
                textBox_HD_SDT.Text = Data_KhachHang.Rows[0].ItemArray[3].ToString();
            }
            else
            {
                MessageBox.Show("Không tồn tại khách hàng!");
            }

        }

        // Done. Hàm list danh sách phòng đã thuê và đã check out
        void HD_List_ThuePhong()
        {
            string cmd_ThuePhong = "select BANGTHUEPHONG.IDTrangThai, BANGTHUEPHONG.ID, BANGTHUEPHONG.NgayCheckout " +
                                    "from DBO.BANGTHUEPHONG where (IDKhachHang='" + textBox_HD_IDKH.Text + "')";
            DataTable Data_ThuePhong = DataProvider.Instance.ExecuteQuery(cmd_ThuePhong);
            comboBox_HD_IDThuePhong.Items.Clear();
            for (int i = 0; i < Data_ThuePhong.Rows.Count; i++)
            {
                string cmd_TinhTrang = "select TENTRANGTHAI from TRANGTHAITHUEPHONG where (ID = '" + Data_ThuePhong.Rows[i].ItemArray[0].ToString() + "')";
                DataTable Data_TinhTrang = DataProvider.Instance.ExecuteQuery(cmd_TinhTrang);
                if (Data_TinhTrang.Rows[0].ItemArray[0].ToString() == "Đã check out")
                {
                    string IDThuePhong = Data_ThuePhong.Rows[i].ItemArray[1].ToString();
                    string NgayCheckOut = Data_ThuePhong.Rows[i].ItemArray[2].ToString();

                    string ThuePhong_Info = IDThuePhong + " - Checkout: " + NgayCheckOut;
                    comboBox_HD_IDThuePhong.Items.Add(ThuePhong_Info);
                }
            }
        }

        void HD_Check_Phong(string IDThuePhong)
        {
            string cmd_PHONG = "SELECT PHONG.TENPHONG, LOAIPHONG.TENLOAIPHONG, LOAIPHONG.GIA, LOAIPHONG.PHUTHU, BANGTHUEPHONG.SoNguoi, LOAIPHONG.SONGUOITOIDA " +
                                "FROM BANGTHUEPHONG " +
                                "INNER JOIN PHONG ON BANGTHUEPHONG.IDPhong = PHONG.ID " +
                                "INNER JOIN LOAIPHONG ON PHONG.IDLOAIPHONG = LOAIPHONG.ID " +
                                "WHERE (BANGTHUEPHONG.ID=" + IDThuePhong + ")";

            DataTable Data_Phong = DataProvider.Instance.ExecuteQuery(cmd_PHONG);

            label_HD_Phong_Value.Text = Data_Phong.Rows[0].ItemArray[0].ToString();
            label_HD_LoaiPhong_Value.Text = Data_Phong.Rows[0].ItemArray[1].ToString();
            label_HD_TienPhong_Value.Text = Data_Phong.Rows[0].ItemArray[2].ToString();
            int SoNguoi = int.Parse(Data_Phong.Rows[0].ItemArray[4].ToString());
            int SoNguoiToiDa = int.Parse(Data_Phong.Rows[0].ItemArray[5].ToString());
            if (SoNguoi > SoNguoiToiDa)
                label_HD_PhuThu_Value.Text = Data_Phong.Rows[0].ItemArray[3].ToString();
            else
                label_HD_PhuThu_Value.Text = "0";
        }

        void HD_List_ThueDichVu(string IDThuePhong)
        {
            string cmd_DichVu = "SELECT DICHVU.TENDICHVU, DICHVU.GIA, BANGTHUEDICHVU.SOLUONG " +
                                "FROM BANGTHUEPHONG " +
                                "INNER JOIN BANGTHUEDICHVU ON BANGTHUEDICHVU.IDBANGTHUEPHONG=BANGTHUEPHONG.ID " +
                                "INNER JOIN DICHVU ON BANGTHUEDICHVU.IDDICHVU = DICHVU.ID " +
                                "WHERE (BANGTHUEPHONG.ID=" + IDThuePhong + ")";
            DataTable Data_DichVu = DataProvider.Instance.ExecuteQuery(cmd_DichVu);
            listView_HD_ThueDichVu.Items.Clear();
            double TienDV = 0;
            for (int i = 0; i < Data_DichVu.Rows.Count; i++)
            {
                listView_HD_ThueDichVu.Items.Add(Data_DichVu.Rows[i].ItemArray[0].ToString());
                listView_HD_ThueDichVu.Items[i].SubItems.Add(Data_DichVu.Rows[i].ItemArray[1].ToString());
                listView_HD_ThueDichVu.Items[i].SubItems.Add(Data_DichVu.Rows[i].ItemArray[2].ToString());

                TienDV += double.Parse(Data_DichVu.Rows[i].ItemArray[1].ToString()) * double.Parse(Data_DichVu.Rows[i].ItemArray[2].ToString());
            }

            label_HD_TienDV_Value.Text = TienDV.ToString();

        }

        void HD_TinhTong()
        {
            double Tong = double.Parse(label_HD_TienPhong_Value.Text) + double.Parse(label_HD_TienDV_Value.Text) + double.Parse(label_HD_PhuThu_Value.Text);
            label_HD_Tong_Value.Text = Tong.ToString();
        }

        int HD_Check_HD(string IDThuePhong)
        {
            int index = 0;
            string query = "SELECT * FROM DBO.HOADON";
            DataTable Data_HD = DataProvider.Instance.ExecuteQuery(query);

            for (int i = 0; i < Data_HD.Rows.Count; i++)
            {
                index = int.Parse(Data_HD.Rows[i].ItemArray[0].ToString());
                if (Data_HD.Rows[i].ItemArray[2].ToString() == IDThuePhong)
                    return -1;
            }
            return index + 1;
        }

        // Hàm load ListView Hóa Đơn
        void HD_List_HD()
        {
            string query = "SELECT HOADON.ID, KHACHHANG.HOTEN, PHONG.TENPHONG, NHANVIEN.HOTEN, HOADON.TONGTIEN, HOADON.NGAYLAPHD " +
                            "FROM HOADON " +
                            "INNER JOIN KHACHHANG ON HOADON.IDKHACHHANG = KHACHHANG.ID " +
                            "INNER JOIN BANGTHUEPHONG ON HOADON.IDBANGTHUEPHONG = BANGTHUEPHONG.ID " +
                            "INNER JOIN PHONG ON BANGTHUEPHONG.IDPhong = PHONG.ID " +
                            "INNER JOIN NHANVIEN ON HOADON.IDNHANVIEN = NHANVIEN.ID";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            listView_HD.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listView_HD.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                listView_HD.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                listView_HD.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                listView_HD.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                listView_HD.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                listView_HD.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
            }
        }

        void HD_Reset_Info()
        {
            textBox_HD_CMND.Clear();
            textBox_HD_TenKH.Clear();
            textBox_HD_SDT.Clear();
            textBox_HD_IDKH.Clear();
            textBox_HD_IDThuePhong.Clear();
            comboBox_HD_IDThuePhong.Items.Clear();
            comboBox_HD_IDThuePhong.ResetText();
            label_HD_NgayLapHD_Value.Text = "";
            label_HD_Phong_Value.Text = "";
            label_HD_LoaiPhong_Value.Text = "";
            label_HD_TienPhong_Value.Text = "";
            label_HD_TienDV_Value.Text = "";
            label_HD_Tong_Value.Text = "";
            label_HD_IDHD_Value.Text = "";
            label_HD_PhuThu_Value.Text = "";
            listView_HD_ThueDichVu.Items.Clear();
            label_HD_IDNhanVien_Value.Text = IDNhanVien_SignedIn;
        }

        void HD_Reset_Search()
        {
            textBox_HD_TimIDHD.Text = "ID hóa đơn";
            textBox_HD_TimKH.Text = "Tên khách hàng";
            textBox_HD_TimPhong.Text = "Phòng";
            textBox_HD_TimNgayLap.Text = "Ngày lập HĐ";
            HD_List_HD();
        }

        void HD_View_HD(string IDHD)
        {
            string cmd_HD = "select * from HOADON where (ID = '" + IDHD + "')";
            DataTable Data_HD = DataProvider.Instance.ExecuteQuery(cmd_HD);

            string cmd_KH = "select * from KHACHHANG where (ID='" + Data_HD.Rows[0].ItemArray[1].ToString() + "')";
            HD_Check_KhachHang(cmd_KH);

            string IDThuePhong = Data_HD.Rows[0].ItemArray[2].ToString();
            HD_Check_Phong(IDThuePhong);
            HD_List_ThueDichVu(IDThuePhong);
            
            label_HD_IDNhanVien_Value.Text = Data_HD.Rows[0].ItemArray[3].ToString();
            label_HD_TenNhanVien_Value.Text = listView_HD.SelectedItems[0].SubItems[3].Text;
            label_HD_Tong_Value.Text = Data_HD.Rows[0].ItemArray[4].ToString();
            label_HD_NgayLapHD_Value.Text = Data_HD.Rows[0].ItemArray[5].ToString();

        }

        // Nhấn vào nút Tìm KH
        private void btn_HD_TimKH_Click(object sender, EventArgs e)
        {
            string TenKH = textBox_HD_TenKH.Text.Trim();
            string CMND = textBox_HD_CMND.Text.Trim();
            string SDT = textBox_HD_SDT.Text.Trim();

            if (CMND != "")
            {
                string cmd_KH = "select * from [dbo].[KHACHHANG] where (CMND='" + CMND + "')";
                HD_Check_KhachHang(cmd_KH);
                HD_List_ThuePhong();
            }
            else if (TenKH != "")
            {
                string cmd_KH = "select * from [dbo].[KHACHHANG] where (HOTEN='" + TenKH + "')";
                HD_Check_KhachHang(cmd_KH);
                HD_List_ThuePhong();
            }
            else if (SDT != "")
            {
                string cmd_KH = "select * from [dbo].[KHACHHANG] where (SDT='" + SDT + "')";
                HD_Check_KhachHang(cmd_KH);
                HD_List_ThuePhong();
            }
            else
            {
                MessageBox.Show("Nhập thông tin khách hàng!");
            }
        }

        // Done. Nhấn vào nút tạo hóa đơn
        private void btn_HD_TaoHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn tạo hóa đơn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            label_HD_IDNhanVien_Value.Text = IDNhanVien_SignedIn;

            HOADON hd = new HOADON();
            string IDKhachHang = textBox_HD_IDKH.Text;
            string IDThuePhong = textBox_HD_IDThuePhong.Text.Trim();
            string IDNhanVien = label_HD_IDNhanVien_Value.Text;
            string Tong = label_HD_Tong_Value.Text;
            DateTime NgayLapHD = DateTime.Now;
            label_HD_NgayLapHD_Value.Text = NgayLapHD.ToString();

            int IDHD = HD_Check_HD(IDThuePhong);

            hd.ID = IDHD;
            hd.IDKhachHang = int.Parse(IDKhachHang);
            hd.IDThuePhong = int.Parse(IDThuePhong);
            hd.IDNhanVien = int.Parse(IDNhanVien);
            hd.TongTien = int.Parse(Tong);
            hd.NgayLapHD = NgayLapHD;

            if (IDKhachHang == "" || IDThuePhong == "" || IDNhanVien == "" || Tong == "")
            {
                MessageBox.Show("Nhập thiếu thông tin!");
                return;
            }
            else if (IDHD == -1)
            {
                MessageBox.Show("Hóa đơn đã tồn tại!");
                return;
            }
            int KQ = hd.TaoHoaDon();

            if (KQ <= 0)
            {
                MessageBox.Show("Thêm hóa đơn lỗi!");
                return;
            }

            MessageBox.Show("Thêm hóa đơn thành công!");
            HD_List_HD();

        }

        // Done. Chọn từ comboBox ID Thuê phòng
        private void comboBox_HD_IDThuePhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] s = comboBox_HD_IDThuePhong.SelectedItem.ToString().Trim().Split('-');
            string IDThuePhong = s[0];
            textBox_HD_IDThuePhong.Text = IDThuePhong;

            //string cmd_ThueDV = "select * from dbo.BANGTHUEDICHVU where (IDBANGTHUEPHONG='" + textBox_HD_IDThuePhong.Text + "')";
            //DataTable dt = DataProvider.Instance.ExecuteQuery(cmd_ThueDV);

            HD_Check_Phong(IDThuePhong);
            HD_List_ThueDichVu(IDThuePhong);
            HD_TinhTong();
        }

        // Done. Chọn 1 hóa đơn từ listview hóa đơn
        private void listView_HD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_HD.SelectedItems.Count < 1)
                return;
            HD_Reset_Info();
            ListViewItem lvi = listView_HD.SelectedItems[0];
            label_HD_IDHD_Value.Text = lvi.SubItems[0].Text;
            string IDHD = lvi.SubItems[0].Text;

            HD_View_HD(IDHD);

        }

        // Done. Hộp tìm kiếm hóa đơn từ ID hóa đơn
        private void textBox_HD_TimIDHD_TextChanged(object sender, EventArgs e)
        {
            HD_List_HD();
            foreach (ListViewItem Items in listView_HD.Items)
            {
                if (Items.SubItems[0].Text == textBox_HD_TimIDHD.Text)
                {
                    listView_HD.Items.Clear();
                    listView_HD.Items.Add(Items);
                }
            }
            if (textBox_HD_TimIDHD.Text == "")
            {
                HD_List_HD();
            }
        }

        // Done. Hộp tìm kiếm hóa đơn từ tên khách hàng
        private void textBox_HD_TimKH_TextChanged(object sender, EventArgs e)
        {
            HD_List_HD();
            int tmp = 0;
            foreach (ListViewItem Items in listView_HD.Items)
            {
                if (Items.SubItems[1].Text == textBox_HD_TimKH.Text)
                {
                    if (tmp == 0)
                    {
                        listView_HD.Items.Clear();
                        tmp = 1;
                    }
                    listView_HD.Items.Add(Items);
                }
            }
            if (textBox_HD_TimKH.Text == "")
            {
                HD_List_HD();
            }
        }

        // Done. Hộp tìm kiếm hóa đơn từ tên phòng
        private void textBox_HD_TimPhong_TextChanged(object sender, EventArgs e)
        {
            HD_List_HD();
            int tmp = 0;
            foreach (ListViewItem Items in listView_HD.Items)
            {
                if (Items.SubItems[2].Text == textBox_HD_TimPhong.Text)
                {
                    if (tmp == 0)
                    {
                        listView_HD.Items.Clear();
                        tmp = 1;
                    }
                    listView_HD.Items.Add(Items);
                }
            }
            if (textBox_HD_TimPhong.Text == "")
            {
                HD_List_HD();
            }
        }

        // Done. Hộp tìm kiếm hóa đơn từ ngày lập
        private void textBox_HD_TimNgayLap_TextChanged(object sender, EventArgs e)
        {
            HD_List_HD();
            int tmp = 0;
            foreach (ListViewItem Items in listView_HD.Items)
            {
                
                string[] NgayLap = Items.SubItems[5].Text.Split(' ');
                //string[] GioLap_Split = NgayLap[1].Split(':');
                //string GioLap = GioLap_Split[0] + "h" + GioLap_Split[1];
                if (NgayLap[0] == textBox_HD_TimNgayLap.Text)
                {
                    if (tmp == 0)
                    {
                        listView_HD.Items.Clear();
                        tmp = 1;
                    }
                    listView_HD.Items.Add(Items);
                }
                //else if (GioLap == textBox_HD_TimNgayLap.Text)
                //{
                //    if (tmp == 0)
                //    {
                //        listView_HD.Items.Clear();
                //        tmp = 1;
                //    }
                //    //listView_HD.Items.Clear();
                //    listView_HD.Items.Add(Items);
                //}
            }
            if (textBox_HD_TimNgayLap.Text == "")
            {
                HD_List_HD();
            }
        }

        // Done. Nhấn vào nút xóa hóa đơn
        private void btn_HD_XoaHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa hóa đơn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string cmd_XoaHD = "DELETE FROM HOADON where (ID='" + label_HD_IDHD_Value.Text + "')";

            int KQ = DataProvider.Instance.ExecuteNonQuery(cmd_XoaHD);

            if (KQ <= 0)
            {
                MessageBox.Show("Xóa hóa đơn lỗi!");
                return;
            }

            MessageBox.Show("Xóa hóa đơn thành công!");
            HD_List_HD();

        }

        // Done. Nút đặt lại tìm kiếm hóa đơn
        private void button_HD_Reset_Search_Click(object sender, EventArgs e)
        {
            HD_Reset_Search();
        }

        //Done. Nút đặt lại thông tin khách hàng và hóa đơn
        private void button_HD_Reset_Info_Click(object sender, EventArgs e)
        {
            HD_Reset_Info();
        }

        // Text Holder Tìm hóa đơn qua ID hóa đơn
        private void textBox_HD_TimIDHD_Leave(object sender, EventArgs e)
        {
            if (textBox_HD_TimIDHD.Text == "")
                textBox_HD_TimIDHD.Text = "ID hóa đơn";
            textBox_HD_TimIDHD.ForeColor = Color.DarkGray;
        }

        private void textBox_HD_TimIDHD_Enter(object sender, EventArgs e)
        {
            if (textBox_HD_TimIDHD.Text == "ID hóa đơn")
                textBox_HD_TimIDHD.Text = null;
            textBox_HD_TimIDHD.ForeColor = Color.Black;
        }

        // Text Holder Tìm hóa đơn qua khách hàng
        private void textBox_HD_TimKH_Enter(object sender, EventArgs e)
        {
            if (textBox_HD_TimKH.Text == "Tên khách hàng")
                textBox_HD_TimKH.Text = null;
            textBox_HD_TimKH.ForeColor = Color.Black;
        }

        private void textBox_HD_TimKH_Leave(object sender, EventArgs e)
        {
            if (textBox_HD_TimKH.Text == "")
                textBox_HD_TimKH.Text = "Tên khách hàng";
            textBox_HD_TimKH.ForeColor = Color.DarkGray;
        }

        // Text Holder Tìm hóa đơn qua phòng
        private void textBox_HD_TimPhong_Enter(object sender, EventArgs e)
        {
            if (textBox_HD_TimPhong.Text == "Phòng")
                textBox_HD_TimPhong.Text = null;
            textBox_HD_TimPhong.ForeColor = Color.Black;
        }

        private void textBox_HD_TimPhong_Leave(object sender, EventArgs e)
        {
            if (textBox_HD_TimPhong.Text == "")
                textBox_HD_TimPhong.Text = "Phòng";
            textBox_HD_TimPhong.ForeColor = Color.DarkGray;
        }

        // Text Holder Tìm hóa đơn qua ngày lập
        private void textBox_HD_TimNgayLap_Enter(object sender, EventArgs e)
        {
           if (textBox_HD_TimNgayLap.Text == "Ngày lập HĐ")
                textBox_HD_TimNgayLap.Text = null;
            textBox_HD_TimNgayLap.ForeColor = Color.Black;
        }

        private void textBox_HD_TimNgayLap_Leave(object sender, EventArgs e)
        {
            if (textBox_HD_TimNgayLap.Text == "")
                textBox_HD_TimNgayLap.Text = "Ngày lập HĐ";
            textBox_HD_TimNgayLap.ForeColor = Color.DarkGray;
        }

        #endregion

        #region Quanlybaocao
        string BC_TinhDoanhThu(string Thang, string Nam)
        {
            string query_doanhthu = "select SUM(HOADON.TONGTIEN) " +
                                    "from HOADON " +
                                    "where (month(HOADON.NGAYLAPHD) = '" + Thang + "' and year(HOADON.NGAYLAPHD) = '" + Nam + "')";

            DataTable Data_DoanhThu = DataProvider.Instance.ExecuteQuery(query_doanhthu);
            string DoanhThu = Data_DoanhThu.Rows[0].ItemArray[0].ToString(); // tinh doanh thu = sum (hoa don)

            return DoanhThu;
        }

        // Kiểm tra có phải là số hay không
        bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (Char.IsDigit(c) == false)
                {
                    MessageBox.Show(c.ToString());
                    return false;
                }
            }
            return true;
        }

        // xác nhận thêm báo cáo
        bool BC_Xacnhan(String message)
        {
            string title = "Xác nhận";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        void BC_Load_BaoCao(string query = "select BC.ID, BC.TENBAOCAO, BC.THANGBAOCAO, BC.NAMBAOCAO, NV.ID, NV.HOTEN, BC.NGAYLAPBC, BC.DOANHTHU " +
                                            "from BAOCAO BC inner join NHANVIEN NV on BC.IDNHANVIEN = NV.ID")
        {
            listView_BC.Items.Clear();
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            int i;
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                listView_BC.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                listView_BC.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                listView_BC.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                listView_BC.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                listView_BC.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                listView_BC.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                listView_BC.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                listView_BC.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
            }

        }

        // Hàm tìm báo cáo theo tên báo cáo
        private void textBox_BC_TimTenBC_TextChanged(object sender, EventArgs e)
        {
            int tmp = 0;
            BC_Load_BaoCao();
            foreach (ListViewItem Items in listView_BC.Items)
            {
                if (Items.SubItems[1].Text == textBox_BC_TimTenBC.Text)
                {
                    if(tmp == 0)
                    {
                        tmp = 1;
                        listView_BC.Items.Clear();
                    }    
                    listView_BC.Items.Add(Items);
                }
            }
            if (textBox_BC_TimTenBC.Text == "")
            {
                BC_Load_BaoCao();
            }
        }

        // Hàm tìm tháng báo cáo
        private void textBox_BC_TimThangBC_TextChanged(object sender, EventArgs e)
        {
            BC_Load_BaoCao();
            int tmp = 0;
            string[] ThangBC = comboBox_BC_TimThangBC.SelectedItem.ToString().Split(' ');
            foreach (ListViewItem Items in listView_BC.Items)
            {   
                if (Items.SubItems[2].Text == ThangBC[1])
                {
                    if (tmp == 0)
                    {
                        tmp = 1;
                        listView_BC.Items.Clear();
                    }
                    listView_BC.Items.Add(Items);
                }
            }
            if (ThangBC[1] == "")
            {
                BC_Load_BaoCao();
            }
        }
        
        // Hàm tìm năm báo cáo
        private void textBox_BC_TimNamTimNamBC_TextChanged(object sender, EventArgs e)
        {
            BC_Load_BaoCao();
            int tmp = 0;
            foreach (ListViewItem Items in listView_BC.Items)
            {
                if (Items.SubItems[3].Text == textBox_BC_TimNamBC.Text)
                {
                    if (tmp == 0)
                    {
                        tmp = 1;
                        listView_BC.Items.Clear();
                    }
                    listView_BC.Items.Add(Items);
                }
            }
            if (textBox_BC_TimNamBC.Text == "")
            {
                BC_Load_BaoCao();
            }
        }

        
        private void button_BC_ThemBC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm báo cáo?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            // Chuẩn bị thông tin báo cáo
            string TenBC = textBox_BC_TenBC.Text;

            string[] com_ThangBC = comboBox_BC_ThangBC.SelectedItem.ToString().Split(' ');
            string ThangBC = com_ThangBC[1];

            string NamBC = textBox_BC_NamBC.Text.Trim();
            string IDNV = IDNhanVien_SignedIn;
            string TenNV = TenNhanVien_SignedIn;
            DateTime NgayLapBC = DateTime.Now;
            string DoanhThu = BC_TinhDoanhThu(ThangBC, NamBC);

            BAOCAO bc = new BAOCAO();
            bc.TenBaoCao = TenBC;
            bc.ThangBaoCao = int.Parse(ThangBC);
            bc.NamBaoCao = int.Parse(NamBC);
            bc.IDNhanVien = int.Parse(IDNV);
            bc.NgayLapBC = NgayLapBC;
            bc.DoanhThu = DoanhThu;

            // Kiểm tra dữ liệu
            if (textBox_BC_TenBC.Text.Trim() == "" || textBox_BC_NamBC.Text.Trim() == "" || comboBox_BC_ThangBC.Text.Trim() == "")
            {
                MessageBox.Show("Nhập thiếu thông tin!");
                return;
            }
            else if (IsNumber(NamBC) == false)
            {
                MessageBox.Show("Nhập sai kiểu dữ liệu!");
                return;
            }


            if (BC_Xacnhan("Xác nhận thêm báo cáo") == false)
            {
                return;
            }

            int KQ = bc.ThemBaoCao();

            if (KQ <= 0)
            {
                MessageBox.Show("Thêm báo cáo lỗi!");
                return;
            }


            MessageBox.Show("Thêm báo cáo thành công!");
            listView_BC.Items.Clear();
            BC_Load_BaoCao();
        }

        private void listView_BC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_BC.SelectedItems.Count == 0) return;
            // Lay phan tu duoc chon tren listview
            ListViewItem lvi = listView_BC.SelectedItems[0];
            // Hien thi thong tin tu listview sang cac textbox
            textBox_BC_IDBC.Text = lvi.SubItems[0].Text;
            textBox_BC_TenBC.Text = lvi.SubItems[1].Text;
            comboBox_BC_ThangBC.Text = lvi.SubItems[2].Text;
            textBox_BC_NamBC.Text = lvi.SubItems[3].Text;
            tb_BC_IDNV.Text = lvi.SubItems[4].Text;
            tb_BC_TenNV.Text = lvi.SubItems[5].Text;
            tb_BC_NgayLapBC.Text = lvi.SubItems[6].Text;
            tb_BC_DoanhThu.Text = lvi.SubItems[7].Text;
        }

        #endregion

        #region DangXuat_Doimatkhau


        NHANVIEN nhanvien;
        public Form1(NHANVIEN nHANVIEN)
        {
            InitializeComponent();
            if (nHANVIEN.HOTEN != null)
            {
                this.nhanvien = nHANVIEN;
                this.lb_form1_tennv.Text = "Chào " + nhanvien.HOTEN + "";
                this.lb_form1_tennv.Visible = true;
                IDNhanVien_SignedIn = nhanvien.ID.ToString();
                TenNhanVien_SignedIn = nhanvien.HOTEN.ToString();
            }
            else
            {
                this.lb_form1_tennv.Visible = false;
            }
            databind();
        }
        private void databind()
        {

            txb_hoten.DataBindings.Clear();
            txb_cmnd.DataBindings.Clear();
            txb_sdt.DataBindings.Clear();

            txb_hoten.DataBindings.Add("Text", nhanvien, "HOTEN", true, DataSourceUpdateMode.OnPropertyChanged);
            txb_cmnd.DataBindings.Add("Text", nhanvien, "CMND", true, DataSourceUpdateMode.OnPropertyChanged);
            txb_sdt.DataBindings.Add("Text", nhanvien, "SDT", true, DataSourceUpdateMode.OnPropertyChanged);
            if (nhanvien.GIOITINH.Equals("true"))
            {
                rbtn_nam.Checked = true;
            }
            else
            {
                rbtn_nu.Checked = true;
            }

        }

        private void btn_xacnhan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đổi mật khẩu?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (nhanvien != null)
            {
                if (txb_matkhaucu.Text == "" && txb_matkhaucu.Text == "" && txb_matkhauxacnhan.Text == "")
                {
                    MessageBox.Show("Không được bỏ trống!");
                    return;
                }
                if (!txb_matkhaumoi.Text.Equals(txb_matkhauxacnhan.Text))
                {
                    MessageBox.Show("Mật khẩu mới và mật khẩu xác nhận không giống nhau! Nhập lại!");
                    return;
                }
                else if (txb_matkhauxacnhan.Text.Equals(txb_matkhaucu.Text))
                {
                    MessageBox.Show("Mật khẩu cũ và mới giống nhau! Thử mật khẩu khác!");
                    return;
                }

                {
                    NGUOIDUNG n = GetNGUOIDUNG(nhanvien.ID);
                    if (n == null)
                    {
                        MessageBox.Show("Lỗi hệ thống");
                        return;
                    }
                    if (n.MATKHAU == txb_matkhaucu.Text)
                    {

                        if (HasNonASCIIChars(txb_matkhaucu.Text))
                        {
                            MessageBox.Show("Mật khẩu mới chứa kí tự đặc biệt");
                            return;
                        }
                        else if (txb_matkhaucu.Text.Contains(" "))
                        {
                            MessageBox.Show("Mật khẩu mới chứa kí tự khoảng trắng");
                            return;
                        }
                        else
                        {
                            n.MATKHAU = txb_matkhaumoi.Text;
                            bool ketqua = updateNguoidung(n);

                            if (ketqua)
                            {
                                MessageBox.Show("Thành công", "Thông báo");
                                phong_control.Visible = true;
                                khachhang_control.Visible = false;
                                nhanvien_control.Visible = false;
                                dichvu_control.Visible = false;
                                hoadon_control.Visible = false;
                                baocao_control.Visible = false;
                                DoiMatKhau_control.Visible = false;
                            }
                            else
                            {
                                MessageBox.Show("Thất bại", "Thông báo");
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu cũ nhập sai! Vui lòng nhập lại");
                    }
                }
            }
            else
            {
                MessageBox.Show("Không được phép!");
                return;
            }

        }
        bool HasNonASCIIChars(string str)
        {
            return (System.Text.Encoding.UTF8.GetByteCount(str) != str.Length);
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            phong_control.Visible = true;
            khachhang_control.Visible = false;
            nhanvien_control.Visible = false;
            dichvu_control.Visible = false;
            hoadon_control.Visible = false;
            baocao_control.Visible = false;
            DoiMatKhau_control.Visible = false;
        }

        private NGUOIDUNG GetNGUOIDUNG(int IDNHANVIEN)
        {
            NGUOIDUNG login_user = new NGUOIDUNG();
           
            
            string query = "Select IDNHANVIEN, TAIKHOAN, MATKHAU from NGUOIDUNG where IDNHANVIEN = '" + IDNHANVIEN + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                login_user.IDNHANVIEN = IDNHANVIEN;
                login_user.TAIKHOAN = dt.Rows[i].ItemArray[1].ToString();
                login_user.MATKHAU = dt.Rows[i].ItemArray[2].ToString();
            }
            return login_user;
        }
        private bool updateNguoidung(NGUOIDUNG nguoidung)
        {
            string query = "UPDATE NGUOIDUNG SET MATKHAU = '" + nguoidung.MATKHAU + "' WHERE IDNHANVIEN = '" + nhanvien.ID + "'";
            int dt = DataProvider.Instance.ExecuteNonQuery(query);
            while (dt > 0)
            {
                return true;
            }
            return false;
        }


        #endregion
    
        #region QuanLyNhanVien
        //Tab control
        bool DV_ktDulieu(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= 48 && str[i] <= 57) == false)
                {
                    if (str[i] != '.')
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        
        // kiểm tra dữ liệu nhập vào
        bool ktDuLieuNV()
        {
            string ten = tb_NV_tenNV.Text.Trim();
            string sdt = tb_NV_sdtNV.Text.Trim();
            string cmnd = tb_NV_cmndNV.Text.Trim();
            string diachi = tb_NV_dcNV.Text.Trim();
            string luongcb = tb_NV_lcbNV.Text.Trim();
            string chucvu = cb_NV_chucvu.Text.Trim();

            string errorTen = "";
            string errorSDT = "";
            string errorCMND = "";
            string errorDiaChi = "";
            string errorLuongcb = "";
            string errorChucVu = "";

            bool check = false;
            if (ten.Trim() == "")
            {
                errorTen = "Nhập thiếu tên!\n";
                check = true;
            }

            if (chucvu.Trim() == "")
            {
                errorChucVu = "Nhập thiếu chức vụ!\n";
                check = true;
            }

            if (diachi.Trim() == "")
            {
                errorDiaChi = "Nhập thiếu địa chỉ!\n";
                check = true;
            }

            if (tb_NV_lcbNV.Text.Trim() != "")
            {
                //luongcb = tb_NV_lcbNV.Text.Trim().Split('.');
                if (DV_ktDulieu(luongcb) == false)
                {
                    errorLuongcb = "Nhập sai lương!\n";
                    check = true;
                }
            }
            else
            {
                errorLuongcb = "Nhập thiếu lương!\n";
                check = true;
            }

            if (sdt.Length == 0)
            {
                errorSDT = "Nhập thiếu số điện thoại!\n";
                check = true;
            }
            else
            {
                if (sdt.Length != 10)
                {
                    errorSDT = "Nhập sai số điện thoại!\n";
                    check = true;
                }
                else
                {
                    if (DV_ktDulieu(sdt) == false)
                    {
                        errorSDT = "Nhập sai số điện thoại!\n";
                        check = true;
                    }

                }
            }

            if (cmnd.Length == 0)
            {
                errorCMND = "Nhập thiếu CMND/CCCD!\n";
                check = true;
            }
            else
            {
                if (cmnd.Length == 9 || cmnd.Length == 12)
                {
                    if (DV_ktDulieu(cmnd) == false)
                    {
                        errorCMND = "Nhập sai CMND/CCCD!\n";
                        check = true;
                    }
                }
                else
                {
                    errorCMND = "Nhập sai CMND/CCCD!\n";
                    check = true;
                }
            }


            if (check)
            {
                DialogResult result = MessageBox.Show(errorTen + errorSDT + errorCMND + errorDiaChi + errorLuongcb + errorChucVu, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }

        // Load data to listview
        void NV_loadNV(string query = "select * from NHANVIEN NV INNER JOIN CHUCVU CV ON NV.IDCHUCVU = CV.ID")
        {
            listView_NhanVien.Items.Clear();
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            int i;
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                listView_NhanVien.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                listView_NhanVien.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                listView_NhanVien.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                listView_NhanVien.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                listView_NhanVien.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                listView_NhanVien.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                listView_NhanVien.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                listView_NhanVien.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());
            }

        }
        //Refresh
        private void btn_NV_RefreshNV_Click(object sender, EventArgs e)
        {
            listView_NhanVien.Items.Clear();
            tb_NV_idNV.Clear();
            tb_NV_tenNV.Clear();
            tb_NV_sdtNV.Clear();
            tb_NV_cmndNV.Clear();
            tb_NV_dcNV.Clear();
            tb_NV_dcNV.Clear();
            tb_NV_lcbNV.Clear();
            radiobtn_NV_GTnam.Checked = false;
            radiobtn_NV_GTnu.Checked = false;
            cb_NV_chucvu.SelectedIndex = -1;
            NV_loadNV();
        }

        // Binding listview
        private void listView_NhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_NhanVien.SelectedItems.Count == 0) return;
            
            btn_NV_suaNV.Enabled = true;
            btn_NV_xoaNV.Enabled = true;

            ListViewItem lvi = listView_NhanVien.SelectedItems[0];

            tb_NV_idNV.Text = lvi.SubItems[0].Text;
            tb_NV_tenNV.Text = lvi.SubItems[1].Text;
            tb_NV_sdtNV.Text = lvi.SubItems[3].Text;
            tb_NV_cmndNV.Text = lvi.SubItems[4].Text;
            tb_NV_dcNV.Text = lvi.SubItems[5].Text;
            tb_NV_lcbNV.Text = lvi.SubItems[6].Text;
            string gender = lvi.SubItems[2].Text;
            cb_NV_chucvu.Text = lvi.SubItems[7].Text;
            if (gender == "Nữ")
            {
                radiobtn_NV_GTnu.Checked = true;
            }
            else
            {
                radiobtn_NV_GTnam.Checked = true;
            }
        }

    

        // Tìm kiếm nhân viên
        private void tb_NV_TimKiemNV_TextChanged(object sender, EventArgs e)
        {
            string tenNV = tb_NV_TimKiemNV.Text.Trim();
            string query = "select* from NHANVIEN NV INNER JOIN CHUCVU CV ON NV.IDCHUCVU = CV.ID where NV.HOTEN like N'" + tenNV + "%'";
            NV_loadNV(query);
        }

        //Thêm nhân viên
        private void btn_NV_themNV_Click(object sender, EventArgs e)
        {
            string tennv = tb_NV_tenNV.Text.Trim();
            string gt;
            if (radiobtn_NV_GTnam.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            string sdt = tb_NV_sdtNV.Text.Trim();
            string cmnd = tb_NV_cmndNV.Text.Trim();
            string diachi = tb_NV_dcNV.Text.Trim();
            string chucvu = this.cb_NV_chucvu.GetItemText(this.cb_NV_chucvu.SelectedItem);
            NHANVIEN NV = new NHANVIEN();
            NV.HOTEN = tennv;
            NV.GIOITINH = gt;
            NV.SDT = sdt;
            NV.CMND = cmnd;
            NV.DIACHI = diachi;
            string query = "select ID from CHUCVU where TENCHUCVU = N'" + chucvu + "'";

            if (ktDuLieuNV())
            {
                return;
            }

            decimal luongcb = 0;
            luongcb = decimal.Parse(tb_NV_lcbNV.Text.Trim());
            NV.LUONGCB = luongcb;
            int idcv = Int32.Parse(DataProvider.Instance.ExecuteScalar(query).ToString());
            NV.IDCHUCVU = idcv;
            query = "SELECT COUNT(*) FROM NHANVIEN WHERE NHANVIEN.CMND = N'" + cmnd + "'";
            int k = Int32.Parse(DataProvider.Instance.ExecuteScalar(query).ToString());

            if (k != 0)
            {
                DialogResult result = MessageBox.Show("Nhân viên đã tồn tại!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DV_Xacnhan("Bạn có đồng ý thêm nhân viên?") == true)
            {
                int kq = NV.ThemNhanVien();
            }
            else
            {
                return;
            }


            MessageBox.Show("Thêm nhân viên thành công!");
            listView_NhanVien.Items.Clear();
            NV_loadNV();

            query = "SELECT ID FROM NHANVIEN WHERE NHANVIEN.CMND = N'" + cmnd + "'";
            string id = DataProvider.Instance.ExecuteScalar(query).ToString();
            query = "INSERT[dbo].[NGUOIDUNG]([TAIKHOAN], [MATKHAU], [IDNHANVIEN]) VALUES(N'NV" + id + "', N'1', " + id + ")";
            DataProvider.Instance.ExecuteNonQuery(query);
            MessageBox.Show("Thêm tài khoản mật khẩu thành công");

        }

        private void btn_NV_suaNV_Click(object sender, EventArgs e)
        {
            if (ktDuLieuNV())
            {
                return;
            }
            string chucvu = this.cb_NV_chucvu.GetItemText(this.cb_NV_chucvu.SelectedItem);
            string query = "select ID from CHUCVU where TENCHUCVU = N'" + chucvu + "'";

            int idnv = int.Parse(tb_NV_idNV.Text.Trim());

            string tennv = tb_NV_tenNV.Text.Trim();
            string gt;

            if (radiobtn_NV_GTnam.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            string sdt = tb_NV_sdtNV.Text.Trim();
            string cmnd = tb_NV_cmndNV.Text.Trim();
            string diachi = tb_NV_dcNV.Text.Trim();
            decimal luongcb = decimal.Parse(tb_NV_lcbNV.Text.Trim());

            if ((radiobtn_NV_GTnam.Checked == false && radiobtn_NV_GTnu.Checked == false) || tennv == "" || diachi == "")
            {
                DialogResult result = MessageBox.Show("Nhập thiếu thông tin!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idcv = Int32.Parse(DataProvider.Instance.ExecuteScalar(query).ToString());
            NHANVIEN NV = new NHANVIEN();
            NV.ID = idnv;
            NV.HOTEN = tennv;
            NV.GIOITINH = gt;
            NV.SDT = sdt;
            NV.CMND = cmnd;
            NV.DIACHI = diachi;
            NV.IDCHUCVU = idcv;
            NV.LUONGCB = luongcb;
            if (DV_Xacnhan("Bạn có đồng ý sửa nhân viên?") == true)
            {
                NV.SuaNhanVien();
            }
            else
            {
                return;
            }
            MessageBox.Show("Sửa thông tin nhân viên thành công");
            NV_loadNV();
        }

        //Xóa nhân viên
        private void btn_NV_xoaNV_Click(object sender, EventArgs e)
        {
            if (IDNhanVien_SignedIn == tb_NV_idNV.Text)
            {
                MessageBox.Show("Không được xóa chính mình?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idnv = int.Parse(tb_NV_idNV.Text.Trim());
            NHANVIEN NV = new NHANVIEN();
            NV.ID = idnv;
            string query = "SELECT COUNT (*) FROM NHANVIEN AS NV, HOADON AS HD WHERE NV.ID = HD.IDNhanVien AND NV.ID = " + idnv;
            int kq = int.Parse(DataProvider.Instance.ExecuteScalar(query).ToString());
            if (kq > 0)
            {
                MessageBox.Show("Dữ liệu nhân viên đang được dùng", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DV_Xacnhan("Bạn có đồng ý xóa nhân viên?") == true)
            {
                NV.XoaNhanVien();
            }
            else
            {
                return;
            }


            MessageBox.Show("Xóa nhân viên thành công!");
            NV_loadNV();
        }

        #endregion

    }
}
