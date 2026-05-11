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
                name: "PlatformSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformSetting", x => x.Id);
                });

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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
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
                        principalColumn: "Id",
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
                name: "Enrolment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_Enrolment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalColumn: "Id",
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
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attempt_UserAccount_UserAccountId",
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
                    { new Guid("00000001-0000-0000-0000-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Admin", new DateTime(2026, 5, 11, 10, 52, 39, 210, DateTimeKind.Utc).AddTicks(4620) },
                    { new Guid("00000002-0000-0000-0000-000000000002"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Teacher", new DateTime(2026, 5, 11, 10, 52, 39, 210, DateTimeKind.Utc).AddTicks(4638) },
                    { new Guid("00000003-0000-0000-0000-000000000003"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Student", new DateTime(2026, 5, 11, 10, 52, 39, 210, DateTimeKind.Utc).AddTicks(4641) }
                });

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Email", "FullName", "IsActive", "PasswordHash", "RoleId", "TempPassword", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@mcq2.com", "Admin User", true, "$2a$11$7gLx9q20ak3c2B9.jdlXIusF/QiW.4J2YOfX8H3FQ9zju9ltX011O", new Guid("00000001-0000-0000-0000-000000000001"), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "teacher@mcq2.com", "John Smith", true, "$2a$11$1jUWig/oUUvB1snzd7CSyOo9ZjfBXl6R5WzUsZ4I.Pf0wUFpUoJXq", new Guid("00000002-0000-0000-0000-000000000002"), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "teacher" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "student@mcq2.com", "Alice Johnson", true, "$2a$11$gepOaEjFuWqINM1bQR4/ou8BqEFcNuKxKvA7ETurFDwW5mByV/XzS", new Guid("00000003-0000-0000-0000-000000000003"), false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "student" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "Address", "Code", "ContactNo", "CreatedAt", "Email", "FatherContact", "FatherName", "IsActive", "MotherContact", "MotherName", "NID", "Name", "PhotoUrl", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("c62dbc2a-5221-4e49-9151-1cdb441e48da"), "456 Student Ave", "STU001", "555-5678", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "student@mcq2.com", "555-5679", "Robert Johnson", true, "555-5680", "Mary Johnson", "STU001NID", "Alice Johnson", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.InsertData(
                table: "Teacher",
                columns: new[] { "Id", "Address", "ContactNo", "CreatedAt", "Email", "IsActive", "NID", "Name", "PhotoUrl", "Title", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "123 Teacher St", "555-1234", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "teacher@mcq2.com", true, "TCH001", "John Smith", null, "Senior Instructor", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "Id", "CreatedAt", "Description", "IsArchived", "TeacherId", "Title", "UpdatedAt", "UserAccountId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn C# fundamentals", false, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "C# Programming", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn data structures", false, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Data Structures", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Learn SQL fundamentals", false, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "SQL Basics", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
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

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOption_QuestionId",
                table: "AnswerOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_ExamId_StudentId_AttemptNumber",
                table: "Attempt",
                columns: new[] { "ExamId", "StudentId", "AttemptNumber" },
                unique: true);

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
                name: "IX_Enrolment_StudentId_ChapterId",
                table: "Enrolment",
                columns: new[] { "StudentId", "ChapterId" },
                unique: true);

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
                name: "IX_Student_UserId",
                table: "Student",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

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
                unique: true);

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
                name: "IX_Teacher_UserId",
                table: "Teacher",
                column: "UserId",
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
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PasswordReset");

            migrationBuilder.DropTable(
                name: "PlatformSetting");

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
