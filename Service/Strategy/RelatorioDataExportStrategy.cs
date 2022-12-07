using Domain.Exporters;
using Domain.Models;
using Domain.SearchParameters;
using Service.Strategy.DataInfo;
using Service.Strategy.Interfaces;

namespace Service.Strategy
{
    public class RelatorioDataExportStrategy : IRelatorioDataExportStrategy
    {
        private long QntLancamentos = 0;

        private const int DefaultStartIndex = 1;
        private const int DefaultTotalPerPages = 1000;

        private static readonly string[] CabecalhoExcel = new string[]
        {
            "VALOR","TIPO DE PAGAMENTO","DATA"
        };

        public async Task<FileModel> ExportRelatorioDiario(RelatorioDiarioDataInfo dataInfo)
        {
            using (var exporter = new ExcelExporter())
            {
                exporter.CreateDocument();
                await CreateRelatorioDiarioSheet(exporter, dataInfo);

                if (QntLancamentos == 0)
                {
                    return new FileModel(false);
                }

                var stream = exporter.EndDocument();
                var result = stream.ToArray();

                var data = DateTime.Now;

                var fileName = $@"RelatorioDiario-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";

                File.WriteAllBytes($"{Path.Combine(Path.GetTempPath(), fileName)}", result);

                return new FileModel(result, fileName);
            }
        }

        private async Task CreateRelatorioDiarioSheet(ExcelExporter excelExporter, RelatorioDiarioDataInfo dataInfo)
        {
            long totalItems;
            var lancamentos = new List<LancamentoModel>();
            var parameters = SearchParameters.CreateSearchParameters(DefaultStartIndex, DefaultTotalPerPages);

            do
            {
                var pageableResult = await dataInfo.ControleCaixaRepository.BuscaLancamentoDiario(parameters);
                totalItems = pageableResult.Total;

                if (totalItems == 0)
                {
                    return;
                }

                QntLancamentos = totalItems;

                lancamentos.AddRange(pageableResult.Lancamentos);
                parameters.NextPage();

            } while (parameters.StartIndex < totalItems);

            excelExporter.CreateSheet("Relatório");
            excelExporter.AddHeaderColumns(CabecalhoExcel);

            foreach (var item in lancamentos.ToArray())
            {
                excelExporter.AddRow();

                excelExporter.AddRowValue(item.Valor.Total);
                excelExporter.AddRowValue(item.TipoPagamento.Tipo.ToString());
                excelExporter.AddRowValue(item.Data.ToString("dd/MM/yyyy"));
            }
        }
    }
}
