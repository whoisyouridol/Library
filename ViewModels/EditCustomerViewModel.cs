using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ViewModels
{
    public class EditCustomerViewModel
    {
        public string Id { get; set; }
        [Display(Name = "New (you can leave it as it is now)")]
        [Required(ErrorMessage = "Please input email")]
        public string Email { get; set; }

        [Display(Name = "Old password")]
        [Required(ErrorMessage = "Please input old password")]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        [Required(ErrorMessage = "Please input new password")]
        public string NewPassword { get; set; }
    }
}
