using Microsoft.AspNetCore.Identity;

namespace APISenaiNotes
{
    public interface IArgon2PasswordHasher<T>
    {
        string HashPassword(T user, string password);
        PasswordVerificationResult VerifyHashedPassword(T user, string hashedPassword, string providedPassword);
    }
    public class Argon2PasswordHasher<T> : IArgon2PasswordHasher<T>
    {
        public string HashPassword(T user, string password)
        {
            // Implementação do hash usando Argon2
            // Aqui você pode usar uma biblioteca como o Konscious.Security.Cryptography.Argon2
            throw new NotImplementedException();
        }
        public PasswordVerificationResult VerifyHashedPassword(T user, string hashedPassword, string providedPassword)
        {
            // Implementação da verificação do hash usando Argon2
            throw new NotImplementedException();
        }
    }

}