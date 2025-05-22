//using APISenaiNotes.Models;
//using Microsoft.EntityFrameworkCore;
//using APISenaiNotes.Context;
//namespace APISenaiNotes.Users
//{


//    public static class SeedData
//    {
//        public static void Inicializar(IServiceProvider serviceProvider)
//        {
//            var options = serviceProvider.GetRequiredService<DbContextOptions<SenaiNotesContext>>();
//            var config = serviceProvider.GetRequiredService<IConfiguration>();

//            var context = new SenaiNotesContext(options, config);
//            {
                
//                if (!context.Usuarios.Any())
//                {
//                    context.Usuarios.Add(new Usuario
//                    {
//                        UsuarioId = 1,
//                        NomeUsuario = "admin",
//                        Email = "admin@senai.com",
//                        Senha = "admin123",
//                        DataCriacao = DateTime.Now
//                    });

//                    context.SaveChanges();
//                }
//            }
//        }
//    }
//}
