using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IControleCaixaService
    {
        Task Lancamento(LancamentoModel[] lancamento);
        Task<FileModel> DownloadRelatorioDiario();
    }
}
