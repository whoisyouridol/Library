using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.ViewModels
{
    public class RemoveBooksViewModel
    {
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public bool IsReserved { get; set; }
    }
}
