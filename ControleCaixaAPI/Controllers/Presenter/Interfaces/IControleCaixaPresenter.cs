using ControleCaixaAPI.Controllers.ViewItems;

namespace ControleCaixaAPI.Controllers.Presenter.Interfaces
{
    public interface IControleCaixaPresenter
    {
        Task OnLancamento(LancamentoDTO[] dto);
        Task<FileDTO> OnDownloadRelatorioDiario();
    }
}
