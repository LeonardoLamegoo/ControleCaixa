using Domain.Models;
using Service.Strategy.DataInfo;

namespace Service.Strategy.Interfaces
{
    public interface IRelatorioDataExportStrategy
    {
        Task<FileModel> ExportRelatorioDiario(RelatorioDiarioDataInfo dataInfo);
    }
}
