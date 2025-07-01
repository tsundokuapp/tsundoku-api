using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class CampoVisualizacoesNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Visualizacoes",
                table: "Novels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Visualizacoes",
                table: "Comics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Visualizacoes" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2133), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2214), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2133), null });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Visualizacoes" },
                values: new object[] { new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(1920), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(2082), new DateTime(2025, 1, 17, 8, 22, 39, 420, DateTimeKind.Local).AddTicks(1916), null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Visualizacoes",
                table: "Novels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Visualizacoes",
                table: "Comics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Visualizacoes" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4907), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4983), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4906), 0 });

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "Visualizacoes" },
                values: new object[] { new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4711), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4878), new DateTime(2025, 1, 15, 8, 50, 21, 547, DateTimeKind.Local).AddTicks(4705), 0 });

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
    }
}
