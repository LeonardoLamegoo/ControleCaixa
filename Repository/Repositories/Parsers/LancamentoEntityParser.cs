using Domain.Enums;
using Domain.Models;
using Repository.Repositories.Entities;

namespace Repository.Repositories.Parsers
{
    internal class LancamentoEntityParser
    {
        internal static LancamentoModel ParserTo(LancamentoEntity entity)
        {
            return new LancamentoModel(
                            new ValorModel(entity.Valor),
                            new TipoPagamentoModel((PagamentoType)entity.TipoPagamento),
                            entity.Data);
        }
    }
}
