using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BurgerQueen.UI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var user = context.HttpContext.User;

            // Hata loglaması
            _logger.LogError(exception, "İstisna oluştu. Kullanıcı: {UserId}, İstek Yolu: {Path}", user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonim", context.HttpContext.Request.Path);

            // Hata işleme - bir hata sayfasına yönlendirme
            context.Result = new RedirectToActionResult("Error", "Home", null);
            context.ExceptionHandled = true; // İstisnayı işlediğimizi belirtiyoruz
        }
    }
}
