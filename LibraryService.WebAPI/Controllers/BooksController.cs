using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("{libraryId}/{ints}")]
        public async Task<IActionResult> Get(int libraryId, int[] ints)
        {
            var library = (await _booksService.Get(libraryId , ints)).FirstOrDefault();
            if (library == null)
                return NotFound();
            return Ok(library);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book b)
        {
            await _booksService.Add(b);
            return Ok(b);
        }

        [HttpPut("{libraryId}/{ints}")]
        public async Task<IActionResult> Update(int libraryId, int[] ints, Book b)
        {
            var existingLibrary = (await _booksService.Get(libraryId, ints)).FirstOrDefault();
            if (existingLibrary == null)
                return NotFound();

            await _booksService.Update(b);
            return NoContent();
        }

        [HttpDelete("{libraryId}/{ints}")]
        public async Task<IActionResult> Delete(int libraryId, int[] ints)
        {
            var library = (await _booksService.Get(libraryId, ints)).FirstOrDefault();
            if (library == null)
                return NotFound();

            await _booksService.Delete(library);

            return NoContent();
        }
    }
}