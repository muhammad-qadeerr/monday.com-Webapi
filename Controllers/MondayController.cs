using ApiTestProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTestProject.Controllers
{
    [ApiController]
    [Route("api/monday")]
    public class MondayController : Controller
    {
        private readonly MondayService _mondayService;

        public MondayController(MondayService mondayService)
        {
            _mondayService = mondayService;
        }

        [HttpGet("GetItemFromMondayBoard")]
        public async Task<IActionResult> GetItemFromMondayBoard()
        {
            var items = await _mondayService.GetItemsFromBoard();
            if (items == null)
                return NotFound("No items found");
            return Ok(items);
        }
    }
}
