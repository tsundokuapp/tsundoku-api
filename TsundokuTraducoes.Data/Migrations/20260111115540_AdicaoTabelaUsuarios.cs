using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TsundokuTraducoes.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoTabelaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdTsun = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Avatar = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CapitulosComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(4090), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(4089) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3407), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3407) });

            migrationBuilder.UpdateData(
                table: "CapitulosNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3739), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3738) });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "Id",
                keyValue: new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3062), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3132), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3061) });

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("64329027-9111-418c-a6ff-842689916083"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2649), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2649) });

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2614), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2610) });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Descricao", "Slug", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { new Guid("00693b2a-c514-4da7-a6ee-10485ab3b07b"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2671), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2670), "Isekai", "isekai", "Admin", "Admin" },
                    { new Guid("1c2c6ae2-e901-4d65-b9d6-2b23ec61bfff"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2660), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2659), "Ação", "acao", "Admin", "Admin" },
                    { new Guid("301dbc3c-8c5a-46b4-80a8-86031b075d91"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2665), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2664), "Drama", "drama", "Admin", "Admin" },
                    { new Guid("93e32f53-054f-4138-8804-76e93daf57eb"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2675), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2675), "Horror", "horror", "Admin", "Admin" },
                    { new Guid("95c6cb7b-1e66-4765-b4e6-d8f1eecc1a0e"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2668), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2667), "Slice of Life", "slice-of-life", "Admin", "Admin" },
                    { new Guid("e1ab71cd-237f-4d33-bb59-7b88b924df36"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2663), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2662), "Comédia", "comedia", "Admin", "Admin" },
                    { new Guid("e560ef46-a688-408a-896a-0f7b717faa9d"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2673), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2672), "Harém", "harem", "Admin", "Admin" },
                    { new Guid("f9bd09d6-843b-4b92-8c0b-a99825e50032"), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2683), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2683), "Fantasia", "fantasia", "Admin", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Novels",
                keyColumn: "Id",
                keyValue: new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                columns: new[] { "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2858), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3033), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(2857) });

            migrationBuilder.UpdateData(
                table: "VolumesComic",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3295), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3294) });

            migrationBuilder.UpdateData(
                table: "VolumesNovel",
                keyColumn: "Id",
                keyValue: new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3160), new DateTime(2026, 1, 11, 8, 55, 39, 773, DateTimeKind.Local).AddTicks(3159) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("00693b2a-c514-4da7-a6ee-10485ab3b07b"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("1c2c6ae2-e901-4d65-b9d6-2b23ec61bfff"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("301dbc3c-8c5a-46b4-80a8-86031b075d91"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("93e32f53-054f-4138-8804-76e93daf57eb"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("95c6cb7b-1e66-4765-b4e6-d8f1eecc1a0e"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e1ab71cd-237f-4d33-bb59-7b88b924df36"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e560ef46-a688-408a-896a-0f7b717faa9d"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("f9bd09d6-843b-4b92-8c0b-a99825e50032"));

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
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2419), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2418) });

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                columns: new[] { "DataAlteracao", "DataInclusao" },
                values: new object[] { new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2415), new DateTime(2025, 4, 5, 15, 26, 19, 354, DateTimeKind.Local).AddTicks(2411) });

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
    }
}
