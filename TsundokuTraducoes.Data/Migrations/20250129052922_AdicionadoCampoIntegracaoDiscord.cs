using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCampoIntegracaoDiscord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("262f3806-0ed9-4fd7-8cd5-c69a931ba8b8"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("54bedec0-9122-4ed7-9325-6fc35ba7ae65"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("8194a6d9-c221-4156-95da-1fa437f254dc"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("85b2479c-a75e-4be3-9c80-f3596bd6ffda"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("87c236a7-d87d-42c2-8d0b-ee64fc5428d1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("8fa6790f-0a7b-4ee1-91d6-5bfc4aaa1836"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("cc8a649c-18f9-4147-b67c-a94444b591a8"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("eee15f56-25ff-4cb3-974b-7d050afe6cef"));

            migrationBuilder.AddColumn<bool>(
                name: "IntegracaoDiscord",
                table: "Novels",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IntegracaoDiscord",
                table: "Comics",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "IntegracaoDiscord" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(931), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(1015), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(930), false });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "IntegracaoDiscord" },
                values: new object[] { new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(720), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(898), new DateTime(2025, 1, 29, 2, 29, 21, 547, DateTimeKind.Local).AddTicks(716), false });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IntegracaoDiscord",
                table: "Novels");

            migrationBuilder.DropColumn(
                name: "IntegracaoDiscord",
                table: "Comics");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(6827), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5932), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5932) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(6421), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5592), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5694), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5592) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("262f3806-0ed9-4fd7-8cd5-c69a931ba8b8"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("54bedec0-9122-4ed7-9325-6fc35ba7ae65"), null, null, "Horror", "horror", null, null },
                    { new Guid("8194a6d9-c221-4156-95da-1fa437f254dc"), null, null, "Drama", "drama", null, null },
                    { new Guid("85b2479c-a75e-4be3-9c80-f3596bd6ffda"), null, null, "Ação", "acao", null, null },
                    { new Guid("87c236a7-d87d-42c2-8d0b-ee64fc5428d1"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("8fa6790f-0a7b-4ee1-91d6-5bfc4aaa1836"), null, null, "Harém", "harem", null, null },
                    { new Guid("cc8a649c-18f9-4147-b67c-a94444b591a8"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("eee15f56-25ff-4cb3-974b-7d050afe6cef"), null, null, "Isekai", "isekai", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5179), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5519), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5163) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5837), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5837) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5727), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5727) });
        }
    }
}
