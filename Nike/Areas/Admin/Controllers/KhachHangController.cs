﻿using Nike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        private QuanLySanPhamEntities _db = new QuanLySanPhamEntities();
        public ActionResult Index(string searchStr)
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv.MaChucVu == 1)
            {
                return RedirectToAction("Index", "Order");
            }
            var dsKhachHang = _db.KhachHangs.ToList();

            // Tìm kiếm khách hàng trong quản lí khách hàng bằng email
            if (!String.IsNullOrEmpty(searchStr))
            {
                searchStr = searchStr.ToLower();
                dsKhachHang = _db.KhachHangs.Where(s => !string.IsNullOrEmpty(s.Email) && s.Email.ToLower().Contains(searchStr)).ToList();
                ViewBag.dsKH = dsKhachHang;
            }
            else
            {
                ViewBag.dsKH = dsKhachHang;
            }

            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachhang = _db.KhachHangs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }
    }
}