using Domain.Models;
using Domain.SearchParameters;
using Repository.Context;
using Repository.Repositories.Daos;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ControleCaixaRepository : IControleCaixaRepository
    {
        private readonly DapperContext context;

        public ControleCaixaRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task MakePersistenceLancamento(LancamentoModel[] lancamento)
        {
            using (var connectionContext = context.CreateConnection())
            {
                var controleCaixaDataStore = new ControleCaixaDataStore(connectionContext);
                await controleCaixaDataStore.MakePersistenceLancamento(lancamento);
            }
        }

        public async Task<LancamentoPageableResultModel> BuscaLancamentoDiario(SearchParameters parameters)
        {
            using (var connectionContext = context.CreateConnection())
            {
                var controleCaixaDAO = new ControleCaixaDAO(connectionContext);
                return await controleCaixaDAO.BuscaLancamentoDiario(parameters);
            }
        }
    }
}
