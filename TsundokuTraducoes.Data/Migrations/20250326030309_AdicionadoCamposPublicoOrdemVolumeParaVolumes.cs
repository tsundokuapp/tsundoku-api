using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCamposPublicoOrdemVolumeParaVolumes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "OrdemVolume",
                table: "VolumesNovel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Publicado",
                table: "VolumesNovel",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrdemVolume",
                table: "VolumesComic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Publicado",
                table: "VolumesComic",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 967, DateTimeKind.Local).AddTicks(83), new DateTime(2025, 3, 26, 0, 3, 7, 967, DateTimeKind.Local).AddTicks(82) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9352), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9352) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9668), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9667) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9019), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9152), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9019) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("063216e7-fdc5-45f6-b497-f325ecff98e0"), null, null, "Ação", "acao", null, null },
                    { new Guid("2315bb4f-929c-47d4-9712-873a168fbb55"), null, null, "Fantasia", "fantasia", null, null },
                    { new Guid("2b273d16-ebbe-4bbc-b6df-0d69fbab68c3"), null, null, "Slice of Life", "slice-of-life", null, null },
                    { new Guid("3d208749-03ef-4dd5-81a9-fde7e1c16db1"), null, null, "Horror", "horror", null, null },
                    { new Guid("4f19e568-b1a5-460e-b560-e5839dabc128"), null, null, "Comédia", "comedia", null, null },
                    { new Guid("650236f4-34da-4dc4-8303-6fb55867004a"), null, null, "Drama", "drama", null, null },
                    { new Guid("74b81ceb-6207-4d9f-a878-859e954bc09e"), null, null, "Harém", "harem", null, null },
                    { new Guid("bb28228b-caae-4ef0-a5a5-aab244fa426a"), null, null, "Isekai", "isekai", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(8817), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(8990), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(8813) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao", "OrdemVolume", "Publicado" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9270), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9270), 0, false });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao", "OrdemVolume", "Publicado" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9180), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9180), 0, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("063216e7-fdc5-45f6-b497-f325ecff98e0"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2315bb4f-929c-47d4-9712-873a168fbb55"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2b273d16-ebbe-4bbc-b6df-0d69fbab68c3"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("3d208749-03ef-4dd5-81a9-fde7e1c16db1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("4f19e568-b1a5-460e-b560-e5839dabc128"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("650236f4-34da-4dc4-8303-6fb55867004a"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("74b81ceb-6207-4d9f-a878-859e954bc09e"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("bb28228b-caae-4ef0-a5a5-aab244fa426a"));

            migrationBuilder.DropColumn(
                name: "OrdemVolume",
                table: "VolumesNovel");

            migrationBuilder.DropColumn(
                name: "Publicado",
                table: "VolumesNovel");

            migrationBuilder.DropColumn(
                name: "OrdemVolume",
                table: "VolumesComic");

            migrationBuilder.DropColumn(
                name: "Publicado",
                table: "VolumesComic");

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
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5156), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5396), new DateTime(2025, 2, 14, 6, 56, 0, 726, DateTimeKind.Local).AddTicks(5148) });

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
    }
}
