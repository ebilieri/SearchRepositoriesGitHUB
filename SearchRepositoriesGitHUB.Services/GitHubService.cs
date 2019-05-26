using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubRepository _gitHubRepository;

        public GitHubService(IGitHubRepository itemsRepository)
        {
            _gitHubRepository = itemsRepository;
        }


        public Repositorio Get(long id)
        {
            return _gitHubRepository.Get(id);
        }

        public async Task<int> GetCount(string filtro)
        {
           return await _gitHubRepository.GetCount(filtro);
        }

        public async Task<List<Repositorio>> GetPaginatedResult(string filtro, int currentPage, int pageSize = 10)
        {
            return await _gitHubRepository.GetPaginatedResult(filtro, currentPage, pageSize);
        }
        
        public void Salvar(Repositorio repositorio)
        {
            var itemExistente = Get(repositorio.Id);

            if (itemExistente == null)
            {
                if (string.IsNullOrWhiteSpace(repositorio.Description))
                    repositorio.Description = repositorio.Name;
                                
                _gitHubRepository.Salvar(repositorio);
            }
        }
    }
}
