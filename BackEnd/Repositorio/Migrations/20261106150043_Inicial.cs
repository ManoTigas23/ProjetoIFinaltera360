using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "GuiasSalvos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jogos",
                table: "Jogos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guias",
                table: "Guias");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Jogos",
                newName: "Jogo");

            migrationBuilder.RenameTable(
                name: "Guias",
                newName: "Guia");

            migrationBuilder.RenameColumn(
                name: "NomeJogo",
                table: "Jogo",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "CapaJogo",
                table: "Jogo",
                newName: "Link da Capa");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Guia",
                newName: "UsuarioID");

            migrationBuilder.RenameColumn(
                name: "JogoId",
                table: "Guia",
                newName: "JogoID");

            migrationBuilder.RenameColumn(
                name: "GuiaId",
                table: "Guia",
                newName: "GuiaID");

            migrationBuilder.RenameColumn(
                name: "VideoYoutube",
                table: "Guia",
                newName: "LinkVideo");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Guia",
                newName: "TituloGuia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jogo",
                table: "Jogo",
                column: "JogoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guia",
                table: "Guia",
                column: "GuiaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jogo",
                table: "Jogo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guia",
                table: "Guia");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Jogo",
                newName: "Jogos");

            migrationBuilder.RenameTable(
                name: "Guia",
                newName: "Guias");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Jogos",
                newName: "NomeJogo");

            migrationBuilder.RenameColumn(
                name: "Link da Capa",
                table: "Jogos",
                newName: "CapaJogo");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Guias",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "JogoID",
                table: "Guias",
                newName: "JogoId");

            migrationBuilder.RenameColumn(
                name: "GuiaID",
                table: "Guias",
                newName: "GuiaId");

            migrationBuilder.RenameColumn(
                name: "TituloGuia",
                table: "Guias",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "LinkVideo",
                table: "Guias",
                newName: "VideoYoutube");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jogos",
                table: "Jogos",
                column: "JogoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guias",
                table: "Guias",
                column: "GuiaId");

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ComentarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GuiaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Texto = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ComentarioId);
                });

            migrationBuilder.CreateTable(
                name: "GuiasSalvos",
                columns: table => new
                {
                    GuiaSalvoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuiaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiasSalvos", x => x.GuiaSalvoId);
                });
        }
    }
}
