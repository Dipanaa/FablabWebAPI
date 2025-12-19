using FablabWebAPI.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FablabWebAPI.Validators
{
    public class UsuarioValidator: AbstractValidator<Usuario>
    {
        private string RutValidador = @"\b[0-9|.]{1,10}\-[K|k|0-9]";
        private string EmailValidador = @"^.+@inacapmail\.cl$";
        private string PhoneValidador = @"^[0-9]+$";

        public UsuarioValidator()
        {
            RuleFor(user => user.Rut).Matches(RutValidador, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            RuleFor(user => user.Email).Matches(EmailValidador, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            RuleFor(user => user.PhoneNumber).Matches(PhoneValidador, RegexOptions.IgnoreCase | RegexOptions.Multiline);


        }
    }
}
