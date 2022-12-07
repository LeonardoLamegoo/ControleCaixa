using Dapper;
using Domain.Models;
using Domain.SearchParameters;
using Repository.Repositories.Entities;
using Repository.Repositories.Parsers;
using System.Data;

namespace Repository.Repositories.Daos
{
    public class ControleCaixaDAO
    {
        private readonly IDbConnection connection;

        public ControleCaixaDAO(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<LancamentoPageableResultModel> BuscaLancamentoDiario(SearchParameters parameters) 
        {
            var count = (await connection.QueryAsync<long>(
                $@"SELECT COUNT(1) FROM Lancamento WHERE
                      CAST(Data as Date) = CAST(GETDATE() as Date)")).SingleOrDefault();

            var items = (await connection.QueryAsync<LancamentoEntity>($@"
                   SELECT * FROM (
                       SELECT ROW_NUMBER() OVER (ORDER BY DataCriacao ASC) AS RowNum,
                           Id, Valor, TipoPagamento, Data, DataCriacao
                       FROM
                           Lancamento WITH(NOLOCK)) AS P
                    WHERE CAST(Data as Date) = CAST(GETDATE() as Date) AND 
                            RowNum >= @Start AND RowNum < @End ORDER BY RowNum",
             new 
             {
                 Start = parameters.StartIndex,
                 End = parameters.End
             }));

            if (items == null || !items.Any())
            {
                return new LancamentoPageableResultModel(count);
            }

            return new LancamentoPageableResultModel(count, items.Select(x => LancamentoEntityParser.ParserTo(x)).ToArray());
        }
    }
}
