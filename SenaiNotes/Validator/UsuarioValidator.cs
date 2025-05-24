using APISenaiNotes.Models;
using FluentValidation;

namespace APISenaiNotes.Validator
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(usuario => usuario.NomeUsuario)
                .NotEmpty()
                .WithMessage("O nome do usuário não pode ser vazio.")
                .MaximumLength(50)
                .WithMessage("O nome do usuário deve ter no máximo 50 caracteres.");
            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .WithMessage("O email do usuário não pode ser vazio.")
                .EmailAddress()
                .WithMessage("O email do usuário deve ser um endereço de email válido.");
            RuleFor(usuario => usuario.Senha)
                .NotEmpty()
                .WithMessage("A senha do usuário não pode ser vazia.")
                .MinimumLength(6)
                .WithMessage("A senha do usuário deve ter pelo menos 6 caracteres.");
        }
    }
}
