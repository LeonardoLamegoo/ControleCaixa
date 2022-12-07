using Dapper.Contrib.Extensions;

namespace Repository.Repositories.Entities
{
    [Table("Lancamento")] 
    public class LancamentoEntity
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public int TipoPagamento { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
