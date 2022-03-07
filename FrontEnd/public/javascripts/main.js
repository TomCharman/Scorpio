import { getComments, getMe, postComment, putVote } from "./api.js"
import { makeAvatar, makeComment } from "./components.js"

console.log('hi')
var comments = []

const commentContainer =  document.getElementById('commentContainer')
const commentButton = document.getElementById('commentButton')
const myAvatar = document.getElementById('myAvatar')
const commentText = document.getElementById('commentText')

const setMe = (me) => {
    console.log('I am me', me)
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
            comment.onclick = () => onVote(c.id)
        }
        
        // .onclick = () => onVote(c.id)
        console.log('comment', c.id, comment)
    })
}

const onVote = (commentId) => {
    console.log('does this even work', commentId)
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