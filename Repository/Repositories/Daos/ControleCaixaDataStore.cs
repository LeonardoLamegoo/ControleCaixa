using Domain.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Repository.Repositories.Daos
{
    public class ControleCaixaDataStore
    {
        private readonly IDbConnection connection;
        private readonly DataTable dataTable;

        public ControleCaixaDataStore(IDbConnection connection)
        {
            this.connection = connection;
            dataTable = CreateDataTable();
        }

        private DataTable CreateDataTable() 
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Guid));
            dataTable.Columns.Add("Valor", typeof(decimal));
            dataTable.Columns.Add("TipoPagamento", typeof(int));
            dataTable.Columns.Add("Data", typeof(DateTime));
            dataTable.Columns.Add("DataCriacao", typeof(DateTime));

            return dataTable;
        }

        internal async Task MakePersistenceLancamento(LancamentoModel[] lancamento) 
        {
            AddLancamento(lancamento);

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)connection))
            {
                connection.Open();
                bulkCopy.DestinationTableName = "Lancamento";
                await bulkCopy.WriteToServerAsync(dataTable);
            }

            dataTable.Rows.Clear();
        }

        private void AddLancamento(LancamentoModel[] lancamento) 
        {
            foreach (var item in lancamento)
            {
                dataTable.Rows.Add(new object[]
                {
                    Guid.NewGuid(),
                    item.Valor.Total,
                    (int)item.TipoPagamento.Tipo,
                    item.Data,
                    DateTime.Now
                });
            }
        }
    }
}
