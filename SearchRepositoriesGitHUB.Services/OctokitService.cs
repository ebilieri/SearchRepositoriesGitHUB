using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.Services
{
    public class OctokitService : IOctokitService
    {
        private readonly IOctokitRepository _octokitRepository;

        public OctokitService(IOctokitRepository itemsRepository)
        {
            _octokitRepository = itemsRepository;
        }


        public Repositorio Get(long id)
        {
            return _octokitRepository.Get(id);
        }

        public async Task<int> GetCount(string filtro)
        {
           return await _octokitRepository.GetCount(filtro);
        }

        public async Task<List<Repositorio>> GetPaginatedResult(string filtro, int currentPage, int pageSize = 10)
        {
            return await _octokitRepository.GetPaginatedResult(filtro, currentPage, pageSize);
        }

        
        public void Salvar(Repositorio item)
        {
            var itemExistente = Get(item.Id);

            if (itemExistente == null)
            {                                               
                _octokitRepository.Salvar(item);
            }
        }
    }
}
