import LikeButton from "../react/Upvote.js"
import { getComments, getMe, postComment, putVote } from "./api.js"
import { makeAvatar, makeComment } from "./components.js"

var comments = []

const commentContainer = document.getElementById('commentContainer')
const commentButton = document.getElementById('commentButton')
const myAvatar = document.getElementById('myAvatar')
const commentText = document.getElementById('commentText')

const setMe = (me) => {
    myAvatar.innerHTML = makeAvatar(me)
}

const updateNestedComment = (parentCommentId, newComment) => {
    comments = comments.map(c => c.id === parentCommentId
        ? { ...c, childComments: [newComment, ...(c.childComments || [])] }
        : c)
    drawComments()
}

const applyLinksToComment = (c) => {
    var comment = document.getElementById(`comment-${c.id}`)

    if (comment) {
        const upvoteContainer = comment.getElementsByClassName("upvote")[0]
        ReactDOM.render(React.createElement(LikeButton, { comment: c, onVote }), upvoteContainer);

        const replyLinks = comment.getElementsByClassName("replyLink")
        if (replyLinks.length > 0) {
            replyLinks[0].onclick = () => onReply(c.id)
        }

        const commentButtons = comment.getElementsByClassName("commentButton")
        if (commentButtons.length > 0) {
            commentButtons[0].onclick = () => {
                const replyText = document.getElementById(`postReply-${c.id}`)
                    .getElementsByClassName('commentText')[0]
                postComment(replyText.value, c.id)
                    .then(data => updateNestedComment(c.id, data))
            }
        }
    }

    c.childComments?.forEach(nc => applyLinksToComment(nc))
}

const drawComments = () => {
    const html = comments
        .map(c => makeComment(c, true))
        .join('\n')

    commentContainer.innerHTML = html

    comments.forEach(c => {
        applyLinksToComment(c)
    })
}

const onVote = (commentId) => {
    putVote(commentId)
        .then(data => {
            comments = comments.map(c => c.id === commentId ? data : c)
            drawComments()
        })
}

const onReply = (commentId) => {
    const replyBox = document.getElementById(`postReply-${commentId}`)
    replyBox.classList.remove("hidden")
}

// On initial page load

commentButton.onclick = function (e) {
    postComment(commentText.value)
        .then(data => {
            commentText.value = ''
            comments = [data, ...comments]
            drawComments()
        })
}

getMe()
    .then(response => setMe(response))

getComments()
    .then(data => {
        comments = data
        drawComments()
    })

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`https://localhost:7001/hubs/upvote`)
    .withAutomaticReconnect()
    .build()

connection.start()
    .then(result => {
        connection.on('ReceiveMessage', message => {
            comments = comments.map(c => message.commentId == c.id
                ? { ...c, voteCount: message.voteCount }
                : {
                    ...c, childComments: c.childComments?.map(cc => cc.id === message.commentId
                        ? { ...cc, voteCount: message.voteCount }
                        : cc)
                })
            drawComments()
        });
    })
    .catch(e => console.error('Connection failed: ', e))