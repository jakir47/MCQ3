using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCQ3.DataConnect.Migrations
{
    /// <inheritdoc />
    public partial class MediaFileVideoLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_UserAccount_DeletedByUserId",
                table: "MediaFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_UserAccount_UploadedByUserId",
                table: "MediaFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaFiles",
                table: "MediaFiles");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: new Guid("71747bf7-1032-47b8-8c85-5a4dc9f43b17"));

            migrationBuilder.RenameTable(
                name: "MediaFiles",
                newName: "MediaFile");

            migrationBuilder.RenameIndex(
                name: "IX_MediaFiles_UploadedByUserId",
                table: "MediaFile",
                newName: "IX_MediaFile_UploadedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaFiles_DeletedByUserId",
                table: "MediaFile",
                newName: "IX_MediaFile_DeletedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "VideoLinkUrl",
                table: "MediaFile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaFile",
                table: "MediaFile",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 12, 7, 57, 21, 343, DateTimeKind.Utc).AddTicks(4539));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 12, 7, 57, 21, 343, DateTimeKind.Utc).AddTicks(4558));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 12, 7, 57, 21, 343, DateTimeKind.Utc).AddTicks(4561));

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "Address", "Code", "ContactNo", "CreatedAt", "Email", "FatherContact", "FatherName", "IsActive", "MotherContact", "MotherName", "NID", "Name", "PhotoUrl", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("30dd1f9d-ef11-4740-b2d5-6a028da0862c"), "456 Student Ave", "STU001", "555-5678", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "student@mcq2.com", "555-5679", "Robert Johnson", true, "555-5680", "Mary Johnson", "STU001NID", "Alice Johnson", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$KEJZE1dc0n6ddmLfVnshquTCbod38WZtQ/qZgURsCAXPZ2.bbd6K2");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$B2GpGs5bzo0VQqpazAIICe22yACDhxfR2nH7FdeXxPno0Vdn.z3lC");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$OwQNCS2aTcYVbuy1hvsxr.qXXaCZxSRfGR/7Gfv9kDA4RXRy1e9VK");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFile_UserAccount_DeletedByUserId",
                table: "MediaFile",
                column: "DeletedByUserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFile_UserAccount_UploadedByUserId",
                table: "MediaFile",
                column: "UploadedByUserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFile_UserAccount_DeletedByUserId",
                table: "MediaFile");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaFile_UserAccount_UploadedByUserId",
                table: "MediaFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaFile",
                table: "MediaFile");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Id",
                keyValue: new Guid("30dd1f9d-ef11-4740-b2d5-6a028da0862c"));

            migrationBuilder.DropColumn(
                name: "VideoLinkUrl",
                table: "MediaFile");

            migrationBuilder.RenameTable(
                name: "MediaFile",
                newName: "MediaFiles");

            migrationBuilder.RenameIndex(
                name: "IX_MediaFile_UploadedByUserId",
                table: "MediaFiles",
                newName: "IX_MediaFiles_UploadedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaFile_DeletedByUserId",
                table: "MediaFiles",
                newName: "IX_MediaFiles_DeletedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaFiles",
                table: "MediaFiles",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_UserAccount_DeletedByUserId",
                table: "MediaFiles",
                column: "DeletedByUserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_UserAccount_UploadedByUserId",
                table: "MediaFiles",
                column: "UploadedByUserId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
