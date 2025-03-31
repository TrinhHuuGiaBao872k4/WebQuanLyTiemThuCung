using Nike.Models; // Đảm bảo namespace phù hợp với dự án của bạn
using System;

namespace Nike.DesignPattern
{
    public sealed class DbContextSingleton
    {
        // Sử dụng Lazy<T> để đảm bảo instance chỉ được tạo khi cần
        private static readonly Lazy<QuanLySanPhamEntities> _instance =
            new Lazy<QuanLySanPhamEntities>(() => new QuanLySanPhamEntities());

        // Property để truy cập instance
        public static QuanLySanPhamEntities Instance => _instance.Value;

        // Constructor private để ngăn việc tạo instance từ bên ngoài
        private DbContextSingleton() { }
    }
}