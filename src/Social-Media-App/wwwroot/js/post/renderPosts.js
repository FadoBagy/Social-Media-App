const connection = new signalR.HubConnectionBuilder().withUrl('/postshub').build();
const templatePostElements = document.querySelectorAll('.loadingIndicator');

connection.on('ReceivePosts', (posts) => {
    const postsElement = document.querySelector('.posts');

    hideAllTemplatePosts();

    if (posts.length > 0) {
        posts.forEach((post) => {
            postsElement.appendChild(createPostElement(post));
        });
    } else {
        postsElement.innerHTML = "<p>No posts available.</p>";
    }
});

connection.start()
    .then(() => {
        showAllTemplatePosts();
        return connection.invoke("GetPosts");
    })
    .then(() => {
        hideAllTemplatePosts();
    })
    .catch((error) => console.error(error.toString()));

function createPostElement(post) {
    const username = post.user.userName.substring(0, post.user.userName.indexOf('@'));

    const postElement = document.createElement('article');
    postElement.className = 'post';

    const infoDiv = document.createElement('div');
    infoDiv.className = 'info';

    const detailsDiv = document.createElement('div');
    detailsDiv.className = 'details';

    const userLink = document.createElement('a');
    userLink.href = `/User/Profile/${post.user.id}`;

    const userImage = document.createElement('img');
    userImage.src = post.user.profileImage;

    const usernameLink = document.createElement('a');
    usernameLink.href = `/User/Profile/${post.user.id}`;

    const usernameParagraph = document.createElement('p');
    usernameParagraph.textContent = username;

    const firstSeparator = document.createElement('span');
    firstSeparator.textContent = '•';

    const creationDate = new Date(post.creationDate);
    const dateParagraph = document.createElement('time');
    dateParagraph.textContent = creationDate.toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    });

    const secondSeparator = document.createElement('span');
    secondSeparator.textContent = '•';

    const timeParagraph = document.createElement('time');
    timeParagraph.textContent = 'UTC ' + creationDate.toLocaleTimeString('en-US', {
        hour: 'numeric',
        minute: 'numeric',
        hour12: true
    });

    const postImage = document.createElement('img');
    postImage.className = 'post-image';
    postImage.src = `/uploads/${post.imagePath}`;
    postImage.alt = 'Post image';

    const actionsDiv = document.createElement('div');
    actionsDiv.className = 'actions';

    const likeDiv = document.createElement('div');
    const likeImage = document.createElement('img');
    likeImage.src = '/img/feed/like.png';
    const likeCountParagraph = document.createElement('p');
    likeCountParagraph.textContent = post.likes.length.toString();
    likeDiv.appendChild(likeImage);
    likeDiv.appendChild(likeCountParagraph);

    const commentDiv = document.createElement('div');
    const commentImage = document.createElement('img');
    commentImage.src = '/img/feed/comment.png';
    const commentCountParagraph = document.createElement('p');
    commentCountParagraph.textContent = post.comments.length.toString();
    commentDiv.appendChild(commentImage);
    commentDiv.appendChild(commentCountParagraph);

    const shareDiv = document.createElement('div');
    const shareImage = document.createElement('img');
    shareImage.src = '/img/feed/share.png';
    const shareCountParagraph = document.createElement('p');
    shareCountParagraph.textContent = '20k';
    shareDiv.appendChild(shareImage);
    shareDiv.appendChild(shareCountParagraph);

    const captionDiv = document.createElement('div');
    captionDiv.className = 'caption';

    const captionUserLink = document.createElement('a');
    captionUserLink.href = `/User/Profile/${post.user.id}`;

    const captionUsernameParagraph = document.createElement('p');
    captionUsernameParagraph.textContent = username;

    const captionParagraph = document.createElement('p');
    captionParagraph.textContent = post.caption;

    const commentsPreviewDiv = document.createElement('div');
    commentsPreviewDiv.className = 'comments-preview';
    const viewCommentsParagraph = document.createElement('p');
    viewCommentsParagraph.textContent = 'View comments';
    commentsPreviewDiv.appendChild(viewCommentsParagraph);

    userLink.appendChild(userImage);
    usernameLink.appendChild(usernameParagraph);
    detailsDiv.appendChild(userLink);
    detailsDiv.appendChild(usernameLink);
    detailsDiv.appendChild(firstSeparator);
    detailsDiv.appendChild(dateParagraph);
    detailsDiv.appendChild(secondSeparator);
    detailsDiv.appendChild(timeParagraph);
    infoDiv.appendChild(detailsDiv);
    postElement.appendChild(infoDiv);
    postElement.appendChild(postImage);
    actionsDiv.appendChild(likeDiv);
    actionsDiv.appendChild(commentDiv);
    actionsDiv.appendChild(shareDiv);
    postElement.appendChild(actionsDiv);
    captionUserLink.appendChild(captionUsernameParagraph);
    captionDiv.appendChild(captionUserLink);
    captionDiv.appendChild(captionParagraph);
    postElement.appendChild(captionDiv);
    postElement.appendChild(commentsPreviewDiv);

    return postElement;
}

function hideAllTemplatePosts() {
    templatePostElements.forEach(template => {
        template.style.display = 'none';
    });
}

function showAllTemplatePosts() {
    templatePostElements.forEach(template => {
        template.style.display = 'flex';
    });
}