using AutoMapper;
using LibraryProject.Data;
using LibraryProject.Models;
using LibraryProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;

namespace LibraryProject.Controllers
{
    public class BooksController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BooksController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;

        }
        [Authorize(Roles = "librarian,customer")]
        public IActionResult Index()
        {
            return View(_context.Books
                .Select(model => _mapper.Map<BooksViewModel>(model)));
        }
        [Authorize(Roles = "librarian")]
        public ActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.BookId == id);
            _context.Books.Remove(book ?? throw new NullReferenceException());
            _context.SaveChanges();
            return RedirectToAction("Index", "Books");
        }
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<ActionResult> Add(AddBookViewModel viewModel)
        {
            var book = _mapper.Map<Book>(viewModel);
            book.ApplicationUserId = _userManager.GetUserId(User);

            _context.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        [Authorize(Roles = "customer")]
        public ActionResult Reserve()
        {
            return View(_context.Books
                .Where(x => x.IsAllowed)
                .Select(model => _mapper.Map<ReserveBooksViewModel>(model))
                .ToList());
        }

        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<ActionResult> Reserve(List<ReserveBooksViewModel> viewModel)
        {
            var booksToReserve = viewModel
                .Where(x => x.IsSelected)
                .Select(book => _mapper.Map<Book>(book))
                .ToList();

            var listToChange = _context
                .Books
                .Where(x => booksToReserve.Select(y => y.BookId).Contains(x.BookId))
                .ToList();

            foreach (var el in listToChange)
            {
                el.IsAllowed = false;
                el.ApplicationUserId = _userManager.GetUserId(User);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult RemoveReserve()
        {
            return View(_context
                .Books
                .Where(x => !x.IsAllowed && x.ApplicationUserId == _userManager.GetUserId(User))
                .Select(book => _mapper.Map<RemoveBooksViewModel>(book))
                .ToList());
        }
        public async Task<ActionResult> RemoveReserve(List<RemoveBooksViewModel> viewModel)
        {
            var booksToEnable = viewModel
                .Where(x => x.IsReserved)
                .Select(bookViewModel => _mapper.Map<Book>(bookViewModel));

            var booksToAllow = _context
                .Books
                .Where(x => booksToEnable.Select(y => y.BookId).Contains(x.BookId) && !x.IsAllowed);
                
            foreach (var el in booksToAllow)
            {
                el.IsAllowed = true;
                el.ApplicationUserId = (await _userManager.GetUsersInRoleAsync("librarian")).FirstOrDefault()?.Id;
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }
    }
}