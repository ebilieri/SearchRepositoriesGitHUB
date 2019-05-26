using SearchRepositoriesGitHUB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.Repositories
{
    public interface IOctokitRepository
    {
        Repositorio Get(long id);

        void Salvar(Repositorio item);
       
        Task<List<Repositorio>> GetPaginatedResult(string filtro, int currentPage, int pageSize = 10);

        Task<int> GetCount(string filtro);
    }
}
