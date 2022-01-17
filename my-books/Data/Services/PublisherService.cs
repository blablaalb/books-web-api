using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM authorVM)
        {
            var publihser = new Publisher()
            {
                Name = authorVM.Name
            };

            _context.Publishers.Add(publihser);
            _context.SaveChanges();
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var publisherData = _context.Publishers.Where(x => x.Id == publisherId).Select(x => new PublisherWithBooksAndAuthorsVM()
            {
                Name = x.Name,
                BookAuthors = x.Books.Select(x => new BookAuthorVM()
                {
                    BookName = x.Title,
                    BookAuthors = x.Book_Authors.Select(x => x.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();
            return publisherData;
        }

        public void DeletePublisherById(int id)
        {
           var publisher = _context.Publishers.FirstOrDefault(x => x.Id == id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
        }
    }
}
