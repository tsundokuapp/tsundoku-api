using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TsundokuTraducoes.Api.Migrations
{
    public partial class CriacaoBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TituloAlternativo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alias = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Autor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Artista = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ano = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Visualizacoes = table.Column<int>(type: "int", nullable: false),
                    UsuarioInclusao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioAlteracao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemCapaPrincipal = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sinopse = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EhObraMaiorIdade = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EhRecomendacao = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CodigoCorHexaObra = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemBanner = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoObraDiscord = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiretorioImagemObra = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusObraSlug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoObraSlug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NacionalidadeSlug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemCapaUltimoVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroUltimoVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SlugUltimoVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroUltimoCapitulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SlugUltimoCapitulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataAtualizacaoUltimoCapitulo = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comics", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Novels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TituloAlternativo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alias = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Autor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Artista = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ano = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Visualizacoes = table.Column<int>(type: "int", nullable: false),
                    UsuarioInclusao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioAlteracao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemCapaPrincipal = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sinopse = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EhObraMaiorIdade = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EhRecomendacao = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CodigoCorHexaObra = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemBanner = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoObraDiscord = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiretorioImagemObra = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusObraSlug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoObraSlug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NacionalidadeSlug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemCapaUltimoVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroUltimoVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SlugUltimoVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroUltimoCapitulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SlugUltimoCapitulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataAtualizacaoUltimoCapitulo = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VolumesComic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sinopse = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioInclusao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioAlteracao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DiretorioImagemVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ComicId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumesComic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumesComic_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GenerosComic",
                columns: table => new
                {
                    ComicId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GeneroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerosComic", x => new { x.ComicId, x.GeneroId });
                    table.ForeignKey(
                        name: "FK_GenerosComic_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenerosComic_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GenerosNovel",
                columns: table => new
                {
                    NovelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GeneroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerosNovel", x => new { x.NovelId, x.GeneroId });
                    table.ForeignKey(
                        name: "FK_GenerosNovel_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenerosNovel_Novels_NovelId",
                        column: x => x.NovelId,
                        principalTable: "Novels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VolumesNovel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sinopse = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioInclusao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioAlteracao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DiretorioImagemVolume = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NovelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumesNovel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumesNovel_Novels_NovelId",
                        column: x => x.NovelId,
                        principalTable: "Novels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CapitulosComic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Numero = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Parte = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ListaImagens = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioInclusao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioAlteracao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DiretorioImagemCapitulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrdemCapitulo = table.Column<int>(type: "int", nullable: false),
                    VolumeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapitulosComic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapitulosComic_VolumesComic_VolumeId",
                        column: x => x.VolumeId,
                        principalTable: "VolumesComic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CapitulosNovel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Numero = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Parte = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConteudoNovel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioInclusao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioAlteracao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DiretorioImagemCapitulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrdemCapitulo = table.Column<int>(type: "int", nullable: false),
                    EhIlustracoesNovel = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    VolumeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tradutor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Revisor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapitulosNovel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapitulosNovel_VolumesNovel_VolumeId",
                        column: x => x.VolumeId,
                        principalTable: "VolumesNovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Comics",
                columns: new[] { "Id", "Alias", "Ano", "Artista", "Autor", "CargoObraDiscord", "CodigoCorHexaObra", "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "DiretorioImagemObra", "EhObraMaiorIdade", "EhRecomendacao", "ImagemBanner", "ImagemCapaPrincipal", "ImagemCapaUltimoVolume", "NacionalidadeSlug", "NumeroUltimoCapitulo", "NumeroUltimoVolume", "Sinopse", "Slug", "SlugUltimoCapitulo", "SlugUltimoVolume", "StatusObraSlug", "TipoObraSlug", "Titulo", "TituloAlternativo", "UsuarioAlteracao", "UsuarioInclusao", "Visualizacoes" },
                values: new object[] { new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"), "Hatsukoi Losstime", "2019", "Nanora & Zerokich", "Nishina Yuuki", "@Hatsukoi Losstime", "#01DFD7", new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(47), null, new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(46), "C:\\Users\\edsra\\source\\repos\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime", false, false, "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg", "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg", "", "japonesa", "", "", "Em um mundo onde apenas duas pessoas se moviam...", "hatsukoi-losstime", "", "", "em-andamento", "comic", "Hatsukoi Losstime", "初恋ロスタイム", "", "Bravo", 0 });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Descricao", "Slug" },
                values: new object[,]
                {
                    { new Guid("26bbd17d-588b-408e-b6ea-80001d1626a3"), "Harém", "harem" },
                    { new Guid("35bf70fe-adc0-406b-a57b-cd10e754589c"), "Horror", "horror" },
                    { new Guid("47aab5c7-ca3f-4498-9ea4-0fc876ba5057"), "Ação", "acao" },
                    { new Guid("63785531-7128-425b-9943-08abf1737161"), "Isekai", "isekai" },
                    { new Guid("64329027-9111-418c-a6ff-842689916083"), "Seinen", "seinen" },
                    { new Guid("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), "Aventura", "aventura" },
                    { new Guid("acedd822-00fc-4d22-a1cf-9cf2111b78d9"), "Drama", "drama" },
                    { new Guid("db6059d6-1da0-4cb1-91b1-906d2b9a2c7d"), "Fantasia", "fantasia" },
                    { new Guid("f063d184-91cb-4900-9c2c-688ceaac6eb0"), "Comédia", "comedia" },
                    { new Guid("f1b50f58-e27f-4802-9b7c-d02592218d19"), "Slice of Life", "slice-of-life" }
                });

            migrationBuilder.InsertData(
                table: "Novels",
                columns: new[] { "Id", "Alias", "Ano", "Artista", "Autor", "CargoObraDiscord", "CodigoCorHexaObra", "DataAlteracao", "DataAtualizacaoUltimoCapitulo", "DataInclusao", "DiretorioImagemObra", "EhObraMaiorIdade", "EhRecomendacao", "ImagemBanner", "ImagemCapaPrincipal", "ImagemCapaUltimoVolume", "NacionalidadeSlug", "NumeroUltimoCapitulo", "NumeroUltimoVolume", "Sinopse", "Slug", "SlugUltimoCapitulo", "SlugUltimoVolume", "StatusObraSlug", "TipoObraSlug", "Titulo", "TituloAlternativo", "UsuarioAlteracao", "UsuarioInclusao", "Visualizacoes" },
                values: new object[] { new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"), "Bruxa Errante", "2017", "Azure", "Shiraishi Jougi", "@Bruxa Errante, a Jornada de Elaina", "#81F7F3", new DateTime(2023, 8, 30, 16, 38, 54, 899, DateTimeKind.Local).AddTicks(8607), null, new DateTime(2023, 8, 30, 16, 38, 54, 899, DateTimeKind.Local).AddTicks(8606), "C:\\Users\\edsra\\source\\repos\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante", false, false, "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg", "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg", "", "japonesa", "", "", "A Bruxa, Sim, eu.", "bruxa-errante-a-jornada-de-elaina", "", "", "em-andamento", "light-novel", "Bruxa Errante, a Jornada de Elaina", "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々", "", "Bravo", 0 });

            migrationBuilder.InsertData(
                table: "GenerosComic",
                columns: new[] { "ComicId", "GeneroId" },
                values: new object[] { new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"), new Guid("64329027-9111-418c-a6ff-842689916083") });

            migrationBuilder.InsertData(
                table: "GenerosNovel",
                columns: new[] { "GeneroId", "NovelId" },
                values: new object[] { new Guid("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8") });

            migrationBuilder.InsertData(
                table: "VolumesComic",
                columns: new[] { "Id", "ComicId", "DataAlteracao", "DataInclusao", "DiretorioImagemVolume", "ImagemVolume", "Numero", "Sinopse", "Slug", "Titulo", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[] { new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50"), new Guid("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(1237), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(1236), "C:\\Users\\edsra\\source\\repos\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime\\Volume01", "https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg", "1", "", "volume-1", "", null, "Bravo" });

            migrationBuilder.InsertData(
                table: "VolumesNovel",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "DiretorioImagemVolume", "ImagemVolume", "NovelId", "Numero", "Sinopse", "Slug", "Titulo", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[] { new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a"), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(644), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(643), "C:\\Users\\edsra\\source\\repos\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01", "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg", new Guid("97722a6d-2210-434b-ae48-1a3c6da4c7a8"), "1", "", "volume-1", "", null, "Bravo" });

            migrationBuilder.InsertData(
                table: "CapitulosComic",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo", "ListaImagens", "Numero", "OrdemCapitulo", "Parte", "Slug", "Titulo", "UsuarioAlteracao", "UsuarioInclusao", "VolumeId" },
                values: new object[] { new Guid("08dba6c0-f903-469b-866c-223f5ab45e56"), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(2495), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(2494), "C:\\Users\\edsra\\source\\repos\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\HatsukoiLosstime\\Volume01\\Capitulo01", "[{\\\"Id\\\": 1,\\\"Ordem\\\": 1,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/0-46.jpg\\\"},{\\\"Id\\\": 2,\\\"Ordem\\\": 2,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/0-47.jpg\\\"},{\\\"Id\\\": 3,\\\"Ordem\\\": 3,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/1-60.jpg\\\"},{\\\"Id\\\": 4,\\\"Ordem\\\": 4,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/2-60.jpg\\\"},{\\\"Id\\\": 5,\\\"Ordem\\\": 5,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/3-61.jpg\\\"},{\\\"Id\\\": 6,\\\"Ordem\\\": 6,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/4-61.jpg\\\"},{\\\"Id\\\": 7,\\\"Ordem\\\": 7,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/5-61.jpg\\\"},{\\\"Id\\\": 8,\\\"Ordem\\\": 8,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/6-61.jpg\\\"},{\\\"Id\\\": 9,\\\"Ordem\\\": 9,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/7-61.jpg\\\"},{\\\"Id\\\": 10,\\\"Ordem\\\": 10,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/8-61.jpg\\\"},{\\\"Id\\\": 10,\\\"Ordem\\\": 10,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/9-61.jpg\\\"},{\\\"Id\\\": 10,\\\"Ordem\\\": 10,\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2022/01/10-108.jpg\\\"}]", "1", 1, null, "capitulo-1", null, null, "Bravo", new Guid("08dba651-ec33-4964-8f67-eecd4cbaea50") });

            migrationBuilder.InsertData(
                table: "CapitulosNovel",
                columns: new[] { "Id", "ConteudoNovel", "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo", "EhIlustracoesNovel", "Numero", "OrdemCapitulo", "Parte", "QC", "Revisor", "Slug", "Titulo", "Tradutor", "UsuarioAlteracao", "UsuarioInclusao", "VolumeId" },
                values: new object[] { new Guid("08dba6b4-3619-4cc6-8857-0bbe53a6f670"), "[{\\\"Id\\\": 1,\\\"Ordem\\\": 1,\\\"Alt\\\" = \\\"Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg\\\"},{\\\"Id\\\": 2,\\\"Ordem\\\": 2,\\\"Alt\\\" = \\\"MJ_V1_ilust_01\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_01.jpg\\\"},{\\\"Id\\\": 3,\\\"Ordem\\\": 3,\\\"Alt\\\" = \\\"MJ_V1_ilust_02\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_02.jpg\\\"},{\\\"Id\\\": 4,\\\"Ordem\\\": 4,\\\"Alt\\\" = \\\"MJ_V1_ilust_03\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_03.jpg\\\"},{\\\"Id\\\": 5,\\\"Ordem\\\": 5,\\\"Alt\\\" = \\\"MJ_V1_ilust_04\\\",\\\"Url\\\": \\\"http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_04.jpg\\\"}]", new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(1833), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(1832), "C:\\Users\\edsra\\source\\repos\\tsundoku-api\\TsundokuTraducoes\\wwwroot\\assets\\images\\BruxaErrante\\Volume01\\Ilustracoes", true, "Ilustrações", 1, null, null, null, "ilustracoes", null, null, null, "Bravo", new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a") });

            migrationBuilder.InsertData(
                table: "CapitulosNovel",
                columns: new[] { "Id", "ConteudoNovel", "DataAlteracao", "DataInclusao", "DiretorioImagemCapitulo", "EhIlustracoesNovel", "Numero", "OrdemCapitulo", "Parte", "QC", "Revisor", "Slug", "Titulo", "Tradutor", "UsuarioAlteracao", "UsuarioInclusao", "VolumeId" },
                values: new object[] { new Guid("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"), "\r\n            <p>Era um país tranquilo, cercado por montanhas proibidas e escondido atrás de muros altos. Ninguém do mundo exterior poderia visitar.</p>\r\n            <p>Acima de uma face rochosa brilhando com o calor da luz do sol, uma única vassoura voava pelo ar quente. A pessoa que a pilotava era uma linda jovem. Ela usava um robe preto e um chapéu pontudo, e seus cabelos cinzentos voavam ao vento. Se alguém estivesse por perto, viraria-se para olhar, imaginando com um suspiro quem seria aquela beldade a voar...</p>\r\n            <p>Isso aí. Eu mesma.</p>\r\n            <p>Ah, era uma piada.</p>\r\n            <p>— Quase lá...</p>\r\n            <p>O muro alto parecia ter sido esculpido na própria montanha. Olhando um pouco para baixo, vi o portão e guiei minha vassoura na direção dele.</p>\r\n            <p>Com certeza foi trabalhoso, mas suponho que as pessoas que moravam aqui o haviam planejado dessa maneira – para impedir que as pessoas entrassem por engano. Afinal, não há como alguém caminhar por um lugar desses sem uma boa razão.</p>\r\n            <p>Desci da minha vassoura bem em frente ao portão. Um sentinela local, aparentemente conduzindo inspeções de imigração, aproximou-se de mim.</p>\r\n            <p>Depois de me olhar lentamente da cabeça aos pés e examinar o broche no meu peito, sorriu alegremente.</p>\r\n            <p>— Bem-vinda ao País dos Magos. Por aqui, Madame Bruxa.</p>\r\n            <p>— Hmm? Você não precisa testar se posso fazer magia ou não?</p>\r\n            <p>Ouvi dizer que quem visitava este país tinha que provar sua capacidade mágica antes de entrar; qualquer pessoa que não alcançasse um determinado nível teria seu acesso negado.</p>\r\n            <p>— Eu a vi voando. E, além disso, esse broche que está usando indica que é uma bruxa. Então, por favor, continue em frente.</p>\r\n            <p><em>Ah sim, é mesmo. Ser capaz de voar em uma vassoura é um dos pré-requisitos mínimos para a entrada. É claro que puderam ver minha aproximação lá da guarita. Que boba que fui!</em></p>\r\n            <p>Depois de me inclinar um pouco para o guarda, passei pelo portão enorme. Aqui estava o País dos Magos. Usuários iniciantes de magia, aprendizes e bruxas de pleno direito – desde que pudessem usar magia, estariam autorizados a entrar neste país curioso, enquanto todos os outros seriam impedidos.</p>\r\n            <p>Ao passar pelo imenso portão, duas placas estranhas, lado a lado, chamaram minha atenção. Olhei para elas um pouco confusa.</p>\r\n            <p>A primeira mostrava um mago montado em uma vassoura, com um círculo ao seu redor. Aquela ao lado mostrava a imagem de um soldado andando, com um triângulo em sua volta.</p>\r\n            <p><em>O que há com essas placas?</em></p>\r\n            <p>Eu soube a resposta assim que olhei para cima – acima do monte de casas de tijolos e sob o sol cintilante, magos de todos os tipos atravessavam o céu em todas as direções.</p>\r\n            <p><em>Entendo. Deve ser uma regra nos países em que permitem apenas a entrada de magos – quase todo mundo está voando em uma vassoura, por isso poucas pessoas escolhem andar.</em></p>\r\n            <p>Satisfeita com minha explicação para as placas, peguei minha vassoura e me sentei de lado. Com um impulso, levantei suavemente no ar em uma demonstração viva do desenho da placa.</p>\r\n            ", new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(2456), new DateTime(2023, 8, 30, 16, 38, 54, 900, DateTimeKind.Local).AddTicks(2455), null, true, "1", 2, null, null, null, "capitulo-1-pais-dos-magos", null, null, null, "Bravo", new Guid("08dba651-c8ee-460a-8b4a-56573c446d2a") });

            migrationBuilder.CreateIndex(
                name: "IX_CapitulosComic_VolumeId",
                table: "CapitulosComic",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitulosNovel_VolumeId",
                table: "CapitulosNovel",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_GenerosComic_GeneroId",
                table: "GenerosComic",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_GenerosNovel_GeneroId",
                table: "GenerosNovel",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumesComic_ComicId",
                table: "VolumesComic",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumesNovel_NovelId",
                table: "VolumesNovel",
                column: "NovelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapitulosComic");

            migrationBuilder.DropTable(
                name: "CapitulosNovel");

            migrationBuilder.DropTable(
                name: "GenerosComic");

            migrationBuilder.DropTable(
                name: "GenerosNovel");

            migrationBuilder.DropTable(
                name: "VolumesComic");

            migrationBuilder.DropTable(
                name: "VolumesNovel");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Comics");

            migrationBuilder.DropTable(
                name: "Novels");
        }
    }
}
