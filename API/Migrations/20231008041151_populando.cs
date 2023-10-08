using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class populando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "DataNascimento", "Email", "Nome" },
                values: new object[] { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cliente1@example.com", "Cliente 1" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "DataNascimento", "Email", "Nome" },
                values: new object[] { 2, new DateTime(1995, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cliente2@example.com", "Cliente 2" });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "LivroId", "Autor", "DataPublicacao", "Editora", "Titulo" },
                values: new object[] { 1, "Autor 1", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Editora 1", "Livro 1" });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "LivroId", "Autor", "DataPublicacao", "Editora", "Titulo" },
                values: new object[] { 2, "Autor 2", new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Editora 2", "Livro 2" });

            migrationBuilder.InsertData(
                table: "Emprestimos",
                columns: new[] { "EmprestimoId", "ClienteId", "DataDevolucao", "DataEmprestimo", "LivroId" },
                values: new object[] { 1, 1, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Emprestimos",
                columns: new[] { "EmprestimoId", "ClienteId", "DataDevolucao", "DataEmprestimo", "LivroId" },
                values: new object[] { 2, 2, new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Emprestimos",
                keyColumn: "EmprestimoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Emprestimos",
                keyColumn: "EmprestimoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "LivroId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "LivroId",
                keyValue: 2);
        }
    }
}
