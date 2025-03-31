using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class CustomerPrototype : IClonableModel<KhachHang>
{
    private readonly KhachHang _prototype;

    public CustomerPrototype(KhachHang prototype)
    {
        _prototype = prototype;
    }

    public KhachHang ShallowCopy()
    {
        return (KhachHang)_prototype.MemberwiseClone();
    }

    public KhachHang DeepCopy()
    {
        var clone = new KhachHang
        {
            HoTen = _prototype.HoTen,
            Email = _prototype.Email,
            Sdt = _prototype.Sdt,
            DiaChi = _prototype.DiaChi,
            // Copy tất cả các thuộc tính nguyên thủy
            // Riêng các thuộc tính reference cần xử lý đặc biệt
        };
        return clone;
    }
}