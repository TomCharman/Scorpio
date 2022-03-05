import { getComments, getMe, postComment } from "./api.js"
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
}

commentButton.onclick = function(e) {
    console.log('hi', commentText.value)
    postComment(commentText.value)
        .then(data => {
            comments = [...comments, data]
            drawComments()
        })
}


// On load

getMe()
    .then(response => setMe(response))

getComments()
    .then(data => {
        comments = data
        drawComments()
    })