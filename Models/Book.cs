using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Author`s name is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Publisher`s name is required")]
        public string Publisher { get; set; }

        public bool IsAllowed { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")] 
        public ApplicationUser User{ get; set; }
    }
}
