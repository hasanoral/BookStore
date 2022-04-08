using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperation.DeleteBook;
using WebApi.BookOperation.UpdateBook;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using static WebApi.BookOperation.UpdateBook.UpdateBookCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;


namespace WebApi.AddControllers{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDbContext _context;
        
        public BookController (BookStoreDbContext context)
        {
            _context = context;//readonly sadece constructor içinden atanan değer ile değiştirilebilir.
        }

        
        // private static List<Book> BookList = new List<Book>(){
        //     new Book{
        //         Id = 1,
        //         Title = "Kitap1", //Personal Grow-up
        //         GenreId = 1,
        //         PageCount = 200,
        //         PublishDate = new DateTime(2001,06,12)

        //     },

        //     new Book{
        //         Id = 2,
        //         Title = "Kitap2", //Science-fiction
        //         GenreId = 2,
        //         PageCount = 250,
        //         PublishDate = new DateTime(2010,06,12)

        //     },

        //     new Book{
        //         Id = 3,
        //         Title = "Kitap3", //Science-fication
        //         GenreId = 2,
        //         PageCount = 540,
        //         PublishDate = new DateTime(2001,12,21)

        //     }
        // };

        [HttpGet]
        public IActionResult GetBooks()
        {
            /*var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();

            return bookList;*/

            GetBooksQuery query = new GetBooksQuery(_context);
            var result =  query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBooksById(int id)
        {

            BookDetailViewModel result;
            
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
            
            /*var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();

            return book;*/
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
            return Ok();
            

            /*var book = _context.Books.SingleOrDefault(b => b.Title == newBook.Title);

            if(book is not null){
                return BadRequest();
            }

            _context.Books.Add(newBook);
            
            _context.SaveChanges();
            return Ok();*/
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
            
            /*var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if(book is null){
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;

            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;

            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();*/
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
                
            }

            return Ok();
            /*var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if(book is null){
                return BadRequest();
            }

            _context.Books.Remove(book);

            _context.SaveChanges();
            return Ok();*/
        }


    }
}