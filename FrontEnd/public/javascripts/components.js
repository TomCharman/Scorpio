import { getRelativeTime } from "./utils.js"

export const makeAvatar = (user) => {
    return `<img class="avatar" src="https://localhost:7001/api/user/${user.id}/avatar" />`
}

export const makeComment = (comment, isTopLevel) => {
    const hasChildren = comment.childComments?.length > 0
    const childComments = hasChildren ? comment.childComments?.map(cc => makeComment(cc, false)).join('\n') : ''

    return `<div id="comment-${comment.id}" class="comment">
        <div class="commentSidebar">
            ${makeAvatar(comment.user)}
            ${isTopLevel && hasChildren ? '<span class="threadIndicator" />' : ''}
        </div>
        <div class="commentDetails">
            <div class="commentHeader">
                <span class="name">${comment.user.name}</span>
                •
                <span class="date">${getRelativeTime(comment.postedDate)}</span>
            </div>
            <div class="commentText">${comment.text}</div>
            <div class="commentLinks">
                <span class="upvote"></span>
                ${isTopLevel ? '<a class="replyLink">Reply</a>' : ''}
            </div>
            ${isTopLevel ? `<div id="postReply-${comment.id}" class="postComment hidden">
                <div id="myAvatar"></div>
                <textarea class="commentText" placeholder="What are your thoughts?"></textarea>
                <button class="commentButton">Comment</button>
            </div>` : ''}
            ${isTopLevel ? `<div class="nestedComments">${childComments}</div>` : ''}
        </div>
    </div>`
}