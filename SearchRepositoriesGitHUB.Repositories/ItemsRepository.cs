using Microsoft.EntityFrameworkCore;
using SearchRepositoriesGitHUB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly SearchRepositoriesGitHUBDBContext _searchContext;

        public ItemsRepository(SearchRepositoriesGitHUBDBContext searchRepositoriesGitHUBDBContext)
        {
            _searchContext = searchRepositoriesGitHUBDBContext;
        }

        public Item Get(long id)
        {
            return _searchContext.Items.Where(s => s.IdGitHub == id).FirstOrDefault();
        }

        public async Task<int> GetCount(string filtro)
        {
            var data = await List(filtro);

            return data.Count;
        }

        public async Task<List<Item>> GetPaginatedResult(string filtro, int currentPage, int pageSize = 10)
        {
            var data = await List(filtro);

            return data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

               
        public void Salvar(Item item)
        {
            _searchContext.Items.Add(item);
            _searchContext.SaveChanges();
        }

        private async Task<IList<Item>> List(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return await _searchContext.Items.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
            }
            else
            {
                filtro = string.Format("%{0}%", filtro);

                return await (from s in _searchContext.Items
                              where EF.Functions.Like(s.Description, filtro)
                              || EF.Functions.Like(s.Name, filtro)
                              select s).ToListAsync();
            }
        }
    }
}
