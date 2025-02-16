using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoCampoPublicadoNovelcs : Migration
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
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(6421), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(6421) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5709), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5709) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(6056), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(6055) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5430), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5511), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5429) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("28330e22-bd1c-489b-ace7-7e55e9b9573b"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("773e1482-4765-41ce-9b0b-8441de1677a0"), null, null, "Harém", "harem", null, null },
                    { new Guid("8a371dfa-2ca0-48ae-bb97-82213ae463ef"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("9b566314-f13c-42a6-a40c-ee11752424e3"), null, null, "Horror", "horror", null, null },
                    { new Guid("bbbbd7c3-0e04-4540-b039-0aa94f3d5ac6"), null, null, "Drama", "drama", null, null },
                    { new Guid("c501e19d-2f50-4e98-a8f1-54d9dd145474"), null, null, "Ação", "acao", null, null },
                    { new Guid("dda695de-b958-4009-84e8-987863d3046a"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("f516c212-8bd7-4755-86f0-dfd45ade654f"), null, null, "Fantasia", "fantasia", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Publicado" },
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5156), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5396), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5148), true });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5627), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5626) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5540), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5539) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("28330e22-bd1c-489b-ace7-7e55e9b9573b"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("773e1482-4765-41ce-9b0b-8441de1677a0"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("8a371dfa-2ca0-48ae-bb97-82213ae463ef"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("9b566314-f13c-42a6-a40c-ee11752424e3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("bbbbd7c3-0e04-4540-b039-0aa94f3d5ac6"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("c501e19d-2f50-4e98-a8f1-54d9dd145474"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("dda695de-b958-4009-84e8-987863d3046a"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("f516c212-8bd7-4755-86f0-dfd45ade654f"));

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
