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
using BurgerQueen.Shared.Enums;


namespace BurgerQueen.UI.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUserController> _logger;
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
        { "UserAlreadyExists", "Bu kullanıcı zaten mevcut." },
        { "InvalidOperation", "Bu işlem geçersiz." },
        { "InvalidLoginAttempt", "Geçersiz giriş denemesi." },
        { "UserIsLockedOut", "Kullanıcı hesabı kilitlenmiş." },
        { "UserIsNotAllowed", "Bu kullanıcı oturum açamaz." },
        { "InvalidSecurityStamp", "Güvenlik damgası geçersiz." },
        { "InvalidPassword", "Geçersiz şifre." },
        { "UserNotEnabled", "Kullanıcı hesabı etkin değil." },
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
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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
                    // Kullanıcı adını oluşturma
                    var baseUsername = userVM.Email.Split('@')[0];
                    var userName = baseUsername;
                    int counter = 1;

                    // Kullanıcı adının benzersiz olmasını sağla
                    while (await _userManager.FindByNameAsync(userName) != null)
                    {
                        userName = $"{baseUsername}{counter++}";
                    }

                    var user = new ApplicationUser
                    {
                        UserName = userName, // Sistem tarafından oluşturulan kullanıcı adı
                        Email = userVM.Email,
                        Gender = userVM.Gender,
                        FirstName = userVM.FirstName,
                        LastName = userVM.LastName,
                        PhoneNumber = userVM.PhoneNumber,
                        Address = userVM.Address,
                        DateOfBirth = userVM.DateOfBirth
                    };
                    var result = await _userManager.CreateAsync(user, userVM.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Kullanıcı {Email} başarıyla oluşturuldu.", userVM.Email);

                        await _userManager.AddToRoleAsync(user, "User");

                        // Cinsiyete göre varsayılan profil resmi atama
                        string defaultImagePath = user.Gender == Gender.Male ? "images/default-male.png" : "images/default-female.png";
                        user.ProfilePictureUrl = defaultImagePath;
                        await _userManager.UpdateAsync(user);

                        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        var maskedIpAddress = IPAddressMasker.MaskIpAddress(ipAddress);
                        _logger.LogInformation("Kullanıcı {Email} kayıt oldu. IP Adresi: {IpAddress}", userVM.Email, maskedIpAddress);

                        return RedirectToAction("Login", "ApplicationUser");
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
                            // E-posta doğrulama ve 2FA kontrolünü kaldırdık
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordVM());
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

                // Kullanıcı var mı kontrolü yapıyoruz ama doğrulama için bir eylem gerçekleştirmiyoruz.
                var user = await _userManager.FindByEmailAsync(model.Email) ??
                           await _userManager.FindByNameAsync(model.UserName);

                // Kullanıcıya yönetici ile iletişime geçmesi gerektiğini bildirmek için onay sayfasına yönlendiriyoruz.
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

        // Profil İşlemleri
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
                LastName = profileUser.LastName,
                Address = profileUser.Address,
                PhoneNumber = profileUser.PhoneNumber,
                DateOfBirth = profileUser.DateOfBirth,
                Gender = profileUser.Gender,
                ProfilePictureUrl = GetProfilePictureUrl(profileUser.Gender, profileUser.ProfilePictureUrl)
            };

            return View(userVM);
        }

        private string GetProfilePictureUrl(Gender gender, string profilePictureUrl)
        {
            if (string.IsNullOrEmpty(profilePictureUrl))
            {
                switch (gender)
                {
                    case Gender.Male:
                        return "~/images/default-male.png";
                    case Gender.Female:
                        return "~/images/default-female.png";
                    default:
                        return "~/images/default-profile.png"; // Diğer durumlar için
                }
            }
            else
            {
                // profilePictureUrl zaten tam yolu içeriyor, bu yüzden direkt döndürüyoruz
                return profilePictureUrl;
            }
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

            var model = new EditProfileVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth, // DateOfBirth nullable değilse direkt atama yapabiliriz
                UserName = user.UserName,
                Gender = user.Gender, // Gender nullable değilse, nullable ise user.Gender?
                ProfilePictureUrl = user.ProfilePictureUrl // Yeni eklenen dosyanın URL'sini buraya aktarıyoruz
            };

            return View(model);
        }

        // Post metodunun geri kalanı aynı kalabilir

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileVM model)
        {
            ModelState.Remove("ProfilePictureUrl");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState Invalid! Hatalar:");

                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Hata Alanı: {key} - Hata Mesajı: {error.ErrorMessage}");
                    }
                }

                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("EditProfile işlemi için kullanıcı bulunamadı.");
                return NotFound();
            }

            // Kullanıcı adının benzersizliğini kontrol et
            if (user.UserName != model.UserName && await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanımda.");
                return View(model);
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.DateOfBirth;
            user.UserName = model.UserName;
            user.Gender = model.Gender; // Nullable olarak eklendi

            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                var fileName = Path.GetFileName(model.ProfilePicture.FileName).Replace(" ", "_"); // Dosya adındaki boşlukları kaldır
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                _logger.LogInformation($"Dosya yükleme yolu: {uploadsPath}");
                _logger.LogInformation($"Dosya tam yolu: {filePath}");
                _logger.LogInformation($"Dosya boyutu: {model.ProfilePicture.Length}");
                _logger.LogInformation($"Dosya adı: {model.ProfilePicture.FileName}");

                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfilePicture.CopyToAsync(fileStream);
                    }
                    _logger.LogInformation($"Dosya yüklendi: {filePath}");

                    // Eski resmi silme işlemini sadece eğer yüklenen resim default değilse yapalım
                    if (!string.IsNullOrEmpty(user.ProfilePictureUrl) &&
                        !user.ProfilePictureUrl.Contains("default-male.png") &&
                        !user.ProfilePictureUrl.Contains("default-female.png"))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfilePictureUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Veritabanında sadece yolu ve dosya adını sakla
                    user.ProfilePictureUrl = "images/profiles/" + uniqueFileName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Dosya yüklenirken hata oluştu.");
                    ModelState.AddModelError("ProfilePicture", "Profil resmi yüklenirken bir hata oluştu.");
                    return View(model);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Kullanıcı {user.UserName} profilini başarıyla güncelledi. Profil resmi URL'si: {user.ProfilePictureUrl}");
                return RedirectToAction(nameof(Profile));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Veritabanı güncelleme hatası: {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        //if (!ModelState.IsValid)
        //{
        //    foreach (var key in ModelState.Keys)
        //    {
        //        var state = ModelState[key];
        //        foreach (var error in state.Errors)
        //        {
        //            _logger.LogWarning($"Field: {key}, Error: {error.ErrorMessage}");
        //        }
        //    }
        //    return View(model);
        //}

        //if (!ModelState.IsValid)
        //{
        //    var errorsToClear = new List<string>();

        //    foreach (var key in ModelState.Keys)
        //    {
        //        var state = ModelState[key];
        //        foreach (var error in state.Errors)
        //        {
        //            _logger.LogWarning($"Field: {key}, Error: {error.ErrorMessage}");
        //            if (key == "ProfilePictureUrl")
        //            {
        //                errorsToClear.Add(key);
        //            }
        //        }
        //    }

        //    // Döngü bittikten sonra hataları temizleyelim
        //    foreach (var key in errorsToClear)
        //    {
        //        ModelState[key].Errors.Clear();
        //    }
        //}




        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            _logger.LogInformation("Kullanıcı şifre değiştirme sayfasına erişti.");
            return View(new ChangePasswordVM());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
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

                    // Kullanıcıya şifre değiştirme işlemi tamamlandığında doğrudan giriş sayfasına yönlendiriyoruz.
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

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Şifre yanlış.");
                return View(model);
            }

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


    }
    
}

