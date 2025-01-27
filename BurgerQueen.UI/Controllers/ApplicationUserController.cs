using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models.VM.ApplicationUserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using BurgerQueen.UI.Extensions;


namespace BurgerQueen.UI.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly IEmailSender _emailSender; // Bu interface'i uygulamanızda tanımlamanız gerekecek

        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUserController> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Kullanıcı bulunamadı.");
            }

            var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(authenticatorKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            return View(new Enable2faViewModel
            {
                SharedKey = FormatKey(authenticatorKey),
                AuthenticatorUri = GenerateQrCodeUri(user.Email, authenticatorKey)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication(Enable2faViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Kullanıcı bulunamadı.");
            }

            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                ModelState.AddModelError("Code", "Doğrulama kodu geçersiz.");
                return View(model);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            _logger.LogInformation("Kullanıcı {UserId} için iki faktörlü doğrulama aktifleştirildi.", user.Id);

            var userId = await _userManager.GetUserIdAsync(user);
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            var recoveryCodeList = recoveryCodes.ToList();

            // Kullanıcıya kurtarma kodlarını göstermek için bir sayfaya yönlendiriyoruz
            TempData["RecoveryCodes"] = recoveryCodeList;
            return RedirectToAction(nameof(ShowRecoveryCodes));
        }

        [HttpGet]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Kullanıcı bulunamadı.");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new ApplicationException($"Kullanıcı için iki faktörlü doğrulama zaten devre dışı.");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication(Disable2faViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Kullanıcı bulunamadı.");
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultAuthenticatorProvider);
            if (!string.Equals(token, model.TwoFactorCode, StringComparison.CurrentCultureIgnoreCase))
            {
                ModelState.AddModelError("TwoFactorCode", "Doğrulama kodu geçersiz.");
                return View(model);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            _logger.LogInformation("Kullanıcı {UserId} için iki faktörlü doğrulama devre dışı bırakıldı.", user.Id);
            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        public IActionResult ShowRecoveryCodes()
        {
            var recoveryCodes = (IEnumerable<string>)TempData["RecoveryCodes"];
            if (recoveryCodes == null)
            {
                return RedirectToAction(nameof(Profile));
            }

            return View(recoveryCodes);
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
            return string.Format(
                AuthenticatorUriFormat,
                WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("BurgerQueen")),
                WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(email)),
                unformattedKey);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserRegisterVM userVM)
        {
            if (ModelState.IsValid)
            {
                if (userVM.Password != userVM.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Şifreler eşleşmiyor.");
                    return View(userVM);
                }

                try
                {
                    var user = new ApplicationUser { UserName = userVM.Email, Email = userVM.Email };
                    var result = await _userManager.CreateAsync(user, userVM.Password);

                    if (result.Succeeded)
                    {
                        // E-posta doğrulama linki gönderme
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            "ConfirmEmail",
                            "ApplicationUser",
                            new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme);

                        await _emailSender.SendEmailAsync(user.Email, "E-posta Doğrulama",
                            $"Lütfen e-postanızı doğrulamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

                        return RedirectToAction("EmailConfirmationSent", "Account");
                    }

                    ModelState.AddIdentityErrorsToModelState(result.Errors);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Register işleminde hata oluştu. Email: {Email}", MaskEmail(userVM.Email));
                    ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu.");
                }
            }
            return View(userVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Güvenlik nedeniyle, kullanıcı var olmasa bile "e-posta gönderildi" mesajı döndürüyoruz
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "ApplicationUser",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(model.Email, "Şifre Sıfırlama",
                    $"Şifrenizi sıfırlamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmailConfirmationSent()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    // Hem e-posta hem de kullanıcı adı ile arama yap
                    var user = await _userManager.FindByEmailAsync(model.Email) ?? await _userManager.FindByNameAsync(model.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            if (await _userManager.GetTwoFactorEnabledAsync(user))
                            {
                                // 2FA aktifse, 2FA doğrulama sayfasına yönlendir.
                                return RedirectToAction("LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                            }
                            else if (!await _userManager.IsEmailConfirmedAsync(user))
                            {
                                ModelState.AddModelError(string.Empty, "Lütfen e-postanızı doğrulayın.");
                                await _signInManager.SignOutAsync();
                                return View(model);
                            }
                            return RedirectToLocal(returnUrl);
                        }
                        else if (result.IsLockedOut)
                        {
                            ModelState.AddModelError(string.Empty, $"Hesabınız kilitlendi. Lütfen {TimeSpan.FromMinutes(5).Minutes} dakika sonra tekrar deneyin.");
                            return View("Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Login işleminde hata oluştu. Email: {Email}", MaskEmail(model.Email));
                    ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu.");
                }
            }
            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Kullanıcı iki faktörlü doğrulama için bulunamadı.");
            }

            return View(new LoginWith2faVM
            {
                RememberMe = rememberMe,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faVM model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Kullanıcı iki faktörlü doğrulama için bulunamadı.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Hesabınız kilitlendi. Lütfen daha sonra tekrar deneyin.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Doğrulama kodu geçersiz.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Profile(string userId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Yönetici rolüne sahip kullanıcılar herkesin profilini görebilir
            if (!User.IsInRole("Admin") && userId != null && userId != user.Id)
            {
                return Forbid();
            }

            var profileUser = await _userManager.FindByIdAsync(userId ?? user.Id);
            if (profileUser == null)
            {
                return NotFound();
            }

            var userDTO = new ApplicationUserListDTO
            {
                Id = profileUser.Id,
                UserName = profileUser.UserName,
                Email = profileUser.Email,
                FirstName = profileUser.FirstName,
                LastName = profileUser.LastName
            };

            return View(userDTO);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new ApplicationUserUpdateVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(userDTO);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ApplicationUserUpdateVM userVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userVM.Id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.Email = userVM.Email;
                    user.FirstName = userVM.FirstName;
                    user.LastName = userVM.LastName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Profile));
                    }
                    ModelState.AddIdentityErrorsToModelState(result.Errors);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Profil güncellenirken hata oluştu. Email: {Email}", MaskEmail(userVM.Email));
                    ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu.");
                }
            }
            return View(userVM);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        ModelState.AddModelError(string.Empty, "Yeni şifreler eşleşmiyor.");
                        return View(model);
                    }

                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("Login");
                    }
                    ModelState.AddIdentityErrorsToModelState(result.Errors);
                }
                catch (Exception ex)
                {
                    var user = await _userManager.GetUserAsync(User);
                    _logger.LogError(ex, "Şifre değiştirilirken hata oluştu. Email: {Email}", MaskEmail(user?.Email));
                    ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu.");
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccountConfirmed()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddIdentityErrorsToModelState(result.Errors);
            }
            catch (Exception ex)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogError(ex, "Hesap silinirken hata oluştu. Email: {Email}", MaskEmail(user?.Email));
                ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu.");
            }
            return View("DeleteAccount");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;
            var parts = email.Split('@');
            if (parts.Length != 2) return email; // Eğer geçerli bir e-posta formatı yoksa, olduğu gibi döndür

            var name = parts[0];
            var domain = parts[1];
            if (name.Length <= 3) return email; // Çok kısa isimler için tamamını gizleme

            var maskedName = name.Substring(0, 1) + new string('*', Math.Max(0, name.Length - 2)) + name.Substring(name.Length - 1);
            return $"{maskedName}@{domain}";
        }
    }

    public class Enable2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "Kod 6 karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [Display(Name = "Doğrulama Kodu")]
        public string Code { get; set; }

        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }
    }


    public class Disable2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "Kod 6 karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [Display(Name = "Doğrulama Kodu")]
        public string TwoFactorCode { get; set; }
    }
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

}
