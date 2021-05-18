// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const id = 7;
const query = 'life'

const uri = 'https://api.adviceslip.com/advice';
const uriById = `https://api.adviceslip.com/advice/${id}`;
const uriByQuery = `https://api.adviceslip.com/advice/search/${query}`;

const getItems = (url, num) => {
    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log('data', data);
            document.getElementById(`rand-advice-${num}`).innerHTML = data.slip.advice;
        })
        .catch(error => console.error('Unable to get items.', error));
}

const getItemsById = (url) => {
    fetch(url)
        .then(response => response.text())
        .then(data => {
            console.log('data', url, data);
        })
        .catch(error => console.error('Unable to get items.', error));
}

getItems(uri, 1);
getItems(uri, 2);
getItems(uri, 3);