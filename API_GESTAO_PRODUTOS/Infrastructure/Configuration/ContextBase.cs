using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            #region ApplicationUser
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(m => m.Id);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(m => m.Produtos)
                .WithOne(m => m.ApplicationUser)
                .HasForeignKey(m => m.UserId);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(m => m.Fornecedores)
                .WithOne(m => m.ApplicationUser)
                .HasForeignKey(m => m.UserId);
            #endregion

            #region Produto
            modelBuilder.Entity<Produto>().ToTable("TB_PRODUTO").HasKey(m => m.Id);
            modelBuilder.Entity<Produto>()
                .HasOne(m => m.ApplicationUser)
                .WithMany(m => m.Produtos)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Produto>()
                .HasOne(m => m.Fornecedor)
                .WithMany(m => m.Produtos)
                .HasForeignKey(m => m.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Fornecedor
            modelBuilder.Entity<Fornecedor>().ToTable("TB_FORNECEDOR").HasKey(m => m.Id);
            modelBuilder.Entity<Fornecedor>()
                .HasOne(m => m.ApplicationUser)
                .WithMany(m => m.Fornecedores)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Fornecedor>()
                .HasMany(m => m.Produtos)
                .WithOne(m => m.Fornecedor)
                .HasForeignKey(m => m.FornecedorId);
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public string ObterStringConexao()
        {
            return "Data Source=DESKTOP-27P7RL9\\SQLEXPRESS;Initial Catalog=GESTAO_PRODUTOS;Integrated Security=False;User ID=sa;Password=admin123;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            //return "Data Source=DESKTOP-27P7RL9\\SQLEXPRESS;Initial Catalog=GESTAO_PRODUTOS;User ID=sa;Password=admin123;Encrypt=False";
        }
    }
}
