console.log("foi");
const addLaneButton = document.querySelector('.new-lane-btn');
const laneForm = document.querySelector('.create-lane-form');
const laneInput = document.querySelector('.lane-input');
const cardForms = document.querySelectorAll('.create-card-form');

addLaneButton.addEventListener('click', function(event) {
    event.stopPropagation();
    laneForm.removeAttribute('hidden');
    laneInput.focus();
});
console.log("foi2");