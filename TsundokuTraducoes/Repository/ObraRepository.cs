using Dapper;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Models;

namespace TsundokuTraducoes.Api.Repository
{
    public class ObraRepository : IObraRepository
    {
        private readonly TsundokuContext _context;
        private readonly IDbConnection _contextDapper;

        public ObraRepository(TsundokuContext context)
        {
            _context = context;
            _contextDapper = new TsundokuContextDapper().RetornaSqlConnetionDapper();
        }

        public void Adiciona<t>(t entity) where t : class
        {
            _context.Add(entity);
        }

        public void Atualiza<t>(t entity) where t : class
        {
            _context.Update(entity);
        }

        public void Exclui<t>(t entity) where t : class
        {
            _context.Remove(entity);
        }

        public bool AlteracoesSalvas()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Obra> RetornaListaObras()
        {
            var listaObras = new List<Obra>();
            _contextDapper.Query(RetornaQueryListaObra(),
                new[]
                {
                    typeof(Obra),
                    typeof(Volume),
                    typeof(CapituloNovel),
                    typeof(CapituloManga),
                    typeof(Genero)
                },
                (objects) =>
                {
                    var obra = (Obra)objects[0];
                    var volume = (Volume)objects[1];
                    var capitulo = (CapituloNovel)objects[2];
                    var urlImagensManga = (CapituloManga)objects[3];
                    var genero = (Genero)objects[4];

                    if (listaObras.SingleOrDefault(o => o.Id == obra.Id) == null)
                    {
                        listaObras.Add(obra);
                    }
                    else
                    {
                        obra = listaObras.SingleOrDefault(o => o.Id == obra.Id);
                    }
                    
                    AdicionaVolumesCapitulosObra(obra, volume, capitulo, urlImagensManga);

                    return obra;
                });

            return listaObras;
        }

        public Obra RetornaObraPorId(int obraId)
        {
            return RetornaListaObras().FirstOrDefault(f => f.Id == obraId);
        }

        public Obra AtualizaObra(ObraDTO obraDTO)
        {
            var obraEncontrada = _context.Obras.SingleOrDefault(p => p.Id == obraDTO.Id);
            CarregaListaGeneros(obraDTO, obraEncontrada, false);

            if (string.IsNullOrEmpty(obraDTO.EnderecoUrlCapa))
            {
                obraDTO.EnderecoUrlCapa = obraEncontrada.EnderecoUrlCapa;
            }

            _context.Entry(obraEncontrada).CurrentValues.SetValues(obraDTO);
            obraEncontrada.DataAlteracao = DateTime.Now;

            return obraEncontrada;
        }

        public void InsereGenerosObra(ObraDTO obraDTO)
        {
            foreach (var genero in obraDTO.ListaGeneros)
            {
                var param = new
                {
                    generoId = genero,
                    obraId = obraDTO.Id
                };

                var sql = "INSERT INTO GeneroObras VALUES(@generoId,@obraId);";

                _contextDapper.QueryAsync(sql, param);
            }
        }

        public void AdicionaVolumesCapitulosObra(Obra obra, Volume volume, CapituloNovel capitulo, CapituloManga urlImagensManga)
        {
            //if (volume != null)
            //{
            //    CarregaVolumes(obra.Volumes, volume);
            //    CarregaCapitulos(obra.Volumes, capitulo, urlImagensManga);
            //}
        }

        public void CarregaVolumes(List<Volume> volumes, Volume volume)
        {
            //if (volumes.SingleOrDefault(v => v.Id == volume.Id) == null)
            //{
            //    volume.Capitulos = new List<CapituloNovel>();
            //    volumes.Add(volume);
            //}
        }

        public void CarregaCapitulos(List<Volume> volumes, CapituloNovel capitulo, CapituloManga urlImagensManga)
        {
            //if (capitulo != null)
            //{
            //    foreach (var volumeObra in volumes)
            //    {
            //        if (volumeObra.Capitulos.SingleOrDefault(c => c.Id == capitulo.Id) == null)
            //        {
            //            capitulo.ListaImagensManga = new List<CapituloManga>();
            //            volumeObra.Capitulos.Add(capitulo);
            //            CarregaImagensManga(volumeObra.Capitulos, urlImagensManga);
            //        }
            //    }
            //}
        }

        public void CarregaImagensManga(List<CapituloNovel> capitulos, CapituloManga urlImagensManga = null)
        {
            //if (urlImagensManga != null)
            //{
            //    foreach (var capituloVolumeObra in capitulos)
            //    {
            //        if (capituloVolumeObra.ListaImagensManga.FirstOrDefault(l => l.Id == urlImagensManga.Id) == null)
            //        {
            //            capituloVolumeObra.ListaImagensManga.Add(urlImagensManga);
            //        }
            //    }
            //}
        }

        public string RetornaQueryListaObra()
        {
            var sql = @"SELECT O.*, V.*, C.*, UIM.*, G.* 
                          FROM Obras O
                          LEFT JOIN Volumes V on V.ObraId = O.Id 
                          LEFT JOIN Capitulos C on C.VolumeId = V.Id
                          LEFT JOIN ImagensManga UIM on UIM.CapituloId = C.Id
                         INNER JOIN GeneroObra GO on GO.ObrasId = O.Id
                         INNER JOIN Generos G on G.Id = GO.GenerosId
                         ORDER BY O.Titulo;";

            return sql;
        }

        public List<Genero> RetornaListaGeneros()
        {   
            return _contextDapper.Query<Genero>("SELECT * FROM Generos ORDER BY Id").ToList();
        }

        public void CarregaListaGeneros(ObraDTO obraDTO, Obra obraEncontrada, bool inclusao)
        {
            //if (inclusao)
            //{
            //    obraEncontrada.Generos = new List<Genero>();
            //}
            //else
            //{
            //    obraEncontrada.Generos.Clear();
            //}

            //var arrayGenero = obraDTO.ListaGeneros[0]?.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);

            //if (arrayGenero != null && arrayGenero.Length > 0)
            //{
            //    foreach (var genero in arrayGenero)
            //    {
            //        if (inclusao)
            //        {
            //            obraEncontrada.Generos.Add(new Genero
            //            {
            //                Id = RetornaListaGeneros().Find(f => f.Descricao == genero).Id,
            //            });
            //        }
            //        else
            //        {
            //            var generoEncontrado = _context.Generos.Single(s => s.Descricao == genero);
            //            obraEncontrada.Generos.Add(generoEncontrado);
            //        }
            //    }
            //}            
        }
    }
}
