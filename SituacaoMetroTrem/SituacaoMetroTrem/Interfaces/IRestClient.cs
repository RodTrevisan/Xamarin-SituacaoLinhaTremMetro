using System.Collections.Generic;
using System.Threading.Tasks;

namespace SituacaoMetroTrem.Interfaces
{
    public interface IRestClient<T> where T : class
    {
        Task<IEnumerable<T>> HttpGetMethod(string url);
    }
}
