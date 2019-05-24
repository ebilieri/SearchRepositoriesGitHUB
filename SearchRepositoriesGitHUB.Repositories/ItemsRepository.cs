using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchRepositoriesGitHUB.Models;

namespace SearchRepositoriesGitHUB.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly SearchRepositoriesGitHUBDBContext _searchContext;

        public ItemsRepository(SearchRepositoriesGitHUBDBContext searchRepositoriesGitHUBDBContext)
        {
            _searchContext = searchRepositoriesGitHUBDBContext;
        }

        public Item Get(int id)
        {
            return _searchContext.Items.Find(id);
        }

        public IList<Item> Listar()
        {
            return _searchContext.Items.ToList();
        }

        public void Salvar(Item item)
        {
            _searchContext.Items.Add(item);
            _searchContext.SaveChanges();
        }
    }
}
