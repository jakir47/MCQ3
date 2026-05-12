using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCQ3.DataConnect.Migrations
{
    /// <inheritdoc />
    public partial class MediaFileAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: new Guid("c62dbc2a-5221-4e49-9151-1cdb441e48da"));

            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoragePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFiles_UserAccount_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaFiles_UserAccount_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 12, 7, 47, 39, 954, DateTimeKind.Utc).AddTicks(9341));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 12, 7, 47, 39, 954, DateTimeKind.Utc).AddTicks(9373));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 12, 7, 47, 39, 954, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "Address", "Code", "ContactNo", "CreatedAt", "Email", "FatherContact", "FatherName", "IsActive", "MotherContact", "MotherName", "NID", "Name", "PhotoUrl", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("71747bf7-1032-47b8-8c85-5a4dc9f43b17"), "456 Student Ave", "STU001", "555-5678", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "student@mcq2.com", "555-5679", "Robert Johnson", true, "555-5680", "Mary Johnson", "STU001NID", "Alice Johnson", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$ADDb7QOrqcaHikGAVhnf/.KBSkm8DdYorNlXdCYB4hFr78JWgQuVK");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$aEkAmErI8QgSfOwvZQaOueyeH2YZEsoytweXC3kAtqmbZuulHvSeO");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$0Zrd12P28g3SrerGaABR/edLF5H2pV.Th/FO1kGkWGrcx3FF9Wdxa");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_DeletedByUserId",
                table: "MediaFiles",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_UploadedByUserId",
                table: "MediaFiles",
                column: "UploadedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: new Guid("71747bf7-1032-47b8-8c85-5a4dc9f43b17"));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 11, 10, 52, 39, 210, DateTimeKind.Utc).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 11, 10, 52, 39, 210, DateTimeKind.Utc).AddTicks(4638));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 11, 10, 52, 39, 210, DateTimeKind.Utc).AddTicks(4641));

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "Address", "Code", "ContactNo", "CreatedAt", "Email", "FatherContact", "FatherName", "IsActive", "MotherContact", "MotherName", "NID", "Name", "PhotoUrl", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("c62dbc2a-5221-4e49-9151-1cdb441e48da"), "456 Student Ave", "STU001", "555-5678", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "student@mcq2.com", "555-5679", "Robert Johnson", true, "555-5680", "Mary Johnson", "STU001NID", "Alice Johnson", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$7gLx9q20ak3c2B9.jdlXIusF/QiW.4J2YOfX8H3FQ9zju9ltX011O");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$1jUWig/oUUvB1snzd7CSyOo9ZjfBXl6R5WzUsZ4I.Pf0wUFpUoJXq");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$gepOaEjFuWqINM1bQR4/ou8BqEFcNuKxKvA7ETurFDwW5mByV/XzS");
        }
    }
}
