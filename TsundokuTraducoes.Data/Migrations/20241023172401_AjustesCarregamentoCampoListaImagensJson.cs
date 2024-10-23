using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesCarregamentoCampoListaImagensJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("0394625b-409d-4762-a072-0abb866709c3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1e6426d3-e49e-4826-8817-edaac68eb6d0"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("76c5c6e0-c98f-4596-8c8c-ba170210fd87"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("a15b147a-caf9-4920-a22c-03462e472338"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("a7f87b2c-ed13-4d62-9d3f-e684236410e3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("abe509a6-6e57-4fc2-ab88-ba51e8bf284c"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("b6567eae-444c-4eb8-bae4-d40f14c51dd2"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("d756a833-caf1-4ee9-b255-d0fa525fdd51"));

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(2233), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(2232) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "ConteudoNovel", "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { "", new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1539), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1539), "[{\"Id\":1,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg\",\"Alt\":\"Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01\",\"Ordem\":1},{\"Id\":2,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_01.jpg\",\"Alt\":\"MJ_V1_ilust_01\",\"Ordem\":2},{\"Id\":3,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_02.jpg\",\"Alt\":\"MJ_V1_ilust_02\",\"Ordem\":3},{\"Id\":4,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_03.jpg\",\"Alt\":\"MJ_V1_ilust_03\",\"Ordem\":4},{\"Id\":5,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_04.jpg\",\"Alt\":\"MJ_V1_ilust_04\",\"Ordem\":5}]" });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1877), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1876), "" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1284), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1357), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1283) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("034a4b82-0642-4d08-8f89-2104f76bc573"), null, null, "Ação", "acao", null, null },
                    { new Guid("1637be38-3cd5-4781-8c10-b2d5f8b63049"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("26282a96-d6ef-470b-8447-3b0545fdeafb"), null, null, "Harém", "harem", null, null },
                    { new Guid("9bc411f9-8541-47e3-a4f8-40cb58a92991"), null, null, "Horror", "horror", null, null },
                    { new Guid("c8130f08-8262-453b-9c4a-31779a0d91f7"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("e5dad33d-a812-4717-a787-af3e91b720ef"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("e68e796f-ad62-49ca-a41c-fbb0744a46d5"), null, null, "Drama", "drama", null, null },
                    { new Guid("fd06bfd1-3782-4cee-a428-926c7d20f102"), null, null, "Slice of Life", "slice-of-life", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1071), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1252), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1066) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1467), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1467) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1386), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1385) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("034a4b82-0642-4d08-8f89-2104f76bc573"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1637be38-3cd5-4781-8c10-b2d5f8b63049"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("26282a96-d6ef-470b-8447-3b0545fdeafb"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("9bc411f9-8541-47e3-a4f8-40cb58a92991"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("c8130f08-8262-453b-9c4a-31779a0d91f7"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e5dad33d-a812-4717-a787-af3e91b720ef"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e68e796f-ad62-49ca-a41c-fbb0744a46d5"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("fd06bfd1-3782-4cee-a428-926c7d20f102"));

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(5322), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(5321) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "ConteudoNovel", "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { "[{\"Id\":1,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg\",\"Alt\":\"Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01\",\"Ordem\":1},{\"Id\":2,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_01.jpg\",\"Alt\":\"MJ_V1_ilust_01\",\"Ordem\":2},{\"Id\":3,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_02.jpg\",\"Alt\":\"MJ_V1_ilust_02\",\"Ordem\":3},{\"Id\":4,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_03.jpg\",\"Alt\":\"MJ_V1_ilust_03\",\"Ordem\":4},{\"Id\":5,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_04.jpg\",\"Alt\":\"MJ_V1_ilust_04\",\"Ordem\":5}]", new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4841), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4840), null });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4940), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4939), null });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4351), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4424), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4351) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("0394625b-409d-4762-a072-0abb866709c3"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("1e6426d3-e49e-4826-8817-edaac68eb6d0"), null, null, "Horror", "horror", null, null },
                    { new Guid("76c5c6e0-c98f-4596-8c8c-ba170210fd87"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("a15b147a-caf9-4920-a22c-03462e472338"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("a7f87b2c-ed13-4d62-9d3f-e684236410e3"), null, null, "Harém", "harem", null, null },
                    { new Guid("abe509a6-6e57-4fc2-ab88-ba51e8bf284c"), null, null, "Ação", "acao", null, null },
                    { new Guid("b6567eae-444c-4eb8-bae4-d40f14c51dd2"), null, null, "Drama", "drama", null, null },
                    { new Guid("d756a833-caf1-4ee9-b255-d0fa525fdd51"), null, null, "Isekai", "isekai", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4132), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4321), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4128) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4533), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4533) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4450), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4449) });
        }
    }
}
