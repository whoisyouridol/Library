using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ViewModels
{
    public class CreateCustomerViewModel
    {
        [Display(Name = "Input email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Input password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
