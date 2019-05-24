using SearchRepositoriesGitHUB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchRepositoriesGitHUB.Repositories
{
    public interface IItemsRepository
    {
        Item Get(long id);

        void Salvar(Item item);

        IList<Item> Listar();
    }
}
