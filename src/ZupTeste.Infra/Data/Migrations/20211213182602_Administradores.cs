using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZupTeste.Infra.Data.Migrations
{
    public partial class Administradores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(512)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "varchar(512)", maxLength: 256, nullable: false),
                    Senha = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Administrador",
                columns: new[] { "Id", "CreatedAt", "Email", "LastUpdatedAt", "Nome", "Senha" },
                values: new object[] { new Guid("a04eac1c-f748-47f7-b98a-8851fe4354ba"), new DateTime(2021, 3, 8, 12, 0, 0, 0, DateTimeKind.Utc), "admin@admin.com", new DateTime(2021, 3, 8, 12, 0, 0, 0, DateTimeKind.Utc), "admin", "b16f5428b3b26c8782e791dc4261f57fb54847ce8372694e6841553edf16ab26;Jhhz7rO40tPtKqk" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");
        }
    }
}
