namespace Social_Media_App.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Social_Media_App.Infrastructure.Extensions;
    using Social_Media_App.Services.Post;

    [Authorize]
    public class PostHub : Hub
    {
        private readonly IPostService post;

        public PostHub(IPostService post)
        {
            this.post = post;
        }

        public async Task GetPosts()
        {
            var allPosts = await post.GetAllPostsForUserFeed(Context.User.Id());
            await Clients.Caller.SendAsync("ReceivePosts", allPosts);
        }

        public async Task GetPost(int postId)
        {
            var currentUserId = Context.User.Id();
            var currentPosts = await post.GetPostForSingleView(postId, currentUserId);
            await Clients.Caller.SendAsync("ReceivePost", currentPosts, currentUserId);
        }

        public async Task GetGalleryPosts(string userId)
        {
            var allPosts = post.GetPostsForGalleryByUserId(userId);
            await Clients.Caller.SendAsync("ReceiveGalleryPosts", allPosts);
        }

        public async Task LikePostBtnHandler(int postId)
        {
            var currentUserId = Context.User.Id();
            if (post.PostIsLikedByUser(postId, currentUserId))
            {
                await post.UnlikePost(postId, currentUserId);
            }
            else
            {
                await post.LikePost(postId, currentUserId);
            }
            await Clients.All.SendAsync("ReceiveUpdatedLikes", postId, post.GetPostLikeCount(postId));
        }
    }
}
