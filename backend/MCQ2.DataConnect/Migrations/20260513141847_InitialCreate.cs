using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MCQ3.DataConnect.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TempPassword = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccount_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserAccount_UserAccount_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLog_UserAccount_ActorId",
                        column: x => x.ActorId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailVerification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailVerification_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoragePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoLinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFile_UserAccount_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaFile_UserAccount_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordReset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordReset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordReset_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Student_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Teacher_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subject_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapter_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalMarks = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    PassingScore = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    TimeLimitSeconds = table.Column<int>(type: "int", nullable: true),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvailableUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAttempts = table.Column<int>(type: "int", nullable: true),
                    NegativeMarking = table.Column<bool>(type: "bit", nullable: false),
                    ShuffleQuestions = table.Column<bool>(type: "bit", nullable: false),
                    ShuffleOptions = table.Column<bool>(type: "bit", nullable: false),
                    ShowAnswersAfter = table.Column<bool>(type: "bit", nullable: false),
                    AutoReleaseResults = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exam_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StemText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StemImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StemAudioPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StemVideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositiveMarks = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    NegativeMarks = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_Question_SourceQuestionId",
                        column: x => x.SourceQuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentChapter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AssignedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentChapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentChapter_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentChapter_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentChapter_UserAccount_AssignedById",
                        column: x => x.AssignedById,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttemptNumber = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSpentSecs = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    IsPassed = table.Column<bool>(type: "bit", nullable: true),
                    IsReleased = table.Column<bool>(type: "bit", nullable: false),
                    AutoSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    ResumeData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attempt_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attempt_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Attempt_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Enrolment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EnrolledById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransferredFromChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrolment_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrolment_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Enrolment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Enrolment_UserAccount_EnrolledById",
                        column: x => x.EnrolledById,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrolment_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnswerOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AudioPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerOption_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttemptAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttemptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true),
                    MarksAwarded = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttemptAnswer_AnswerOption_SelectedOptionId",
                        column: x => x.SelectedOptionId,
                        principalTable: "AnswerOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AttemptAnswer_Attempt_AttemptId",
                        column: x => x.AttemptId,
                        principalTable: "Attempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttemptAnswer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Admin", new DateTime(2026, 5, 13, 14, 18, 46, 814, DateTimeKind.Utc).AddTicks(8146) },
                    { new Guid("00000002-0000-0000-0000-000000000002"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Teacher", new DateTime(2026, 5, 13, 14, 18, 46, 814, DateTimeKind.Utc).AddTicks(8160) },
                    { new Guid("00000003-0000-0000-0000-000000000003"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Student", new DateTime(2026, 5, 13, 14, 18, 46, 814, DateTimeKind.Utc).AddTicks(8162) }
                });

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Email", "FullName", "IsActive", "PasswordHash", "RoleId", "TempPassword", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@mcq2.com", "Admin User", true, "$2a$11$9zMGAFj81zb0.N5oJfHa2eIhaT5gwvA8TxSRUzLEMq/OynlS5yr5m", new Guid("00000001-0000-0000-0000-000000000001"), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "teacher@mcq2.com", "John Smith", true, "$2a$11$6zlrpfd35ew0OgYgws2C2eg.Ra2lP3vujF/5AVnF4JN7IuLRgJH3S", new Guid("00000002-0000-0000-0000-000000000002"), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "teacher" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "student@mcq2.com", "Alice Johnson", true, "$2a$11$GamJVXp2C6vO1F0H4lRUQuelLQG2jz4Ze9BH.oR1QrWTAevMrhGa6", new Guid("00000003-0000-0000-0000-000000000003"), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "student" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "UserId", "Address", "Code", "ContactNo", "CreatedAt", "Email", "FatherContact", "FatherName", "IsActive", "MotherContact", "MotherName", "NID", "Name", "PhotoUrl", "UpdatedAt" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), "456 Student Ave", "STU001", "555-5678", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "student@mcq2.com", "555-5679", "Robert Johnson", true, "555-5680", "Mary Johnson", "STU001NID", "Alice Johnson", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Teacher",
                columns: new[] { "UserId", "Address", "ContactNo", "CreatedAt", "Email", "IsActive", "NID", "Name", "PhotoUrl", "Title", "UpdatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "123 Teacher St", "555-1234", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "teacher@mcq2.com", true, "TCH001", "John Smith", null, "Senior Instructor", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id", "CreatedAt", "Description", "IsArchived", "TeacherId", "Title", "UpdatedAt", "UserAccountId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn C# fundamentals", false, new Guid("22222222-2222-2222-2222-222222222222"), "C# Programming", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn data structures", false, new Guid("22222222-2222-2222-2222-222222222222"), "Data Structures", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn SQL fundamentals", false, new Guid("22222222-2222-2222-2222-222222222222"), "SQL Basics", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "Chapter",
                columns: new[] { "Id", "CreatedAt", "Description", "IsArchived", "OrderIndex", "SubjectId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Variables, types, and control flow", false, 1, new Guid("44444444-4444-4444-4444-444444444444"), "C# Basics", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Arrays, lists, and stacks", false, 1, new Guid("88888888-8888-8888-8888-888888888888"), "Data Structures Basics", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SELECT, WHERE, and JOIN", false, 1, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "SQL Basics", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "Id", "ChapterId", "CreatedAt", "Difficulty", "Explanation", "NegativeMarks", "PositiveMarks", "SourceQuestionId", "StemAudioPath", "StemImagePath", "StemText", "StemVideoUrl", "Tags", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-0001-0001-0001-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "In C#, variables are declared by specifying a type followed by an identifier.", 0m, 1m, null, null, null, "Which keyword is used to declare a variable in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0002-0002-0002-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Value types in C# have default values. int defaults to 0.", 0m, 1m, null, null, null, "What is the default value of an int in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0003-0003-0003-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "C# identifiers must start with a letter or underscore and can contain letters, digits, and underscores.", 0m, 1m, null, null, null, "Which of these is a valid C# identifier?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0004-0004-0004-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "typeof returns a Type object representing the specified type.", 0m, 1m, null, null, null, "What does 'typeof' operator return?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0005-0005-0005-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Arrays in C# are zero-indexed, meaning the first element is at index 0.", 0m, 1m, null, null, null, "Which collection is zero-indexed in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0006-0006-0006-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "object is the base class for all types in C#.", 0m, 1m, null, null, null, "What is the base class of all types in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0007-0007-0007-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "The colon (:) syntax is used for inheritance in C#.", 0m, 1m, null, null, null, "Which keyword is used to inherit a class?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0008-0008-0008-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A namespace is a container for classes and other types to organize code.", 0m, 1m, null, null, null, "What is a namespace in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0009-0009-0009-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "If no access modifier is specified, classes are internal by default.", 0m, 1m, null, null, null, "Which access modifier is default for a class?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0010-0010-0010-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "using statement ensures proper disposal of resources.", 0m, 1m, null, null, null, "What is the purpose of 'using' statement?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0011-0011-0011-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "string is a reference type, not a value type.", 0m, 1m, null, null, null, "Which of these is NOT a value type?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0012-0012-0012-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Boxing is converting a value type to object type.", 0m, 1m, null, null, null, "What is boxing in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0013-0013-0013-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "struct is a value type (stack) while class is a reference type (heap).", 0m, 1m, null, null, null, "What is the difference between 'struct' and 'class'?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0014-0014-0014-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Polymorphism allows methods to have different implementations based on the object type.", 0m, 1m, null, null, null, "What is polymorphism?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11111111-0015-0015-0015-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "An interface defines a contract that classes can implement.", 0m, 1m, null, null, null, "What is an interface in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0001-0001-0001-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Array provides O(1) constant time access by index.", 0m, 1m, null, null, null, "What is the time complexity of accessing an element in an array by index?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0002-0002-0002-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Stack follows Last In First Out (LIFO) order.", 0m, 1m, null, null, null, "Which data structure follows LIFO order?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0003-0003-0003-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "List<T> starts with an empty capacity and grows as needed.", 0m, 1m, null, null, null, "What is the default capacity of a List<T> in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0004-0004-0004-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "HashSet<T> does not allow duplicate elements.", 0m, 1m, null, null, null, "Which collection does not allow duplicate elements?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0005-0005-0005-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Linear search in unsorted array takes O(n) time.", 0m, 1m, null, null, null, "What is the complexity of searching in an unsorted array?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0006-0006-0006-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "2D arrays in C# are declared as type[,] variableName.", 0m, 1m, null, null, null, "How do you declare a 2D array in C#?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0007-0007-0007-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Add() method adds an element at the end of the List.", 0m, 1m, null, null, null, "Which method is used to add an element at the end of a List?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0008-0008-0008-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A hash table maps keys to values using a hash function.", 0m, 1m, null, null, null, "What is a hash table?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0009-0009-0009-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Dictionary<TKey, TValue> is best for key-value pairs.", 0m, 1m, null, null, null, "Which collection is best for key-value pairs?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0010-0010-0010-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Array is strongly typed, ArrayList can hold any type.", 0m, 1m, null, null, null, "What is the difference between Array and ArrayList?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0011-0011-0011-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Inserting at beginning is O(n) because all elements shift.", 0m, 1m, null, null, null, "What is the time complexity of inserting at the beginning of a List?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0012-0012-0012-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Queue follows First In First Out (FIFO) order.", 0m, 1m, null, null, null, "What is a queue data structure?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0013-0013-0013-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Stack is LIFO, Queue is FIFO.", 0m, 1m, null, null, null, "What is the difference between Stack and Queue?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0014-0014-0014-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A linked list is a linear collection of nodes.", 0m, 1m, null, null, null, "What is a linked list?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-0015-0015-0015-222222222222"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Array has O(n) space complexity for n elements.", 0m, 1m, null, null, null, "What is the space complexity of an array?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0001-0001-0001-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "SELECT is the SQL keyword used to retrieve data.", 0m, 1m, null, null, null, "Which SQL keyword is used to retrieve data from a database?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0002-0002-0002-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "HAVING clause filters groups after GROUP BY.", 0m, 1m, null, null, null, "Which clause is used to filter groups in SQL?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0003-0003-0003-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "JOIN combines rows from two tables based on a related column.", 0m, 1m, null, null, null, "What does JOIN do in SQL?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0004-0004-0004-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "COUNT() function counts the number of rows.", 0m, 1m, null, null, null, "Which function is used to count rows in SQL?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0005-0005-0005-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "ORDER BY defaults to ASC (ascending).", 0m, 1m, null, null, null, "What is the default order of ORDER BY?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0006-0006-0006-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "FULL OUTER JOIN, LEFT JOIN, RIGHT JOIN, INNER JOIN are the types.", 0m, 1m, null, null, null, "Which is NOT a type of JOIN?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0007-0007-0007-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "DISTINCT returns only unique values.", 0m, 1m, null, null, null, "What does DISTINCT do?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0008-0008-0008-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "UPDATE statement modifies existing data in a table.", 0m, 1m, null, null, null, "Which is used to update data in a table?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0009-0009-0009-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Primary key uniquely identifies each row in a table.", 0m, 1m, null, null, null, "What is a primary key?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0010-0010-0010-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "FOREIGN KEY creates a relationship between tables.", 0m, 1m, null, null, null, "What does FOREIGN KEY do?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0011-0011-0011-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "GROUP BY is used with aggregate functions.", 0m, 1m, null, null, null, "Which clause is used with aggregate functions?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0012-0012-0012-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WHERE filters before grouping, HAVING filters after.", 0m, 1m, null, null, null, "What is the difference between WHERE and HAVING?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0013-0013-0013-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "A subquery is a query nested inside another query.", 0m, 1m, null, null, null, "What is a subquery?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0014-0014-0014-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "UNION combines results of two SELECT statements.", 0m, 1m, null, null, null, "What does UNION do?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-0015-0015-0015-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Normalization organizes data to reduce redundancy.", 0m, 1m, null, null, null, "What is normalization?", null, "[]", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "AnswerOption",
                columns: new[] { "Id", "AudioPath", "CreatedAt", "ImagePath", "IsCorrect", "OptionText", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("007de665-5d14-4481-900a-7ba0805c23d8"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7080), null, false, "Array", 2, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7081) },
                    { new Guid("00a076ce-6145-42cf-bb33-e2450b2bdbdb"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7603), null, true, "A linear collection of nodes where each node points to the next", 1, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7603) },
                    { new Guid("031909ff-5a28-4496-a3c4-087f51c1f33b"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7823), null, false, "Updates records", 3, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7824) },
                    { new Guid("05a24496-1d7f-403f-ae38-020d47bb5f59"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6910), null, false, "struct supports inheritance", 2, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6910) },
                    { new Guid("06a72bd9-ea98-4ea0-ac5d-66dd8088ad4c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6778), null, true, "To ensure proper disposal of resources", 1, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6778) },
                    { new Guid("06b070ad-76b7-4cd8-ba55-6b2cc49d1ab5"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8047), null, false, "INSERT", 0, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8047) },
                    { new Guid("0918c831-2aa6-4e69-b315-0fa15fff8032"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6313), null, true, "int", 1, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6313) },
                    { new Guid("099ece2e-bdcf-40a5-81a7-cbb5e3017aff"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7845), null, false, "SUM()", 0, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7845) },
                    { new Guid("0ab8cc9a-219a-4a8f-b56c-d22c1f3068f3"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6695), null, false, "A loop structure", 2, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6695) },
                    { new Guid("0b3956cf-beb5-4906-8933-aeb5f30f2e8f"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7574), null, false, "Both are the same", 2, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7575) },
                    { new Guid("0c6a6367-421f-495b-85d5-b4d1fbe8ba5c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6374), null, false, "1", 3, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6374) },
                    { new Guid("0d136b1a-6498-4514-a4da-e261ad81d049"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6731), null, false, "public", 0, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6731) },
                    { new Guid("0d364272-c81d-4b14-aeb8-590e91894fdf"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7311), null, false, "Push()", 2, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7311) },
                    { new Guid("12ecdabf-55f9-4cef-a7b9-6e5b2604ddb8"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8203), null, false, "HAVING", 3, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8203) },
                    { new Guid("1465b162-1614-4345-ad08-aaadf13a7313"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6624), null, false, "ValueType", 2, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6625) },
                    { new Guid("14c37dd7-0037-46c2-aab3-6eae4215bfc6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8061), null, false, "CHANGE", 3, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8061) },
                    { new Guid("18d049ed-bb60-46f1-89bc-dc3522c2a14d"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6653), null, true, ":", 1, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6653) },
                    { new Guid("192dab3f-6cf9-4c19-9ab7-484606929d3e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8015), null, false, "Sorts data", 0, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8015) },
                    { new Guid("199f3c93-7e82-4e87-a8af-ef8497d4d038"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7114), null, false, "8", 2, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7114) },
                    { new Guid("1a3cd5ce-23d8-4617-b16a-3281efdd093d"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6439), null, false, "string", 0, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6439) },
                    { new Guid("1b705daa-b08b-476a-b4e6-313c76d2babf"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7042), null, false, "O(n)", 1, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7042) },
                    { new Guid("1cb611ad-4c2b-4acd-91ab-bd0c6c25c755"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7753), null, false, "RETRIEVE", 3, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7753) },
                    { new Guid("1cff72e8-ad96-4f65-b31d-85e518e4fae1"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6485), null, false, "HashSet", 3, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6485) },
                    { new Guid("1d432324-1d0d-44a6-8728-fd60bd58a452"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6743), null, false, "protected", 3, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6743) },
                    { new Guid("1df04eee-16aa-4dc1-a5e5-197971e38cc4"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7308), null, true, "Add()", 1, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7308) },
                    { new Guid("21dc1dcf-b112-4118-885f-3ca5fd787599"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6816), null, false, "To handle exceptions", 3, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6817) },
                    { new Guid("255ac0cd-503c-4d0d-93dd-8409b4857216"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7543), null, false, "Random access", 2, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7543) },
                    { new Guid("26337fb6-06b4-4163-95e0-f2911badb63b"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8320), null, false, "Adding more tables", 0, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8321) },
                    { new Guid("28f454e4-45b0-45ce-87fe-a3e96faa7057"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7049), null, false, "O(n^2)", 3, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7049) },
                    { new Guid("2932118a-969c-4d75-b9bc-1981f79eba72"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8362), null, false, "Encrypting data", 2, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8362) },
                    { new Guid("2bfd481b-7766-4f85-bedc-c9da103c625e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8291), null, true, "Combines results of two queries, removes duplicates", 1, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8291) },
                    { new Guid("2e74328b-3646-4b59-871c-88af4a6cc8e9"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8230), null, false, "HAVING is faster", 2, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8230) },
                    { new Guid("2f1a7aa1-598f-4313-85d8-e2b3ad62622d"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7784), null, false, "FILTER", 2, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7784) },
                    { new Guid("2f6e2128-e93a-4441-8ef0-a979509374e5"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6944), null, false, "Creating multiple objects", 3, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6945) },
                    { new Guid("3096a4d8-0c5e-4898-a0d8-7ef26cc4a754"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6841), null, false, "bool", 1, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6841) },
                    { new Guid("311ec6df-0685-478b-9777-6331a62477d1"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7409), null, true, "Dictionary<TKey, TValue>", 1, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7410) },
                    { new Guid("31a0e3e7-b612-41b0-8123-cfa0fa8b1971"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7186), null, false, "O(n log n)", 3, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7186) },
                    { new Guid("349cd04f-faab-408c-a81a-77393a158f51"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6409), null, false, "2var", 1, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6409) },
                    { new Guid("35e7ca79-1b8a-4675-b310-4089cacce6b2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6659), null, false, "implements", 3, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6660) },
                    { new Guid("3b1ffb72-b812-48d1-84b0-0ca3616d1df3"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8259), null, true, "A query inside another query", 1, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8259) },
                    { new Guid("3c64d625-432e-49c0-b769-a539dd1ad47c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7416), null, false, "Stack<T>", 3, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7416) },
                    { new Guid("3da9873b-d70a-4085-ab3b-b6c5752af992"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6869), null, false, "Converting object to value type", 0, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6869) },
                    { new Guid("3f54e514-cf9e-47ca-9e26-d375c116d721"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8020), null, true, "Returns only unique values", 1, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8021) },
                    { new Guid("3f5ab9b9-2269-49aa-ae39-c3561354569a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7314), null, false, "Enqueue()", 3, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7314) },
                    { new Guid("409bed7a-48c1-4f25-be6c-6f7f4677614a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7960), null, false, "None", 3, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7960) },
                    { new Guid("40b801bf-8a51-43c5-b370-08a9ee1cd705"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8023), null, false, "Filters data", 2, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8024) },
                    { new Guid("457ca314-2edc-4b88-a6df-2358751c06da"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8287), null, false, "Combines all rows", 0, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8288) },
                    { new Guid("461e3e58-d0d2-428b-b5df-6fd89a9da249"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8088), null, true, "Unique identifier for each row", 1, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8088) },
                    { new Guid("4a22e302-369d-4185-a11d-de33a70aa563"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7956), null, false, "Random", 2, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7957) },
                    { new Guid("4a2eb4e1-d717-4234-b2c0-6e364f7cac2a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6932), null, false, "Multiple inheritance", 0, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6933) },
                    { new Guid("4e916c02-b3ac-4d0d-9393-043f06ca302a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6478), null, true, "Array", 1, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6479) },
                    { new Guid("5098bdf1-ec1a-4e32-8100-bb3020d25ee9"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8165), null, false, "Deletes records", 2, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8165) },
                    { new Guid("53dab9d8-f6ef-4638-aa18-d1d4024cc6c2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7477), null, false, "ArrayList uses less memory", 2, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7478) },
                    { new Guid("55086c11-9c91-4bed-bc55-c1640861dade"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8162), null, false, "Creates a new table", 1, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8162) },
                    { new Guid("55c73542-cccd-4c5e-8d92-97d8e2e7fc84"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7474), null, true, "Array is strongly typed, ArrayList can hold any type", 1, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7474) },
                    { new Guid("5614d3e9-0bed-436a-a8d7-18c2f26a96ae"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7086), null, false, "List", 3, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7086) },
                    { new Guid("56c01933-50ea-459a-af2c-12795d4cc0b0"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6692), null, true, "A container for organizing types", 1, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6692) },
                    { new Guid("575aa2f4-eea9-466f-8ffe-a195f4542cf3"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6774), null, false, "To import namespaces", 0, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6775) },
                    { new Guid("576ced53-ae20-4663-8782-29b6369275b7"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6938), null, true, "Same method behaves differently based on object type", 1, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6938) },
                    { new Guid("57c04051-8196-476c-9d4f-c5315582d776"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7994), null, true, "LOOP JOIN", 3, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7994) },
                    { new Guid("597298a6-9d7b-4935-8755-8c2c1b77eaa2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7279), null, false, "List<int[]> arr", 2, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7279) },
                    { new Guid("5a667333-0f6e-4126-a262-af132d28259c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7811), null, false, "Deletes records", 0, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7811) },
                    { new Guid("5aaa5e15-8036-4314-b0c7-e50af5523018"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7857), null, false, "MAX()", 3, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7857) },
                    { new Guid("5c18db5b-45ce-4239-8932-12adde8a2831"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7538), null, true, "First In First Out", 1, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7538) },
                    { new Guid("5c84a8e1-7583-47a1-87b6-80502650ba88"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6364), null, true, "0", 1, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6364) },
                    { new Guid("5c96338c-6184-41fb-b36c-ea542942d04f"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7119), null, false, "16", 3, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7120) },
                    { new Guid("5c9fb73e-2f1c-4f79-988b-ed7ab37c2d89"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8194), null, true, "GROUP BY", 1, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8195) },
                    { new Guid("5e2f5811-4d4b-4bf3-839c-8f6638972a27"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7851), null, false, "TOTAL()", 2, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7852) },
                    { new Guid("5ebcfea0-e3fa-487c-8f88-ceb09beaa716"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7183), null, false, "O(log n)", 2, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7183) },
                    { new Guid("5fbb4521-ab9c-4c0e-8375-15ba7684b26c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6837), null, false, "int", 0, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6837) },
                    { new Guid("6141f693-f09e-4b0d-890b-8b0bdaae4bc2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8255), null, false, "A JOIN operation", 0, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8255) },
                    { new Guid("6197d575-17f2-4a69-89b8-195f683100d8"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7483), null, false, "They are the same", 3, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7483) },
                    { new Guid("624f7bc8-eb68-4d0c-bca3-35a6f3cfb771"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7566), null, false, "Stack is FIFO, Queue is LIFO", 0, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7566) },
                    { new Guid("62584360-379c-41a7-9a73-fc02ce452678"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6297), null, false, "var", 0, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6298) },
                    { new Guid("6424fcf0-f2bd-4e8f-afc0-db4e55f7b984"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6913), null, false, "class cannot have methods", 3, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6913) },
                    { new Guid("64db8bcc-ff6d-4838-ad2c-2c2ee9c934cf"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7951), null, true, "ASC", 1, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7951) },
                    { new Guid("6558e622-ed31-4057-a319-622d06dda6e8"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8084), null, false, "A foreign identifier", 0, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8085) },
                    { new Guid("6798fd09-b35d-4c26-9ac4-27edc53d1774"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7406), null, false, "List<T>", 0, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7406) },
                    { new Guid("6ae4c1ef-9a8d-49f9-a1fe-815a2eb6565c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6482), null, false, "Dictionary", 2, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6482) },
                    { new Guid("6b05175c-2bd4-4dcb-8187-88236311f7f0"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7141), null, false, "List", 0, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7141) },
                    { new Guid("6b560255-8cef-4618-8033-6046cd2d62e2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7074), null, false, "Queue", 0, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7074) },
                    { new Guid("6b62fe7b-4c6b-40ec-91d5-2a82f9edceef"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7534), null, false, "Last In First Out", 0, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7534) },
                    { new Guid("6ce3a0dd-1304-46be-9657-52f38aad773e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8227), null, true, "WHERE filters rows, HAVING filters groups", 1, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8227) },
                    { new Guid("6eaede37-f2b5-4f42-9542-837dbca1d511"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7275), null, true, "int[,] arr", 1, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7276) },
                    { new Guid("6f08cb98-10d3-4c5d-a2b3-4863d03221d9"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7597), null, false, "A sequential collection with fixed size", 0, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7598) },
                    { new Guid("70af2de6-857c-4fa0-aa55-af1648c2eca4"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7270), null, false, "int[][] arr", 0, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7270) },
                    { new Guid("72194b18-1ab9-415f-9f39-426f9f2fe7d9"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7510), null, false, "O(log n)", 2, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7510) },
                    { new Guid("72d8a1b1-843f-4ecf-baa9-3610ebe68670"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6418), null, false, "class", 3, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6418) },
                    { new Guid("75ca6014-65f2-44d9-8cb8-9a228b7024bc"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7503), null, false, "O(1)", 0, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7503) },
                    { new Guid("77795ec5-8484-413f-82b7-f139daa83994"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7787), null, false, "GROUP", 3, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7787) },
                    { new Guid("782c5608-524a-476e-837b-5a0325c4ae09"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8094), null, false, "A table constraint", 3, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8095) },
                    { new Guid("7854a01c-d562-4533-a558-b3fede6ce50b"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7878), null, false, "DESC", 0, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7878) },
                    { new Guid("788dac2a-84b1-4d22-a1a4-92609b3afce9"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6849), null, false, "struct", 3, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6849) },
                    { new Guid("78b1fbbc-7e3d-447b-96d4-ec8e4f1a3735"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6627), null, false, "Base", 3, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6628) },
                    { new Guid("7a7f8359-3ae8-4936-b2d1-d6ff63930af2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7982), null, false, "INNER JOIN", 0, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7982) },
                    { new Guid("7a882ea4-1d01-46cb-b7ff-4c0a6dd2809f"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7818), null, false, "Creates new tables", 2, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7818) },
                    { new Guid("7ce22989-31ed-4865-9bb2-6cd403e5f8bc"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6415), null, false, "my-variable", 2, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6415) },
                    { new Guid("7f8c44ce-d34a-46b2-a1e9-2958313bce52"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7150), null, true, "HashSet", 2, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7150) },
                    { new Guid("814fcb67-90ff-43d1-8bb1-586220892c48"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6405), null, true, "myVariable", 0, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6405) },
                    { new Guid("81b28bd3-9288-4ef2-89f2-259f3a0bd523"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6323), null, false, "define", 3, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6324) },
                    { new Guid("81d4aa5f-64e5-4542-baf3-9fc3ad85e9fa"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6901), null, false, "struct is a reference type", 0, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6901) },
                    { new Guid("85fbb5bc-1d63-456f-9f2b-85d245db01e0"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7848), null, true, "COUNT()", 1, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7848) },
                    { new Guid("869bffc5-ae6f-49b0-b10f-4bf84f261cc0"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7045), null, false, "O(log n)", 2, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7046) },
                    { new Guid("86a88194-5c0a-458a-9624-600c48e6b67a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7412), null, false, "Queue<T>", 2, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7413) },
                    { new Guid("87c7f5af-de7d-4db9-a92a-562f416f8932"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7439), null, false, "Array is slower", 0, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7439) },
                    { new Guid("899db9d8-9d20-46b4-8e6d-7f15df3693b6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7153), null, false, "Stack", 3, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7153) },
                    { new Guid("8e750e54-1932-4d12-a6a9-f74f5c3c187b"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6649), null, false, "extends", 0, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6650) },
                    { new Guid("8fc9e4b9-2523-4c53-8f5a-1dba3ab3c50a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6969), null, true, "A contract that classes can implement", 1, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6970) },
                    { new Guid("9002ac28-a7bd-441c-8af3-c35455e3dadb"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6735), null, true, "internal", 1, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6735) },
                    { new Guid("957a269d-fc47-4905-bbba-04f48ab233f7"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7986), null, false, "LEFT JOIN", 1, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7986) },
                    { new Guid("95c2e707-facf-462f-b3b0-1d83192ca950"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6966), null, false, "A base class", 0, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6966) },
                    { new Guid("9801e768-681b-4438-82be-f172beed48e5"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6621), null, true, "object", 1, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6621) },
                    { new Guid("98422533-f59c-4a89-913b-6abad204c9f6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7369), null, true, "A data structure that maps keys to values", 1, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7369) },
                    { new Guid("99ad795d-ab51-4807-ba25-8cf3215d2bf6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6698), null, false, "A method", 3, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6698) },
                    { new Guid("99d6db67-232b-46b2-a9b0-2e6e93f86745"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6976), null, false, "An abstract method", 3, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6976) },
                    { new Guid("9a0c07df-5c1e-4307-9982-b9eb600bd48c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7077), null, true, "Stack", 1, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7077) },
                    { new Guid("9b81b21d-dd53-4a85-9ac2-01fd41da58a4"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7177), null, true, "O(n)", 1, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7178) },
                    { new Guid("9f45a6d6-dc6c-427c-bc6d-634a8f26bc70"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6811), null, false, "To create threads", 2, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6811) },
                    { new Guid("a0b02591-4a4a-44fd-a31e-a5811f779fbb"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8058), null, false, "MODIFY", 2, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8058) },
                    { new Guid("a3092c66-59fa-4014-aeb9-dd770f15f1e4"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7302), null, false, "Insert()", 0, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7302) },
                    { new Guid("a3205641-2083-4674-acd9-7ad03f676b25"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7145), null, false, "ArrayList", 1, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7145) },
                    { new Guid("a50ca753-13ee-4edf-b806-9bbd64b710d1"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8054), null, true, "UPDATE", 1, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8055) },
                    { new Guid("a58d8bdd-a357-48c9-bcce-ea5ca2913890"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8264), null, false, "A view", 2, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8264) },
                    { new Guid("a7064111-34cf-4c0e-98c3-f1932a3497a1"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6367), null, false, "undefined", 2, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6367) },
                    { new Guid("a7157dd9-c44a-4eba-994b-055bd0a21ce6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8267), null, false, "A stored procedure", 3, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8267) },
                    { new Guid("a9f477e3-a388-4eba-8379-61f89e708ee7"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6738), null, false, "private", 2, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6738) },
                    { new Guid("aa415317-7e50-4bc3-81bc-94a90a6c2bd2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7682), null, false, "O(n^2)", 3, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7682) },
                    { new Guid("aa8e347e-7d77-4c7a-96bd-0c309245b757"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7777), null, false, "WHERE", 0, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7777) },
                    { new Guid("ab4af413-16da-4ddb-93ea-7156763836d1"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8296), null, false, "Joins tables", 2, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8297) },
                    { new Guid("ae7deb13-3dc3-4bd4-b5df-6de6a339bf54"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7679), null, false, "O(log n)", 2, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7679) },
                    { new Guid("aed1122c-5f21-434f-9d92-a7884ef2381c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8198), null, false, "ORDER BY", 2, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8198) },
                    { new Guid("b2970aa5-7f87-47d4-8ba7-a3e6816429b7"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7578), null, false, "Stack uses array, Queue uses list", 3, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7578) },
                    { new Guid("b594557d-0af7-4985-88e0-63ad24ab35e6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6941), null, false, "Hiding implementation", 2, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6941) },
                    { new Guid("b5c907a9-8a1a-4461-b107-22e4d3400a0c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6688), null, false, "A data type", 0, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6688) },
                    { new Guid("b9d3b033-7ea1-4d98-b095-0d28f6f33ffd"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8223), null, false, "They are the same", 0, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8223) },
                    { new Guid("ba1c4c18-9971-4be1-9196-55688d31fa3e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7747), null, true, "SELECT", 1, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7747) },
                    { new Guid("ba84db6d-c265-4095-a6ab-f32f05d87f6a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8300), null, false, "Creates a new table", 3, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8300) },
                    { new Guid("bcfaf072-b842-43a0-8438-d741ed5145e5"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8358), null, true, "Organizing data to reduce redundancy", 1, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8359) },
                    { new Guid("bdf8c4cf-e8b8-46a1-bf95-6e1f4dcc3e2c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8158), null, true, "Links tables together", 0, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8158) },
                    { new Guid("bdf8d528-e58e-4738-81cf-00ce8e6daa8e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6360), null, false, "null", 0, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6361) },
                    { new Guid("bf08f9e1-2705-478d-8427-4dcdaf501d0e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7781), null, true, "HAVING", 1, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7781) },
                    { new Guid("bf53cfde-1a51-4f3a-8c1c-ba5841f95ec6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6881), null, false, "Converting int to string", 3, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6881) },
                    { new Guid("c335501a-95f1-4de9-b142-f18d61b5dd95"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6472), null, false, "ArrayList", 0, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6473) },
                    { new Guid("c543ebea-1e11-4f58-91ac-61712c869fe5"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6615), null, false, "System", 0, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6615) },
                    { new Guid("ca01505f-1bfe-4abe-b50a-e731fcc20885"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7174), null, false, "O(1)", 0, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7174) },
                    { new Guid("cb229675-d8d6-4bee-bcf9-07a07d4f88c5"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6316), null, false, "let", 2, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6316) },
                    { new Guid("cce2700c-2b80-4f2d-9dd2-e1d98250e029"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7108), null, false, "4", 0, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7108) },
                    { new Guid("cf34cb66-a284-41f5-8b49-41a4b8dc7a9b"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6878), null, false, "Converting string to int", 2, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6878) },
                    { new Guid("cf5cc1cf-75b2-4805-b7be-0dc74df15f8d"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7750), null, false, "FETCH", 2, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7750) },
                    { new Guid("d03aff7d-91a6-4a23-95a1-06814d364906"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8168), null, false, "Updates data", 3, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8169) },
                    { new Guid("d179fa1b-68a9-4ca3-9851-cc284595771e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7609), null, false, "A hash-based structure", 3, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7610) },
                    { new Guid("d48d4572-7a12-4cf0-a0c3-d1d3992337f6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8235), null, false, "WHERE can use aggregates", 3, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8235) },
                    { new Guid("d6b88b26-41d7-44f5-9fbf-42d3dcd318df"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7030), null, true, "O(1)", 0, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7031) },
                    { new Guid("d6c1f212-a579-4a1a-8d72-de0c70156781"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7546), null, false, "No specific order", 3, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7546) },
                    { new Guid("d85575f7-f14f-420d-8d0a-7f954cd9c213"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7374), null, false, "A type of array", 2, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7374) },
                    { new Guid("dab08129-a610-4b40-9054-80a60ed402c1"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7339), null, false, "A sorting algorithm", 0, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7339) },
                    { new Guid("dd9581e3-0c00-4ed3-98df-df6341bb8ca2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6906), null, true, "struct is a value type, class is a reference type", 1, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6907) },
                    { new Guid("de1226e5-145f-4177-a9e8-78c38ada636c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7670), null, false, "O(1)", 0, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7670) },
                    { new Guid("e1a9b73f-2e32-4d13-9a57-0a61c43395b3"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7515), null, false, "O(n^2)", 3, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7515) },
                    { new Guid("e3bc593f-8266-4ffe-b461-f5eb11ef8719"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7815), null, true, "Combines rows from two tables", 1, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7815) },
                    { new Guid("e4cac592-6cf0-4893-8f15-37a4872f0ba7"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7606), null, false, "A type of tree", 2, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7607) },
                    { new Guid("e4ff84bb-5bcd-4738-bfbb-0d67c2fb4efb"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6448), null, false, "bool", 2, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6449) },
                    { new Guid("e52e5aea-48e8-444c-8639-ff70ec74329a"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6846), null, true, "string", 2, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6846) },
                    { new Guid("e59ca73a-22f7-44d9-82be-4703e7fcdc54"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8365), null, false, "Backing up data", 3, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8365) },
                    { new Guid("e704106d-ffd7-4f85-8b17-fd26ddd172c0"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6443), null, true, "Type object", 1, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6443) },
                    { new Guid("e951e640-c6cc-4b59-bb40-48d9c655bbc3"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7378), null, false, "A search algorithm", 3, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7378) },
                    { new Guid("ea190de2-1ccd-4197-ade3-15c4a87db1e2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6973), null, false, "A sealed class", 2, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6973) },
                    { new Guid("ea2903b1-0744-41fd-8b66-1655c7223133"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6873), null, true, "Converting value type to object", 1, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6873) },
                    { new Guid("eb8dac99-5af2-4c33-a4c3-7ad15fda7e89"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8027), null, false, "Groups data", 3, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8027) },
                    { new Guid("f275c9e4-c5c8-4770-b1ec-5909794c32a6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7282), null, false, "Array<int> arr", 3, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7282) },
                    { new Guid("f2a55edd-31e9-4fb1-b603-b83e4cb4385c"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7743), null, false, "GET", 0, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7743) },
                    { new Guid("f2ff7c1f-3159-4c35-816e-73082adbdeba"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7569), null, true, "Stack is LIFO, Queue is FIFO", 1, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7569) },
                    { new Guid("f3b5066a-1170-44da-b89b-abac5617606e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8091), null, false, "A column name", 2, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8091) },
                    { new Guid("f5302a2a-b816-4beb-9dae-ef03f2729bf2"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6452), null, false, "int", 3, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6452) },
                    { new Guid("f5b7f13b-ad55-473d-b616-658ed178d2a6"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7991), null, false, "CROSS JOIN", 2, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7991) },
                    { new Guid("f8742d95-57a4-4532-a2af-36eef2afa0f5"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8191), null, false, "WHERE", 0, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(8191) },
                    { new Guid("f9ed6437-e516-4545-856f-e7188d744a96"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7111), null, true, "0", 1, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7111) },
                    { new Guid("fb3750e9-d349-4a6d-9e06-e366747c8f04"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6656), null, false, "inherits", 2, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(6656) },
                    { new Guid("fd7f60b4-3da4-4ee6-b159-4ae805c7256e"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7506), null, true, "O(n)", 1, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7507) },
                    { new Guid("ffa1cf8f-5d79-4ee9-8d5c-6d51547b6055"), null, new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7675), null, true, "O(n)", 1, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 14, 18, 47, 192, DateTimeKind.Utc).AddTicks(7675) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOption_QuestionId",
                table: "AnswerOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_ExamId_StudentId_AttemptNumber",
                table: "Attempt",
                columns: new[] { "ExamId", "StudentId", "AttemptNumber" },
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_StudentId",
                table: "Attempt",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_UserAccountId",
                table: "Attempt",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswer_AttemptId",
                table: "AttemptAnswer",
                column: "AttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswer_QuestionId",
                table: "AttemptAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswer_SelectedOptionId",
                table: "AttemptAnswer",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ActorId",
                table: "AuditLog",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_SubjectId",
                table: "Chapter",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerification_UserId",
                table: "EmailVerification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_ChapterId",
                table: "Enrolment",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_EnrolledById",
                table: "Enrolment",
                column: "EnrolledById");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_ExamId",
                table: "Enrolment",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_StudentId_ChapterId",
                table: "Enrolment",
                columns: new[] { "StudentId", "ChapterId" },
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_UserAccountId",
                table: "Enrolment",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_ChapterId",
                table: "Exam",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_ExamId",
                table: "ExamQuestion",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_QuestionId",
                table: "ExamQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFile_DeletedByUserId",
                table: "MediaFile",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFile_UploadedByUserId",
                table: "MediaFile",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordReset_UserId",
                table: "PasswordReset",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ChapterId",
                table: "Question",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_SourceQuestionId",
                table: "Question",
                column: "SourceQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_Code",
                table: "Student",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_Email",
                table: "Student",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_NID",
                table: "Student",
                column: "NID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentChapter_AssignedById",
                table: "StudentChapter",
                column: "AssignedById");

            migrationBuilder.CreateIndex(
                name: "IX_StudentChapter_ChapterId",
                table: "StudentChapter",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentChapter_StudentId_ChapterId",
                table: "StudentChapter",
                columns: new[] { "StudentId", "ChapterId" },
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_TeacherId",
                table: "Subject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UserAccountId",
                table: "Subject",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Email",
                table: "Teacher",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_NID",
                table: "Teacher",
                column: "NID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_CreatedById",
                table: "UserAccount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Email",
                table: "UserAccount",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_RoleId",
                table: "UserAccount",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Username",
                table: "UserAccount",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptAnswer");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "EmailVerification");

            migrationBuilder.DropTable(
                name: "Enrolment");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "MediaFile");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PasswordReset");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "StudentChapter");

            migrationBuilder.DropTable(
                name: "AnswerOption");

            migrationBuilder.DropTable(
                name: "Attempt");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
