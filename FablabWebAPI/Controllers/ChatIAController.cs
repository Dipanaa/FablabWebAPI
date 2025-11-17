using Azure.Core;
using FablabWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/chatia")]
    public class ChatIAController: ControllerBase
    {
        private readonly IChatContexto chatContexto;

        public static string contextoDatos = string.Empty;
        public static string preguntaUsuario = string.Empty;
        

        public ChatIAController(IChatContexto chatContexto)
        {
            this.chatContexto = chatContexto;
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetChatData()
        {

            return await chatContexto.ChatText("hola");
        }


    }
}
