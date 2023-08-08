// JavaScript code for the new view
document.addEventListener('DOMContentLoaded', function() {
    // Fetch user data from API
    fetch('/api/users')
        .then(response => response.json())
        .then(data => console.log(data));

    // Add click event handler for hypothetical button
    document.querySelector('#myButton').addEventListener('click', function() {
        console.log('Button clicked!');
    });
});