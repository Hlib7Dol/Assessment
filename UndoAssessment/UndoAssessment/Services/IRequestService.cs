using System.Threading;
using System.Threading.Tasks;

namespace UndoAssessment.Services
{
    public interface IRequestService
    {
        Task<T> Get<T>(string url, CancellationToken cancellationToken = default); 
    }
}
