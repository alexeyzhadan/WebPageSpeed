using System.Collections.Generic;
using System.Threading.Tasks;
using WebPageSpeed.Models;

namespace WebPageSpeed.Services.ResponseTimeAnalysis.Interface
{
    public interface IResponseTimeAnalyzer
    {
        Task<List<AnalysisOfWebPage>> DoAnalysisOfWebSiteAsync(List<string> links);
    }
}