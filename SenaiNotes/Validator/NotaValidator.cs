using APISenaiNotes.Models;
using FluentValidation;

namespace APISenaiNotes.Validator
{
    public class NotaValidator : AbstractValidator<Nota>
    {
        public NotaValidator()
        { 
            RuleFor(nota => nota.Titulo)
                .NotEmpty()
                .WithMessage("O título não pode ser vazio.")
                .MaximumLength(100)
                .WithMessage("O título deve ter no máximo 100 caracteres.");
            RuleFor(nota => nota.Imagem)
                .MaximumLength(200)
                .WithMessage("A imagem deve ter no máximo 200 caracteres.");
            RuleFor(nota => nota.Conteudo)
                .NotEmpty()
                .WithMessage("O conteúdo não pode ser vazio.");
        }
    }
}
