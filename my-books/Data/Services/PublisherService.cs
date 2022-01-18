using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            if (StringStartsWithNumber(publisherVM.Name)) throw new PublisherNameException("Name starts with number", publisherVM.Name);

            var publihser = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(publihser);
            _context.SaveChanges();
            return publihser;
        }

        public Publisher GetPublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(x => x.Id == id);
            return publisher;
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
            else
            {
                throw new Exception($"The publisher with id {id} does not exist");
            }
        }

        private bool StringStartsWithNumber(string name)
        {
            return Regex.IsMatch(name, @"^\d");
        }
    }
}
