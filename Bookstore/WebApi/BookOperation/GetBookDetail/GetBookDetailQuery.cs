using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public int BookId { get; set; }

        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Kitap Bulunamad─▒.");
            
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return vm;
        }


    }

     public class BookDetailViewModel
    {

        public string Title { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}