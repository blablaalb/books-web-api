using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var author = new Author()
            {
                FullName = authorVM.FullName
            };

            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var author = _context.Authors.Where(x => x.Id == authorId).Select(x => new AuthorWithBooksVM()
            {
                FullName = x.FullName,
                BookTitles = x.Book_Authors.Select(x => x.Book.Title).ToList()
            }).FirstOrDefault();
            return author;
        }
    }
}
