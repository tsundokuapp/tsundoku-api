using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TsundokuTraducoes.Data.Migrations
{
    public partial class AdicaoCampoObservacaoObras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Novels",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Comics",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 44, DateTimeKind.Local).AddTicks(187), new DateTime(2024, 4, 12, 10, 39, 34, 44, DateTimeKind.Local).AddTicks(187) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 44, DateTimeKind.Local).AddTicks(59), new DateTime(2024, 4, 12, 10, 39, 34, 44, DateTimeKind.Local).AddTicks(58) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 44, DateTimeKind.Local).AddTicks(126), new DateTime(2024, 4, 12, 10, 39, 34, 44, DateTimeKind.Local).AddTicks(125) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Observacao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9788), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9887), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9787), "" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("0ded2fcc-e11f-4a2b-9326-784255ca52ed"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("25830544-1fac-4857-90f8-e99e84d1123a"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("48e33c46-bf45-4f93-b4d0-a23a617f63f1"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("89eaaa6c-0942-442e-b30c-b9335439017b"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("904904b2-8a7e-42ce-a98a-cbccab680324"), null, null, "Drama", "drama", null, null },
                    { new Guid("9f67c7bc-494d-4581-86f6-89a40f15868f"), null, null, "Ação", "acao", null, null },
                    { new Guid("b953494a-a405-4bdf-8191-0daf69079c8d"), null, null, "Horror", "horror", null, null },
                    { new Guid("e41792af-c24b-4e6d-8bc7-6dcd7c2f5ffc"), null, null, "Harém", "harem", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Observacao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9608), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9768), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9605), "" });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9986), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9986) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9905), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9904) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("0ded2fcc-e11f-4a2b-9326-784255ca52ed"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("25830544-1fac-4857-90f8-e99e84d1123a"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("48e33c46-bf45-4f93-b4d0-a23a617f63f1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("89eaaa6c-0942-442e-b30c-b9335439017b"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("904904b2-8a7e-42ce-a98a-cbccab680324"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("9f67c7bc-494d-4581-86f6-89a40f15868f"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("b953494a-a405-4bdf-8191-0daf69079c8d"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e41792af-c24b-4e6d-8bc7-6dcd7c2f5ffc"));

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Novels");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Comics");

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
    }
}
