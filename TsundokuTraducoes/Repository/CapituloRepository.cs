using Dapper;
using System;
using System.Data;
using System.Linq;
using TsundokuTraducoes.Api.Data;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Repository.Interfaces;
using System.Threading.Tasks;

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

        #region Comics

        public CapituloComic RetornaCapituloComicPorId(int capituloId)
        {
            return _context.CapituloManga.FirstOrDefault(f => f.Id == capituloId);
        }
        
        public CapituloComic AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _context.CapituloManga.SingleOrDefault(s => s.Id == capituloDTO.Id);

            _context.Entry(capituloEncontrado).CurrentValues.SetValues(capituloDTO);
            capituloEncontrado.DataAlteracao = DateTime.Now;

            return capituloEncontrado;
        }

        public void AtualizaObraPorCapituloComic(Obra obra, CapituloComic capitulo)
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

        public CapituloComic RetornaCapituloComicExistente(int idVolume, CapituloDTO capituloDTO)
        {
            var sql = @"SELECT * FROM CapituloManga 
                         WHERE VolumeId = @IdVolume
                           AND Slug = @Slug";

            var parametros = new { IdVolume = idVolume, Slug = capituloDTO.Slug };
            return _contextDapper.Query<CapituloComic>(sql, parametros).FirstOrDefault();
        }

        #endregion

        #region Novels

        public CapituloNovel RetornaCapituloNovelPorId(int capituloId)
        {
            return _context.CapituloNovel.FirstOrDefault(f => f.Id == capituloId);
        }
        
        public CapituloNovel AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _context.CapituloNovel.SingleOrDefault(s => s.Id == capituloDTO.Id);

            _context.Entry(capituloEncontrado).CurrentValues.SetValues(capituloDTO);
            capituloEncontrado.DataAlteracao = DateTime.Now;

            return capituloEncontrado;
        }

        public void AtualizaObraPorCapitulo(Obra obra, string descritivoCapitulo, string slug, DateTime dataInclusao)
        {
            var parametros = new
            {
                obra.Id,
                NumeroUltimoCapitulo = descritivoCapitulo,
                SlugUltimoCapitulo = slug,
                DataAtualizacaoUltimoCapitulo = dataInclusao
            };

            var sql = @"update Obra 
                           set NumeroUltimoCapitulo = @NumeroUltimoCapitulo, 
                               SlugUltimoCapitulo = @SlugUltimoCapitulo, 
                               DataAtualizacaoUltimoCapitulo = @DataAtualizacaoUltimoCapitulo 
                         where Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }

        public CapituloNovel RetornaCapituloNovelExistente(int idVolume, CapituloDTO capituloDTO)
        {
            var sql = @"SELECT * FROM CapituloNovel 
                         WHERE VolumeId = @IdVolume
                           AND Slug = @Slug";

            var parametros = new { IdVolume = idVolume, Slug = capituloDTO.Slug };
            return _contextDapper.Query<CapituloNovel>(sql, parametros).FirstOrDefault();
        }

        #endregion

        public List<CapituloDTO> RetornaListaCapitulos()
        {            
            return _contextDapper.Query<CapituloDTO>(RetornaQueryListaCapitulos()).ToList();            
        }

        public Volume RetornaVolumePorId(int volumeId)
        {
            return _volumeRepository.RetornaVolumePorId(volumeId);
        }

        public async Task<Obra> RetornaObraPorId(int obraId)
        {
            return await _obraRepository.RetornaObraPorId(obraId);
        }

        private string RetornaQueryListaCapitulos()
        {
            return @"SELECT CN.Id, CN.Titulo, CN.UsuarioCadastro, CN.DataInclusao, O.TipoObraSlug 
                       FROM CapituloNovel CN
                      INNER JOIN Volume V ON V.Id = CN.VolumeId
                      INNER JOIN Obra O ON O.Id = V.ObraId
                      UNION 
                     SELECT CM.Id, CM.Titulo, CM.UsuarioCadastro, CM.DataInclusao, O.TipoObraSlug 
                       FROM CapituloManga CM
                      INNER JOIN Volume V ON V.Id = CM.VolumeId
                      INNER JOIN Obra O ON O.Id = V.ObraId";
        }
    }
}
