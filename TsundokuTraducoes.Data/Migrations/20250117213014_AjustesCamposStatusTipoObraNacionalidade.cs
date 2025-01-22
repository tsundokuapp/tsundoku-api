using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesCamposStatusTipoObraNacionalidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("238ae3a7-cbe4-40ae-81af-84f2abca12a4"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("293bb58a-9b68-43d6-b6a4-2684cd4466d9"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2b4742c0-52a1-4448-a49d-445a8c6728a9"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2ba3656b-1229-4b28-945b-00cadbcaeca1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("4e99898a-4bc3-4392-b3d6-6fb72bbf74c4"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("959c4e1d-fed9-4596-a030-7852f89e7e99"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("a630987d-4330-48e6-8646-0a170bf4163f"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("bfb1ac25-0950-487a-90d3-131dc5b15ed5"));

            migrationBuilder.RenameColumn(
                name: "TipoObraSlug",
                table: "Novels",
                newName: "TipoObra");

            migrationBuilder.RenameColumn(
                name: "StatusObraSlug",
                table: "Novels",
                newName: "StatusObra");

            migrationBuilder.RenameColumn(
                name: "NacionalidadeSlug",
                table: "Novels",
                newName: "Nacionalidade");

            migrationBuilder.RenameColumn(
                name: "TipoObraSlug",
                table: "Comics",
                newName: "TipoObra");

            migrationBuilder.RenameColumn(
                name: "StatusObraSlug",
                table: "Comics",
                newName: "StatusObra");

            migrationBuilder.RenameColumn(
                name: "NacionalidadeSlug",
                table: "Comics",
                newName: "Nacionalidade");

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Nacionalidade", "StatusObra", "TipoObra" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3139), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3221), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3139), "Japonesa", "Em Andamento", "Mangá" });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Nacionalidade", "StatusObra", "TipoObra" },
                values: new object[] { new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(2945), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(3111), new DateTime(2025, 1, 17, 18, 30, 13, 818, DateTimeKind.Local).AddTicks(2941), "Japonesa", "Em Andamento", "Light Novel" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "TipoObra",
                table: "Novels",
                newName: "TipoObraSlug");

            migrationBuilder.RenameColumn(
                name: "StatusObra",
                table: "Novels",
                newName: "StatusObraSlug");

            migrationBuilder.RenameColumn(
                name: "Nacionalidade",
                table: "Novels",
                newName: "NacionalidadeSlug");

            migrationBuilder.RenameColumn(
                name: "TipoObra",
                table: "Comics",
                newName: "TipoObraSlug");

            migrationBuilder.RenameColumn(
                name: "StatusObra",
                table: "Comics",
                newName: "StatusObraSlug");

            migrationBuilder.RenameColumn(
                name: "Nacionalidade",
                table: "Comics",
                newName: "NacionalidadeSlug");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(3083), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(3082) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2400), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2399) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2717), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2716) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "NacionalidadeSlug", "StatusObraSlug", "TipoObraSlug" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2133), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2214), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2133), "japonesa", "em-andamento", "manga" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("238ae3a7-cbe4-40ae-81af-84f2abca12a4"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("293bb58a-9b68-43d6-b6a4-2684cd4466d9"), null, null, "Isekai", "isekai", null, null },
                    { new Guid("2b4742c0-52a1-4448-a49d-445a8c6728a9"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("2ba3656b-1229-4b28-945b-00cadbcaeca1"), null, null, "Horror", "horror", null, null },
                    { new Guid("4e99898a-4bc3-4392-b3d6-6fb72bbf74c4"), null, null, "Ação", "acao", null, null },
                    { new Guid("959c4e1d-fed9-4596-a030-7852f89e7e99"), null, null, "Drama", "drama", null, null },
                    { new Guid("a630987d-4330-48e6-8646-0a170bf4163f"), null, null, "Harém", "harem", null, null },
                    { new Guid("bfb1ac25-0950-487a-90d3-131dc5b15ed5"), null, null, "Fantasia", "fantasia", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "NacionalidadeSlug", "StatusObraSlug", "TipoObraSlug" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(1920), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2082), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(1916), "japonesa", "em-andamento", "light-novel" });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2322), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2321) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2240), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2240) });
        }
    }
}
