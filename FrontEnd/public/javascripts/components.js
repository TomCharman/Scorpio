import { getRelativeTime } from "./utils.js"

export const makeAvatar = (user) => {
    return `<img class="avatar" src="https://localhost:7001/api/user/${user.id}/avatar" />`
}

export const makeComment = (comment) => {
    return `<div id="comment-${comment.id}" class="comment">
        ${makeAvatar(comment.user)}
        <div class="commentDetails">
            <div class="commentHeader">
                <span class="name">${comment.user.name}</span>
                •
                <span class="date">${getRelativeTime(comment.postedDate)}</span>
            </div>
            <div class="commentText">${comment.text}</div>
            <div class="commentLinks">
                <span class="upvote"></span>
                <a>Reply</a>
            </div>
        </div>
    </div>`
}