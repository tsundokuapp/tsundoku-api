using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesCampoListaImagensJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(5322), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(5321), "[{\"Id\":1,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/0-46.jpg\",\"Alt\":null,\"Ordem\":1},{\"Id\":2,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/0-47.jpg\",\"Alt\":null,\"Ordem\":2},{\"Id\":3,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/1-60.jpg\",\"Alt\":null,\"Ordem\":3},{\"Id\":4,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/2-60.jpg\",\"Alt\":null,\"Ordem\":4},{\"Id\":5,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/3-61.jpg\",\"Alt\":null,\"Ordem\":5},{\"Id\":6,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/4-61.jpg\",\"Alt\":null,\"Ordem\":6},{\"Id\":7,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/5-61.jpg\",\"Alt\":null,\"Ordem\":7},{\"Id\":8,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/6-61.jpg\",\"Alt\":null,\"Ordem\":8},{\"Id\":9,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/7-61.jpg\",\"Alt\":null,\"Ordem\":9},{\"Id\":10,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/8-61.jpg\",\"Alt\":null,\"Ordem\":10},{\"Id\":10,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/9-61.jpg\",\"Alt\":null,\"Ordem\":10},{\"Id\":10,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2022/01/10-108.jpg\",\"Alt\":null,\"Ordem\":10}]" });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "ConteudoNovel", "DataAlteracao", "DataInclusao" },
                values: new object[] { "[{\"Id\":1,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg\",\"Alt\":\"Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01\",\"Ordem\":1},{\"Id\":2,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_01.jpg\",\"Alt\":\"MJ_V1_ilust_01\",\"Ordem\":2},{\"Id\":3,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_02.jpg\",\"Alt\":\"MJ_V1_ilust_02\",\"Ordem\":3},{\"Id\":4,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_03.jpg\",\"Alt\":\"MJ_V1_ilust_03\",\"Ordem\":4},{\"Id\":5,\"Url\":\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_04.jpg\",\"Alt\":\"MJ_V1_ilust_04\",\"Ordem\":5}]", new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4841), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4940), new DateTime(2024, 10, 23, 14, 13, 14, 850, DateTimeKind.Local).AddTicks(4939) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "DataAlteracao", "DataInclusao", "ListaImagensJson" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8756), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8756), "[{\\\"Id\\\": 1,\\\"Ordem\\\": 1,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/0-46.jpg\\\"},{\\\"Id\\\": 2,\\\"Ordem\\\": 2,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/0-47.jpg\\\"},{\\\"Id\\\": 3,\\\"Ordem\\\": 3,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/1-60.jpg\\\"},{\\\"Id\\\": 4,\\\"Ordem\\\": 4,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/2-60.jpg\\\"},{\\\"Id\\\": 5,\\\"Ordem\\\": 5,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/3-61.jpg\\\"},{\\\"Id\\\": 6,\\\"Ordem\\\": 6,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/4-61.jpg\\\"},{\\\"Id\\\": 7,\\\"Ordem\\\": 7,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/5-61.jpg\\\"},{\\\"Id\\\": 8,\\\"Ordem\\\": 8,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/6-61.jpg\\\"},{\\\"Id\\\": 9,\\\"Ordem\\\": 9,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/7-61.jpg\\\"},{\\\"Id\\\": 10,\\\"Ordem\\\": 10,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/8-61.jpg\\\"},{\\\"Id\\\": 10,\\\"Ordem\\\": 10,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/9-61.jpg\\\"},{\\\"Id\\\": 10,\\\"Ordem\\\": 10,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/10-108.jpg\\\"}]" });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "ConteudoNovel", "DataAlteracao", "DataInclusao" },
                values: new object[] { "[{\\\"Id\\\": 1,\\\"Ordem\\\": 1,\\\"Alt\\\" = \\\"Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg\\\"},{\\\"Id\\\": 2,\\\"Ordem\\\": 2,\\\"Alt\\\" = \\\"MJ_V1_ilust_01\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_01.jpg\\\"},{\\\"Id\\\": 3,\\\"Ordem\\\": 3,\\\"Alt\\\" = \\\"MJ_V1_ilust_02\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_02.jpg\\\"},{\\\"Id\\\": 4,\\\"Ordem\\\": 4,\\\"Alt\\\" = \\\"MJ_V1_ilust_03\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_03.jpg\\\"},{\\\"Id\\\": 5,\\\"Ordem\\\": 5,\\\"Alt\\\" = \\\"MJ_V1_ilust_04\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_04.jpg\\\"}]", new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8609), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8608) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8681), new DateTime(2024, 10, 23, 13, 25, 37, 628, DateTimeKind.Local).AddTicks(8681) });

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
    }
}
