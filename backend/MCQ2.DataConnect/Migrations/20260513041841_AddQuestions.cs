using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MCQ3.DataConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 13, 4, 18, 40, 327, DateTimeKind.Utc).AddTicks(6709));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 13, 4, 18, 40, 327, DateTimeKind.Utc).AddTicks(6722));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 13, 4, 18, 40, 327, DateTimeKind.Utc).AddTicks(6725));

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$60ANERVFVPsrzuIswbewp.oH8MhVB/E/ihXtcMK88.zGeWR57clfu");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$kiQZAadV7KFeDFgVfgkAkeAslpE7vbPL/0OAhbWZigpxG.Hu4HdVi");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$6JIeTNj07NDzJBNyZoesxeVbgcPd0ImFvID3CPEw436U/2t/R5FNm");

            migrationBuilder.InsertData(
                table: "AnswerOption",
                columns: new[] { "Id", "AudioPath", "CreatedAt", "ImagePath", "IsCorrect", "OptionText", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0153a747-8614-4185-95c3-0594e86ca5c0"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6168), null, false, "WHERE can use aggregates", 3, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6168) },
                    { new Guid("021db8c1-440f-497c-ab46-773963ae91e6"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4587), null, false, "string", 0, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4587) },
                    { new Guid("02d7141b-c85a-4921-b18a-abae3a8778d4"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6265), null, false, "Backing up data", 3, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6265) },
                    { new Guid("05ee03ab-e513-4163-9aee-ba4b30565963"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4516), null, false, "undefined", 2, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4516) },
                    { new Guid("07a58646-3bcb-49e1-9642-c93d16f3d851"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5274), null, false, "O(1)", 0, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5275) },
                    { new Guid("0a1be256-c672-4008-9b96-71732faaeb03"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5733), null, false, "RETRIEVE", 3, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5733) },
                    { new Guid("0b065770-092a-4ae7-ae8b-28c6c5ec4286"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4761), null, false, "implements", 3, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4762) },
                    { new Guid("0b1970af-5c76-4ddc-8112-d4e96a478ba8"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5047), null, false, "Hiding implementation", 2, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5047) },
                    { new Guid("0b7f5180-91d4-417a-9bb8-30a3a967df55"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4633), null, false, "HashSet", 3, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4633) },
                    { new Guid("0ba23a2c-3705-4985-970a-410ab05f866f"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4401), null, false, "define", 3, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4401) },
                    { new Guid("0bfac845-9937-426d-ad42-cc32f92b39f6"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5986), null, false, "Filters data", 2, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5986) },
                    { new Guid("0c18e2d7-f126-497e-881d-d5d70fd7850e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4599), null, false, "int", 3, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4599) },
                    { new Guid("0c3c0940-7175-4eed-90a3-5a575eae0f71"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6259), null, true, "Organizing data to reduce redundancy", 1, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6259) },
                    { new Guid("0c774f48-1da5-4e19-b874-9e01a4fa6042"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4564), null, false, "my-variable", 2, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4564) },
                    { new Guid("0ce66f7e-dfb9-41d7-a972-a66008243925"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5850), null, true, "COUNT()", 1, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5850) },
                    { new Guid("0d5ed85c-9f95-4dc9-bb83-af64e691910a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5880), null, false, "DESC", 0, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5881) },
                    { new Guid("0e3e39db-6864-49f3-98c6-1cd31e10a216"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6119), null, false, "HAVING", 3, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6120) },
                    { new Guid("0ebc5892-f2bd-4d6a-bd99-966641f19c5f"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4596), null, false, "bool", 2, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4596) },
                    { new Guid("0eed2653-7cf5-488e-a5cd-3aebbfde7a03"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5050), null, false, "Creating multiple objects", 3, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5050) },
                    { new Guid("12a90480-f7ee-4cd1-834f-ad2bf7fbd5c2"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5467), null, true, "Array is strongly typed, ArrayList can hold any type", 1, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5467) },
                    { new Guid("179f25a5-b485-4a34-81c4-5907afb2170d"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4620), null, false, "ArrayList", 0, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4620) },
                    { new Guid("1818a9f3-1223-4813-825f-7cc74e11d91b"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6188), null, false, "A JOIN operation", 0, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6189) },
                    { new Guid("1840f8ed-faa1-49db-92ab-279057204244"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5846), null, false, "SUM()", 0, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5847) },
                    { new Guid("18804f0b-6968-48be-b222-083b46d9596e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4975), null, false, "Converting object to value type", 0, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4975) },
                    { new Guid("19b1e961-827a-4ec0-99bf-f8a4fca1dfd7"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5406), null, false, "A type of array", 2, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5406) },
                    { new Guid("19b55315-0a91-4dee-bc44-38b168871267"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5251), null, true, "HashSet", 2, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5251) },
                    { new Guid("1a7bcee4-c515-4b50-8904-00ea12c2e4d9"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6156), null, false, "They are the same", 0, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6156) },
                    { new Guid("2079a74a-d5a5-48c7-b3f5-0dac52462bb1"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5287), null, false, "O(n log n)", 3, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5287) },
                    { new Guid("21c7c5cd-ec92-4fd9-9e99-cab4e74067a8"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5179), null, true, "Stack", 1, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5180) },
                    { new Guid("228cdebc-8a66-4faf-b651-f6c23cb0215d"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4835), null, false, "public", 0, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4835) },
                    { new Guid("24520b22-a046-4512-9aa2-91b68a74c52c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4555), null, true, "myVariable", 0, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4555) },
                    { new Guid("24c585d1-7934-41a9-abb7-892d77bd59b0"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5818), null, true, "Combines rows from two tables", 1, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5818) },
                    { new Guid("26429d2b-bc3d-4167-88af-aa74646dca32"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5553), null, false, "Last In First Out", 0, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5553) },
                    { new Guid("27a4e951-ad1d-4888-81ae-94c3ea17022b"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5597), null, false, "Stack uses array, Queue uses list", 3, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5597) },
                    { new Guid("282aaefe-1fa9-4edf-bf25-032cffb2559e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5496), null, false, "O(1)", 0, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5497) },
                    { new Guid("28685ebb-49a2-4f7b-a8ec-88cfa4dc3d70"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4654), null, false, "System", 0, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4654) },
                    { new Guid("2a5f2151-22cc-40f3-b4ef-5138a8f5f24b"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5924), null, false, "LEFT JOIN", 1, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5924) },
                    { new Guid("2d00165a-55a8-4dc2-b4d0-606d62d80a39"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5071), null, false, "A base class", 0, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5071) },
                    { new Guid("2d424360-ef8e-4d33-8cb6-7456e540e674"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4751), null, false, "extends", 0, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4752) },
                    { new Guid("2d5eb5eb-f084-413e-bb8d-4b2e672f8085"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5977), null, false, "Sorts data", 0, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5977) },
                    { new Guid("34229892-d6d5-4b43-bf73-9c730af0e073"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4393), null, false, "let", 2, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4393) },
                    { new Guid("3536920d-59bd-4a25-b83c-158a3a39f229"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4663), null, false, "ValueType", 2, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4663) },
                    { new Guid("369db9bd-3de6-47b4-9387-69219c140579"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4978), null, true, "Converting value type to object", 1, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4979) },
                    { new Guid("37ed6726-fe95-4351-bece-ebd175d2006e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4755), null, true, ":", 1, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4755) },
                    { new Guid("38ebd861-eba7-4604-b6c1-00ae37ade7d0"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5983), null, true, "Returns only unique values", 1, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5983) },
                    { new Guid("3f970a93-def2-4b83-b748-4082b6df63ff"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5378), null, false, "Enqueue()", 3, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5378) },
                    { new Guid("3fce282d-f371-4c1e-90c6-73fb66e88c68"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5242), null, false, "List", 0, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5242) },
                    { new Guid("3fce2994-83d8-4b6f-8f3d-e150b602ab28"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5284), null, false, "O(log n)", 2, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5284) },
                    { new Guid("44cc6176-5315-48dc-b709-1af17bd3ee48"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6198), null, false, "A view", 2, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6198) },
                    { new Guid("46c2af74-76eb-417c-b719-3b3ebc16590c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5018), null, false, "class cannot have methods", 3, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5018) },
                    { new Guid("47e42579-9c7a-4391-9e57-15790c667016"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4567), null, false, "class", 3, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4567) },
                    { new Guid("49121b07-1d07-4859-9fc0-e8ae5593d691"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6015), null, true, "UPDATE", 1, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6015) },
                    { new Guid("4a530263-50cb-4aaa-b6a9-74a9b2caaeeb"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5210), null, false, "4", 0, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5210) },
                    { new Guid("4afe16eb-975a-4e39-ad22-0a790dfcc8f6"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4983), null, false, "Converting string to int", 2, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4984) },
                    { new Guid("4de93a11-0aaf-481c-b50b-c459218357f2"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5119), null, false, "O(n)", 1, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5119) },
                    { new Guid("4f88889c-b6c8-4642-821a-5a7ee12eba84"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5254), null, false, "Stack", 3, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5254) },
                    { new Guid("51687918-83cf-4931-bde8-71579c570a5c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5371), null, true, "Add()", 1, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5371) },
                    { new Guid("52cada24-548a-4c0d-8ce4-9f0884c30464"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6021), null, false, "CHANGE", 3, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6022) },
                    { new Guid("52d3aa1d-b056-4b69-a47c-9bed223a2a36"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5183), null, false, "Array", 2, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5183) },
                    { new Guid("54ae6fb0-e815-4f71-bd66-34fec62819cd"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5616), null, false, "A sequential collection with fixed size", 0, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5616) },
                    { new Guid("59d2628d-f9e3-4c4d-9569-825783381631"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4916), null, true, "string", 2, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4916) },
                    { new Guid("5a1e32f7-d068-41a1-aa4c-ed575391c0c9"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5006), null, false, "struct is a reference type", 0, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5006) },
                    { new Guid("5b72dc5e-088c-4a65-b835-5512b39425a9"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4796), null, false, "A loop structure", 2, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4796) },
                    { new Guid("5bbf964f-1e93-4386-8618-8954da4e95ba"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5562), null, false, "Random access", 2, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5562) },
                    { new Guid("5d6f806e-91ec-487f-9aaa-2c17ea64860f"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4847), null, false, "protected", 3, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4847) },
                    { new Guid("5f18dc6a-8931-4d7b-9cfe-3bb2234309a2"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6048), null, true, "Unique identifier for each row", 1, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6048) },
                    { new Guid("5f327307-103a-4943-b35a-11cc5807c912"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6082), null, false, "Deletes records", 2, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6083) },
                    { new Guid("5fc6b813-d052-4edf-9ee9-bf2589a98c1c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5858), null, false, "MAX()", 3, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5859) },
                    { new Guid("61988e43-7774-4130-b133-2e59423a0c87"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5677), null, false, "O(1)", 0, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5677) },
                    { new Guid("63052c04-f9a2-4555-b29c-ce88e5d0e6b1"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5125), null, false, "O(n^2)", 3, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5126) },
                    { new Guid("634816e6-c5ce-478c-8a89-ca94502be2cc"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5791), null, false, "GROUP", 3, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5791) },
                    { new Guid("63d15b9e-7961-46ca-b83b-6ae142418cd2"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5898), null, false, "None", 3, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5898) },
                    { new Guid("64df0fee-321c-4b72-85a8-e845880a5d81"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6009), null, false, "INSERT", 0, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6009) },
                    { new Guid("6c7cb44f-bd49-4ab5-a08a-964a332f99b8"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6231), null, false, "Joins tables", 2, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6231) },
                    { new Guid("6d2f8594-0d3e-4633-a4ea-0d181abe8564"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4630), null, false, "Dictionary", 2, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4630) },
                    { new Guid("6e041159-f5ea-4110-9c1e-52296c2cd88f"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5727), null, true, "SELECT", 1, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5727) },
                    { new Guid("6e57cb84-1d9e-4dc5-8300-23d3f2ea435a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5500), null, true, "O(n)", 1, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5500) },
                    { new Guid("708e09f6-46cf-47d4-878a-b0d61375248e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5624), null, false, "A type of tree", 2, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5625) },
                    { new Guid("71b03907-bd67-4db4-bf1f-d8e719983dd5"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5476), null, false, "They are the same", 3, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5476) },
                    { new Guid("72f934b1-1616-4274-82c0-147d705a153a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6054), null, false, "A table constraint", 3, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6054) },
                    { new Guid("746ca31f-7727-4a6c-a41e-60feb4159b46"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4389), null, true, "int", 1, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4389) },
                    { new Guid("755e9af1-8ebe-48cb-818a-d06cd90e6f09"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4591), null, true, "Type object", 1, new Guid("11111111-0004-0004-0004-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4591) },
                    { new Guid("78635e29-0595-4e93-a5dd-b01edd828516"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6076), null, true, "Links tables together", 0, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6076) },
                    { new Guid("78ce1980-8257-4822-b654-ec22fd02d35a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4879), null, true, "To ensure proper disposal of resources", 1, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4879) },
                    { new Guid("79a617cb-c027-45b3-a5b2-6e201a1f082a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4522), null, false, "1", 3, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4522) },
                    { new Guid("7dbb1122-b91a-4d47-80db-137fdbe833be"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5038), null, false, "Multiple inheritance", 0, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5038) },
                    { new Guid("7e0de7fb-bd18-4e78-8a1c-b308324b96bd"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5723), null, false, "GET", 0, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5723) },
                    { new Guid("7e9603a5-79e5-4d62-b32d-d687d8ef3deb"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5432), null, false, "List<T>", 0, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5432) },
                    { new Guid("7f0338b2-9028-43fa-9f8e-e21f7ab11754"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5853), null, false, "TOTAL()", 2, new Guid("33333333-0004-0004-0004-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5853) },
                    { new Guid("7f2c7859-31f0-421a-bae4-a060550476ab"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4876), null, false, "To import namespaces", 0, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4876) },
                    { new Guid("86ef6cdd-37ee-4914-8706-02442f5e86cd"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6202), null, false, "A stored procedure", 3, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6202) },
                    { new Guid("89c2af87-d72a-4803-886a-60c16de6f33a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5399), null, false, "A sorting algorithm", 0, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5400) },
                    { new Guid("8a3b0237-0899-449c-a2dd-8e6fc3ac89a5"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5688), null, false, "O(n^2)", 3, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5689) },
                    { new Guid("8c34c6eb-f998-4159-81bd-368d96d212e0"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5011), null, true, "struct is a value type, class is a reference type", 1, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5012) },
                    { new Guid("8ceb8c82-c82e-47be-8441-720e40060b58"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6110), null, true, "GROUP BY", 1, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6110) },
                    { new Guid("8fd1b514-e3f8-494f-8a96-b0220d720ff2"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4799), null, false, "A method", 3, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4799) },
                    { new Guid("93169d44-9f34-4251-9b22-187d6089c60f"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6018), null, false, "MODIFY", 2, new Guid("33333333-0008-0008-0008-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6018) },
                    { new Guid("931e3e44-78e8-49ef-98c3-bce4fc161033"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5508), null, false, "O(n^2)", 3, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5509) },
                    { new Guid("936677b2-e61b-4534-b838-a86cecc3f04e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5410), null, false, "A search algorithm", 3, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5410) },
                    { new Guid("9894da7a-384f-4d5e-9541-52603dedd3a9"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5074), null, true, "A contract that classes can implement", 1, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5075) },
                    { new Guid("9a360618-9e17-47fe-81df-b78399460b7e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6234), null, false, "Creates a new table", 3, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6234) },
                    { new Guid("9db8bf35-c1b6-4e9d-ac2f-6269072fd928"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5503), null, false, "O(log n)", 2, new Guid("22222222-0011-0011-0011-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5503) },
                    { new Guid("9e2a0910-5073-4156-b282-796f0d09bd9a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5594), null, false, "Both are the same", 2, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5594) },
                    { new Guid("9efe74ad-5aaa-475d-9b5d-6f3729184f7c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5565), null, false, "No specific order", 3, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5566) },
                    { new Guid("9f35bb4e-603d-4167-af5d-cc7cf6fb1860"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4986), null, false, "Converting int to string", 3, new Guid("11111111-0012-0012-0012-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4987) },
                    { new Guid("a087e195-7c7c-4b46-b57a-7c27cc67800d"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4793), null, true, "A container for organizing types", 1, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4793) },
                    { new Guid("a23e5137-8069-49e0-832e-5fa7d1a9848e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5785), null, true, "HAVING", 1, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5785) },
                    { new Guid("a37cdcb4-540f-402c-a40a-6af49f61e092"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4838), null, true, "internal", 1, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4839) },
                    { new Guid("a418452c-dcb7-4a31-b2a0-e55624a48d3e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6224), null, true, "Combines results of two queries, removes duplicates", 1, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6225) },
                    { new Guid("a5c0da48-dbfd-4f51-bdbf-e11c95c84774"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5307), null, false, "int[][] arr", 0, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5307) },
                    { new Guid("a5f7d3b0-dbbb-4f16-9d60-6adda5e8c595"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6043), null, false, "A foreign identifier", 0, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6044) },
                    { new Guid("a7fb4310-5e0c-47d7-b186-128fb818a99d"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4660), null, true, "object", 1, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4660) },
                    { new Guid("aa176565-ee26-4cc3-bfce-00410870f755"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5826), null, false, "Updates records", 3, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5826) },
                    { new Guid("aa9be8db-b26c-4328-891f-4900e077b296"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5585), null, false, "Stack is FIFO, Queue is LIFO", 0, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5585) },
                    { new Guid("ac05b8b3-1a74-47f2-b7e2-7c2970a095a6"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5781), null, false, "WHERE", 0, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5781) },
                    { new Guid("afb9cf75-42be-4a12-a1e2-0262523f5dc9"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6106), null, false, "WHERE", 0, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6106) },
                    { new Guid("aff54d76-4ec7-4394-86f4-2d40aae19cce"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5919), null, false, "INNER JOIN", 0, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5919) },
                    { new Guid("b0544dc2-1afe-4053-9e0b-d350f06149c2"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6079), null, false, "Creates a new table", 1, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6080) },
                    { new Guid("b0b49c52-9d30-4016-b8b8-472ccb956ca2"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4513), null, true, "0", 1, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4513) },
                    { new Guid("b1a23f83-58f6-4a9b-a108-ac72fdddcef8"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5557), null, true, "First In First Out", 1, new Guid("22222222-0012-0012-0012-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5557) },
                    { new Guid("b3650023-b4ab-4f87-89ce-f82ad99943c9"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5122), null, false, "O(log n)", 2, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5123) },
                    { new Guid("b399262e-9426-441c-86a4-6bb97d7f5f8b"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4559), null, false, "2var", 1, new Guid("11111111-0003-0003-0003-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4559) },
                    { new Guid("b45f7bd4-3eb5-4316-8322-d242c89ae7a8"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6192), null, true, "A query inside another query", 1, new Guid("33333333-0013-0013-0013-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6192) },
                    { new Guid("b4c3e302-0005-4bb6-8fa4-37fef0f04446"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5989), null, false, "Groups data", 3, new Guid("33333333-0007-0007-0007-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5989) },
                    { new Guid("b5f09c31-6834-481e-b62c-1188bec4eaa3"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5078), null, false, "A sealed class", 2, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5078) },
                    { new Guid("b643b991-9829-4fbb-a4a0-f9ace188e1eb"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5176), null, false, "Queue", 0, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5176) },
                    { new Guid("b6795d80-0a09-427c-ae8c-52c51a59d72b"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5929), null, false, "CROSS JOIN", 2, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5929) },
                    { new Guid("b6c52e2f-81e7-4cf8-9168-93f10d089afa"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4758), null, false, "inherits", 2, new Guid("11111111-0007-0007-0007-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4758) },
                    { new Guid("b706e804-60ec-40ea-a363-18ac2dc6440c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5439), null, false, "Queue<T>", 2, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5439) },
                    { new Guid("b82fc92e-85a7-48d2-9643-a8df5af00c61"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4789), null, false, "A data type", 0, new Guid("11111111-0008-0008-0008-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4790) },
                    { new Guid("b8d391ab-665c-48b5-b72d-c82cae2767f3"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5621), null, true, "A linear collection of nodes where each node points to the next", 1, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5622) },
                    { new Guid("b8dff3b3-5379-48eb-8693-f09fe4a1d5f4"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6262), null, false, "Encrypting data", 2, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6262) },
                    { new Guid("b9a024d3-753f-484c-aa73-6185effe1208"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5730), null, false, "FETCH", 2, new Guid("33333333-0001-0001-0001-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5730) },
                    { new Guid("bb7508ee-34c0-4e6a-a7ca-9bb6f32a097e"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5216), null, false, "8", 2, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5217) },
                    { new Guid("bc593c41-e724-46ba-8a5a-42c23f0cf531"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5627), null, false, "A hash-based structure", 3, new Guid("22222222-0014-0014-0014-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5628) },
                    { new Guid("bca0d7db-73e7-4fdd-aed7-d03b919254d1"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5464), null, false, "Array is slower", 0, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5464) },
                    { new Guid("bcc601e2-7fd7-48be-87ac-97e67135a4bf"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4378), null, false, "var", 0, new Guid("11111111-0001-0001-0001-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4378) },
                    { new Guid("be8ebc0e-312d-425a-9ccc-7698666c6d0d"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5188), null, false, "List", 3, new Guid("22222222-0002-0002-0002-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5188) },
                    { new Guid("bf938586-2f4a-47b8-a053-9b21d16e747c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5588), null, true, "Stack is LIFO, Queue is FIFO", 1, new Guid("22222222-0013-0013-0013-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5589) },
                    { new Guid("c0486a9a-349d-4c44-8962-85464200febe"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4666), null, false, "Base", 3, new Guid("11111111-0006-0006-0006-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4666) },
                    { new Guid("c06e1bc4-1b43-440f-8461-e2c3096c0b1b"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5470), null, false, "ArrayList uses less memory", 2, new Guid("22222222-0010-0010-0010-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5470) },
                    { new Guid("c2282626-62ec-40c7-abcd-0a0102fdb8ec"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5932), null, true, "LOOP JOIN", 3, new Guid("33333333-0006-0006-0006-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5932) },
                    { new Guid("c2294d17-40b2-4051-8f47-5619ed0a97b6"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5891), null, false, "Random", 2, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5892) },
                    { new Guid("c6022460-4baa-43d2-82df-dff483a8d905"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6163), null, false, "HAVING is faster", 2, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6163) },
                    { new Guid("c7530550-2ae0-4fbc-bb98-3271ee88b0de"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5365), null, false, "Insert()", 0, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5365) },
                    { new Guid("c7eb8944-1b06-4b20-b24a-127232254c53"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5081), null, false, "An abstract method", 3, new Guid("11111111-0015-0015-0015-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5081) },
                    { new Guid("c9f8d1f6-e5c7-4192-9897-756994646b72"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5436), null, true, "Dictionary<TKey, TValue>", 1, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5436) },
                    { new Guid("cb359ff6-2b70-4680-9b29-4b8ac66e6f99"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5374), null, false, "Push()", 2, new Guid("22222222-0007-0007-0007-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5374) },
                    { new Guid("cc5d3833-baaf-46fb-adce-22f67d9c5507"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5245), null, false, "ArrayList", 1, new Guid("22222222-0004-0004-0004-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5246) },
                    { new Guid("cd767b04-5ebd-4bce-bec9-340235d455dc"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6221), null, false, "Combines all rows", 0, new Guid("33333333-0014-0014-0014-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6221) },
                    { new Guid("ce938e57-7951-467a-9958-d422cc6635cf"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4842), null, false, "private", 2, new Guid("11111111-0009-0009-0009-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4842) },
                    { new Guid("cf5ec4c2-7552-4b95-8bac-be8f2839051c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5279), null, true, "O(n)", 1, new Guid("22222222-0005-0005-0005-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5279) },
                    { new Guid("cfad1dbd-720e-4140-a75d-1834b97dfa64"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5814), null, false, "Deletes records", 0, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5815) },
                    { new Guid("d121690f-481c-4234-8645-453f348ceed0"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5222), null, false, "16", 3, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5222) },
                    { new Guid("d1e3558e-ad91-4e6d-827a-6178e83ad0c6"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4887), null, false, "To handle exceptions", 3, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4888) },
                    { new Guid("d2307e21-042a-47be-971f-5286231cea16"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4907), null, false, "int", 0, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4907) },
                    { new Guid("d2e58d39-65b3-448f-916a-ebe0a9127d9a"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5885), null, true, "ASC", 1, new Guid("33333333-0005-0005-0005-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5885) },
                    { new Guid("d2f14162-8920-4cab-9d83-4adc8a9c067c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5320), null, false, "Array<int> arr", 3, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5320) },
                    { new Guid("d900e715-6060-4e12-b398-56773147c507"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5442), null, false, "Stack<T>", 3, new Guid("22222222-0009-0009-0009-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5442) },
                    { new Guid("d92b475e-fcfd-479a-89ca-e403241c7bca"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5108), null, true, "O(1)", 0, new Guid("22222222-0001-0001-0001-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5108) },
                    { new Guid("e2ea9f63-ca08-491b-b918-5449fa8eb9b7"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6254), null, false, "Adding more tables", 0, new Guid("33333333-0015-0015-0015-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6254) },
                    { new Guid("e352b9d2-b6d9-467e-89b3-9e466a0b72ed"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6086), null, false, "Updates data", 3, new Guid("33333333-0010-0010-0010-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6086) },
                    { new Guid("e501ae28-11a6-42e8-8e8a-298cfec2f74c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4626), null, true, "Array", 1, new Guid("11111111-0005-0005-0005-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4627) },
                    { new Guid("e6ad130b-7421-4d73-a1fd-8cabc0d97952"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5312), null, true, "int[,] arr", 1, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5313) },
                    { new Guid("e6d5b89c-d0fa-416a-9dd6-4a17ff3f74a5"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5685), null, false, "O(log n)", 2, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5686) },
                    { new Guid("e7c7b1ba-d9e2-4dcd-a79c-beaf4a20945f"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6159), null, true, "WHERE filters rows, HAVING filters groups", 1, new Guid("33333333-0012-0012-0012-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6160) },
                    { new Guid("ea00bd7c-09d0-4e89-9d29-e387eb663553"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5821), null, false, "Creates new tables", 2, new Guid("33333333-0003-0003-0003-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5821) },
                    { new Guid("ebc3918c-9e92-4dc2-9ac7-3dcc60de2efd"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4911), null, false, "bool", 1, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4911) },
                    { new Guid("ec20618b-7d3e-46d0-8fd5-d4560bc9b93c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5682), null, true, "O(n)", 1, new Guid("22222222-0015-0015-0015-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5683) },
                    { new Guid("ede074b2-ddc5-4477-9a65-0a2736d38dc4"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5213), null, true, "0", 1, new Guid("22222222-0003-0003-0003-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5214) },
                    { new Guid("ede96210-ef20-4d49-af2e-3c450e3a1f13"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4447), null, false, "null", 0, new Guid("11111111-0002-0002-0002-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4448) },
                    { new Guid("edf0bf49-355c-45a4-bb52-0be77910a4fb"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5044), null, true, "Same method behaves differently based on object type", 1, new Guid("11111111-0014-0014-0014-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5044) },
                    { new Guid("f0a3abe7-1f7f-44b6-8c4c-33365b1536b8"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4919), null, false, "struct", 3, new Guid("11111111-0011-0011-0011-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4919) },
                    { new Guid("f14b4759-0f59-4661-a20a-a978a4375390"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5316), null, false, "List<int[]> arr", 2, new Guid("22222222-0006-0006-0006-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5316) },
                    { new Guid("f391bbc7-9e8f-4302-9af8-8e8302475747"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5015), null, false, "struct supports inheritance", 2, new Guid("11111111-0013-0013-0013-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5015) },
                    { new Guid("f42d1a34-02c6-433e-b1ad-a4b695b57c31"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5788), null, false, "FILTER", 2, new Guid("33333333-0002-0002-0002-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5788) },
                    { new Guid("f47e2dcd-d83e-45fb-96da-ac9a0b9dd51c"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6051), null, false, "A column name", 2, new Guid("33333333-0009-0009-0009-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6051) },
                    { new Guid("f8892945-0537-4541-b0d2-0c38d5a5da7d"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4882), null, false, "To create threads", 2, new Guid("11111111-0010-0010-0010-111111111111"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(4882) },
                    { new Guid("fcdca488-35f1-44ad-9e35-1825f113822f"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5403), null, true, "A data structure that maps keys to values", 1, new Guid("22222222-0008-0008-0008-222222222222"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(5403) },
                    { new Guid("fda691b9-a3f5-4351-8d86-3a025cdbbaf4"), null, new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6113), null, false, "ORDER BY", 2, new Guid("33333333-0011-0011-0011-333333333333"), new DateTime(2026, 5, 13, 4, 18, 40, 902, DateTimeKind.Utc).AddTicks(6114) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0153a747-8614-4185-95c3-0594e86ca5c0"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("021db8c1-440f-497c-ab46-773963ae91e6"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("02d7141b-c85a-4921-b18a-abae3a8778d4"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("05ee03ab-e513-4163-9aee-ba4b30565963"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("07a58646-3bcb-49e1-9642-c93d16f3d851"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0a1be256-c672-4008-9b96-71732faaeb03"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0b065770-092a-4ae7-ae8b-28c6c5ec4286"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0b1970af-5c76-4ddc-8112-d4e96a478ba8"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0b7f5180-91d4-417a-9bb8-30a3a967df55"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0ba23a2c-3705-4985-970a-410ab05f866f"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0bfac845-9937-426d-ad42-cc32f92b39f6"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0c18e2d7-f126-497e-881d-d5d70fd7850e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0c3c0940-7175-4eed-90a3-5a575eae0f71"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0c774f48-1da5-4e19-b874-9e01a4fa6042"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0ce66f7e-dfb9-41d7-a972-a66008243925"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0d5ed85c-9f95-4dc9-bb83-af64e691910a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0e3e39db-6864-49f3-98c6-1cd31e10a216"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0ebc5892-f2bd-4d6a-bd99-966641f19c5f"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("0eed2653-7cf5-488e-a5cd-3aebbfde7a03"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("12a90480-f7ee-4cd1-834f-ad2bf7fbd5c2"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("179f25a5-b485-4a34-81c4-5907afb2170d"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("1818a9f3-1223-4813-825f-7cc74e11d91b"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("1840f8ed-faa1-49db-92ab-279057204244"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("18804f0b-6968-48be-b222-083b46d9596e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("19b1e961-827a-4ec0-99bf-f8a4fca1dfd7"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("19b55315-0a91-4dee-bc44-38b168871267"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("1a7bcee4-c515-4b50-8904-00ea12c2e4d9"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("2079a74a-d5a5-48c7-b3f5-0dac52462bb1"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("21c7c5cd-ec92-4fd9-9e99-cab4e74067a8"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("228cdebc-8a66-4faf-b651-f6c23cb0215d"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("24520b22-a046-4512-9aa2-91b68a74c52c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("24c585d1-7934-41a9-abb7-892d77bd59b0"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("26429d2b-bc3d-4167-88af-aa74646dca32"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("27a4e951-ad1d-4888-81ae-94c3ea17022b"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("282aaefe-1fa9-4edf-bf25-032cffb2559e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("28685ebb-49a2-4f7b-a8ec-88cfa4dc3d70"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("2a5f2151-22cc-40f3-b4ef-5138a8f5f24b"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("2d00165a-55a8-4dc2-b4d0-606d62d80a39"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("2d424360-ef8e-4d33-8cb6-7456e540e674"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("2d5eb5eb-f084-413e-bb8d-4b2e672f8085"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("34229892-d6d5-4b43-bf73-9c730af0e073"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("3536920d-59bd-4a25-b83c-158a3a39f229"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("369db9bd-3de6-47b4-9387-69219c140579"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("37ed6726-fe95-4351-bece-ebd175d2006e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("38ebd861-eba7-4604-b6c1-00ae37ade7d0"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("3f970a93-def2-4b83-b748-4082b6df63ff"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("3fce282d-f371-4c1e-90c6-73fb66e88c68"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("3fce2994-83d8-4b6f-8f3d-e150b602ab28"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("44cc6176-5315-48dc-b709-1af17bd3ee48"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("46c2af74-76eb-417c-b719-3b3ebc16590c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("47e42579-9c7a-4391-9e57-15790c667016"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("49121b07-1d07-4859-9fc0-e8ae5593d691"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("4a530263-50cb-4aaa-b6a9-74a9b2caaeeb"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("4afe16eb-975a-4e39-ad22-0a790dfcc8f6"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("4de93a11-0aaf-481c-b50b-c459218357f2"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("4f88889c-b6c8-4642-821a-5a7ee12eba84"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("51687918-83cf-4931-bde8-71579c570a5c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("52cada24-548a-4c0d-8ce4-9f0884c30464"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("52d3aa1d-b056-4b69-a47c-9bed223a2a36"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("54ae6fb0-e815-4f71-bd66-34fec62819cd"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("59d2628d-f9e3-4c4d-9569-825783381631"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("5a1e32f7-d068-41a1-aa4c-ed575391c0c9"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("5b72dc5e-088c-4a65-b835-5512b39425a9"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("5bbf964f-1e93-4386-8618-8954da4e95ba"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("5d6f806e-91ec-487f-9aaa-2c17ea64860f"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("5f18dc6a-8931-4d7b-9cfe-3bb2234309a2"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("5f327307-103a-4943-b35a-11cc5807c912"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("5fc6b813-d052-4edf-9ee9-bf2589a98c1c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("61988e43-7774-4130-b133-2e59423a0c87"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("63052c04-f9a2-4555-b29c-ce88e5d0e6b1"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("634816e6-c5ce-478c-8a89-ca94502be2cc"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("63d15b9e-7961-46ca-b83b-6ae142418cd2"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("64df0fee-321c-4b72-85a8-e845880a5d81"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("6c7cb44f-bd49-4ab5-a08a-964a332f99b8"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("6d2f8594-0d3e-4633-a4ea-0d181abe8564"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("6e041159-f5ea-4110-9c1e-52296c2cd88f"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("6e57cb84-1d9e-4dc5-8300-23d3f2ea435a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("708e09f6-46cf-47d4-878a-b0d61375248e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("71b03907-bd67-4db4-bf1f-d8e719983dd5"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("72f934b1-1616-4274-82c0-147d705a153a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("746ca31f-7727-4a6c-a41e-60feb4159b46"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("755e9af1-8ebe-48cb-818a-d06cd90e6f09"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("78635e29-0595-4e93-a5dd-b01edd828516"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("78ce1980-8257-4822-b654-ec22fd02d35a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("79a617cb-c027-45b3-a5b2-6e201a1f082a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("7dbb1122-b91a-4d47-80db-137fdbe833be"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("7e0de7fb-bd18-4e78-8a1c-b308324b96bd"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("7e9603a5-79e5-4d62-b32d-d687d8ef3deb"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("7f0338b2-9028-43fa-9f8e-e21f7ab11754"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("7f2c7859-31f0-421a-bae4-a060550476ab"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("86ef6cdd-37ee-4914-8706-02442f5e86cd"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("89c2af87-d72a-4803-886a-60c16de6f33a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("8a3b0237-0899-449c-a2dd-8e6fc3ac89a5"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("8c34c6eb-f998-4159-81bd-368d96d212e0"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("8ceb8c82-c82e-47be-8441-720e40060b58"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("8fd1b514-e3f8-494f-8a96-b0220d720ff2"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("93169d44-9f34-4251-9b22-187d6089c60f"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("931e3e44-78e8-49ef-98c3-bce4fc161033"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("936677b2-e61b-4534-b838-a86cecc3f04e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("9894da7a-384f-4d5e-9541-52603dedd3a9"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("9a360618-9e17-47fe-81df-b78399460b7e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("9db8bf35-c1b6-4e9d-ac2f-6269072fd928"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("9e2a0910-5073-4156-b282-796f0d09bd9a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("9efe74ad-5aaa-475d-9b5d-6f3729184f7c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("9f35bb4e-603d-4167-af5d-cc7cf6fb1860"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("a087e195-7c7c-4b46-b57a-7c27cc67800d"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("a23e5137-8069-49e0-832e-5fa7d1a9848e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("a37cdcb4-540f-402c-a40a-6af49f61e092"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("a418452c-dcb7-4a31-b2a0-e55624a48d3e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("a5c0da48-dbfd-4f51-bdbf-e11c95c84774"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("a5f7d3b0-dbbb-4f16-9d60-6adda5e8c595"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("a7fb4310-5e0c-47d7-b186-128fb818a99d"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("aa176565-ee26-4cc3-bfce-00410870f755"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("aa9be8db-b26c-4328-891f-4900e077b296"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("ac05b8b3-1a74-47f2-b7e2-7c2970a095a6"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("afb9cf75-42be-4a12-a1e2-0262523f5dc9"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("aff54d76-4ec7-4394-86f4-2d40aae19cce"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b0544dc2-1afe-4053-9e0b-d350f06149c2"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b0b49c52-9d30-4016-b8b8-472ccb956ca2"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b1a23f83-58f6-4a9b-a108-ac72fdddcef8"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b3650023-b4ab-4f87-89ce-f82ad99943c9"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b399262e-9426-441c-86a4-6bb97d7f5f8b"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b45f7bd4-3eb5-4316-8322-d242c89ae7a8"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b4c3e302-0005-4bb6-8fa4-37fef0f04446"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b5f09c31-6834-481e-b62c-1188bec4eaa3"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b643b991-9829-4fbb-a4a0-f9ace188e1eb"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b6795d80-0a09-427c-ae8c-52c51a59d72b"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b6c52e2f-81e7-4cf8-9168-93f10d089afa"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b706e804-60ec-40ea-a363-18ac2dc6440c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b82fc92e-85a7-48d2-9643-a8df5af00c61"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b8d391ab-665c-48b5-b72d-c82cae2767f3"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b8dff3b3-5379-48eb-8693-f09fe4a1d5f4"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("b9a024d3-753f-484c-aa73-6185effe1208"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("bb7508ee-34c0-4e6a-a7ca-9bb6f32a097e"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("bc593c41-e724-46ba-8a5a-42c23f0cf531"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("bca0d7db-73e7-4fdd-aed7-d03b919254d1"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("bcc601e2-7fd7-48be-87ac-97e67135a4bf"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("be8ebc0e-312d-425a-9ccc-7698666c6d0d"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("bf938586-2f4a-47b8-a053-9b21d16e747c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c0486a9a-349d-4c44-8962-85464200febe"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c06e1bc4-1b43-440f-8461-e2c3096c0b1b"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c2282626-62ec-40c7-abcd-0a0102fdb8ec"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c2294d17-40b2-4051-8f47-5619ed0a97b6"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c6022460-4baa-43d2-82df-dff483a8d905"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c7530550-2ae0-4fbc-bb98-3271ee88b0de"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c7eb8944-1b06-4b20-b24a-127232254c53"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("c9f8d1f6-e5c7-4192-9897-756994646b72"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("cb359ff6-2b70-4680-9b29-4b8ac66e6f99"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("cc5d3833-baaf-46fb-adce-22f67d9c5507"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("cd767b04-5ebd-4bce-bec9-340235d455dc"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("ce938e57-7951-467a-9958-d422cc6635cf"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("cf5ec4c2-7552-4b95-8bac-be8f2839051c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("cfad1dbd-720e-4140-a75d-1834b97dfa64"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("d121690f-481c-4234-8645-453f348ceed0"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("d1e3558e-ad91-4e6d-827a-6178e83ad0c6"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("d2307e21-042a-47be-971f-5286231cea16"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("d2e58d39-65b3-448f-916a-ebe0a9127d9a"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("d2f14162-8920-4cab-9d83-4adc8a9c067c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("d900e715-6060-4e12-b398-56773147c507"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("d92b475e-fcfd-479a-89ca-e403241c7bca"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("e2ea9f63-ca08-491b-b918-5449fa8eb9b7"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("e352b9d2-b6d9-467e-89b3-9e466a0b72ed"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("e501ae28-11a6-42e8-8e8a-298cfec2f74c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("e6ad130b-7421-4d73-a1fd-8cabc0d97952"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("e6d5b89c-d0fa-416a-9dd6-4a17ff3f74a5"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("e7c7b1ba-d9e2-4dcd-a79c-beaf4a20945f"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("ea00bd7c-09d0-4e89-9d29-e387eb663553"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("ebc3918c-9e92-4dc2-9ac7-3dcc60de2efd"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("ec20618b-7d3e-46d0-8fd5-d4560bc9b93c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("ede074b2-ddc5-4477-9a65-0a2736d38dc4"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("ede96210-ef20-4d49-af2e-3c450e3a1f13"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("edf0bf49-355c-45a4-bb52-0be77910a4fb"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("f0a3abe7-1f7f-44b6-8c4c-33365b1536b8"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("f14b4759-0f59-4661-a20a-a978a4375390"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("f391bbc7-9e8f-4302-9af8-8e8302475747"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("f42d1a34-02c6-433e-b1ad-a4b695b57c31"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("f47e2dcd-d83e-45fb-96da-ac9a0b9dd51c"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("f8892945-0537-4541-b0d2-0c38d5a5da7d"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("fcdca488-35f1-44ad-9e35-1825f113822f"));

            migrationBuilder.DeleteData(
                table: "AnswerOption",
                keyColumn: "Id",
                keyValue: new Guid("fda691b9-a3f5-4351-8d86-3a025cdbbaf4"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0001-0001-0001-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0002-0002-0002-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0003-0003-0003-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0004-0004-0004-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0005-0005-0005-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0006-0006-0006-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0007-0007-0007-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0008-0008-0008-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0009-0009-0009-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0010-0010-0010-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0011-0011-0011-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0012-0012-0012-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0013-0013-0013-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0014-0014-0014-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0015-0015-0015-111111111111"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0001-0001-0001-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0002-0002-0002-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0003-0003-0003-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0004-0004-0004-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0005-0005-0005-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0006-0006-0006-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0007-0007-0007-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0008-0008-0008-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0009-0009-0009-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0010-0010-0010-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0011-0011-0011-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0012-0012-0012-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0013-0013-0013-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0014-0014-0014-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0015-0015-0015-222222222222"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0001-0001-0001-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0002-0002-0002-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0003-0003-0003-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0004-0004-0004-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0005-0005-0005-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0006-0006-0006-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0007-0007-0007-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0008-0008-0008-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0009-0009-0009-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0010-0010-0010-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0011-0011-0011-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0012-0012-0012-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0013-0013-0013-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0014-0014-0014-333333333333"));

            migrationBuilder.DeleteData(
                table: "Question",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0015-0015-0015-333333333333"));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 13, 4, 5, 3, 904, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 13, 4, 5, 3, 904, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "UpdatedAt",
                value: new DateTime(2026, 5, 13, 4, 5, 3, 904, DateTimeKind.Utc).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$11$YwSpCFoG3NTEPbp4xv9ji.s8tK4eoHgY/isa8o.XgIN43cxq7rhty");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "PasswordHash",
                value: "$2a$11$evaE/f9F4MOZQU3uf7BJpOnDP7WDdJxiMB6zE/d8a5v1nqbW9ruaC");

            migrationBuilder.UpdateData(
                table: "UserAccount",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "PasswordHash",
                value: "$2a$11$TMyd2wSKcnVJ5CwkqU2tg.6axAuWlBHQwSTw3rcOAgxrZl3Ff1wPS");
        }
    }
}
