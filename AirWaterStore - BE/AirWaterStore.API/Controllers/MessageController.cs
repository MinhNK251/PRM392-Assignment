using AirWaterStore.API.DTOs;
using AirWaterStore.Business.Interfaces;
using AirWaterStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirWaterStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: api/message/chatroom/5
        [HttpGet("chatroom/{chatRoomId}")]
        public async Task<IActionResult> GetByChatRoomId(int chatRoomId)
        {
            var messages = await _messageService.GetMessagesByChatRoomIdAsync(chatRoomId);
            return Ok(messages);
        }

        // POST: api/message
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateMessageDto dto)
        {
            var message = new Message
            {
                ChatRoomId = dto.ChatRoomId,
                UserId = dto.UserId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow
            };

            await _messageService.AddMessageAsync(message);
            return Ok(new
            {
                message.MessageId,
                message.ChatRoomId,
                message.UserId,
                message.Content,
                message.SentAt
            });
        }
    }
}
