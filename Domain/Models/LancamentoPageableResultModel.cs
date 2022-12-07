namespace Domain.Models
{
    public class LancamentoPageableResultModel
    {
        public LancamentoModel[] Lancamentos { get; private set; }
        public long Total { get; private set; }

        public LancamentoPageableResultModel(long total)
        {
            Total = total;
        }

        public LancamentoPageableResultModel(long total, LancamentoModel[] lancamentos)
        {
            Total = total;
            Lancamentos = lancamentos;
        }
    }
}
