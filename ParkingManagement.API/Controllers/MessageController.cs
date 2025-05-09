using Microsoft.AspNetCore.Mvc;
using ParkingManagement.API.DTOs;
using ParkingManagement.API.Models;
using ParkingManagement.API.Services.Interfaces;

namespace ParkingManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var result = await _messageService.SendMessageAsync(messageDto);

            if (!result)
                return StatusCode(500, "Something went wrong while saving the message.");

            return Ok(new { message = "Message sent successfully!" });
        }

        // ✅ Nouveau: Récupérer tous les messages
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            var messages = await _messageService.GetMessagesAsync();
            return Ok(messages);
        }
    }
}
