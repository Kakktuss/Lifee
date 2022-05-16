using PocketSharp;

namespace Lifee.Pocket.Synchronizer.Application.Service;

public class PocketService : IPocketService
{
    private readonly UserRepos
    
    public PocketService()
    {
        
    }
    
    public void CreateArticle()
    {
        PocketClient pocketClient = new PocketClient("", "");

        pocketClient.GetTags();
    }
}