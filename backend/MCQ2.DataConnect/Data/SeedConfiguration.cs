using MCQ3.DataConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCQ3.DataConnect.Data;

public   class SeedConfiguration (ModelBuilder builder)
{
    // Fixed GUIDs
    private static readonly Guid AdminUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TeacherUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid StudentUserId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    private static readonly Guid Subject1Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid Chapter1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
    private static readonly Guid Exam1Id = Guid.Parse("77777777-7777-7777-7777-777777777777");

    private static readonly Guid Subject2Id = Guid.Parse("88888888-8888-8888-8888-888888888888");
    private static readonly Guid Chapter2Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
    private static readonly Guid Exam2Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

    private static readonly Guid Subject3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
    private static readonly Guid Chapter3Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
    private static readonly Guid Exam3Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

    // Deterministic question GUIDs: chapter prefix + question number
    private static Guid QId(int chapterNum, int qNum) =>
        Guid.Parse($"{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}-{qNum:D4}-{qNum:D4}-{qNum:D4}-{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}{chapterNum}");

    // Deterministic option GUIDs: option index within a flat list per chapter
    private static Guid OId(int chapterNum, int flatIndex) =>
        Guid.Parse($"f{chapterNum:D7}-{flatIndex:D4}-{flatIndex:D4}-{flatIndex:D4}-f{chapterNum:D7}{flatIndex:D4}");

    private static readonly DateTime SeededAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

public   void Configure( )
    {
        builder.Entity<Role>().HasData(
            new Role { Id = Guid.Parse("00000001-0000-0000-0000-000000000001"), Name = "Admin", IsActive = true, CreatedAt = SeededAt },
            new Role { Id = Guid.Parse("00000002-0000-0000-0000-000000000002"), Name = "Teacher", IsActive = true, CreatedAt = SeededAt },
            new Role { Id = Guid.Parse("00000003-0000-0000-0000-000000000003"), Name = "Student", IsActive = true, CreatedAt = SeededAt }
        );

        var adminRoleId = Guid.Parse("00000001-0000-0000-0000-000000000001");
        var teacherRoleId = Guid.Parse("00000002-0000-0000-0000-000000000002");
        var studentRoleId = Guid.Parse("00000003-0000-0000-0000-000000000003");

        builder.Entity<UserAccount>().HasData(
            new UserAccount {
                Id = AdminUserId,
                FullName = "Admin User",
                Email = "admin@mcq2.com",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                IsActive = true,
                TempPassword = false,
                RoleId = adminRoleId,
                CreatedAt = SeededAt,
                UpdatedAt = SeededAt
            },
            new UserAccount {
                Id = TeacherUserId,
                FullName = "John Smith",
                Email = "teacher@mcq2.com",
                Username = "teacher",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("teacher123"),
                IsActive = true,
                TempPassword = false,
                RoleId = teacherRoleId,
                CreatedAt = SeededAt,
                UpdatedAt = SeededAt
            },
            new UserAccount {
                Id = StudentUserId,
                FullName = "Alice Johnson",
                Email = "student@mcq2.com",
                Username = "student",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("student123"),
                IsActive = true,
                TempPassword = false,
                RoleId = studentRoleId,
                CreatedAt = SeededAt,
                UpdatedAt = SeededAt
            }
        );

        builder.Entity<Teacher>().HasData(
            new Teacher {
                UserId = TeacherUserId,
                Name = "John Smith",
                Email = "teacher@mcq2.com",
                NID = "TCH001",
                Title = "Senior Instructor",
                Address = "123 Teacher St",
                ContactNo = "555-1234",
                IsActive = true,
                CreatedAt = SeededAt,
                UpdatedAt = SeededAt
            }
        );

        builder.Entity<Student>().HasData(
            new Student {
                UserId = StudentUserId,
                Code = "STU001",
                Name = "Alice Johnson",
                NID = "STU001NID",
                Email = "student@mcq2.com",
                Address = "456 Student Ave",
                ContactNo = "555-5678",
                FatherName = "Robert Johnson",
                FatherContact = "555-5679",
                MotherName = "Mary Johnson",
                MotherContact = "555-5680",
                IsActive = true,
                CreatedAt = SeededAt,
                UpdatedAt = SeededAt
            }
        );

        // Subject and Chapter seed data
        builder.Entity<Subject>().HasData(
            new Subject { Id = Subject1Id, TeacherId = TeacherUserId, Title = "C# Programming", Description = "Learn C# fundamentals", IsArchived = false, CreatedAt = SeededAt, UpdatedAt = SeededAt },
            new Subject { Id = Subject2Id, TeacherId = TeacherUserId, Title = "Data Structures", Description = "Learn data structures", IsArchived = false, CreatedAt = SeededAt, UpdatedAt = SeededAt },
            new Subject { Id = Subject3Id, TeacherId = TeacherUserId, Title = "SQL Basics", Description = "Learn SQL fundamentals", IsArchived = false, CreatedAt = SeededAt, UpdatedAt = SeededAt }
        );

        builder.Entity<Chapter>().HasData(
            new Chapter { Id = Chapter1Id, SubjectId = Subject1Id, Title = "C# Basics", Description = "Variables, types, and control flow", OrderIndex = 1, IsArchived = false, CreatedAt = SeededAt, UpdatedAt = SeededAt },
            new Chapter { Id = Chapter2Id, SubjectId = Subject2Id, Title = "Data Structures Basics", Description = "Arrays, lists, and stacks", OrderIndex = 1, IsArchived = false, CreatedAt = SeededAt, UpdatedAt = SeededAt },
            new Chapter { Id = Chapter3Id, SubjectId = Subject3Id, Title = "SQL Basics", Description = "SELECT, WHERE, and JOIN", OrderIndex = 1, IsArchived = false, CreatedAt = SeededAt, UpdatedAt = SeededAt }
        );
    }

