using Nike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nike.DesignPattern.PrototypePattern
{
    public static class PrototypeFactory
    {
        private static readonly Dictionary<string, IClonableModel<KhachHang>> _prototypes =
            new Dictionary<string, IClonableModel<KhachHang>>();

        public static void AddPrototype(string key, KhachHang prototype)
        {
            _prototypes[key] = new CustomerPrototype(prototype);
        }

        public static KhachHang GetClone(string key)
        {
            if (_prototypes.TryGetValue(key, out var prototype))
            {
                return prototype.DeepCopy();
            }
            throw new KeyNotFoundException($"Prototype with key '{key}' not found");
        }
    }
}
