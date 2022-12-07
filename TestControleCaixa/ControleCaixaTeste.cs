using ControleCaixaAPI.Controllers;
using ControleCaixaAPI.Controllers.Presenter.Interfaces;
using ControleCaixaAPI.Controllers.ViewItems;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestControleCaixa
{
    public class ControleCaixaTeste
    {
        private Mock<IControleCaixaPresenter> mockPresenter = new Mock<IControleCaixaPresenter>();


        ControleCaixaController controller;

        public ControleCaixaTeste()
        {
            controller = new ControleCaixaController(mockPresenter.Object);
        }

        [Fact]
        public async Task Lancamento()
        {
            var parameters = new LancamentoDTO[] { new LancamentoDTO()
            {
                Valor = "59,55",
                TipoPagamento = "0",
                Data = "10/11/2022"
            }};

            // Act
            var result = await controller.Lancamento(parameters);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}