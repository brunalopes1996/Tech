using Tech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration.KeyPerFile;
using Org.BouncyCastle.Asn1.Misc;


namespace GStore.Data;

public class AppDbContext : IdentityDbContext<Usuario>
{
public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
{
}

public DbSet<Banca> Bancas {get; set;}
public DbSet<Historico> Historicos {get; set;}
public DbSet<Questionario> Questionarios {get; set;}
public DbSet<Questoes> Questoes {get; set;}
public DbSet<Usuario> Usuarios {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)

    {  
        base.OnModelCreating(builder);
        #region  Renomeação das tabelas de Identity
        builder.Entity<Usuario>().ToTable("usuario");
        builder.Entity<IdentityRole>().ToTable("perfil");
        builder.Entity<IdentityUserRole<string>>().ToTable("usuario_perfil");
        builder.Entity<IdentityUserClaim<string>>().ToTable("usuario_token");
        builder.Entity<IdentityUserLogin<string>>().ToTable("usuario_regra");
        builder.Entity<IdentityUserLogin<string>>().ToTable("usuario_login");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("perfil_regra");
        #endregion
        
        AppDbSeed seed = new(builder);   

    }

}