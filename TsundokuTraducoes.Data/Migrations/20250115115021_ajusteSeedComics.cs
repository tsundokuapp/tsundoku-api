using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class ajusteSeedComics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("0a1eb72a-1fb1-425d-b57f-89ebd3fed6ae"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("30931e51-ead5-4a99-909e-c7ee25aa1859"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("37b05b44-f198-418a-9bc4-df0a9abd00ac"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("5a99a9af-b8b8-4515-909a-aad4f37f7122"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("7ae4f967-4f9d-4c57-997d-e6c122c00298"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("bf3f00f4-b386-4ca1-ae5e-63350a978918"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("c910335b-0ae7-48fd-afab-63c544197590"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("d4660b0d-c5bd-4c0f-8f40-5d4f1fcebac5"));

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5864), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5864) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5171), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5497), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5496) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "TipoObraSlug" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4907), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4983), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4906), "manga" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("1ca8e138-368a-4c6f-9352-a6387952e86d"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("75dc3e7b-be61-44c7-9b88-767d57aeaf61"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("81b27927-df65-48b4-9aca-b1a71bbc0cd1"), null, null, "Harém", "harem", null, null },
                    { new Guid("a863bbe1-2610-4788-8ef9-b93cc36f47a5"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("cd0a01ee-28f9-49bd-8eaf-133c8e9643c1"), null, null, "Ação", "acao", null, null },
                    { new Guid("db9d5cec-cf68-4a2a-abcd-3637bb7eb4ee"), null, null, "Drama", "drama", null, null },
                    { new Guid("dbcb6e45-558f-4473-8205-08a5ff435802"), null, null, "Horror", "horror", null, null },
                    { new Guid("e165e155-7502-4379-8207-8c805e499d9a"), null, null, "Isekai", "isekai", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4711), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4878), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4705) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5089), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5089) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5009), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(5008) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1ca8e138-368a-4c6f-9352-a6387952e86d"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("75dc3e7b-be61-44c7-9b88-767d57aeaf61"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("81b27927-df65-48b4-9aca-b1a71bbc0cd1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("a863bbe1-2610-4788-8ef9-b93cc36f47a5"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("cd0a01ee-28f9-49bd-8eaf-133c8e9643c1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("db9d5cec-cf68-4a2a-abcd-3637bb7eb4ee"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("dbcb6e45-558f-4473-8205-08a5ff435802"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e165e155-7502-4379-8207-8c805e499d9a"));

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2409), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2408) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1750), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1749) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2048), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2047) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "TipoObraSlug" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1493), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1570), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1492), "comic" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("0a1eb72a-1fb1-425d-b57f-89ebd3fed6ae"), null, null, "Harém", "harem", null, null },
                    { new Guid("30931e51-ead5-4a99-909e-c7ee25aa1859"), null, null, "Ação", "acao", null, null },
                    { new Guid("37b05b44-f198-418a-9bc4-df0a9abd00ac"), null, null, "Drama", "drama", null, null },
                    { new Guid("5a99a9af-b8b8-4515-909a-aad4f37f7122"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("7ae4f967-4f9d-4c57-997d-e6c122c00298"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("bf3f00f4-b386-4ca1-ae5e-63350a978918"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("c910335b-0ae7-48fd-afab-63c544197590"), null, null, "Horror", "horror", null, null },
                    { new Guid("d4660b0d-c5bd-4c0f-8f40-5d4f1fcebac5"), null, null, "Slice of Life", "slice-of-life", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1265), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1427), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1261) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1674), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1674) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1595), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1594) });
        }
    }
}
