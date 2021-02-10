using System.ComponentModel.DataAnnotations;

namespace LibraryProject.ViewModels
{
    public class AddBookViewModel
    {
        [Required(ErrorMessage = "Author`s name is required")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Publisher`s name is required")]
        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

    }
}
