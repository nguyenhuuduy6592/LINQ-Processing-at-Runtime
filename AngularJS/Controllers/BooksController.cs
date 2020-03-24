using AngularJS.Models;
using AngularJS.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace AngularJS.Controllers
{
    public class BooksController : ApiController
    {
        private readonly MongoDbService<Book> _bookService;

        public BooksController()
        {
            _bookService = new MongoDbService<Book>("Books");
        }

        public List<Book> Get()
        {
            return _bookService.Get();
        }

        public IHttpActionResult Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        public IHttpActionResult Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        public IHttpActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return Ok();
        }

        public IHttpActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return Ok();
        }
    }
}