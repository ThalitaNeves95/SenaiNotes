using APISenaiNotes.Models;
using FluentValidation;

namespace APISenaiNotes.Validator
{
    public class TagValidator : AbstractValidator<Tag>
    {
        public TagValidator()
        {
            RuleFor(tag => tag.Nome)
                .NotEmpty()
                .WithMessage("O nome da tag não pode ser vazio.")
                .MaximumLength(50)
                .WithMessage("O nome da tag tera que haver ate 50 caracteres");
        }
    }
}
