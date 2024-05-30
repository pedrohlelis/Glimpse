function openRolesModal() {
    var myModal = new bootstrap.Modal(document.getElementById('rolesModal'));
    myModal.show();
}

// Function to handle editing a role
function editRole(button) {
    ditRoleModal = new bootstrap.Modal(document.getElementById('editRoleModal'));
    editRoleModal.show();
}

// Function to handle deleting a role
function deleteRole(roleId) {
    // Implement your logic to handle deletion here
    console.log("Deleting role with ID: " + roleId);
}

// Event listener to open the modal when the icon is clicked
document.getElementById('project-roles-btn').addEventListener('click', openRolesModal);

//START of sidebar menu ---------------------------
let memberSideMenuBtn = document.querySelector('#member-side-menu-btn')
let memberSideMenu = document.querySelector('.memberSideMenu')
let sideMenuBtn = document.querySelector('#side-menu-btn')
let sideBar = document.querySelector(".sideBar")
let mainContent = document.querySelector(".main-content")
let memberDivs = document.querySelectorAll(".member-div")
let rolesDivs = document.querySelectorAll(".role-container")

if (memberDivs.length === 0) {
console.error('No memberDiv elements found');
}

memberSideMenuBtn.onclick = function () {
    memberDivs.forEach(function (div) {
        div.classList.toggle('d-none');
    })
    rolesDivs.forEach(function (div){
        div.classList.toggle('d-none');
    })
    memberSideMenu.classList.toggle('active');
};

sideMenuBtn.onclick = function () {
    sideBar.classList.toggle('active')
    mainContent.classList.toggle('opacity-50')
};
//END of sidebar menu   ---------------------------

document.addEventListener('DOMContentLoaded', function() {
    const overlay = document.getElementById('overlay');
    const overlayContent = document.getElementById('overlay-content');
    const editForm = document.getElementById('edit-card-form');
    const saveButton = document.getElementById('save-button');
    let isModified = false;

    // Função para mostrar o overlay
    function showOverlay() {
        overlay.removeAttribute('hidden');
    }

    // Função para esconder o overlay
    function hideOverlay() {
        overlay.setAttribute('hidden', true);
    }

    // Event listener para detectar mudanças nos inputs
    overlayContent.addEventListener('input', function() {
        isModified = true;
        saveButton.classList.add('button-save-modified');
    });

    // Event listener para o envio do formulário
    editForm.addEventListener('submit', function(event) {
        if (!isModified) {
            event.preventDefault();
            return false;
        }
    });

    // Event listener para mostrar o overlay quando um card é clicado
    const cards = document.querySelectorAll('.card-link');
    cards.forEach(card => {
        card.addEventListener('click', function(event) {
            const cardId = card.dataset.id;
            const cardName = card.dataset.name;
            const cardDescription = card.dataset.description;
            const cardDate = card.dataset.date;

            document.getElementById('cardId').value = cardId;
            document.getElementById('name').value = cardName;
            document.getElementById('description').value = cardDescription;
            document.getElementById('date').value = cardDate;

            showOverlay();
        });
    });

    // Event listener para esconder o overlay quando clicar fora dele
    overlay.addEventListener('click', function(event) {
        if (event.target === overlay || event.target.classList.contains('close-button')) {
            hideOverlay();
        }
    });
});
// LOGICA DOS FORMS DE CARD E LANE
const addCardButtons = document.querySelectorAll('.add-card-button');
const cardForms = document.querySelectorAll('.create-card-form');
const cardInputs = document.querySelectorAll('.card-input');

addCardButtons.forEach((button, index) => {
    button.addEventListener('click', function() {
        cardForms[index].removeAttribute('hidden');
        cardInputs[index].focus();
    });
});

const addLaneButton = document.querySelector('.new-lane-btn');
const laneForm = document.querySelector('.create-lane-form');
const laneInput = document.querySelector('.lane-input');

addLaneButton.addEventListener('click', function() {
    laneForm.removeAttribute('hidden');
    laneInput.focus();
});

cardInputs.forEach((input) => {
    input.setAttribute('autocomplete', 'off');
});

laneInput.setAttribute('autocomplete', 'off');

document.addEventListener('click', function(event) {
    cardForms.forEach((form, index) => {
        if (!event.target.closest('.create-card-form') && !event.target.closest('.add-card-button')) {
            form.setAttribute('hidden', true);
        }
    });

    if (!event.target.closest('.create-lane-form') && !event.target.closest('.new-lane-btn')) {
        laneForm.setAttribute('hidden', true);
    }
});
// LOGICA DE MOVER CARDS
const columns = document.querySelectorAll(".card-col");

document.addEventListener("dragstart", (e) => {
    e.target.classList.add("dragging");
});

document.addEventListener("dragend", (e) => {
    e.target.classList.remove("dragging");
    saveCardOrder(); // Chama a função para salvar a ordem dos cards após o arrasto
});

columns.forEach((item) => {
    item.addEventListener("dragover", (e) => {
        e.preventDefault(); // Previne o comportamento padrão
        const dragging = document.querySelector(".dragging");
        const applyAfter = getNewPosition(item, e.clientY);

        if (applyAfter) {
            applyAfter.insertAdjacentElement("afterend", dragging);
        } else {
            item.prepend(dragging);
        }
    });
});

function getNewPosition(column, posY) {
    const cards = column.querySelectorAll(".item:not(.dragging)");
    let result;

    for (let refer_card of cards) {
        const box = refer_card.getBoundingClientRect();
        const boxCenterY = box.y + box.height / 2;

        if (posY >= boxCenterY) result = refer_card;
    }

    return result;
}

function saveCardOrder() {
    const lanesData = [];

    columns.forEach((lane) => {
        const laneId = lane.dataset.laneId;
        const cardIds = Array.from(lane.querySelectorAll(".item")).map((card) => card.dataset.cardId);
        lanesData.push({ laneId, cardIds });
    });

    // Enviar dados para o servidor via AJAX
    fetch('/caminho/do/seu/controller', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ lanes: lanesData })
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao salvar a ordem dos cards');
        }
        return response.json();
    })
    .then(data => {
        console.log('Ordem dos cards salva com sucesso:', data);
    })
    .catch(error => {
        console.error('Erro:', error);
    });
}