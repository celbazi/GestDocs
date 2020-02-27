using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestDoc.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adherent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    IsMember = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adherent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeReunion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Libelle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeReunion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reunion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateReunion = table.Column<DateTime>(nullable: false),
                    Remarque = table.Column<string>(nullable: true),
                    TypeReunionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reunion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reunion_TypeReunion_TypeReunionID",
                        column: x => x.TypeReunionID,
                        principalTable: "TypeReunion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    URL = table.Column<string>(nullable: true),
                    ReunionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Document_Reunion_ReunionID",
                        column: x => x.ReunionID,
                        principalTable: "Reunion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReunionID = table.Column<int>(nullable: false),
                    AdherentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Participation_Adherent_AdherentID",
                        column: x => x.AdherentID,
                        principalTable: "Adherent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participation_Reunion_ReunionID",
                        column: x => x.ReunionID,
                        principalTable: "Reunion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_ReunionID",
                table: "Document",
                column: "ReunionID");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_AdherentID",
                table: "Participation",
                column: "AdherentID");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_ReunionID",
                table: "Participation",
                column: "ReunionID");

            migrationBuilder.CreateIndex(
                name: "IX_Reunion_TypeReunionID",
                table: "Reunion",
                column: "TypeReunionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Participation");

            migrationBuilder.DropTable(
                name: "Adherent");

            migrationBuilder.DropTable(
                name: "Reunion");

            migrationBuilder.DropTable(
                name: "TypeReunion");
        }
    }
}
