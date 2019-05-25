using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
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

        public Item Get(long id)
        {
            return _searchContext.Items.Where(s => s.IdGitHub == id).FirstOrDefault();
        }

        public IList<Item> Listar()
        {
            return _searchContext.Items.OrderBy(s => s.Name).ToList();
        }

        public void Salvar(Item item)
        {
            item.IdGitHub = item.Id;
            item.Id = 0;

            item.Owner.IdGitHub = item.Owner.Id;
            item.Owner.Id = 0;

            _searchContext.Items.Add(item);
            _searchContext.SaveChanges();           
        }
    }
}
