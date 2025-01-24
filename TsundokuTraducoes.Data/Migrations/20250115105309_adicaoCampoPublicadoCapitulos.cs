using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class adicaoCampoPublicadoCapitulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "Publicado",
                table: "CapitulosNovel",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Publicado",
                table: "CapitulosComic",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo", "Publicado" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2409), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2408), "H:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime\\Volume01\\Capitulo01", false });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo", "Publicado" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1750), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1749), "H:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01\\Ilustracoes", false });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo", "Publicado" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2048), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(2047), "H:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01\\Ilustracoes", false });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "DiretorioImagemObra" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1493), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1570), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1492), "H:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime" });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "DiretorioImagemObra" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1265), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1427), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1261), "H:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante" });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemVolume" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1674), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1674), "H:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime\\Volume01" });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemVolume" },
                values: new object[] { new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1595), new DateTime(2025, 1, 15, 7, 53, 8, 476, DateTimeKind.Local).AddTicks(1594), "H:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Publicado",
                table: "CapitulosNovel");

            migrationBuilder.DropColumn(
                name: "Publicado",
                table: "CapitulosComic");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(2233), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(2232), "G:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime\\Volume01\\Capitulo01" });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1539), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1539), "G:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01\\Ilustracoes" });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1877), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1876), "G:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01\\Ilustracoes" });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "DiretorioImagemObra" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1284), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1357), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1283), "G:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime" });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "DiretorioImagemObra" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1071), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1252), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1066), "G:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante" });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemVolume" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1467), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1467), "G:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime\\Volume01" });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao", "DiretorioImagemVolume" },
                values: new object[] { new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1386), new DateTime(2024, 10, 23, 14, 24, 1, 368, DateTimeKind.Local).AddTicks(1385), "G:\\Tsundoku\\BackEnd\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01" });
        }
    }
}
