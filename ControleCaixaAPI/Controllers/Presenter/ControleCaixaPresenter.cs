using ControleCaixaAPI.Controllers.Presenter.Interfaces;
using ControleCaixaAPI.Controllers.Presenter.Parsers;
using ControleCaixaAPI.Controllers.ViewItems;
using Service.Services.Interfaces;

namespace ControleCaixaAPI.Controllers.Presenter
{
    public class ControleCaixaPresenter : IControleCaixaPresenter
    {
        private readonly IControleCaixaService controleCaixaService;

        public ControleCaixaPresenter(IControleCaixaService controleCaixaService)
        {
            this.controleCaixaService = controleCaixaService;
        }

        public async Task OnLancamento(LancamentoDTO[] dto)
        {
            await controleCaixaService.Lancamento(dto.Select(x => LancamentoDTOParser.ParserTo(x)).ToArray());
        }

        public async Task<FileDTO> OnDownloadRelatorioDiario()
        {
            var result = await controleCaixaService.DownloadRelatorioDiario();

            if (!result.IsCreated)
            {
                return new FileDTO() { IsCreated = result.IsCreated };
            }

            return new FileDTO()
            {
                File = result.MediaFIle,
                FileName = result.FileName,
                IsCreated = true
            };
        }
    }
}
