// File: DesignPattern/TemplateMethodPattern/CrudTemplate.cs
using System;
using System.Web;
using System.Web.Mvc;

namespace Nike.DesignPattern.TemplateMethodPattern
{
    public abstract class CrudTemplate<T> where T : class
    {
        protected readonly QuanLySanPhamEntities _db;

        public CrudTemplate(QuanLySanPhamEntities db)
        {
            _db = db;
        }

        // Phương thức template cho thao tác Create
        public ActionResult ExecuteCreate(
            T entity,
            HttpPostedFileBase file,
            Action<T> beforeSave = null,
            Func<ActionResult> successResult = null,
            Func<ActionResult> errorResult = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProcessFileUpload(entity, file);
                    beforeSave?.Invoke(entity);
                    
                    _db.Set<T>().Add(entity);
                    _db.SaveChanges();
                    
                    return successResult != null ? successResult() : new RedirectResult("Index");
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return errorResult != null ? errorResult() : new ViewResult();
        }

        // Phương thức template cho thao tác Update
        public ActionResult ExecuteUpdate(
            T model,
            T existing,
            HttpPostedFileBase file,
            Action<T> updateAction,
            Func<ActionResult> successResult = null,
            Func<ActionResult> errorResult = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProcessFileUpload(model, file);
                    updateAction(existing);
                    
                    _db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    
                    return successResult != null ? successResult() : new RedirectResult("Index");
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return errorResult != null ? errorResult() : new ViewResult();
        }

        protected abstract void ProcessFileUpload(T entity, HttpPostedFileBase file);
        
        protected virtual void LogError(Exception ex)
        {
            // Có thể thêm logging vào file/DB ở đây
            System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
        }
    }
}