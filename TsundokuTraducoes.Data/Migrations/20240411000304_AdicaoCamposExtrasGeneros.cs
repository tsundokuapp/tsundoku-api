using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TsundokuTraducoes.Data.Migrations
{
    public partial class AdicaoCamposExtrasGeneros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1b21f901-79a6-4b52-9655-977c8b75e02b"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1c4764bc-a51a-4c20-928b-a3de4383f3d0"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("3b2056b1-7e0a-4671-a97f-15075c78e133"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("3dc4debc-7712-44db-aad0-2816e2560bfb"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("73f3dc77-f599-4b20-adf6-b6d5bfdd707e"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("86bd1103-39ef-4976-8665-d2aec2029817"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("9acb8e7b-c0c8-4a47-b0df-3ebadbaf63bb"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("fbe71c34-39f7-43ac-9f70-297cabdea5ee"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Generos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "Generos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracao",
                table: "Generos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioInclusao",
                table: "Generos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(420), new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(245), new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(329), new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(329) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local), new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(79), new DateTime(2024, 4, 10, 21, 3, 3, 845, DateTimeKind.Local).AddTicks(9999) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("276d8207-e682-4b48-b74c-7f141ede6faa"), null, null, "Horror", "horror", null, null },
                    { new Guid("2e55ab74-9f1b-4f95-93f6-322dee0326b3"), null, null, "Ação", "acao", null, null },
                    { new Guid("4562843a-45ed-45e1-995e-28cbc0db8580"), null, null, "Harém", "harem", null, null },
                    { new Guid("474f9278-5731-404b-914a-10dd85a0b49a"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("49ec8349-6ed2-43ad-8618-5a60c21f5234"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("51a67cc5-1ff8-44c1-97a2-282cb547c522"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("b9afdacf-5616-4411-8a2f-3dc683d2f064"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("fb60727b-6843-4a2f-96e8-a38c80653a32"), null, null, "Drama", "drama", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 10, 21, 3, 3, 845, DateTimeKind.Local).AddTicks(9815), new DateTime(2024, 4, 10, 21, 3, 3, 845, DateTimeKind.Local).AddTicks(9979), new DateTime(2024, 4, 10, 21, 3, 3, 845, DateTimeKind.Local).AddTicks(9811) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(170), new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(170) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(94), new DateTime(2024, 4, 10, 21, 3, 3, 846, DateTimeKind.Local).AddTicks(94) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("276d8207-e682-4b48-b74c-7f141ede6faa"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2e55ab74-9f1b-4f95-93f6-322dee0326b3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("4562843a-45ed-45e1-995e-28cbc0db8580"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("474f9278-5731-404b-914a-10dd85a0b49a"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("49ec8349-6ed2-43ad-8618-5a60c21f5234"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("51a67cc5-1ff8-44c1-97a2-282cb547c522"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("b9afdacf-5616-4411-8a2f-3dc683d2f064"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("fb60727b-6843-4a2f-96e8-a38c80653a32"));

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusao",
                table: "Generos");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9938), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9937) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9759), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9758) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9875), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9874) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9514), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9592), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9514) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Descricao", "Slug" },
                values: new object[,]
                {
                    { new Guid("1b21f901-79a6-4b52-9655-977c8b75e02b"), "Drama", "drama" },
                    { new Guid("1c4764bc-a51a-4c20-928b-a3de4383f3d0"), "Slice of Life", "slice-of-life" },
                    { new Guid("3b2056b1-7e0a-4671-a97f-15075c78e133"), "Isekai", "isekai" },
                    { new Guid("3dc4debc-7712-44db-aad0-2816e2560bfb"), "Ação", "acao" },
                    { new Guid("73f3dc77-f599-4b20-adf6-b6d5bfdd707e"), "Harém", "harem" },
                    { new Guid("86bd1103-39ef-4976-8665-d2aec2029817"), "Horror", "horror" },
                    { new Guid("9acb8e7b-c0c8-4a47-b0df-3ebadbaf63bb"), "Comédia", "comedia" },
                    { new Guid("fbe71c34-39f7-43ac-9f70-297cabdea5ee"), "Fantasia", "fantasia" }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9330), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9493), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9327) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9684), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9683) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9606), new DateTime(2024, 3, 13, 18, 30, 10, 860, DateTimeKind.Local).AddTicks(9606) });
        }
    }
}
