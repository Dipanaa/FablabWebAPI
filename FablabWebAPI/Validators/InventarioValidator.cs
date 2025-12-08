using FablabWebAPI.Entities;
using FluentValidation;

namespace FablabWebAPI.Validators
{
    public class InventarioValidator: AbstractValidator<Inventario>
    {
        public InventarioValidator()
        {
            RuleFor(item => item.Stock).Must(stock => stock >= 0);
            

        }
    }
}
