using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZupTeste.Infra.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(512)", maxLength: 128, nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(512)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "varchar(512)", maxLength: 256, nullable: false),
                    NumeroChapa = table.Column<string>(type: "varchar(512)", maxLength: 30, nullable: false),
                    Senha = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    LiderId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionario_Funcionario_LiderId",
                        column: x => x.LiderId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<string>(type: "varchar(512)", maxLength: 20, nullable: false),
                    FuncionarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefone_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_LiderId",
                table: "Funcionario",
                column: "LiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_NumeroChapa",
                table: "Funcionario",
                column: "NumeroChapa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_FuncionarioId",
                table: "Telefone",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Funcionario");
        }
    }
}
