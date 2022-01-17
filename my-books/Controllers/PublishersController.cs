using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publisherService;
        public PublishersController(PublishersService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("add-publihser")]
        public IActionResult AddBook([FromBody] PublisherVM publisherVM)
        {
            _publisherService.AddPublisher(publisherVM);
            return Ok();
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publisherService.GetPublisherData(id);
            return Ok(response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherByid(int id)
        {
            _publisherService.DeletePublisherById(id);
            return Ok();
        }

    }
}
