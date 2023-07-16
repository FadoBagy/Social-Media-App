namespace Social_Media_App.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Social_Media_App.Services.Post;

    public class PostHub : Hub
    {
        private readonly IPostService post;

        public PostHub(IPostService post)
        {
            this.post = post;
        }

        public async Task GetPosts()
        {
            var allPosts = post.GetAllPosts();
            await Clients.Caller.SendAsync("ReceivePosts", allPosts);
        }
    }
}
