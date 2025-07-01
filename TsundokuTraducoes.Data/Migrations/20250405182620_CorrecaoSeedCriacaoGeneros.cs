using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoSeedCriacaoGeneros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3835), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3834) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3146), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3145) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3464), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3463) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2879), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2949), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2879) });

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("64329027-9111-418c-a6ff-842689916083"),
                columns: new[] { "DataAlteracao", "DataInclusao", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2419), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2418), "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                columns: new[] { "DataAlteracao", "DataInclusao", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2415), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2411), "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("0118aa74-d626-4509-a4fd-2918e491103e"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2448), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2447), "Harém", "harem", "Admin", "Admin" },
                    { new Guid("40aea16e-6794-46a7-ad18-7349ce4032d1"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2428), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2428), "Ação", "acao", "Admin", "Admin" },
                    { new Guid("4ed5b920-6fa1-4823-8dd3-1bbb1210186d"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2450), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2449), "Horror", "horror", "Admin", "Admin" },
                    { new Guid("6f049637-8369-470c-a90b-f844f4f59318"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2439), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2439), "Drama", "drama", "Admin", "Admin" },
                    { new Guid("82f1df8b-d496-4b49-9b90-d04214ccbf55"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2445), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2445), "Isekai", "isekai", "Admin", "Admin" },
                    { new Guid("b7ab05d4-0e22-41d2-8d1f-f7e7ed363e71"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2443), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2442), "Slice of Life", "slice-of-life", "Admin", "Admin" },
                    { new Guid("c652491c-cca4-4421-a77e-3bd4be0bbbee"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2512), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2511), "Fantasia", "fantasia", "Admin", "Admin" },
                    { new Guid("dc03916d-be71-4f39-92df-fc61254f90a3"), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2436), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2436), "Comédia", "comedia", "Admin", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2682), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2851), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2681) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3062), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(3061) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2975), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2974) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("0118aa74-d626-4509-a4fd-2918e491103e"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("40aea16e-6794-46a7-ad18-7349ce4032d1"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("4ed5b920-6fa1-4823-8dd3-1bbb1210186d"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("6f049637-8369-470c-a90b-f844f4f59318"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("82f1df8b-d496-4b49-9b90-d04214ccbf55"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("b7ab05d4-0e22-41d2-8d1f-f7e7ed363e71"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("c652491c-cca4-4421-a77e-3bd4be0bbbee"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("dc03916d-be71-4f39-92df-fc61254f90a3"));

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

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("64329027-9111-418c-a6ff-842689916083"),
                columns: new[] { "DataAlteracao", "DataInclusao", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                columns: new[] { "DataAlteracao", "DataInclusao", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[] { null, null, null, null });

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
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9270), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9180), new DateTime(2025, 3, 26, 0, 3, 7, 966, DateTimeKind.Local).AddTicks(9180) });
        }
    }
}
