using BurgerQueen.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
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

            _logger.LogError(exception, "İstisna oluştu. Kullanıcı: {UserId}, İstek Yolu: {Path}", user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonim", context.HttpContext.Request.Path);

            context.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
                {
                    Model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier }
                }
            };
            context.ExceptionHandled = true;
        }
    }
}
