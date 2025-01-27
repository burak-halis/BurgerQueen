namespace BurgerQueen.UI.Utilities
{
    public class IPAddressMasker
    {
        public static string MaskIpAddress(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress)) return string.Empty;

            if (ipAddress.Contains('.'))
            {
                // IPv4
                var parts = ipAddress.Split('.');
                if (parts.Length == 4)
                {
                    return $"{parts[0]}.{parts[1]}.***.***";
                }
            }
            else if (ipAddress.Contains(':'))
            {
                // IPv6
                string fullIPv6 = ExpandIPv6Address(ipAddress);
                var parts = fullIPv6.Split(':');
                if (parts.Length == 8) // Tam genişletilmiş IPv6 adresi
                {
                    // İlk iki parçayı tut, gerisini maskele
                    var maskedParts = parts.Take(2).Concat(Enumerable.Repeat("***", 6));
                    return string.Join(":", maskedParts);
                }
            }

            // Eğer ne IPv4 ne de IPv6 formatında değilse, orijinal adresi döndür
            return ipAddress;
        }

        private static string ExpandIPv6Address(string ipAddress)
        {
            // Önce adresi parçalara ayır
            var parts = ipAddress.Split(':').ToList();

            // :: kullanımını genişlet
            int emptyIndex = parts.IndexOf("");
            if (emptyIndex != -1)
            {
                int toAdd = 8 - parts.Count + 1;
                parts = parts.Take(emptyIndex)
                             .Concat(Enumerable.Repeat("", toAdd))
                             .Concat(parts.Skip(emptyIndex + 1))
                             .ToList();
            }

            // Her parçanın 4 karaktere tamamlanması
            return string.Join(":", parts.Select(part =>
                part.PadLeft(4, '0')));
        }
    }
}
