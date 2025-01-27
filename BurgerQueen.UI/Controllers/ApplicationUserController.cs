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

        // Kullanıcı işlemleri
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var viewModel = new ApplicationUserRegisterVM
            {
                AvailableRoles = roles.Select(r => new SelectListItem { Value = r, Text = r })
            };
            return View(viewModel);
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
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            "EmailVerificationComplete",
                            "ApplicationUser",
                            new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme);

                        await _emailSender.SendEmailAsync(user.Email, "E-posta Doğrulama",
                            $"Lütfen e-postanızı doğrulamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

                        if (!string.IsNullOrEmpty(userVM.SelectedRole))
                        {
                            await _userManager.AddToRoleAsync(user, userVM.SelectedRole);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                        }

                        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        var maskedIpAddress = IPAddressMasker.MaskIpAddress(ipAddress);
                        _logger.LogInformation("Kullanıcı {Email} kayıt oldu. IP Adresi: {IpAddress}", userVM.Email, maskedIpAddress);

                        return RedirectToAction("EmailVerificationLinkSent", "ApplicationUser");
                    }

                    AddErrorsToModelState(result.Errors);
                }
                catch (Exception ex)
                {
                    var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    var maskedIpAddress = IPAddressMasker.MaskIpAddress(ipAddress);
                    _logger.LogError(ex, "Register işleminde hata oluştu. Email: {Email}, IP Adresi: {IpAddress}", MaskEmail(userVM.Email), maskedIpAddress);
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

        // Şifre İşlemleri
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
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Kullanıcı ID'si {userId} ile eşleşen bir kullanıcı bulunamadı.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View("EmailVerificationComplete");
            }

            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                return NotFound();
            }

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
                    user.Address = userVM.Address;
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.DateOfBirth = userVM.DateOfBirth;
                    user.ProfilePictureUrl = userVM.ProfilePictureUrl;

                    if (userVM.ProfilePicture != null)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profileImages", userVM.ProfilePicture.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await userVM.ProfilePicture.CopyToAsync(stream);
                        }
                        user.ProfilePictureUrl = "/profileImages/" + userVM.ProfilePicture.FileName;
                    }

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await _emailSender.SendEmailAsync(user.Email, "Profil Bilgileri Güncellendi",
                            $"Merhaba {user.FirstName},<br/>Profil bilgileriniz başarıyla güncellendi.");

                        return RedirectToAction(nameof(Profile));
                    }
                    AddErrorsToModelState(result.Errors);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Profil güncellenirken hata oluştu. Email: {Email}", MaskEmail(userVM.Email));
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(userVM);
        }

        // Şifre Değişiklik İşlemleri
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestPasswordChange()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmPasswordChange",
                "ApplicationUser",
                new { userId = user.Id, token = token },
                HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Şifre Değişikliği Onayı",
                $"Şifrenizi değiştirmek istediğinizi onaylamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

            return RedirectToAction("PasswordChangeRequested");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmPasswordChange(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Kullanıcı ID'si {userId} ile eşleşen bir kullanıcı bulunamadı.");
            }

            var model = new PasswordUpdateVM { Token = token };
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordUpdateVM model)
        {
            if (ModelState.IsValid)
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
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();

                    await _emailSender.SendEmailAsync(user.Email, "Şifre Güncellendi",
                        $"Merhaba {user.FirstName},<br/>Şifreniz başarıyla güncellendi. Lütfen tekrar giriş yapın.");

                    return RedirectToAction("Login", "ApplicationUser");
                }
                AddErrorsToModelState(result.Errors);
            }
            return View(model);
        }

        // Hesap Silme
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
                AddErrorsToModelState(result.Errors);
            }
            catch (Exception ex)
            {
                var user = await _userManager.GetUserAsync(User);
                _logger.LogError(ex, "Hesap silinirken hata oluştu. Email: {Email}", MaskEmail(user?.Email));
                return RedirectToAction("Error", "Home");
            }
            return View("DeleteAccount");
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

