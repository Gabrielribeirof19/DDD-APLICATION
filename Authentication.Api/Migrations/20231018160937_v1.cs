using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password_HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password_PreviousHashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password_Salt = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: true),
                    Password_IsFirstTime = table.Column<bool>(type: "bit", nullable: true),
                    Password_Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password_Checked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password_RecoveryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password_RecoveryExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonIdCreated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SocialName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(120)", maxLength: 120, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonIdCreated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Primary = table.Column<bool>(type: "bit", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOrProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressZip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonIdCreated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_Addresses", x => new { x.PersonId, x.Id });
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons_Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: true),
                    IsRecovery = table.Column<bool>(type: "bit", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    Verified = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonIdCreated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_Emails", x => new { x.PersonId, x.Id });
                    table.ForeignKey(
                        name: "FK_Persons_Emails_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRole_Persons_UserId",
                        column: x => x.UserId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Cpf",
                table: "Persons",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons_Addresses");

            migrationBuilder.DropTable(
                name: "Persons_Emails");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
