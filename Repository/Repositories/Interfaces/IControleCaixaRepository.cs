using Domain.Models;
using Domain.SearchParameters;

namespace Repository.Repositories.Interfaces
{
    public interface IControleCaixaRepository
    {
        Task MakePersistenceLancamento(LancamentoModel[] lancamento);
        Task<LancamentoPageableResultModel> BuscaLancamentoDiario(SearchParameters parameters);
    }
}
