using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoCampoPublicadoNovel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("0fa9fda7-d81f-41a5-a722-b757707e8384"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("19e84c83-54bc-467e-ac71-5f55238515b3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("6f285d1e-2826-4d2f-8cfc-532ace1caee0"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("986f0c26-761a-4c11-8f4b-5bb87497a037"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("9ad141fe-9ba9-4783-98dc-2a0cec255e33"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("aa3a56e1-7132-4371-9a03-a08e8864f90f"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("cd9e6827-01c7-4f83-ac09-c3e5c29c603d"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("d3114b1e-0f99-4f1f-8b82-df6a0c091ea1"));

            migrationBuilder.AddColumn<bool>(
                name: "Publicado",
                table: "Novels",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 30, 23, 59, 35, 320, DateTimeKind.Local).AddTicks(715), new DateTime(2025, 1, 30, 23, 59, 35, 320, DateTimeKind.Local).AddTicks(715) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 30, 23, 59, 35, 320, DateTimeKind.Local).AddTicks(3), new DateTime(2025, 1, 30, 23, 59, 35, 320, DateTimeKind.Local).AddTicks(2) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 30, 23, 59, 35, 320, DateTimeKind.Local).AddTicks(327), new DateTime(2025, 1, 30, 23, 59, 35, 320, DateTimeKind.Local).AddTicks(326) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9724), new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9807), new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9724) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("09322623-237a-40de-85c3-12d83a5ca574"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("19bd58e2-f355-4753-8139-ded84895603c"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("23f4525a-ea5d-4e2b-b013-13e89b1b6292"), null, null, "Ação", "acao", null, null },
                    { new Guid("72879179-4f79-4deb-8a25-68fb4e3bd985"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("8262c668-3a92-4560-8711-d85228484b3a"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("bd7eb7be-2aba-4826-8fc6-e29c964ca0da"), null, null, "Horror", "horror", null, null },
                    { new Guid("eddf999e-c3e5-4d4d-80ff-2621b25b1335"), null, null, "Harém", "harem", null, null },
                    { new Guid("f9037e78-0100-4997-87af-ddd5e4043fdb"), null, null, "Drama", "drama", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Publicado" },
                values: new object[] { new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9517), new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9693), new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9513), true });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9918), new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9917) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9833), new DateTime(2025, 1, 30, 23, 59, 35, 319, DateTimeKind.Local).AddTicks(9832) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("09322623-237a-40de-85c3-12d83a5ca574"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("19bd58e2-f355-4753-8139-ded84895603c"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("23f4525a-ea5d-4e2b-b013-13e89b1b6292"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("72879179-4f79-4deb-8a25-68fb4e3bd985"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("8262c668-3a92-4560-8711-d85228484b3a"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("bd7eb7be-2aba-4826-8fc6-e29c964ca0da"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("eddf999e-c3e5-4d4d-80ff-2621b25b1335"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("f9037e78-0100-4997-87af-ddd5e4043fdb"));

            migrationBuilder.DropColumn(
                name: "Publicado",
                table: "Novels");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1967), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1966) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1222), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1221) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1560), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1559) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(931), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1015), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(930) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("0fa9fda7-d81f-41a5-a722-b757707e8384"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("19e84c83-54bc-467e-ac71-5f55238515b3"), null, null, "Ação", "acao", null, null },
                    { new Guid("6f285d1e-2826-4d2f-8cfc-532ace1caee0"), null, null, "Harém", "harem", null, null },
                    { new Guid("986f0c26-761a-4c11-8f4b-5bb87497a037"), null, null, "Drama", "drama", null, null },
                    { new Guid("9ad141fe-9ba9-4783-98dc-2a0cec255e33"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("aa3a56e1-7132-4371-9a03-a08e8864f90f"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("cd9e6827-01c7-4f83-ac09-c3e5c29c603d"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("d3114b1e-0f99-4f1f-8b82-df6a0c091ea1"), null, null, "Horror", "horror", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(720), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(898), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(716) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1133), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1132) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1044), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1044) });
        }
    }
}
