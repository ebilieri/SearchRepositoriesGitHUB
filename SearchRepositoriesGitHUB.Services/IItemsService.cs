using SearchRepositoriesGitHUB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.Services
{
    public interface IItemsService
    {
        Item Get(long id);

        void Salvar(Item item);        

        Task<List<Item>> GetPaginatedResult(string filtro, int currentPage, int pageSize = 10);

        Task<int> GetCount(string filtro);
    }
}
