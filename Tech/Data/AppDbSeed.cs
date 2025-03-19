using Tech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GStore.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder builder)
    {
        List<Banca> bancas = new() {
            new Banca { Id = 1, Nome = "Smartphones" },
            
        };
        builder.Entity<Banca>().HasData(bancas);

        List<Historico>historicos = new List<Historico>
        {
            new Historico { },
        };
        builder.Entity<Historico>().HasData(historicos);

        List<Questionario> produtoFotos = new List<Questionario>
        {
            // Questão 1
            new Questionario { Id = 1, },
            

            // Questão 2
            new Questionario { Id = 4,  },
           

            // Questão 3
            new Questionario { Id = 7,  },
            

            // Questão 4
            new Questionario { Id = 10,  },
           

            // Questão 5
            new Questionario { Id = 13,  },
            
            // Questão 6
            new Questionario { Id = 16, },
            

            // Questão 7
            new Questionario { Id = 19,  },
            
            // Questão 8
            new Questionario { Id = 21, },
            
            // Questão 9
            new Questionario { Id = 24,  },
           

            // Questão 10
            new Questionario { Id = 27,  },
            

            
        };
        builder.Entity<Questionario>().HasData(produtoFotos);


        #region Populate Roles - Perfis de Usuário
        List<IdentityRole> roles = new()
        {
            new IdentityRole() {
               Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
               Name = "Administrador",
               NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole() {
               Id = "bec71b05-8f3d-4849-88bb-0e8d518d2de8",
               Name = "Funcionário",
               NormalizedName = "FUNCIONÁRIO"
            },
            new IdentityRole() {
               Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
               Name = "Cliente",
               NormalizedName = "CLIENTE"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Populate Usuário
        List<Usuario> usuarios = new() {
            new Usuario(){
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Email = "bruuna.oliveiraa1@gmail.com",
                NormalizedEmail = "BRUUNA.OLIVEIRAA1@GMAIL.COM",
                UserName = "BrunaLopes",
                NormalizedUserName = "BRUNALOPES",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Bruna Maria Lopes de Oliveira",                
                Foto = "/img/usuarios/ddf093a6-6cb5-4ff7-9a64-83da34aee005.png"
            }
        };
        foreach (var user in usuarios)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<Usuario>().HasData(usuarios);
        #endregion

        #region Populate UserRole - Usuário com Perfil
        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>() {
                UserId = usuarios[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>() {
                UserId = usuarios[0].Id,
                RoleId = roles[1].Id
            },
            new IdentityUserRole<string>() {
                UserId = usuarios[0].Id,
                RoleId = roles[2].Id
            }
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }
}