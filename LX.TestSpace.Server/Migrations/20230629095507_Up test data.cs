using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LX.TestSpace.Server.Migrations
{
    /// <inheritdoc />
    public partial class Uptestdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table:"Answers",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "IsCorrect", "QuestionId", "Text" },
                values: new object[] {true, 8, "These are any arguments (all arguments have their own name)."  }
                );
            migrationBuilder.UpdateData(
               table: "Tests",
               keyColumn: "Id",
               keyValue: 1,
               columns: new[] { "CreationDate", "Description", "Duration", "IsPublished", "Name", "UserNameCreate" },
               values: new object[] {new DateTime(2023, 6, 16, 10, 50, 48, 521, DateTimeKind.Local).AddTicks(1705), "Test \".NET Welcome\" is designed to assess the knowledge and skills of the developer in working with the .NET framework. This test includes a wide range of .NET-related questions, such as C# syntax, .NET Core, ASP.NET , Entity Framework, LINQ and many others.", new TimeSpan(0, 0, 20, 0, 0), true, ".NET Welcome test", "1@1.com" });
            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "Id", "CreationDate", "Description", "Duration", "IsPublished", "Name", "UserNameCreate" },
                values: new object[] { 2, new DateTime(2023, 6, 29, 12, 55, 7, 828, DateTimeKind.Local).AddTicks(4634), "The \"REST\" test is designed to assess the knowledge and skills of a developer in the field of RESTful API architecture. Questions may relate to various aspects related to REST, such as HTTP methods, URIs, data formatting, security, authentication, authorization, etc.", new TimeSpan(0, 0, 20, 0, 0), true, "What about REST?", "1@1.com" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "ImagePath", "TestId", "Text" },
                values: new object[,]
                {
                    { 25, null, 2, "Is the following statement true?" },
                    { 26, null, 2, "What digit does the status code begin with, signaling an error on the server side?" },
                    { 27, null, 2, "Decryption and encryption are the features of?" },
                    { 28, null, 2, "Which feature of OOP indicates code reusability?" },
                    { 29, null, 2, "What is Dependency Injection used for" },
                    { 30, null, 2, "Which of the following data structures implement FIFO asset management?" },
                    { 31, null, 2, "Which of the following serialization protocols exist?" },
                    { 32, null, 2, "What development methodologies exist?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 90, true, 25, "PUT method adds a child resource under resources collection, when POST modifies a single resource being a part of existing resource collection." },
                    { 91, false, 25, "No, that's incorrect. PUT and POST methods can be used to create new resources and modify existing resources, regardless of whether they are children of a resource collection or not." },
                    { 92, false, 25, "Yes, that's correct, but not completely. The PUT method is used to create or update a resource within a resource collection, while the POST method is used to create a new resource within a collection. PUT can also update an existing resource." },
                    { 93, false, 25, "No, that's incorrect. PUT and POST methods can be used to create new resources and modify existing resources, regardless of whether they are children of a resource collection or not." },
                    { 94, false, 26, "1" },
                    { 95, false, 26, "2" },
                    { 96, false, 26, "3" },
                    { 97, false, 26, "4" },
                    { 98, true, 26, "5" },
                    { 99, false, 27, "Transport layer" },
                    { 100, false, 27, "Session layer" },
                    { 101, true, 27, "Presentation layer" },
                    { 102, false, 27, "Transport Layer" },
                    { 103, false, 28, "Abstraction" },
                    { 104, false, 28, "Polymorphism" },
                    { 105, false, 28, "Encapsulation" },
                    { 106, true, 28, "Inheritance" },
                    { 107, false, 29, "Provide loose coupling" },
                    { 108, false, 29, "Provide high cohesion" },
                    { 109, true, 29, "Injecting unmanaged code" },
                    { 110, false, 29, "Injecting dependent file structures." },
                    { 111, false, 30, "Linked list" },
                    { 112, false, 30, "Stack" },
                    { 113, false, 30, "Binary tree" },
                    { 114, true, 30, "Queue" },
                    { 115, false, 30, "Directed graph" },
                    { 116, true, 31, "XML" },
                    { 117, false, 31, "POST" },
                    { 118, false, 31, "REST" },
                    { 119, true, 31, "JSON" },
                    { 120, true, 31, "Binary " },
                    { 121, true, 31, "SOAP" },
                    { 122, true, 32, "Agile" },
                    { 123, false, 32, "SCAM" },
                    { 124, true, 32, "Waterfall Mode" },
                    { 125, true, 32, "Rapid" },
                    { 126, false, 32, "Datalark" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
