using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Application.Dtos.BorrowTransactionDtos;
using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Application.Dtos.ReviewDtos;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Book
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<AddBookDto, Book>()
    .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Book,UpdateBookDetailsDto>().ReverseMap();
            CreateMap<Member,MemberDto>().ReverseMap();
            CreateMap<Member, AddMemberDto>().ReverseMap();
            CreateMap<Member,UpdateMemberDto>().ReverseMap();
            CreateMap<BorrowTransactions, BorrowBookDto>().ReverseMap();
            CreateMap<ReturnBookDto, BorrowTransactions>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<AddReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
            CreateMap<BookCopy, BookCopyDto>().ReverseMap();
            CreateMap<BookCopy, AddBookCopyDto>().ReverseMap();
            CreateMap<BookCopy, UpdateBookCopyDto>().ReverseMap();
            //.ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(_ => DateTime.Now))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy)); 
        }
    }
}
