using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsService(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }


        public Item Get(long id)
        {
            return _itemsRepository.Get(id);
        }

        public async Task<int> GetCount(string filtro)
        {
           return await _itemsRepository.GetCount(filtro);
        }

        public async Task<List<Item>> GetPaginatedResult(string filtro, int currentPage, int pageSize = 10)
        {
            return await _itemsRepository.GetPaginatedResult(filtro, currentPage, pageSize);
        }

        
        public void Salvar(Item item)
        {
            var itemExistente = Get(item.Id);

            if (itemExistente == null)
            {
                if (!string.IsNullOrWhiteSpace(item.Description))
                    item.Description = "*";
                item.IdGitHub = item.Id;
                item.Id = 0;

                item.Owner.IdGitHub = item.Owner.Id;
                item.Owner.Id = 0;

                _itemsRepository.Salvar(item);
            }
        }
    }
}
