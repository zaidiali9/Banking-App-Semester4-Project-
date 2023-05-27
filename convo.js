


const options = { method: 'GET', headers: { accept: 'application/json' } };

fetch('https://api.fastforex.io/fetch-multi?from=AUD&to=PKR&api_key=4360e08dfb-e5ab6b9479-rupmh7', options)
    .then(response => response.json())
    .then(response => console.log(response))
    .catch(err => console.error(err));
