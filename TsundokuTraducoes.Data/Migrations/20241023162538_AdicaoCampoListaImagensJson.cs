using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoCampoListaImagensJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ListaImagens",
                table: "CapitulosComic",
                newName: "ListaImagensJson");

            migrationBuilder.AddColumn<string>(
                name: "ListaImagensJson",
                table: "CapitulosNovel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8756), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8756) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8609), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8608), null });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8681), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8681), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8316), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8385), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8315) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("1743da17-96b6-4d64-9cc5-c1eca66380cd"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("1ae45619-92d5-4426-b0a7-333c21b657a8"), null, null, "Ação", "acao", null, null },
                    { new Guid("5e5a3f17-8f1d-433f-80e1-37d61fe391b3"), null, null, "Harém", "harem", null, null },
                    { new Guid("71594460-c0f3-4fb1-bafe-9dfbe04582da"), null, null, "Drama", "drama", null, null },
                    { new Guid("84c89efa-64f0-4dfe-b02a-5cc0c1eb1768"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("b8e49c2e-9373-4573-8e2b-cc919c5296f3"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("c49f782f-d83a-4bed-bef7-d2ee808dbe20"), null, null, "Horror", "horror", null, null },
                    { new Guid("e6e3d2d0-e244-47d2-9475-5989e9134af0"), null, null, "Fantasia", "fantasia", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8096), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8283), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8093) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8488), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8488) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8414), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8413) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1743da17-96b6-4d64-9cc5-c1eca66380cd"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1ae45619-92d5-4426-b0a7-333c21b657a8"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("5e5a3f17-8f1d-433f-80e1-37d61fe391b3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("71594460-c0f3-4fb1-bafe-9dfbe04582da"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("84c89efa-64f0-4dfe-b02a-5cc0c1eb1768"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("b8e49c2e-9373-4573-8e2b-cc919c5296f3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("c49f782f-d83a-4bed-bef7-d2ee808dbe20"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e6e3d2d0-e244-47d2-9475-5989e9134af0"));

            migrationBuilder.DropColumn(
                name: "ListaImagensJson",
                table: "CapitulosNovel");

            migrationBuilder.RenameColumn(
                name: "ListaImagensJson",
                table: "CapitulosComic",
                newName: "ListaImagens");

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9788), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9887), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9787) });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9608), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9768), new DateTime(2024, 4, 12, 10, 39, 34, 43, DateTimeKind.Local).AddTicks(9605) });

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
    }
}
