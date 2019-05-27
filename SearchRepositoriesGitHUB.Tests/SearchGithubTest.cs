using Microsoft.EntityFrameworkCore;
using Moq;
using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Repositories;
using SearchRepositoriesGitHUB.Services;
using System;
using Xunit;

namespace SearchRepositoriesGitHUB.Tests
{
    public class SearchGithubTest
    {
        private readonly SearchRepositoriesGitHUBDBContext _searchContext;
        private readonly IGitHubService _gitHubService;
        private readonly IGitHubRepository _gitHubRepository;

        public SearchGithubTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SearchRepositoriesGitHUBDBContext>();
            optionsBuilder.UseSqlServer("Server=ebilieri.database.windows.net;Database=GitHUBDB;User Id=ebilieri; Password=tgn@859XYZ123456789;");

            _searchContext = new SearchRepositoriesGitHUBDBContext(optionsBuilder.Options);
            _gitHubRepository = new GitHubRepository(_searchContext);
            _gitHubService = new GitHubService(_gitHubRepository);
        }

        [Fact]
        public void GetRepositorioByIdTeste()
        {
            long valorEsperado = 77742180;
            var repositorio = _gitHubService.Get(valorEsperado);

            Assert.Equal(valorEsperado, repositorio.Id);
        }

        [Fact]
        public void SalvarRepositorioTeste()
        {
            var repositorio = new Repositorio
            {
                Id = 123456,
                Name = "Teste",
                FullName = "FullName",
                Description = "Description",
                HtmlUrl = "https://github.com/",
                Language = "Java"
            };

            //var repositoryMoq = new Mock<IGitHubRepository>();
            var gitRepositoryMoq = new Mock<IGitHubRepository>();
            var gitHubService = new GitHubService(gitRepositoryMoq.Object);
            gitHubService.Salvar(repositorio);

            gitRepositoryMoq.Verify(r => r.Salvar(
                It.Is<Repositorio>(v => v.Id == repositorio.Id)));


        }
    }
}
