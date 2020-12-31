using System.ComponentModel.DataAnnotations;

namespace AracServisTakipSitesi.ViewModes
{
    public class PasswordResetByAdminViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Yeni şifre")]
        public string NewPassword { get; set; }
    }
}