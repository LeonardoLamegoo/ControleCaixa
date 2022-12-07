using Autofac;
using ControleCaixaAPI.Controllers.Presenter;
using ControleCaixaAPI.Controllers.Presenter.Interfaces;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services;
using Service.Services.Interfaces;
using Service.Strategy;
using Service.Strategy.Interfaces;

namespace ControleCaixaAPI.App_start
{
    public class AutofacConfig : Module
    {
        protected override void Load(ContainerBuilder builder) 
        {
            //Presenter
            builder.RegisterType<ControleCaixaPresenter>().As<IControleCaixaPresenter>();
            //Service
            builder.RegisterType<ControleCaixaService>().As<IControleCaixaService>();
            //Repository
            builder.RegisterType<ControleCaixaRepository>().As<IControleCaixaRepository>();
            //Strategy
            builder.RegisterType<RelatorioDataExportStrategy>().As<IRelatorioDataExportStrategy>();
        }
    }
}
