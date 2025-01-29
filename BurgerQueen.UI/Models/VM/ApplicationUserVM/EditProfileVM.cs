using BurgerQueen.Shared.Enums;
using System.ComponentModel.DataAnnotations;

public class EditProfileVM
{
    public string Id { get; set; }

    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Ad zorunludur.")]
    [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad zorunludur.")]
    [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
    public string LastName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Adres en fazla 100 karakter olabilir.")]
    public string Address { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string UserName { get; set; }

    public Gender Gender { get; set; }

    public string ProfilePictureUrl { get; set; }

    [Display(Name = "Profil Resmi")]
    public IFormFile ProfilePicture { get; set; }
}