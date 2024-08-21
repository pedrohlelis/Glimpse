using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glimpse.Models;

public class GitHubService
{
    private readonly GitHubClient _client;
    
    public GitHubService()
    {
        _client = new GitHubClient(new ProductHeaderValue("GlimpseApp"));
    }
    
    public async Task<IReadOnlyList<GitHubCommit>> GetCommits(string owner, string repoName, string token)
    {
        var client = new GitHubClient(new ProductHeaderValue("YourAppName"));
        client.Credentials = new Credentials(token);

        return await client.Repository.Commit.GetAll(owner, repoName);
    }

    public async Task<IReadOnlyList<Branch>> GetBranches(string owner, string repoName, string token)
    {
        var client = new GitHubClient(new ProductHeaderValue("YourAppName"));
        client.Credentials = new Credentials(token);

        return await client.Repository.Branch.GetAll(owner, repoName);
    }

    public async Task<IReadOnlyList<RepositoryContent>> GetAllRepositoryContent(string owner, string repoName, string token, string path = "")
    {
        _client.Credentials = new Credentials(token);

        try
        {
            if (string.IsNullOrEmpty(path))
            {
                return await _client.Repository.Content.GetAllContents(owner, repoName);
            }
            else
            {
                return await _client.Repository.Content.GetAllContents(owner, repoName, path);
            }
        }
        catch (NotFoundException ex)
        {
            // Trate a exceção de forma adequada, por exemplo, registrando um erro ou retornando uma lista vazia
            Console.Error.WriteLine($"Erro ao obter conteúdo do repositório: {ex.Message}");
            return new List<RepositoryContent>();
        }
    }

    public async Task<string> GetFileContent(string owner, string repoName, string path, string token)
    {
        _client.Credentials = new Credentials(token);
        var file = await _client.Repository.Content.GetAllContents(owner, repoName, path);
        return file.FirstOrDefault()?.Content;
    }
}
