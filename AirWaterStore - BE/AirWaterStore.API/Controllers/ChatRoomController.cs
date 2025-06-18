using AirWaterStore.API.DTOs;
using AirWaterStore.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirWaterStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        // GET: api/chatroom/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetChatRoomsByUserIdAsync(int userId)
        {
            var rooms = await _chatRoomService.GetChatRoomsByUserIdAsync(userId);
            return Ok(rooms);
        }

        // GET: api/chatroom/5
        [HttpGet("{chatRoomId}")]
        public async Task<IActionResult> GetChatRoomByIdAsync(int chatRoomId)
        {
            var room = await _chatRoomService.GetChatRoomByIdAsync(chatRoomId);
            if (room == null) return NotFound();
            return Ok(room);
        }

        // POST: api/chatroom/get-or-create/5
        [HttpPost("get-or-create/{customerId}")]
        public async Task<IActionResult> GetOrCreateChatRoomAsync(int customerId)
        {
            var room = await _chatRoomService.GetOrCreateChatRoomAsync(customerId);

            return Ok(room);
        }

        // PUT: api/chatroom/assign
        [HttpPut("assign")]
        public async Task<IActionResult> AssignStaff([FromBody] AssignStaffToChatRoomDto dto)
        {
            await _chatRoomService.AssignStaffToChatRoomAsync(dto.ChatRoomId, dto.StaffId);
            return NoContent();
        }
    }
}
