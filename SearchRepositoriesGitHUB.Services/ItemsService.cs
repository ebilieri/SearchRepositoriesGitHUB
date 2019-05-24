using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Repositories;
using System.Collections.Generic;

namespace SearchRepositoriesGitHUB.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsService(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }


        public Item Get(int id)
        {
            return _itemsRepository.Get(id);
        }

        public IList<Item> Listar()
        {
            return _itemsRepository.Listar();
        }

        public void Salvar(Item item)
        {
            _itemsRepository.Salvar(item);
        }
    }
}
