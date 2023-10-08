using Microsoft.EntityFrameworkCore;
using API.Models;
using System;

namespace API.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento entre Emprestimo e Livro
            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Livro)
                .WithMany(l => l.Emprestimos)
                .HasForeignKey(e => e.LivroId)
                .OnDelete(DeleteBehavior.Restrict); // Pode ajustar o comportamento de exclusão aqui

            // Configuração do relacionamento entre Emprestimo e Cliente
            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Cliente)
                .WithMany(c => c.Emprestimos)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // Pode ajustar o comportamento de exclusão aqui

            // Populando a base de dados com alguns registros iniciais
            modelBuilder.Entity<Livro>().HasData(
                new Livro
                {
                    LivroId = 1,
                    Titulo = "Livro 1",
                    Autor = "Autor 1",
                    Editora = "Editora 1",
                    DataPublicacao = DateTime.Parse("2022-01-01")
                },
                new Livro
                {
                    LivroId = 2,
                    Titulo = "Livro 2",
                    Autor = "Autor 2",
                    Editora = "Editora 2",
                    DataPublicacao = DateTime.Parse("2022-02-01")
                }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    ClienteId = 1,
                    Nome = "Cliente 1",
                    DataNascimento = DateTime.Parse("1990-01-01"),
                    Email = "cliente1@example.com"
                },
                new Cliente
                {
                    ClienteId = 2,
                    Nome = "Cliente 2",
                    DataNascimento = DateTime.Parse("1995-02-01"),
                    Email = "cliente2@example.com"
                }
            );

            modelBuilder.Entity<Emprestimo>().HasData(
                new Emprestimo
                {
                    EmprestimoId = 1,
                    LivroId = 1,
                    ClienteId = 1,
                    DataEmprestimo = DateTime.Parse("2022-03-01"),
                    DataDevolucao = DateTime.Parse("2022-03-15")
                },
                new Emprestimo
                {
                    EmprestimoId = 2,
                    LivroId = 2,
                    ClienteId = 2,
                    DataEmprestimo = DateTime.Parse("2022-04-01"),
                    DataDevolucao = DateTime.Parse("2022-04-15")
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
