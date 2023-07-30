import { createPostElement, createGalleryPostElement, hideAllTemplatePosts, showAllTemplatePosts } from './postUtils.js';
import { renderOptionsPanel } from './optionsPanel.js';

const connection = new signalR.HubConnectionBuilder().withUrl('/postshub').build();

connection.on('ReceivePost', (post, currentUserId) => {
    const postContainerEl = document.querySelector('.main-container');

    if (post) {
        postContainerEl.appendChild(createPostElement(post, currentUserId));
    } else {
        postContainerEl.innerHTML = '<p>Post unavailable</p>';
    }
});

connection.on('ReceivePosts', (posts) => {
    const postsEl = document.querySelector('.posts');

    if (posts.length > 0) {
        posts.forEach((post) => {
            postsEl.appendChild(createPostElement(post));
        });
    } else {
        postsEl.innerHTML = '<p>No posts available</p>';
    }
});

connection.on('ReceiveGalleryPosts', (posts) => {
    const postsEl = document.querySelector('.user-posts');

    if (posts.length > 0) {
        posts.forEach((post) => {
            postsEl.appendChild(createGalleryPostElement(post));
        });
    } else {
        postsEl.innerHTML = '<p>No posts available</p>';
    }
});

connection.on('ReceiveUpdatedLikes', (postId, likes) => {
    const postLikesEl = document.querySelector(`[data-post-id="${postId}"] div:first-child p`);
    const postLikesimgEl = document.querySelector(`[data-post-id="${postId}"] div:first-child img`);

    postLikesEl.textContent = likes;
    postLikesimgEl.classList.toggle('liked');
});

document.querySelector('.main-container').addEventListener('click', async (e) => {
    if (e.target.classList.contains('like-btn')) {
        const postId = e.target.parentElement.parentElement.dataset.postId;

        try {
            await connection.invoke('LikePostBtnHandler', parseInt(postId));
        } catch (err) {
            console.error(err.toString());
        }
    }
});

const currentURL = window.location.href;
const pathParts = new URL(currentURL).pathname.split('/');

connection.start()
    .then(() => {
        showAllTemplatePosts();

        if (pathParts[1] === '') {
            // Home page feed
            return connection.invoke('GetPosts');
        } else if (pathParts[1] === 'User') {
            // User's profile posts
            return connection.invoke('GetGalleryPosts', currentURL.match(/\/([^/]+)$/)[1]);
        } else if (pathParts[1] === 'Post') {
            // Single post view
            return connection.invoke('GetPost', parseInt(currentURL.match(/\d+$/)[0]));
        }
    })
    .then(() => {
        hideAllTemplatePosts();

        if (pathParts[1] == 'Post') {
            // Single post view
            renderOptionsPanel();
        }
    })
    .catch((err) => {
        console.error(err.toString());
    });