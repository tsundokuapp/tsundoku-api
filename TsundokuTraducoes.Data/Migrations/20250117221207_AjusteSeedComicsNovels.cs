using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteSeedComicsNovels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2145e079-a2ce-4543-a9bc-1b2b6a94ea16"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2abd2506-1b43-4e64-b9cc-e379dcc03fc1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("611e1ec5-1d76-4037-90ba-933c1dc7a648"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("8ec24110-2134-4b6b-8dbc-ff8963c4380b"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("996a22d6-6e34-4f1e-b2b2-ff97b143e140"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("d430cdfc-19fc-4329-9eae-f29bc4b01832"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("d90c302c-5301-4486-9ddc-6d7097624649"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("ee6a6f9d-d94a-4ffc-86bc-0e773e8d7843"));

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "StatusObra" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5592), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5694), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5592), "Em andamento" });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "StatusObra" },
                values: new object[] { new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5179), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5519), new DateTime(2025, 1, 17, 19, 12, 7, 306, DateTimeKind.Local).AddTicks(5163), "Em andamento" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(4095), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(4095) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3417), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3417) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3723), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3722) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "StatusObra" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3139), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3221), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3139), "Em Andamento" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("2145e079-a2ce-4543-a9bc-1b2b6a94ea16"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("2abd2506-1b43-4e64-b9cc-e379dcc03fc1"), null, null, "Drama", "drama", null, null },
                    { new Guid("611e1ec5-1d76-4037-90ba-933c1dc7a648"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("8ec24110-2134-4b6b-8dbc-ff8963c4380b"), null, null, "Ação", "acao", null, null },
                    { new Guid("996a22d6-6e34-4f1e-b2b2-ff97b143e140"), null, null, "Horror", "horror", null, null },
                    { new Guid("d430cdfc-19fc-4329-9eae-f29bc4b01832"), null, null, "Harém", "harem", null, null },
                    { new Guid("d90c302c-5301-4486-9ddc-6d7097624649"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("ee6a6f9d-d94a-4ffc-86bc-0e773e8d7843"), null, null, "Isekai", "isekai", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "StatusObra" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(2945), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3111), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(2941), "Em Andamento" });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3333), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3332) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3248), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3248) });
        }
    }
}
