﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBH.Model_Class;
using QLBH.Repository;
using System.Data.SqlClient;
using System.Data.Common;


namespace QLBH
{
    public partial class frm_HoSoNhanVien : DevExpress.XtraEditors.XtraForm
    {
        List<Class_Que> list_que= new List<Class_Que>();
        List<Class_NhanVien> nv;
        ConnectAndQuery query = new ConnectAndQuery();
        public frm_HoSoNhanVien()
        {
            InitializeComponent();
            //  list_que = staticData._Que;
        }

        private void add()
        {
            cbb_gioitinh.Items.Add("Nam");
            cbb_gioitinh.Items.Add("Nữ");
            cbb_gioitinh.Items.Add("khác");
        }
        private void fill()
        {
            dataGridView1.DataSource = query.DocBang("select * from NhanVien");
        }
        private List<Class_NhanVien> Select()
        {
            string sql = "SELECT * FROM NhanVien";
            List<Class_NhanVien> list = new List<Class_NhanVien>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-RPK6PAD\SQLEXPRESS;Initial Catalog=QLNhaHang;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string MaNhanVien = reader.GetString(0);
                        string TenNhanVien = reader.GetString(1);
                        string GioiTinh = reader.GetString(2);
                        DateTime NgaySinh = reader.GetDateTime(3);
                        string DiaChi = reader.GetString(4);
                        string MaQue = reader.GetString(5);
                        string DienThoai = reader.GetString(6);
                        Class_NhanVien nv = new Class_NhanVien(MaNhanVien, TenNhanVien, GioiTinh, NgaySinh, DiaChi, MaQue, DienThoai);
                        list.Add(nv);
                    }
                }
            }
            con.Close();
            return list;
        }

        private void Button_themmoi_Click(object sender, EventArgs e)
        {
            nv = Select();
            list_que = new Frm_QueQuan().Select();
            bool check = false;
            bool kiemtra = false;

            for(int i=0;i<list_que.Count;i++)
            {
                if (list_que[i].MaQue1.Equals(txt_maque.Text))
                {
                    kiemtra = true;
                    check = false;       
                }
            }

            if (kiemtra == false)
            {
                MessageBox.Show("không có mã "+txt_maque.Text+" này");
            }
            //for(int i=0;i<que)
            for (int i = 0; i < nv.Count; i++)
            {
                if (nv[i].MaNhanVien1.Equals(txt_manv.Text))
                {
                    check = true;
                    MessageBox.Show("đã có mã nhân viên này, vui lòng nhập lại");
                    break;
                }
            }

            if (check == false)
            {
                if (txt_manv.Text.Trim() != "" || txt_tennv.Text.Trim() != "" || cbb_gioitinh.Text.Trim() != "" || txt_diachi.Text.Trim() != "" || txt_dienthoai.Text.Trim() != "" || txt_maque.Text.Trim() != "")
                {
                    string sql = "insert into NhanVien values('" + txt_manv.Text + "',N'" + txt_tennv.Text + "',N'" + cbb_gioitinh.Text + "','" + txt_ngaysinh.Text + "',N'" + txt_diachi.Text + "','" + txt_maque.Text + "','" + txt_dienthoai.Text + "')";
                    query.CapNhatDuLieu(sql);
                    fill();

                    nv.Add(new Class_NhanVien(txt_manv.Text, txt_tennv.Text, cbb_gioitinh.Text, Convert.ToDateTime(txt_ngaysinh.Text), txt_diachi.Text, txt_maque.Text, txt_dienthoai.Text));
                    txt_manv.Text = "";
                    txt_tennv.Text = "";
                    cbb_gioitinh.Text = "";
                    txt_ngaysinh.Text = "";
                    txt_diachi.Text = "";
                    txt_maque.Text = "";
                    txt_dienthoai.Text = "";
                    txt_manv.Enabled = true;
                }
                else
                {
                    MessageBox.Show("vui lòng nhập đầy đủ thông tin");
                }
            }
        }

        private void Frm_HoSoNhanVien_Load(object sender, EventArgs e)
        {
            fill();
            add();
        }

        private void Button_capnhat_Click(object sender, EventArgs e)
        {
            nv = Select();
            // bool check = false;

            //for(int i=0;i<que)
            //for (int i = 0; i < nv.Count; i++)
            //{
            //    if (nv[i].MaNhanVien1.Equals(txt_manv.Text))
            //    {
            //        check = true;
            //        MessageBox.Show("đã có mã nhân viên này, vui lòng nhập lại");
            //        break;
            //    }
            //}

            //if (check == false)
            //{
            if (txt_manv.Text.Trim() != "" || txt_tennv.Text.Trim() != "" || cbb_gioitinh.Text.Trim() != "" || txt_diachi.Text.Trim() != "" || txt_dienthoai.Text.Trim() != "" || txt_maque.Text.Trim() != "")
            {
                string sql = "update NhanVien set TenNhanVien=N'" + txt_tennv.Text + "',GioiTinh=N'" + cbb_gioitinh.Text + "',NgaySinh=N'" + txt_ngaysinh.Text + "',DiaChi=N'" + txt_diachi.Text + "',MaQue=N'" + txt_maque.Text + "',DienThoai=N'" + txt_dienthoai.Text + "' where MaNhanVien =N'" + txt_manv.Text + "'";
                query.CapNhatDuLieu(sql);
                fill();

                nv.Add(new Class_NhanVien(txt_manv.Text, txt_tennv.Text, cbb_gioitinh.Text, Convert.ToDateTime(txt_ngaysinh.Text), txt_diachi.Text, txt_maque.Text, txt_dienthoai.Text));
                txt_manv.Text = "";
                txt_tennv.Text = "";
                cbb_gioitinh.Text = "";
                txt_ngaysinh.Text = "";
                txt_diachi.Text = "";
                txt_maque.Text = "";
                txt_dienthoai.Text = "";
                txt_manv.Enabled = true;
            }
            else
            {
                MessageBox.Show("vui lòng nhập đầy đủ thông tin ");
            }
            //}
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            txt_manv.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_tennv.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbb_gioitinh.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_ngaysinh.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txt_diachi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txt_maque.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_dienthoai.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txt_manv.Enabled = false;
        }

        private void Button_xoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có muốn xóa không ?", "thông báo", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "delete from NhanVien where MaNhanVien = N'" + txt_manv.Text + "'";
                query.CapNhatDuLieu(sql);
                fill();
                txt_manv.Enabled = true;
                txt_manv.Text = "";
                txt_tennv.Text = "";
                cbb_gioitinh.Text = "";
                txt_ngaysinh.Text = "";
                txt_diachi.Text = "";
                txt_maque.Text = "";
                txt_dienthoai.Text = "";

            }
        }

        private void Button_thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có muốn thoát không ?", "thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Button_timkiem_Click(object sender, EventArgs e)
        {
            string sql = "select * from NhanVien where MaNhanVien is not null";
            if (txt_tkma.Text != "")
            {
                sql += " and MaNhanVien like N'" + txt_tkma.Text.Trim() + "%'";
            }
            if (txt_tkten.Text != "")
            {
                sql += " and TenNhanVien like N'" + txt_tkten.Text.Trim() + "%'";
            }
            DataTable a = query.DocBang(sql);
            dataGridView1.DataSource = a;
        }
    }
}