using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryProject.Models;
using LibraryProject.ViewModels;

namespace LibraryProject.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookViewModel, Book>()
                .ForMember(x => x.Genre, b => b.MapFrom(a => a.Genre))
                .ForMember(x => x.Author, b => b.MapFrom(a => a.Author))
                .ForMember(x => x.Publisher, b => b.MapFrom(a => a.Publisher))
                .ForMember(x => x.IsAllowed, b => b.MapFrom(a => true));

            CreateMap<BooksViewModel, Book>()
                .ForMember(x => x.Genre, b => b.MapFrom(a => a.Genre))
                .ForMember(x => x.Author, b => b.MapFrom(a => a.Author))
                .ForMember(x => x.BookId, b => b.MapFrom(a => a.Id))
                .ForMember(x => x.Publisher, b => b.MapFrom(a => a.Publisher))
                .ForMember(x => x.IsAllowed, b => b.MapFrom(a => a.IsAllowed));
            CreateMap<Book, BooksViewModel>()
                .ForMember(x => x.Genre, b => b.MapFrom(a => a.Genre))
                .ForMember(x => x.Id, b => b.MapFrom(a => a.BookId))
                .ForMember(x => x.Author, b => b.MapFrom(a => a.Author))
                .ForMember(x => x.Publisher, b => b.MapFrom(a => a.Publisher))
                .ForMember(x => x.IsAllowed, b => b.MapFrom(a => a.IsAllowed));

            CreateMap<Book, ReserveBooksViewModel>()
                .ForMember(x => x.Genre, b => b.MapFrom(a => a.Genre))
                .ForMember(x => x.BookId, b => b.MapFrom(a => a.BookId))
                .ForMember(x => x.Author, b => b.MapFrom(a => a.Author))
                .ForMember(x => x.Publisher, b => b.MapFrom(a => a.Publisher))
                .ForMember(x => x.IsSelected,b => b.MapFrom(a => false)); 
            CreateMap<ReserveBooksViewModel, Book>()
                .ForMember(x => x.Genre, b => b.MapFrom(a => a.Genre))
                .ForMember(x => x.BookId, b => b.MapFrom(a => a.BookId))
                .ForMember(x => x.Author, b => b.MapFrom(a => a.Author))
                .ForMember(x => x.Publisher, b => b.MapFrom(a => a.Publisher))
                .ForMember(x => x.IsAllowed,b => b.MapFrom(a => a.IsSelected));
            CreateMap<RemoveBooksViewModel, Book>()
                .ForMember(x => x.Genre, b => b.MapFrom(a => a.Genre))
                .ForMember(x => x.BookId, b => b.MapFrom(a => a.BookId))
                .ForMember(x => x.Author, b => b.MapFrom(a => a.Author))
                .ForMember(x => x.Publisher, b => b.MapFrom(a => a.Publisher))
                .ForMember(x => x.IsAllowed,b => b.MapFrom(a => a.IsReserved));
            CreateMap<Book, RemoveBooksViewModel>()
                .ForMember(x => x.Genre, b => b.MapFrom(a => a.Genre))
                .ForMember(x => x.BookId, b => b.MapFrom(a => a.BookId))
                .ForMember(x => x.Author, b => b.MapFrom(a => a.Author))
                .ForMember(x => x.Publisher, b => b.MapFrom(a => a.Publisher))
                .ForMember(x => x.IsReserved,b => b.MapFrom(a => a.IsAllowed));
        }
    }
}
