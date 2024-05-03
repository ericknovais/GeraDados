using GeraDados.DataModel.models;

namespace GeraDados.DataModel.repository;

public interface IAtivoRepository : IRepositoryBase<Ativo>
{
    List<Ativo> ObtemAtivosPorTipoDeAtivo(TipoDeAtivo? tipoDeAtivo);
}