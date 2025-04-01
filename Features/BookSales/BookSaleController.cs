using Features.BookSales;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/bookSale")]
    public class BookSaleController : ControllerBase
    {
        private readonly IBookSaleService _bookSaleService;

        public BookSaleController(IBookSaleService bookSaleService)
        {
            _bookSaleService = bookSaleService;
        }

        [HttpGet]
        public async Task<IEnumerable<BookSale>> GetBookSales(
            [FromQuery] int? clientId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            return await _bookSaleService.GetBookSalesAsync(clientId, startDate, endDate);
        }

        [HttpPost]
        public async Task<ActionResult> AddBookSale(BookSaleDto bookSaleDto)
        {
            var id = await _bookSaleService.AddBookSaleAsync(bookSaleDto);
            return Ok(id);
        }
    }
}

