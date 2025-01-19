using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared
{
    public enum OrderStatus
    {
        Pending,            // Sipariş beklemede
        Confirmed,          // Sipariş onaylandı
        Preparing,          // Sipariş hazırlanıyor
        Ready,              // Sipariş hazır
        OutForDelivery,     // Sipariş yolda
        Delivered,          // Sipariş teslim edildi
        Cancelled           // Sipariş iptal edildi
    }
}
