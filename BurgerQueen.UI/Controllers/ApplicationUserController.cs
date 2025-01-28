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
using System.Diagnostics;
using BurgerQueen.UI.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BurgerQueen.UI.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUserController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private static readonly Dictionary<string, string> _customErrorMessages = new Dictionary<string, string>
    {
        { "DuplicateUserName", "Bu kullanıcı adı zaten kullanımda." },
    { "DuplicateEmail", "Bu e-posta adresi zaten kayıtlı." },
    { "PasswordRequiresDigit", "Şifreniz en az bir sayı içermelidir." },
    { "PasswordRequiresLower", "Şifreniz en az bir küçük harf içermelidir." },
    { "PasswordRequiresUpper", "Şifreniz en az bir büyük harf içermelidir." },
    { "PasswordRequiresNonAlphanumeric", "Şifreniz en az bir özel karakter içermelidir." },
    { "PasswordTooShort", "Şifreniz en az 8 karakter olmalıdır." },
    { "InvalidEmail", "Geçerli bir e-posta adresi giriniz." },
    { "InvalidUserName", "Kullanıcı adınız geçersiz karakterler içeriyor." },
    { "ConcurrencyFailure", "Bu işlem sırasında başka bir değişiklik yapıldı. Lütfen tekrar deneyin." },
    { "UserAlreadyHasPassword", "Bu kullanıcı için zaten bir şifre belirlenmiş." },
    { "UserLockoutNotEnabled", "Hesap kilitleme aktif değil." },
    { "UserAlreadyInRole", "Kullanıcı zaten bu rolde." },
    { "UserNotInRole", "Kullanıcı bu rolde değil." },
    { "RoleAlreadyExists", "Bu rol zaten var." },
    { "RoleDoesNotExist", "Bu rol mevcut değil." },
    { "InvalidToken", "Geçersiz doğrulama tokenı." },
    { "RecoveryCodeRedemptionFailed", "Kurtarma kodu geçersiz." },
    { "LoginAlreadyAssociated", "Bu oturum açma bilgisi zaten başka bir hesaba bağlı." },
    { "InvalidPhoneNumber", "Geçersiz telefon numarası." },
    { "DefaultError", "Bilinmeyen bir hata oluştu." },
    { "UserNotFound", "Kullanıcı bulunamadı." },
    { "PasswordMismatch", "Şifreler eşleşmiyor." },
    { "EmailNotConfirmed", "E-posta adresiniz doğrulanmamış." },
    { "UserAlreadyExists", "Bu kullanıcı zaten mevcut." },
    { "InvalidOperation", "Bu işlem geçersiz." },
    { "InvalidLoginAttempt", "Geçersiz giriş denemesi." },
    { "UserIsLockedOut", "Kullanıcı hesabı kilitlenmiş." },
    { "UserIsNotAllowed", "Bu kullanıcı oturum açamaz." },
    { "InvalidSecurityStamp", "Güvenlik damgası geçersiz." },
    { "InvalidTwoFactorCode", "Geçersiz iki faktörlü kod." },
    { "UserRequiresTwoFactor", "Bu kullanıcı iki faktörlü doğrulama gerektiriyor." },
    { "InvalidPassword", "Geçersiz şifre." },
    { "UserNotEnabled", "Kullanıcı hesabı etkin değil." },
    { "UserAlreadyConfirmed", "Kullanıcı zaten doğrulanmış." },
    { "InvalidEmailConfirmationToken", "Geçersiz e-posta doğrulama tokenı." },
    { "InvalidPhoneConfirmationToken", "Geçersiz telefon doğrulama tokenı." },
    { "UserRequiresEmailConfirmation", "Bu kullanıcı e-posta doğrulaması gerektiriyor." },
    { "UserRequiresPhoneConfirmation", "Bu kullanıcı telefon doğrulaması gerektiriyor." },
    { "InvalidUserRole", "Geçersiz kullanıcı rolü." },
    { "UserRoleNotFound", "Kullanıcı rolü bulunamadı." },
    { "UserRoleAlreadyExists", "Kullanıcı rolü zaten mevcut." },
    { "UserRoleNotAssigned", "Kullanıcı rolü atanmadı." },
    { "InvalidUserClaim", "Geçersiz kullanıcı talebi." },
    { "UserClaimNotFound", "Kullanıcı talebi bulunamadı." },
    { "UserClaimAlreadyExists", "Kullanıcı talebi zaten mevcut." },
    { "UserClaimNotAssigned", "Kullanıcı talebi atanmadı." },
    { "InvalidUserToken", "Geçersiz kullanıcı tokenı." },
    { "UserTokenNotFound", "Kullanıcı tokenı bulunamadı." },
    { "UserTokenAlreadyExists", "Kullanıcı tokenı zaten mevcut." },
    { "UserTokenNotAssigned", "Kullanıcı tokenı atanmadı." },
    { "InvalidUserSession", "Geçersiz kullanıcı oturumu." },
    { "UserSessionNotFound", "Kullanıcı oturumu bulunamadı." },
    { "UserSessionAlreadyExists", "Kullanıcı oturumu zaten mevcut." },
    { "UserSessionNotAssigned", "Kullanıcı oturumu atanmadı." }
};

        private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                var customMessage = _customErrorMessages.ContainsKey(error.Code)
                    ? _customErrorMessages[error.Code]
                    : error.Description;
                ModelState.AddModelError(string.Empty, customMessage);
            }
        }

        public ApplicationUserController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<ApplicationUserController> logger,
        IEmailSender emailSender,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM userVM)
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
                        _logger.LogInformation("Kullanıcı {Email} başarıyla oluşturuldu.", userVM.Email);

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            "EmailVerificationComplete",
                            "ApplicationUser",
                            new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme);

                        try
                        {
                            await _emailSender.SendEmailAsync(user.Email, "E-posta Doğrulama",
                                $"Lütfen e-postanızı doğrulamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

                            _logger.LogInformation("E-posta doğrulama linki {Email} adresine gönderildi.", user.Email);

                            await _userManager.AddToRoleAsync(user, "User");

                            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                            var maskedIpAddress = IPAddressMasker.MaskIpAddress(ipAddress);
                            _logger.LogInformation("Kullanıcı {Email} kayıt oldu. IP Adresi: {IpAddress}", userVM.Email, maskedIpAddress);

                            return RedirectToAction("EmailVerificationLinkSent", "ApplicationUser");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "E-posta gönderimi sırasında hata oluştu. Email: {Email}", user.Email);
                            ModelState.AddModelError(string.Empty, "E-posta doğrulama linki gönderilirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                            return View(userVM);
                        }
                    }
                    else
                    {
                        AddErrorsToModelState(result.Errors);
                    }
                }
                catch (Exception ex)
                {
                    var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    var maskedIpAddress = IPAddressMasker.MaskIpAddress(ipAddress);
                    _logger.LogError(ex, "Register işleminde genel bir hata oluştu. Email: {Email}, IP Adresi: {IpAddress}", MaskEmail(userVM.Email), maskedIpAddress);
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(userVM);
        }

        // Giriş / Çıkış işlemleri
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
                    var user = await _userManager.FindByEmailAsync(model.Email) ?? await _userManager.FindByNameAsync(model.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            // Eğer 2FA aktifse burada kontrol edilir
                            if (await _userManager.GetTwoFactorEnabledAsync(user))
                            {
                                return RedirectToAction(nameof(LoginWith2fa), new { rememberMe = model.RememberMe, returnUrl = returnUrl });
                            }

                            // Eğer e-posta doğrulama gerekiyorsa burada kontrol edilir
                            if (!await _userManager.IsEmailConfirmedAsync(user))
                            {
                                ModelState.AddModelError(string.Empty, "E-posta adresiniz doğrulanmamış.");
                                return View(model);
                            }

                            // Giriş başarılı ise returnUrl'e yönlendir
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            if (result.IsLockedOut)
                            {
                                ModelState.AddModelError(string.Empty, "Hesabınız kilitli. Lütfen daha sonra tekrar deneyin.");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                            }
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
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // İki Faktörlü Doğrulama
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

            return View(new Enable2faVM
            {
                SharedKey = FormatKey(authenticatorKey),
                AuthenticatorUri = GenerateQrCodeUri(user.Email, authenticatorKey)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication(Enable2faVM model)
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

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            var recoveryCodeList = recoveryCodes.ToList();

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
        public async Task<IActionResult> DisableTwoFactorAuthentication(Disable2faVM model)
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

            return View(new ShowRecoveryCodesVM { RecoveryCodes = recoveryCodes.ToList() });
        }

        // Şifre İşlemleri
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                // Captcha doğrulaması için basit bir kontrol. Gerçek uygulamalarda daha karmaşık kontroller gerekebilir.
                if (!string.Equals(model.Captcha, "1234", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Captcha", "Captcha doğrulaması başarısız.");
                    return View(model);
                }

                var user = await _userManager.FindByEmailAsync(model.Email) ??
                           await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "ApplicationUser",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);

                try
                {
                    await _emailSender.SendEmailAsync(model.Email, "Şifre Sıfırlama",
                        $"Şifrenizi sıfırlamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Şifre sıfırlama e-postası gönderilirken hata oluştu.");
                    ModelState.AddModelError(string.Empty, "Şifre sıfırlama e-postası gönderilemedi. Lütfen daha sonra tekrar deneyin.");
                    return View(model);
                }

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmailVerificationComplete(string userId, string code)
        {
            if (userId == null || code == null)
            {
                _logger.LogWarning("EmailVerificationComplete metodu için eksik parametreler.");
                return RedirectToAction("Index", "Home");
            }

            if (!Guid.TryParse(userId, out _))
            {
                _logger.LogWarning($"Geçersiz kullanıcı ID'si: {userId}");
                return BadRequest("Geçersiz kullanıcı ID'si.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"Kullanıcı ID'si {userId} ile eşleşen bir kullanıcı bulunamadı.");
                return NotFound($"Kullanıcı ID'si {userId} ile eşleşen bir kullanıcı bulunamadı.");
            }

            try
            {
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Kullanıcı {userId} için e-posta doğrulama başarılı oldu.");
                    return View("EmailVerificationComplete");
                }
                else
                {
                    _logger.LogWarning($"Kullanıcı {userId} için e-posta doğrulama başarısız oldu. Hatalar: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcı {userId} için e-posta doğrulama işlemi sırasında hata oluştu.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmailVerificationLinkSent()
        {
            return View();
        }

        // Profil İşlemleri
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Profile(string userId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("Profil görüntüleme işlemi için kullanıcı bulunamadı.");
                return NotFound();
            }

            if (userId != null)
            {
                if (!User.IsInRole("Admin"))
                {
                    _logger.LogWarning($"Kullanıcı {User.Identity.Name} başka bir kullanıcının (ID: {userId}) profilini görüntülemeye çalıştı.");
                    return Forbid();
                }
            }

            var profileUser = userId == null ? user : await _userManager.FindByIdAsync(userId);
            if (profileUser == null)
            {
                _logger.LogWarning($"ID'si {userId} olan kullanıcı bulunamadı.");
                return NotFound();
            }

            var userVM = new ProfileVM
            {
                Id = profileUser.Id,
                UserName = profileUser.UserName,
                Email = profileUser.Email,
                FirstName = profileUser.FirstName,
                LastName = profileUser.LastName
            };

            return View(userVM);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("EditProfile işlemi için kullanıcı bulunamadı.");
                return NotFound();
            }

            var userVM = new EditProfileVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                DateOfBirth = user.DateOfBirth,
                ProfilePictureUrl = user.ProfilePictureUrl ?? string.Empty
            };

            return View(userVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("EditProfile işlemi için kullanıcı bulunamadı.");
                return NotFound();
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.DateOfBirth;

            // Profil resmi yükleme işlemi
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");

                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }

                // Eski resmi silme işlemi (isteğe bağlı)
                if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfilePictureUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Yeni profil resmi URL'si
                user.ProfilePictureUrl = Path.Combine("images", "profiles", uniqueFileName);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("Kullanıcı {UserName} profilini başarıyla güncelledi.", user.UserName);
                return RedirectToAction(nameof(Profile));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestPasswordChange()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("Şifre değişikliği talebi için kullanıcı bulunamadı.");
                return NotFound();
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                _logger.LogWarning("Kullanıcı {UserId} için email doğrulanmamış, şifre değişikliği talebi reddedildi.", user.Id);
                ModelState.AddModelError(string.Empty, "E-postanız doğrulanmamış, lütfen önce e-postanızı doğrulayın.");
                return View("Error"); // veya uygun bir hata sayfasına yönlendirme
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmPasswordChange",
                "ApplicationUser",
                new { userId = user.Id, token = token },
                HttpContext.Request.Scheme);

            try
            {
                await _emailSender.SendEmailAsync(user.Email, "Şifre Değişikliği Onayı",
                    $"Şifrenizi değiştirmek istediğinizi onaylamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

                // ViewModel ile bilgi ver
                return RedirectToAction("PasswordChangeRequested", new PasswordChangeRequestedVM
                {
                    Message = "Şifre değişiklik talebiniz için bir e-posta gönderildi. Lütfen e-postanızı kontrol edin."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şifre değişikliği doğrulama e-postası gönderilirken hata oluştu.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmPasswordChange(string userId, string token)
        {
            if (userId == null || token == null)
            {
                _logger.LogWarning("ConfirmPasswordChange için eksik kullanıcı ID'si veya token.");
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"ConfirmPasswordChange için kullanıcı ID'si {userId} ile eşleşen kullanıcı bulunamadı.");
                return NotFound($"Kullanıcı ID'si {userId} ile eşleşen bir kullanıcı bulunamadı.");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "E-postanız doğrulanmamış. Lütfen önce e-postanızı doğrulayın.");
                return View(new PasswordUpdateVM());
            }

            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token))
            {
                _logger.LogWarning($"Kullanıcı {userId} için geçersiz token.");
                ModelState.AddModelError(string.Empty, "Geçersiz veya süresi dolmuş token.");
                return View(new PasswordUpdateVM());
            }

            var model = new PasswordUpdateVM { Token = token };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmPasswordChange(string userId, string token, PasswordUpdateVM model)
        {
            // Modelin geçerli olup olmadığını kontrol et
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kullanıcıyı ID'si ile bul
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"ConfirmPasswordChange için kullanıcı ID'si {userId} ile eşleşen kullanıcı bulunamadı.");
                return NotFound($"Kullanıcı ID'si {userId} ile eşleşen bir kullanıcı bulunamadı.");
            }

            // Token'ı doğrula
            if (!await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token))
            {
                _logger.LogWarning($"Kullanıcı {userId} için geçersiz token.");
                ModelState.AddModelError(string.Empty, "Geçersiz veya süresi dolmuş token.");
                return View(model);
            }

            // Şifreyi sıfırla
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Kullanıcı {user.Id} için şifre başarıyla değiştirildi.");
                return RedirectToAction("PasswordChanged");
            }

            // Eğer hata varsa, hataları ekle ve formu tekrar göster
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            _logger.LogInformation("Kullanıcı şifre değiştirme sayfasına erişti.");
            return View(new ChangePasswordVM());
        }

        [Authorize] // AllowAnonymous yerine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordUpdateVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Yeni şifreler eşleşmiyor.");
                    return View(model);
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Kullanıcı {user.Id} şifresini başarıyla değiştirdi.");
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();

                    await _emailSender.SendEmailAsync(user.Email, "Şifre Güncellendi",
                        $"Merhaba {user.FirstName},<br/>Şifreniz başarıyla güncellendi. Lütfen tekrar giriş yapın.");

                    return RedirectToAction("Login", "ApplicationUser");
                }
                else
                {
                    _logger.LogWarning($"Kullanıcı {user.Id} şifre değiştirme işlemi başarısız oldu. Hatalar: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
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

            _logger.LogInformation($"Kullanıcı {user.Id} hesap silme sayfasına erişti.");

            var deleteAccountVM = new DeleteAccountVM
            {
                Id = user.Id,
                Email = user.Email
                // Diğer alanlar buraya eklenebilir
            };

            return View(deleteAccountVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(DeleteAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Şifre doğrulaması
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Şifre yanlış.");
                return View(model);
            }

            // Hesap silme işlemi
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation($"Kullanıcı {user.Id} hesabını silmeyi başarıyla tamamladı.");
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteAccountConfirmed()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var confirmDeleteVM = new ConfirmDeleteAccountVM { Email = user.Email };
            return View(confirmDeleteVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccountConfirmed(ConfirmDeleteAccountVM model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                // ViewModel kullanarak ek bir doğrulama yapabiliriz, ama burada sadece email adresini kontrol ediyoruz:
                if (model.Email != user.Email)
                {
                    ModelState.AddModelError(string.Empty, "Email adresi eşleşmiyor.");
                    return View(model);
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    _logger.LogInformation($"Kullanıcı {user.Id} hesabını silmeyi başarıyla tamamladı.");
                    return RedirectToAction("Index", "Home");
                }
                AddErrorsToModelState(result.Errors);
            }
            catch (Exception ex)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogError(ex, "Hesap silinirken hata oluştu. Email: {Email}", MaskEmail(user?.Email));
                return RedirectToAction("Error", "Home");
            }
            return View(model);
        }

        // Yardımcı Metodlar
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

        private string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;
            var parts = email.Split('@');
            if (parts.Length != 2) return email;

            var name = parts[0];
            var domain = parts[1];
            if (name.Length <= 3) return email;

            var maskedName = name.Substring(0, 1) + new string('*', Math.Max(0, name.Length - 2)) + name.Substring(name.Length - 1);
            return $"{maskedName}@{domain}";
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                _logger.LogWarning($"Kullanıcı iki faktörlü doğrulama için bulunamadı.");
                return RedirectToAction("Error", "Home");
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
                _logger.LogWarning("İki faktörlü doğrulama için kullanıcı bulunamadı.");
                return RedirectToAction("Error", "Home");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Kullanıcı {user.Id} iki faktörlü doğrulama ile başarıyla giriş yaptı.");
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning($"Kullanıcı {user.Id} iki faktörlü doğrulama ile giriş yapamadı. Hesap kilitli.");
                ModelState.AddModelError(string.Empty, "Hesabınız kilitlendi. Lütfen daha sonra tekrar deneyin.");
                return View("Lockout");
            }
            else
            {
                _logger.LogWarning($"Kullanıcı {user.Id} iki faktörlü doğrulama ile giriş yapamadı. Geçersiz kod.");
                ModelState.AddModelError(string.Empty, "Doğrulama kodu geçersiz.");
                return View(model);
            }
        }
    }
}

