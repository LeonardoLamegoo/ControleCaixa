using Domain.Enums;

namespace Domain.Models
{
    public class LancamentoModel
    {
        public ValorModel Valor { get; private set; }
        public TipoPagamentoModel TipoPagamento { get; private set; }
        public DateTime Data { get; private set; }

        public LancamentoModel(ValorModel valor, TipoPagamentoModel tipoPagamento, DateTime data)
        {
            Valor = valor;
            TipoPagamento = tipoPagamento;
            Data = data;
        }
    }
}
