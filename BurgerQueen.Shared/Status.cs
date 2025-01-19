using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared
{
    public enum Status
    {
        Available,      // Kullanılabilir, satışta
        OutOfStock,     // Stokta kalmadı
        Unavailable,    // Geçici olarak kullanılamaz (örneğin, teknik sorun)
        InPreparation,  // Hazırlık aşamasında, menüye eklenecek
        Discontinued,   // Eski, artık satışta değil
        Seasonal,       // Mevsimlik, belirli dönemlerde satışta
        LimitedEdition, // Sınırlı üretim, belirli bir süre satışta
        Archived,       // Arşivlenmiş, artık menüde yer almıyor ama geçmiş referans için saklanıyor
        SoftDeleted,    // Mantıken silinmiş, ama veritabanında hala mevcut
        PendingReview,  // İnceleme bekleyen (yeni eklenen ürünler için)
        Approved,       // Onaylanmış, yayına hazır
        Rejected        // Reddedilmiş, değişiklik veya onay gerektiriyor
    }
}
