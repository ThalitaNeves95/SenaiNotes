using APISenaiNotes.Models;


namespace APISenaiNotes.Services
{
    internal class _hasher
    {
        internal static string Hash(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }


    }
}
