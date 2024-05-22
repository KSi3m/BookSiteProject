using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Mappings
{
    public class BookMappingProfile: Profile
    {

        public BookMappingProfile()
        {
     
            CreateMap<AuthorDto, Author>().ReverseMap(); 
            CreateMap<CategoryDto, Category>();
           /* CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.BooksIds, opt => opt.MapFrom(src => src.Books.Select(a => a.Id)));*/


            CreateMap<BookDto, Book>()
            .ForMember(dest => dest.typeOfBookOwnership, opt => opt.MapFrom(src => (TypeOfBookOwnership)src.typeOfBookOwnership))
           ;

            CreateMap<Book, BookDto>()
            .ForMember(dest => dest.typeOfBookOwnership, opt => opt.MapFrom(src => (int)src.typeOfBookOwnership))
            .ForMember(dest => dest.AuthorsIds, opt => opt.MapFrom(src => src.Authors.Select(a => a.Id)))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category != null ? src.Category.Id : (int?)null));

        }
    }
}