    // ── Option factories (now accept pre-computed base GUID) ─────────────────
    // Each call produces 4 options; oBase is OId(chapter, (q-1)*4)
    // We derive the 4 option GUIDs by offsetting the flat index by 0..3.
    // Since GUIDs aren't arithmetic, we just pass the base index and recompute.

    private static List<AnswerOption> CreateOptions(Guid qId, int qNum, Guid _) =>
        CreateOptions(qId, qNum);

    private static List<AnswerOption> CreateDsOptions(Guid qId, int qNum, Guid _) =>
        CreateDsOptions(qId, qNum);

    private static List<AnswerOption> CreateSqlOptions(Guid qId, int qNum, Guid _) =>
        CreateSqlOptions(qId, qNum);

    // ── C# question text / explanations ──────────────────────────────────────
    private static string GetQuestionText(int i) => i switch
    {
        1 => "Which keyword is used to declare a variable in C#?",
        2 => "What is the default value of an int in C#?",
        3 => "Which of these is a valid C# identifier?",
        4 => "What does 'typeof' operator return?",
        5 => "Which collection is zero-indexed in C#?",
        6 => "What is the base class of all types in C#?",
        7 => "Which keyword is used to inherit a class?",
        8 => "What is a namespace in C#?",
        9 => "Which access modifier is default for a class?",
        10 => "What is the purpose of 'using' statement?",
        11 => "Which of these is NOT a value type?",
        12 => "What is boxing in C#?",
        13 => "What is the difference between 'struct' and 'class'?",
        14 => "What is polymorphism?",
        15 => "What is an interface in C#?",
        _ => ""
    };

    private static string GetExplanation(int i) => i switch
    {
        1 => "In C#, variables are declared by specifying a type followed by an identifier.",
        2 => "Value types in C# have default values. int defaults to 0.",
        3 => "C# identifiers must start with a letter or underscore and can contain letters, digits, and underscores.",
        4 => "typeof returns a Type object representing the specified type.",
        5 => "Arrays in C# are zero-indexed, meaning the first element is at index 0.",
        6 => "object is the base class for all types in C#.",
        7 => "The colon (:) syntax is used for inheritance in C#.",
        8 => "A namespace is a container for classes and other types to organize code.",
        9 => "If no access modifier is specified, classes are internal by default.",
        10 => "using statement ensures proper disposal of resources.",
        11 => "string is a reference type, not a value type.",
        12 => "Boxing is converting a value type to object type.",
        13 => "struct is a value type (stack) while class is a reference type (heap).",
        14 => "Polymorphism allows methods to have different implementations based on the object type.",
        15 => "An interface defines a contract that classes can implement.",
        _ => ""
    };

