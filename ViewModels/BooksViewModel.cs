namespace LibraryProject.ViewModels
{
    public class BooksViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public bool IsAllowed { get; set; }
    }
}
