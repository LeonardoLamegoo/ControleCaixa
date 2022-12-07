using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.Strategy.DataInfo;
using Service.Strategy.Interfaces;

namespace Service.Services
{
    public class ControleCaixaService : IControleCaixaService
    {
        private readonly IControleCaixaRepository controleCaixaRepository;
        private readonly IRelatorioDataExportStrategy relatorioDataExport;

        public ControleCaixaService(IControleCaixaRepository controleCaixaRepository, IRelatorioDataExportStrategy relatorioDataExport)
        {
            this.controleCaixaRepository = controleCaixaRepository;
            this.relatorioDataExport = relatorioDataExport;
        }

        public async Task Lancamento(LancamentoModel[] lancamento)
        {
            await controleCaixaRepository.MakePersistenceLancamento(lancamento);
        }

        public async Task<FileModel> DownloadRelatorioDiario()
        {
            return await relatorioDataExport.ExportRelatorioDiario(new RelatorioDiarioDataInfo(controleCaixaRepository));
        }
    }
}
