namespace Domain.Models
{
    public class ValorModel
    {
        public decimal Total { get; private set; }

        public ValorModel(string valor)
        {
            ValidaValor(valor);
        }

        public ValorModel(decimal valor) 
        {
            Total = valor;
        }

        private void ValidaValor(string valor) 
        {
            if (decimal.TryParse(valor, out decimal result))
            {
                Total = result;
            }
            else
            {
                throw new ArgumentException("O Valor é inválido");
            }
        }
    }
}
