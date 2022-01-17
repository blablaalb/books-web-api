using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBookWithAuthors(BookVM bookVM)
        {
            var book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.IsRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            foreach (var id in bookVM.AuthorIds)
            {
                var bookAuthor = new Book_Author()
                {
                    BookId = book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(bookAuthor);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAllBooks()
        {
            var allBooks = _context.Books.ToList();
            return allBooks;
        }

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(bookVM => new BookWithAuthorsVM()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.IsRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                PublisherName = bookVM.Publisher.Name,
                AuthorNames = bookVM.Book_Authors.Select(x => x.Author.FullName).ToList()
            }).FirstOrDefault();
            return bookWithAuthors;
        }

        public Book UpdateBookById(int bookid, BookVM bookVM)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == bookid);
            if (book != null)
            {
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.IsRead = bookVM.IsRead;
                book.DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null;
                book.Rate = bookVM.IsRead ? bookVM.Rate.Value : null;
                book.Genre = bookVM.Genre;
                book.CoverUrl = bookVM.CoverUrl;

                _context.SaveChanges();
            }
            return book;
        }

        public void DeleteBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
