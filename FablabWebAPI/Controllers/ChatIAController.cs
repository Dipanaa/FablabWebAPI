using Azure.Core;
using FablabWebAPI.DTOs.ChatIADto;
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


        [HttpPost]
        public async Task<ActionResult<ChatRespuestaDto>> PostChatData(ChatPreguntasDto chatPreguntasDto)
        {
            if (chatPreguntasDto.Pregunta.Equals(""))
            {
                return BadRequest();
            }

            var respuesta = await chatContexto.ChatText(chatPreguntasDto.Pregunta);

            var respuestaDto = new ChatRespuestaDto
            {
                Respuesta = respuesta,
            };

            return respuestaDto; 
        }


    }
}
