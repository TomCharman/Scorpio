import LikeButton from "../react/Upvote.js"
import { getComments, getMe, postComment, putVote } from "./api.js"
import { makeAvatar, makeComment } from "./components.js"

console.log('hi')
var comments = []

const commentContainer =  document.getElementById('commentContainer')
const commentButton = document.getElementById('commentButton')
const myAvatar = document.getElementById('myAvatar')
const commentText = document.getElementById('commentText')

const setMe = (me) => {
    myAvatar.innerHTML = makeAvatar(me)
}

const drawComments = () => {
    const html = comments
        .map(c => makeComment(c))
        .join('\n')

    commentContainer.innerHTML = html

    comments.forEach(c => {
        var comment = document.getElementById(`comment-${c.id}`)
        
        if (comment) {
            const upvoteContainer = comment.getElementsByClassName("upvote")[0]
            ReactDOM.render(React.createElement(LikeButton, { comment: c, onVote }), upvoteContainer);
        }
    })
}

const onVote = (commentId) => {
    putVote(commentId)
        .then(data => {
            comments = comments.map(c => c.id === commentId ? data : c)
            drawComments()
        })
}

// On initial page load

commentButton.onclick = function(e) {
    console.log('hi', commentText.value)
    postComment(commentText.value)
        .then(data => {
            comments = [...comments, data]
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