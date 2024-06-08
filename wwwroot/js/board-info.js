function openRolesModal() {
    var myModal = new bootstrap.Modal(document.getElementById('rolesModal'));
    myModal.show();
}

function createRole() {
    var createRoleModal = new bootstrap.Modal(document.getElementById('createRoleModal'));
    createRoleModal.show();
}

// Function to handle editing a role
function editRole(button) {

    var listItem = button.closest('li');

    document.getElementById('roleId').value = listItem.dataset.id;
    document.getElementById('roleName').value = listItem.dataset.name;
    document.getElementById('roleDescription').value = listItem.dataset.description;
    document.getElementById('roleColor').value = listItem.dataset.color;
    document.getElementById('canManageMembers').checked = listItem.dataset.mngMembers  === 'True';
    document.getElementById('canManageRoles').checked = (listItem.dataset.mngRoles === 'True');
    document.getElementById('canManageCards').checked = listItem.dataset.mngCards === "True";
    document.getElementById('canManageTags').checked = listItem.dataset.mngTags === 'True';
    document.getElementById('canManageChecklist').checked = listItem.dataset.mngChecklist === "True";

    var editRoleModal = new bootstrap.Modal(document.getElementById('editRoleModal'));
    editRoleModal.show();
}

// Function to handle deleting a role
function deleteRole(button) {
    var listItem = button.closest('li');
    document.getElementById('roleToDeleteId').value = listItem.dataset.id;

    var deleteRoleModal = new bootstrap.Modal(document.getElementById('ConfirmDeleteRoleModal'));
    deleteRoleModal.show();
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
let invButton = document.querySelector(".invite-btn")

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
    invButton.classList.toggle('d-none');
    memberSideMenu.classList.toggle('active');
};

sideMenuBtn.onclick = function () {
    sideBar.classList.toggle('active')
    mainContent.classList.toggle('opacity-50')
};
//END of sidebar menu   ---------------------------

document.addEventListener('DOMContentLoaded', function() {
    //CARDS E LANES DRAG&DROP
    const board = document.querySelector(".board-main");

    document.addEventListener("dragstart", (e) => {
        if (e.target.classList.contains("lane")) {
            e.target.classList.add("dragging-lane");
        } else if (e.target.classList.contains("card-link")) {
            e.target.classList.add("dragging-card");
        }
    });

    document.addEventListener("dragend", (e) => {
        if (e.target.classList.contains("lane")) {
            e.target.classList.remove("dragging-lane");
            saveLaneOrder(); // Save the lane order after dragging
        } else if (e.target.classList.contains("card-link")) {
            e.target.classList.remove("dragging-card");
            saveCardOrder(); // Save the card order after dragging
        }
    });

    board.addEventListener("dragover", (e) => {
        e.preventDefault();
        const draggingLane = document.querySelector(".dragging-lane");
        const draggingCard = document.querySelector(".dragging-card");

        if (draggingLane) {
            const lanes = [...board.querySelectorAll(".lane:not(.dragging-lane)")];
            const afterElement = getDragAfterElement(lanes, e.clientX);
            if (afterElement == null) {
                board.appendChild(draggingLane);
            } else {
                board.insertBefore(draggingLane, afterElement);
            }
        }
    });

    function getDragAfterElement(elements, x) {
        return elements.reduce((closest, child) => {
            const box = child.getBoundingClientRect();
            const offset = x - box.left - box.width / 2;
            if (offset < 0 && offset > closest.offset) {
                return { offset: offset, element: child };
            } else {
                return closest;
            }
        }, { offset: Number.NEGATIVE_INFINITY }).element;
    }

    // Handle dragging cards over the correct lane
    const lanes = document.querySelectorAll(".lane");
    lanes.forEach((lane) => {
        const cardContainer = lane.querySelector(".card-col");

        cardContainer.addEventListener("dragover", (e) => {
            e.preventDefault();
            const dragging = document.querySelector(".dragging-card");
            if (dragging) {
                const afterElement = getCardAfterElement(cardContainer, e.clientY);

                if (afterElement) {
                    afterElement.insertAdjacentElement("afterend", dragging);
                } else {
                    cardContainer.prepend(dragging);
                }
            }
        });
    });

    function getCardAfterElement(container, y) {
        const cards = [...container.querySelectorAll(".card-link:not(.dragging-card)")];
        return cards.reduce((closest, card) => {
            const box = card.getBoundingClientRect();
            const offset = y - box.top - box.height / 2;
            if (offset < 0 && offset > closest.offset) {
                return { offset: offset, element: card };
            } else {
                return closest;
            }
        }, { offset: Number.NEGATIVE_INFINITY }).element;
    }

    // function getCardAfterElement(elements, y) {
    //     return elements.reduce((closest, child) => {
    //         const box = child.getBoundingClientRect();
    //         const offset = x - box.left - box.width / 2;
    //         if (offset < 0 && offset > closest.offset) {
    //             return { offset: offset, element: child };
    //         } else {
    //             return closest;
    //         }
    //     }, { offset: Number.NEGATIVE_INFINITY }).element;
    // }

    function saveLaneOrder() {
        // Your logic to save lane order goes here
        console.log("Lane order saved");
    }

    function saveCardOrder() {
        const cardOrder = [];
        document.querySelectorAll(".card-link").forEach(card => {
            cardOrder.push(card.getAttribute("data-id"));
        });
        console.log(cardOrder)
    
        fetch('/Card/SaveCardOrder', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cardOrder)
        })
        .then(response => response.json())
        .then(data => {
            console.log(data.message); // Success message from the server
        })
        .catch(error => {
            console.error('Error saving card order:', error);
        });
    }
    

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