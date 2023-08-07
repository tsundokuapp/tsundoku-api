using Dapper;
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
    public class CapituloRepository : ICapituloRepository
    {
        private readonly TsundokuContext _context;
        private readonly IDbConnection _contextDapper;
        private readonly IObraRepository _obraRepository;
        private readonly IVolumeRepository _volumeRepository;

        public CapituloRepository(TsundokuContext context, IObraRepository obraRepository, IVolumeRepository volumeRepository)
        {
            _context = context;
            _obraRepository = obraRepository;
            _volumeRepository = volumeRepository;
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

        public List<CapituloNovel> RetornaListaCapitulos()
        {
            var listaCapitulos = new List<CapituloNovel>();
            _contextDapper.Query<CapituloNovel, CapituloManga, CapituloNovel>(RetornaQueryListaCapitulos(),
                (capitulo, urlImagensManga) =>
                {
                    if (listaCapitulos.SingleOrDefault(c => c.Id == capitulo.Id) == null)
                    {
                        listaCapitulos.Add(capitulo);
                    }
                    else
                    {
                        capitulo = listaCapitulos.SingleOrDefault(o => o.Id == capitulo.Id);
                    }

                    _obraRepository.CarregaImagensManga(listaCapitulos, urlImagensManga);
                    return capitulo;
                });

            return listaCapitulos;
        }

        public CapituloNovel RetornaCapituloPorId(int capituloId)
        {
            return RetornaListaCapitulos().FirstOrDefault(f => f.Id == capituloId);
        }

        public void ExcluiTabelaUrlImagensManga(CapituloNovel capitulo)
        {
            //foreach (var urlImagensManga in capitulo.ListaImagensManga)
            //{
            //    _context.Remove(urlImagensManga);
            //}
        }

        public CapituloNovel AtualizaCapitulo(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _context.CapitulosNovel.SingleOrDefault(s => s.Id == capituloDTO.Id);

            _context.Entry(capituloEncontrado).CurrentValues.SetValues(capituloDTO);
            capituloEncontrado.DataAlteracao = DateTime.Now;

            return capituloEncontrado;
        }

        private string RetornaQueryListaCapitulos()
        {
            return @"SELECT C.*, UIM.*
                       FROM Capitulos C
                       LEFT JOIN UrlsImagensManga UIM ON UIM.CapituloId = C.Id
                      ORDER BY C.Id, UIM.Id ";
        }

        public Volume RetornaVolumePorId(int volumeId)
        {
            return _volumeRepository.RetornaVolumePorId(volumeId);
        }

        public Obra RetornaObraPorId(int obraId)
        {
            return _obraRepository.RetornaObraPorId(obraId);
        }

        public void AtualizaObraPorCapitulo(Obra obra, CapituloNovel capitulo)
        {
            var parametros = new
            {
                obra.Id,
                NumeroUltimoCapitulo = capitulo.DescritivoCapitulo,
                SlugUltimoCapitulo = capitulo.Slug,
                DataAtualizacaoUltimoCapitulo = capitulo.DataInclusao
            };

            var sql = @"update Obras 
                           set NumeroUltimoCapitulo = @NumeroUltimoCapitulo, 
                               SlugUltimoCapitulo = @SlugUltimoCapitulo, 
                               DataAtualizacaoUltimoCapitulo = @DataAtualizacaoUltimoCapitulo 
                         where Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }
    }
}
