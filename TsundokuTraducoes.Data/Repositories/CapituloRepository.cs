using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Data.Repositories
{
    public class CapituloRepository : ICapituloRepository
    {
        private readonly ContextBase _context;

        public CapituloRepository(ContextBase context)
        {
            _context = context;
        }

        public List<CapituloNovel> RetornaListaCapitulosNovel(Guid? volumeId = null)
        {
            var listaCapitulosNovel = volumeId != null ? _context.CapitulosNovel.AsNoTracking().Where(w => w.VolumeId == volumeId.Value) : _context.CapitulosNovel;
            return listaCapitulosNovel.ToList();
        }
        
        public List<CapituloComic> RetornaListaCapitulosComic(Guid? volumeId = null)
        {
            var listaCapitulosComic = volumeId != null ? _context.CapitulosComic.AsNoTracking().Where(w => w.VolumeId == volumeId.Value) : _context.CapitulosComic;
            return listaCapitulosComic.ToList();
        }


        public CapituloNovel RetornaCapituloNovelPorId(Guid capituloId)
        {
            var capitulos = RetornaListaCapitulosNovel();
            return capitulos.FirstOrDefault(w => w.Id == capituloId);
        }
        
        public CapituloComic RetornaCapituloComicPorId(Guid capituloId)
        {
            var capitulos = RetornaListaCapitulosComic();
            return capitulos.FirstOrDefault(w => w.Id == capituloId);
        }
        

        public void AdicionaCapituloNovel(CapituloNovel capituloNovel)
        {
            _context.Add(capituloNovel);
        }
        
        public void AdicionaCapituloComic(CapituloComic capituloComic)
        {
            _context.Add(capituloComic);
        }


        public CapituloNovel AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _context.CapitulosNovel.SingleOrDefault(s => s.Id == capituloDTO.Id);

            capituloDTO.DiretorioImagemCapitulo = capituloEncontrado.DiretorioImagemCapitulo;
            _context.Entry(capituloEncontrado).CurrentValues.SetValues(capituloDTO);
            capituloEncontrado.DataAlteracao = DateTime.Now;

            return capituloEncontrado;
        }

        public CapituloComic AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _context.CapitulosComic.SingleOrDefault(s => s.Id == capituloDTO.Id);

            capituloDTO.DiretorioImagemCapitulo = capituloEncontrado.DiretorioImagemCapitulo;
            _context.Entry(capituloEncontrado).CurrentValues.SetValues(capituloDTO);
            capituloEncontrado.DataAlteracao = DateTime.Now;

            return capituloEncontrado;
        }

        
        public void ExcluiCapituloNovel(CapituloNovel capituloNovel)
        {
            _context.Remove(capituloNovel);
        }

        public void ExcluiCapituloComic(CapituloComic capituloComic)
        {
            _context.Remove(capituloComic);
        }


        public CapituloNovel RetornaCapituloNovelExistente(CapituloDTO capituloDTO)
        {
            var capituloExistente = _context.CapitulosNovel.AsNoTracking()
                                    .Where(w => w.VolumeId == capituloDTO.VolumeId && EF.Functions.Like(w.Slug, capituloDTO.Slug));

            return capituloExistente.FirstOrDefault();
        }

        public CapituloComic RetornaCapituloComicExistente(CapituloDTO capituloDTO)
        {
            var capituloExistente = _context.CapitulosComic.AsNoTracking()
                                    .Where(w => w.VolumeId == capituloDTO.VolumeId && EF.Functions.Like(w.Slug, capituloDTO.Slug));
            
            return capituloExistente.FirstOrDefault();
        }


        public async Task<bool> AlteracoesSalvas()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
