using ControleCaixaAPI.Controllers.ViewItems;
using Domain.Models;
using Domain.Parsers;

namespace ControleCaixaAPI.Controllers.Presenter.Parsers
{
    internal class LancamentoDTOParser
    {
        internal static LancamentoModel ParserTo(LancamentoDTO dto) 
        {
            return new LancamentoModel(new ValorModel(dto.Valor), 
                                       new TipoPagamentoModel(dto.TipoPagamento),
                                       DateTimeParser.Parse(dto.Data));
        }
    }
}
