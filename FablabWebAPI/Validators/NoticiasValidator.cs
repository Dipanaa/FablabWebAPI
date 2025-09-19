using FablabWebAPI.Entities;
using FluentValidation;

namespace FablabWebAPI.Validators
{
    public class NoticiasValidator: AbstractValidator<Noticias>
    {
        public NoticiasValidator() {

            RuleFor(noticia => noticia.Estado).Must(estado => estado == "Activo" || estado == "Deshabilitado");
            

        }


    }
}
