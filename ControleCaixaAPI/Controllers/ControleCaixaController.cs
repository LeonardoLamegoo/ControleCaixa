using ControleCaixaAPI.Controllers.Presenter.Interfaces;
using ControleCaixaAPI.Controllers.ViewItems;
using ICSharpCode.SharpZipLib.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ControleCaixaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControleCaixaController : ControllerBase
    {
        private readonly IControleCaixaPresenter controleCaixaPresenter;

        public ControleCaixaController(IControleCaixaPresenter controleCaixaPresenter)
        {
            this.controleCaixaPresenter = controleCaixaPresenter;
        }

        [HttpPost]
        [Route("Lancamento")]
        public async Task<IActionResult> Lancamento(LancamentoDTO[] dto)
        {
            await controleCaixaPresenter.OnLancamento(dto);
            return Ok("Lançamento(s) adicionado");
        }

        [HttpGet]
        [Route("DownloadRelatorioDiario")]
        public async Task<IActionResult> DownloadRelatorioDiario() 
        {
            var file = await controleCaixaPresenter.OnDownloadRelatorioDiario();

            if (!file.IsCreated)
            {
                return Ok("Não foram encontrados registros");
            }

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(".xlsx", out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(file.File, contentType, file.FileName);
        }
    }
}