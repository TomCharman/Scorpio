const defaultHeaders = {
    'Content-Type': 'application/json'
}

const baseApi = 'https://localhost:7001/api'

export const getMe = () => {
    return fetch(`${baseApi}/user/me`)
        .then(response => response.json())
        .catch(error => console.warn('error!', error))
}

export const getComments = () => {
    console.log('I am get comments')
    return fetch(`${baseApi}/comment`)
    .then(response => response.json())
    .catch(error => console.warn('error!', error))
}

export const postComment = (commentText) => {
    return fetch(`${baseApi}/comment`, {
        method: 'POST',
        headers: defaultHeaders,
        mode: 'cors',
        body: JSON.stringify({ text: commentText })
    })
        .then(response => {
            console.log('posted', response)
            return response.json()
        })
        .catch(error => console.warn('error!', error))
}

export const putVote = (commentId) => {
    return fetch(`${baseApi}/comment/${commentId}/vote`, {
        method: 'PUT',
        headers: defaultHeaders,
    })
        .then(response => response.json())
        .catch(error => console.warn('error!', error))
}