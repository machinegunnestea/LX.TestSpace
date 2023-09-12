using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LX.TestSpace.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserNameCreate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestScore = table.Column<double>(type: "float", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: true),
                    QuestionSnapshots = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestResults_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5328e587-251f-4f02-9d79-f4dfccae023c", 1, "0733a141-df04-4717-ae0f-83767851d6e2", null, false, true, null, "Test", null, null, null, null, true, "e4e96d11-6db4-41df-92e2-8096e52240c1", "Test", true, "TestUser" });

            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "Id", "CreationDate", "Description", "Duration", "IsPublished", "Name", "UserNameCreate" },
                values: new object[] { 1, new DateTime(2023, 6, 16, 10, 50, 48, 521, DateTimeKind.Local).AddTicks(1705), "Test", new TimeSpan(0, 0, 0, 0, 0), true, "Test", "TestUser" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "ImagePath", "TestId", "Text" },
                values: new object[,]
                {
                    { 1, null, 1, "What is CLR?" },
                    { 2, null, 1, "What does EntityFramework allow?" },
                    { 3, null, 1, "What are the main data types in c#?" },
                    { 4, null, 1, "What is the difference between value types and reference types?" },
                    { 5, null, 1, "Can the String type be passed by reference?" },
                    { 6, null, 1, "What's the difference between String and StringBuilder?" },
                    { 7, null, 1, "How is data passed from method to method?" },
                    { 8, null, 1, "What are named arguments" },
                    { 9, null, 1, "Why is C# called an object-oriented language?" },
                    { 10, null, 1, "What is the main difference between C# and JavaScript?" },
                    { 11, null, 1, "How does C# solve the memory cleanup problem?" },
                    { 12, null, 1, "What is the difference between Scruct and Class?" },
                    { 13, null, 1, "Generics: what is it and what problem do they solve?" },
                    { 14, null, 1, "What is unmanaged code?" },
                    { 15, null, 1, "Can we create a constructor in a static class?" },
                    { 16, null, 1, "Access Modifiers" },
                    { 17, null, 1, "Who can access protected variables?" },
                    { 18, null, 1, "What is the benefit of using an Interface?" },
                    { 19, null, 1, "Can an Interface implement methods?" },
                    { 20, null, 1, "Can an Interface describe properties?" },
                    { 21, null, 1, "What is Entity Framework?" },
                    { 22, null, 1, "What is a using directive, when and what is it used for?" },
                    { 23, null, 1, "What is JSON?" },
                    { 24, null, 1, "What is needed so that our collection can be iterated by ForEach?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, true, 1, "Common language runtime" },
                    { 2, false, 1, "Exception handling mechanism" },
                    { 3, false, 1, "Memory release mechanism" },
                    { 4, false, 1, "Code containing a set of instructions" },
                    { 5, true, 2, "Abstract from the database and work with data regardless of storage" },
                    { 6, false, 2, "Create applications regardless of which OS the code will run on" },
                    { 7, false, 2, "Use the latest versions of C# and Visual Studio" },
                    { 8, true, 3, "Reference, value, primitive" },
                    { 9, false, 3, "CustomTypes, reference, value" },
                    { 10, false, 3, "Value, reference" },
                    { 11, false, 3, "String, Object, Double, Int32, Int64" },
                    { 12, true, 4, "Value types are more used in code than reference types" },
                    { 13, false, 4, "Almost nothing, since reference types still refer to value types" },
                    { 14, false, 4, "Value types significantly outnumber reference types in terms of memory footprint" },
                    { 15, false, 4, "Reference types reside on the managed heap, value on the stack" },
                    { 16, false, 4, "For reference types, space is allocated on the hard disk, for significant types - in RAM" },
                    { 17, true, 5, "Yes, it's a reference type, and like all reference types, it can be passed by reference." },
                    { 18, false, 5, "It depends on Visual Studio settings" },
                    { 19, false, 5, "No, strings are immutable, their value cannot be changed after creation" },
                    { 20, true, 6, "StringBuilder has less \"weight\"" },
                    { 21, false, 6, "StringBuilder is a deprecated type that has to be supported because older applications still exist." },
                    { 22, false, 6, "Which StringBuilder? This type doesn't exist at all." },
                    { 23, false, 6, "When the String type is changed, a new string is always created, when the StringBuilder is changed, the current string is changed." },
                    { 24, true, 7, "Using a special JSON format" },
                    { 25, false, 7, "Using Method Parameters" },
                    { 26, false, 7, "With the help of a teammate" },
                    { 27, false, 7, "Using EntityFramework" },
                    { 28, false, 8, "These are any arguments (all arguments have their own name)." },
                    { 29, false, 8, "These are the options that use the reserved names." },
                    { 30, false, 8, "These are the arguments whose names we force when creating an object." },
                    { 31, true, 9, "Because it was necessary to somehow distinguish between languages like C and C#." },
                    { 32, false, 9, "Because in C# we operate on objects." },
                    { 33, false, 9, "Because the C# language allows us to navigate the code more." },
                    { 34, false, 9, "Because it is very similar to C++, and C++ is an object-oriented language." },
                    { 35, true, 10, "Everything is simple here, one language is used to write the Backend, the second - for the Frontend, this is the main difference." },
                    { 36, false, 10, "C# is an object-oriented language, JavaScript is not." },
                    { 37, false, 10, "There are no differences, these are languages from different companies." },
                    { 38, false, 10, "C# applications can be compiled on Linux, JavaScript cannot." },
                    { 39, true, 11, "There is a special mechanism for this, (GC) which is responsible for garbage collection." },
                    { 40, false, 11, "We must constantly remember that unused objects must be deleted." },
                    { 41, false, 11, "It is necessary to use the Using directive more often." },
                    { 42, true, 12, "Struct is an obsolete type that cannot be used, every time we create a Struct we must mark it with the Obsolete attribute." },
                    { 43, true, 12, "Struct is a value type and Class is a reference type." },
                    { 44, false, 12, "Struct is the same as Class, only serves to denote small classes." },
                    { 45, false, 12, "Struct is generally from JavaScript and has nothing to do with C#." },
                    { 46, true, 13, "Serves to generate random values." },
                    { 47, false, 13, "This marks the main Class, and serves to be able to use previously defined methods." },
                    { 48, false, 13, "This is a generic mechanism that allows you to substitute any type in a method or class, thereby avoiding the use of boxing and unboxing." },
                    { 49, false, 13, "It's just \"syntactic sugar\" that serves to make it easier to write code." },
                    { 50, true, 14, "This is code that we didn't write, so we can't control it." },
                    { 51, false, 14, "This is a code in which we absolutely do not understand what is happening, so we cannot influence it." },
                    { 52, false, 14, "Code that doesn't run in the CLR." },
                    { 53, false, 14, "Code uploaded on GitHub." },
                    { 54, true, 15, "Yes." },
                    { 55, false, 15, "No." },
                    { 56, false, 15, "We can, if the class has at least one non-static method." },
                    { 57, false, 15, "No, only if you do not enable this option in the Visual Studio settings." },
                    { 58, true, 16, "Private, public, private protected, protected, internal, protected internal" },
                    { 59, false, 16, "Private, public, public protected, protected, internal, public internal" },
                    { 60, false, 16, "Private, public, public protected, protected, internal, public internal, private internal" },
                    { 61, true, 17, "Such class components are available only in its assembly." },
                    { 62, false, 17, "Such class components are accessible from anywhere in its class or in derived classes." },
                    { 63, false, 17, "Such class components are available to all team members." },
                    { 64, false, 17, "Nobody, this is a protected class, and serves to work only inside this class." },
                    { 65, true, 18, "Interface allows us to implement loose coupling" },
                    { 66, false, 18, "As such, there is no advantage, Interface came to replace Abstract classes" },
                    { 67, false, 18, "Interface should only be used when we need multiple inheritance" },
                    { 68, true, 19, "No, it can only define" },
                    { 69, false, 19, "It can if they are marked as virtual" },
                    { 70, false, 19, "It can if we use a new version of the C# language" },
                    { 71, true, 20, "Yes, if they are marked as virtual" },
                    { 72, false, 20, "No, only methods are described in Interface" },
                    { 73, false, 20, "Yes" },
                    { 74, true, 21, "It is an open source ORM framework for ADO.NET that is part of the .NET Framework." },
                    { 75, false, 21, "This is a technology that allows us to use C# types" },
                    { 76, false, 21, "This is what IL code is generated with." },
                    { 77, false, 21, "This is the last thing I will use in my application." },
                    { 78, true, 22, "Often used in opening a database connection session, opening a file open thread." },
                    { 79, false, 22, "Used to connect relational databases" },
                    { 80, false, 22, "Used to connect non-relational databases" },
                    { 81, false, 22, "Used to debug an application" },
                    { 82, true, 23, "This is developer’s nickname ( Jason Statham )" },
                    { 83, false, 23, "Text exchange data format based on JavaScript." },
                    { 84, false, 23, "Text-based data exchange format based on XML." },
                    { 85, false, 23, "This is an extension that allows the use of JavaScript in the application." },
                    { 86, true, 24, "All you need to do is write array.ForEach( x => x…..)" },
                    { 87, false, 24, "We need to make sure our collection implements the IEnumerable interface." },
                    { 88, false, 24, "Our collection needs to be sorted" },
                    { 89, false, 24, "We need to make sure our collection implements the IEnumerable interface and only has primitive types." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestId",
                table: "TestResults",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_UserId",
                table: "TestResults",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
