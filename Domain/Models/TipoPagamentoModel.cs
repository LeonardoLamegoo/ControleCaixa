using Domain.Enums;

namespace Domain.Models
{
    public class TipoPagamentoModel
    {
        public PagamentoType Tipo { get; private set; }

        public TipoPagamentoModel(PagamentoType tipoPagamento)
        {
            Tipo = tipoPagamento;
        }

        public TipoPagamentoModel(string tipoPagamento)
        {
            ValidaTipo(tipoPagamento);
        }

        private void ValidaTipo(string tipoPagamento) 
        {
            if (int.TryParse(tipoPagamento, out int result))
            {
                if (Enum.IsDefined(typeof(PagamentoType), result))
                {
                    Tipo = (PagamentoType)result;
                }
                else
                {
                    throw new ArgumentException("O Tipo de pagamento é inválido");
                }
            }
            else
            {
                throw new ArgumentException("O Tipo de pagamento deverá ser um número inteiro");
            }
        }
    }
}
