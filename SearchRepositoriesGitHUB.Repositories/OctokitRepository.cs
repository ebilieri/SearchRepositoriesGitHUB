using Microsoft.EntityFrameworkCore;
using SearchRepositoriesGitHUB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.Repositories
{
    public class OctokitRepository : IOctokitRepository
    {
        private readonly SearchRepositoriesGitHUBDBContext _searchContext;

        public OctokitRepository(SearchRepositoriesGitHUBDBContext searchRepositoriesGitHUBDBContext)
        {
            _searchContext = searchRepositoriesGitHUBDBContext;
        }

        public Repositorio Get(long id)
        {
            return _searchContext.Repositorios.Where(s => s.Id == id).FirstOrDefault();
        }

        public async Task<int> GetCount(string filtro)
        {
            var data = await List(filtro);

            return data.Count;
        }

        public async Task<List<Repositorio>> GetPaginatedResult(string filtro, int currentPage, int pageSize = 10)
        {
            var data = await List(filtro);

            return data.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

               
        public void Salvar(Repositorio item)
        {
            _searchContext.Repositorios.Add(item);
            _searchContext.SaveChanges();
        }

        private async Task<IList<Repositorio>> List(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return await _searchContext.Repositorios.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
            }
            else
            {
                filtro = string.Format("%{0}%", filtro);

                return await (from s in _searchContext.Repositorios
                              where EF.Functions.Like(s.Description, filtro)
                              || EF.Functions.Like(s.Name, filtro)
                              select s).ToListAsync();
            }
        }
    }
}