    private static List<AnswerOption> CreateOptions(Guid qId, int qNum) => qNum switch
    {
        1 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "var",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "int",    IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "let",    IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "define", IsCorrect = false, OrderIndex = 3 }
        },
        2 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "null",      IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "0",         IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "undefined", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "1",         IsCorrect = false, OrderIndex = 3 }
        },
        3 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "myVariable",   IsCorrect = true,  OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "2var",          IsCorrect = false, OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "my-variable",   IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "class",         IsCorrect = false, OrderIndex = 3 }
        },
        4 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "string",      IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Type object", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "bool",        IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "int",         IsCorrect = false, OrderIndex = 3 }
        },
        5 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "ArrayList",  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Array",      IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Dictionary", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "HashSet",    IsCorrect = false, OrderIndex = 3 }
        },
        6 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "System",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "object",    IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "ValueType", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Base",      IsCorrect = false, OrderIndex = 3 }
        },
        7 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "extends",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = ":",          IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "inherits",   IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "implements", IsCorrect = false, OrderIndex = 3 }
        },
        8 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A data type",                       IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A container for organizing types",  IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A loop structure",                  IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A method",                          IsCorrect = false, OrderIndex = 3 }
        },
        9 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "public",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "internal",  IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "private",   IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "protected", IsCorrect = false, OrderIndex = 3 }
        },
        10 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "To import namespaces",                    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "To ensure proper disposal of resources",  IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "To create threads",                       IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "To handle exceptions",                    IsCorrect = false, OrderIndex = 3 }
        },
        11 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "int",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "bool",   IsCorrect = false, OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "string", IsCorrect = true,  OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "struct", IsCorrect = false, OrderIndex = 3 }
        },
        12 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Converting object to value type", IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Converting value type to object", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Converting string to int",        IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Converting int to string",        IsCorrect = false, OrderIndex = 3 }
        },
        13 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "struct is a reference type",                           IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "struct is a value type, class is a reference type",    IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "struct supports inheritance",                          IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "class cannot have methods",                            IsCorrect = false, OrderIndex = 3 }
        },
        14 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Multiple inheritance",                                  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Same method behaves differently based on object type",  IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Hiding implementation",                                 IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Creating multiple objects",                             IsCorrect = false, OrderIndex = 3 }
        },
        15 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A base class",                                IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A contract that classes can implement",       IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A sealed class",                              IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "An abstract method",                          IsCorrect = false, OrderIndex = 3 }
        },
        _ => new()
    };

    // ── Data Structures question text / explanations ──────────────────────────
    private static string GetDsQuestionText(int i) => i switch
    {
        1 => "What is the time complexity of accessing an element in an array by index?",
        2 => "Which data structure follows LIFO order?",
        3 => "What is the default capacity of a List<T> in C#?",
        4 => "Which collection does not allow duplicate elements?",
        5 => "What is the complexity of searching in an unsorted array?",
        6 => "How do you declare a 2D array in C#?",
        7 => "Which method is used to add an element at the end of a List?",
        8 => "What is a hash table?",
        9 => "Which collection is best for key-value pairs?",
        10 => "What is the difference between Array and ArrayList?",
        11 => "What is the time complexity of inserting at the beginning of a List?",
        12 => "What is a queue data structure?",
        13 => "What is the difference between Stack and Queue?",
        14 => "What is a linked list?",
        15 => "What is the space complexity of an array?",
        _ => ""
    };

    private static string GetDsExplanation(int i) => i switch
    {
        1 => "Array provides O(1) constant time access by index.",
        2 => "Stack follows Last In First Out (LIFO) order.",
        3 => "List<T> starts with an empty capacity and grows as needed.",
        4 => "HashSet<T> does not allow duplicate elements.",
        5 => "Linear search in unsorted array takes O(n) time.",
        6 => "2D arrays in C# are declared as type[,] variableName.",
        7 => "Add() method adds an element at the end of the List.",
        8 => "A hash table maps keys to values using a hash function.",
        9 => "Dictionary<TKey, TValue> is best for key-value pairs.",
        10 => "Array is strongly typed, ArrayList can hold any type.",
        11 => "Inserting at beginning is O(n) because all elements shift.",
        12 => "Queue follows First In First Out (FIFO) order.",
        13 => "Stack is LIFO, Queue is FIFO.",
        14 => "A linked list is a linear collection of nodes.",
        15 => "Array has O(n) space complexity for n elements.",
        _ => ""
    };

    private static List<AnswerOption> CreateDsOptions(Guid qId, int qNum) => qNum switch
    {
        1 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(1)",      IsCorrect = true,  OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n)",      IsCorrect = false, OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(log n)",  IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n^2)",    IsCorrect = false, OrderIndex = 3 }
        },
        2 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Queue", IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Stack", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Array", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "List",  IsCorrect = false, OrderIndex = 3 }
        },
        3 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "4",  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "0",  IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "8",  IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "16", IsCorrect = false, OrderIndex = 3 }
        },
        4 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "List",      IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "ArrayList", IsCorrect = false, OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "HashSet",   IsCorrect = true,  OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Stack",     IsCorrect = false, OrderIndex = 3 }
        },
        5 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(1)",       IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n)",       IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(log n)",   IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n log n)", IsCorrect = false, OrderIndex = 3 }
        },
        6 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "int[][] arr",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "int[,] arr",     IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "List<int[]> arr",IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Array<int> arr", IsCorrect = false, OrderIndex = 3 }
        },
        7 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Insert()",  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Add()",     IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Push()",    IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Enqueue()", IsCorrect = false, OrderIndex = 3 }
        },
        8 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A sorting algorithm",                       IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A data structure that maps keys to values", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A type of array",                           IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A search algorithm",                        IsCorrect = false, OrderIndex = 3 }
        },
        9 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "List<T>",                  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Dictionary<TKey, TValue>", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Queue<T>",                 IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Stack<T>",                 IsCorrect = false, OrderIndex = 3 }
        },
        10 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Array is slower",                                          IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Array is strongly typed, ArrayList can hold any type",     IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "ArrayList uses less memory",                               IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "They are the same",                                        IsCorrect = false, OrderIndex = 3 }
        },
        11 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(1)",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n)",    IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(log n)",IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n^2)",  IsCorrect = false, OrderIndex = 3 }
        },
        12 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Last In First Out",  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "First In First Out", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Random access",      IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "No specific order",  IsCorrect = false, OrderIndex = 3 }
        },
        13 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Stack is FIFO, Queue is LIFO",  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Stack is LIFO, Queue is FIFO",  IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Both are the same",             IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Stack uses array, Queue uses list", IsCorrect = false, OrderIndex = 3 }
        },
        14 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A sequential collection with fixed size",                                  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A linear collection of nodes where each node points to the next",          IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A type of tree",                                                           IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A hash-based structure",                                                   IsCorrect = false, OrderIndex = 3 }
        },
        15 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(1)",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n)",    IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(log n)",IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "O(n^2)",  IsCorrect = false, OrderIndex = 3 }
        },
        _ => new()
    };

    // ── SQL question text / explanations ─────────────────────────────────────
    private static string GetSqlQuestionText(int i) => i switch
    {
        1 => "Which SQL keyword is used to retrieve data from a database?",
        2 => "Which clause is used to filter groups in SQL?",
        3 => "What does JOIN do in SQL?",
        4 => "Which function is used to count rows in SQL?",
        5 => "What is the default order of ORDER BY?",
        6 => "Which is NOT a type of JOIN?",
        7 => "What does DISTINCT do?",
        8 => "Which is used to update data in a table?",
        9 => "What is a primary key?",
        10 => "What does FOREIGN KEY do?",
        11 => "Which clause is used with aggregate functions?",
        12 => "What is the difference between WHERE and HAVING?",
        13 => "What is a subquery?",
        14 => "What does UNION do?",
        15 => "What is normalization?",
        _ => ""
    };

    private static string GetSqlExplanation(int i) => i switch
    {
        1 => "SELECT is the SQL keyword used to retrieve data.",
        2 => "HAVING clause filters groups after GROUP BY.",
        3 => "JOIN combines rows from two tables based on a related column.",
        4 => "COUNT() function counts the number of rows.",
        5 => "ORDER BY defaults to ASC (ascending).",
        6 => "FULL OUTER JOIN, LEFT JOIN, RIGHT JOIN, INNER JOIN are the types.",
        7 => "DISTINCT returns only unique values.",
        8 => "UPDATE statement modifies existing data in a table.",
        9 => "Primary key uniquely identifies each row in a table.",
        10 => "FOREIGN KEY creates a relationship between tables.",
        11 => "GROUP BY is used with aggregate functions.",
        12 => "WHERE filters before grouping, HAVING filters after.",
        13 => "A subquery is a query nested inside another query.",
        14 => "UNION combines results of two SELECT statements.",
        15 => "Normalization organizes data to reduce redundancy.",
        _ => ""
    };

    private static List<AnswerOption> CreateSqlOptions(Guid qId, int qNum) => qNum switch
    {
        1 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "GET",      IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "SELECT",   IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "FETCH",    IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "RETRIEVE", IsCorrect = false, OrderIndex = 3 }
        },
        2 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "WHERE",  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "HAVING", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "FILTER", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "GROUP",  IsCorrect = false, OrderIndex = 3 }
        },
        3 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Deletes records",              IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Combines rows from two tables",IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Creates new tables",           IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Updates records",              IsCorrect = false, OrderIndex = 3 }
        },
        4 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "SUM()",   IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "COUNT()", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "TOTAL()", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "MAX()",   IsCorrect = false, OrderIndex = 3 }
        },
        5 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "DESC",   IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "ASC",    IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Random", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "None",   IsCorrect = false, OrderIndex = 3 }
        },
        6 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "INNER JOIN", IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "LEFT JOIN",  IsCorrect = false, OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "CROSS JOIN", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "LOOP JOIN",  IsCorrect = true,  OrderIndex = 3 }
        },
        7 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Sorts data",                IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Returns only unique values",IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Filters data",              IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Groups data",               IsCorrect = false, OrderIndex = 3 }
        },
        8 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "INSERT", IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "UPDATE", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "MODIFY", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "CHANGE", IsCorrect = false, OrderIndex = 3 }
        },
        9 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A foreign identifier",              IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Unique identifier for each row",    IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A column name",                     IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A table constraint",                IsCorrect = false, OrderIndex = 3 }
        },
        10 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Links tables together", IsCorrect = true,  OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Creates a new table",  IsCorrect = false, OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Deletes records",      IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Updates data",         IsCorrect = false, OrderIndex = 3 }
        },
        11 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "WHERE",    IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "GROUP BY", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "ORDER BY", IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "HAVING",   IsCorrect = false, OrderIndex = 3 }
        },
        12 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "They are the same",                      IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "WHERE filters rows, HAVING filters groups",IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "HAVING is faster",                       IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "WHERE can use aggregates",               IsCorrect = false, OrderIndex = 3 }
        },
        13 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A JOIN operation",           IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A query inside another query",IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A view",                     IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "A stored procedure",         IsCorrect = false, OrderIndex = 3 }
        },
        14 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Combines all rows",                                  IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Combines results of two queries, removes duplicates", IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Joins tables",                                       IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Creates a new table",                                IsCorrect = false, OrderIndex = 3 }
        },
        15 => new() {
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Adding more tables",                          IsCorrect = false, OrderIndex = 0 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Organizing data to reduce redundancy",        IsCorrect = true,  OrderIndex = 1 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Encrypting data",                             IsCorrect = false, OrderIndex = 2 },
            new() { Id = Guid.NewGuid(), QuestionId = qId, OptionText = "Backing up data",                             IsCorrect = false, OrderIndex = 3 }
        },
        _ => new()
    };
}