import { getComments, getMe, postComment } from "./api.js"
import { makeComment } from "./components.js"

console.log('hi')

const commentContainer =  document.getElementById('commentContainer')
const commentButton = document.getElementById('commentButton')
const myAvatar = document.getElementById('myAvatar')
const commentText = document.getElementById('commentText')

commentButton.onclick = function(e) {
    console.log('hi', commentText.value)
    postComment(commentText.value)
}


const setMe = (me) => {
    console.log('I am me', me)
    myAvatar.innerText = me.name
}

getMe()
    .then(response => setMe(response))

getComments()
    .then(comments => {
        console.log('i gots the data', comments)
        const html = comments.map(c => makeComment(c))
            .join('\n')

        console.log('html!', html)
        commentContainer.innerHTML = html
    })