// File: DesignPattern/TemplateMethodPattern/NhanVienCrudTemplate.cs
using System.IO;
using System.Web;
using Nike.Models;

namespace Nike.DesignPattern.TemplateMethodPattern
{
    public class NhanVienCrudTemplate : CrudTemplate<NhanVien>
    {
        public NhanVienCrudTemplate(QuanLySanPhamEntities db) : base(db) { }

        protected override void ProcessFileUpload(NhanVien entity, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string uploadPath = HttpContext.Current.Server.MapPath("~/Hinh/NhanVien");
                Directory.CreateDirectory(uploadPath);

                string fullPath = Path.Combine(uploadPath, fileName);
                file.SaveAs(fullPath);
                entity.Picture = fileName;
            }
            else
            {
                entity.Picture = "user.jpg";
            }
        }

        protected override void LogError(Exception ex)
        {
            base.LogError(ex);
            // Có thể thêm xử lý đặc biệt cho nhân viên
        }
    }
}