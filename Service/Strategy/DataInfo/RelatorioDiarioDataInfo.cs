using Repository.Repositories.Interfaces;

namespace Service.Strategy.DataInfo
{
    public class RelatorioDiarioDataInfo
    {
        public IControleCaixaRepository ControleCaixaRepository { get; private set; }

        public RelatorioDiarioDataInfo(IControleCaixaRepository controleCaixaRepository)
        {
            ControleCaixaRepository = controleCaixaRepository;
        }
    }
}
