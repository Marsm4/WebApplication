using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApplication1.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("messages/{movieId}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetMessagesByMovieId(int movieId)
        {
            var messages = await _chatService.GetMessagesByMovieIdAsync(movieId);
            return Ok(messages);
        }

        [HttpPost("messages")]
        public async Task<ActionResult<ChatMessage>> PostMessage(ChatMessage message)
        {
            var result = await _chatService.SendMessageAsync(message);
            return Ok(result);
        }

        [HttpGet("private-messages/{userId}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetPrivateMessages(int userId)
        {
            var messages = await _chatService.GetPrivateMessagesAsync(userId);
            return Ok(messages);
        }

        [HttpPost("private-messages/{userId}")]
        public async Task<ActionResult<ChatMessage>> PostPrivateMessage(int userId, ChatMessage message)
        {
            var result = await _chatService.SendPrivateMessageAsync(userId, message);
            return Ok(result);
        }
    }
}