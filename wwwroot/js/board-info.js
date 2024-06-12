const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

const IsMemberSideBarActiveInputs = document.querySelectorAll('.IsMemberSideBarActiveInput');

function openTagsModal() {
    var tagsModal = new bootstrap.Modal(document.getElementById('tagsModal'));
    tagsModal.show();
}

function createTag() {
    var createTagModal = new bootstrap.Modal(document.getElementById('createTagModal'));
    createTagModal.show();
}

function editTag(button) {
        var listitem = button.closest('li');
        document.getElementById('tagToEditId').value = listitem.dataset.id;
        document.getElementById('tagEditName').value = listitem.dataset.name;
        document.getElementById('tagEditColor').value = listitem.dataset.color;

        var editTagModal = new bootstrap.Modal(document.getElementById('editTagModal'));
        editTagModal.show();
    }

function openRolesModal() {
    var myModal = new bootstrap.Modal(document.getElementById('rolesModal'));
    myModal.show();
}

function openManageUserModal(button) {
    // let form =  document.getElementById('form-role');

    document.getElementById('ManagedUserName').innerText = button.dataset.name;
    document.getElementById('ManagedUserEmail').innerText = button.dataset.email;
    document.getElementById('userPicture').src = button.dataset.picture;
    document.getElementById('userToManageId').value = button.dataset.id;
}

function createRole() {
    var createRoleModal = new bootstrap.Modal(document.getElementById('createRoleModal'));
    createRoleModal.show();
}

// Function to handle editing a role
function editRole(button) {

    var listItem = button.closest('li');

    document.getElementById('roleToEditId').value = listItem.dataset.id;
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

function removeUser(button) {
    document.getElementById('userId').value = button.dataset.id;

    var removeMemberModal = new bootstrap.Modal(document.getElementById('expulsarMembroModal'));
    removeMemberModal.show();
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
let IsMemberSideBarActive = document.querySelector('.IsMemberSideBarActive')

if (memberDivs.length === 0) {
console.error('No memberDiv elements found');
}

memberSideMenuBtn.onclick = function () {
    ToggleMemberSideBar()
};


function ToggleMemberSideBar() {
    let invDiv = document.querySelector(".invite-member-div")

    memberDivs.forEach(function (div) {
        div.classList.toggle('d-none');
    })
    rolesDivs.forEach(function (div){
        div.classList.toggle('d-none');
    })
    invButton.classList.toggle('d-none');
    memberSideMenu.classList.toggle('active');
    invDiv.classList.toggle('d-none');
    if(memberSideMenu.classList.contains('active')){
        IsMemberSideBarActiveInputs.forEach(function(input) {
            input.value = 'true';
        });
        IsMemberSideBarActive.value = 'true'
    }else{
        IsMemberSideBarActive.value = 'false'
    }
}

sideMenuBtn.onclick = function () {
    sideBar.classList.toggle('active')
    mainContent.classList.toggle('opacity-50')
};
//END of sidebar menu   ---------------------------

let toggleMemberBarDiv = document.querySelector('.toggleMemberBarDiv');
if (toggleMemberBarDiv != null)
{
    ToggleMemberSideBar()
};

document.addEventListener('DOMContentLoaded', function() {
    // CARDS AND LANES DRAG&DROP
    const board = document.querySelector(".lanes");

    const saveCardOrderForm = document.querySelector('.save-card-order-form');
    const cardOrderInput = saveCardOrderForm.querySelector('input[name="taskIndexDictionary"]');

    const saveLaneOrderForm = document.querySelector('.save-lane-order-form');
    const laneOrderInput = saveLaneOrderForm.querySelector('input[name="laneIndexDictionary"]');

    saveCardOrderForm.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevent the default form submission behavior
        
        const formData = new FormData(saveCardOrderForm);

        // Send the form data using an AJAX request
        fetch(saveCardOrderForm.action, {
            method: 'POST',
            body: formData
        })
        .then(response => {
            if (response.ok) {
                // Optionally handle success response
                console.log('Card order saved successfully!');
                // Reload the page or update specific parts of the page with the new data
                location.reload(); // Reload the page to reflect changes
            } else {
                // Optionally handle error response
                console.error('Failed to save card order.');
            }
        })
        .catch(error => {
            console.error('An error occurred:', error);
        });
    });

    saveLaneOrderForm.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevent the default form submission behavior
        
        const formData = new FormData(saveLaneOrderForm);

        // Send the form data using an AJAX request
        fetch(saveLaneOrderForm.action, {
            method: 'POST',
            body: formData
        })
        .then(response => {
            if (response.ok) {
                // Optionally handle success response
                console.log('Lane order saved successfully!');
                // Reload the page or update specific parts of the page with the new data
                location.reload(); // Reload the page to reflect changes
            } else {
                // Optionally handle error response
                console.error('Failed to save card order.');
            }
        })
        .catch(error => {
            console.error('An error occurred:', error);
        });
    });

    // Function to update the card order input value
    function updateCardOrderInput() {
        const swimLanes = document.querySelectorAll('.swim-lane');
        let isMemberSideBarActive = saveCardOrderForm.querySelector('input[name=IsMemberSideBarActive]')
        let taskIndexDictionary = {}; // Initialize an empty object to store the task index for each task ID

        swimLanes.forEach((lane) => {
            const laneId = lane.dataset.id;
            const cards = lane.querySelectorAll('.task');

            cards.forEach((card, index) => {
                const taskId = card.dataset.id;
                const indexPosition = index + 1;
                
                // If the task ID doesn't exist in the dictionary, initialize it with an empty list
                if (!taskIndexDictionary[taskId]) {
                    taskIndexDictionary[taskId] = [];
                }

                // Add lane ID and task index to the list for this task ID
                taskIndexDictionary[taskId].push(String(laneId), String(indexPosition));
            });
        });

        // Convert the object to JSON and assign it to the cardOrderInput value
        if(memberSideMenu.classList.contains('active')){
            isMemberSideBarActive.value = true;
        }
        else{
            isMemberSideBarActive.value = false;
        }
        cardOrderInput.value = JSON.stringify(taskIndexDictionary);
    }

    function updateLaneOrderInput() {
        const lanes = document.querySelectorAll('.lanes');
        let isMemberSideBarActive = saveLaneOrderForm.querySelector('input[name=IsMemberSideBarActive]')
        let laneIndexDictionary = {}; // Initialize an empty object to store the task index for each task ID

        lanes.forEach((lane) => {
            const swimLanes = lane.querySelectorAll('.swim-lane');

            swimLanes.forEach((swimLane, index) => {
                const swimLaneId = swimLane.dataset.id;
                const indexPosition = index + 1;

                // Store the task index in the taskIndexDictionary using the task ID as the key
                laneIndexDictionary[swimLaneId] = indexPosition;
            });
        });
        // Convert the array to JSON and assign it to the cardOrderInput value
        if(memberSideMenu.classList.contains('active')){
            isMemberSideBarActive.value = true;
        }
        else{
            isMemberSideBarActive.value = false;
        }
        laneOrderInput.value = JSON.stringify(laneIndexDictionary);
    }

    // Drag and drop functionality for tasks
    const draggables = document.querySelectorAll(".task");
    const droppables = document.querySelectorAll(".swim-lane");

    draggables.forEach((task) => {
        task.addEventListener("dragstart", () => {
            task.classList.add("is-dragging");
        });
        task.addEventListener("dragend", () => {
            task.classList.remove("is-dragging");
            updateCardOrderInput()
            saveCardOrderForm.submit()
        });
    });

    droppables.forEach((zone) => {
        zone.addEventListener("dragover", (e) => {
            e.preventDefault();

            const bottomTask = insertAboveTask(zone, e.clientY);
            const curTask = document.querySelector(".is-dragging");

            if (!bottomTask) {
                zone.appendChild(curTask);
            } else {
                zone.insertBefore(curTask, bottomTask);
            }
        });
    });

    const insertAboveTask = (zone, mouseY) => {
        const els = zone.querySelectorAll(".task:not(.is-dragging)");

        let closestTask = null;
        let closestOffset = Number.NEGATIVE_INFINITY;

        els.forEach((task) => {
            const { top } = task.getBoundingClientRect();
            const offset = mouseY - top;

            if (offset < 0 && offset > closestOffset) {
                closestOffset = offset;
                closestTask = task;
            }
        });

        return closestTask;
    };

    // Drag and drop functionality for lanes
const lanes = document.querySelectorAll(".swim-lane");

lanes.forEach((lane) => {
    // Drag start for lanes
    lane.addEventListener("dragstart", (e) => {
        if (e.target.classList.contains("swim-lane")) {
            lane.classList.add("is-dragging-lane");
        }
        e.stopPropagation(); // Prevent triggering dragstart on parent elements
    });

    // Drag end for lanes
    lane.addEventListener("dragend", (e) => {
        if (e.target.classList.contains("swim-lane")) {
            lane.classList.remove("is-dragging-lane");
            updateLaneOrderInput();
            saveLaneOrderForm.submit();
        }
        e.stopPropagation(); // Prevent triggering dragend on parent elements
    });
})

    board.addEventListener("dragover", (e) => {
        e.preventDefault();

        const draggingLane = document.querySelector(".is-dragging-lane");
        const afterElement = getDragAfterElement(board, e.clientX);

        if (draggingLane) {
            if (afterElement == null) {
                board.appendChild(draggingLane);
            } else {
                board.insertBefore(draggingLane, afterElement);
            }
        }
    });

    function getDragAfterElement(container, x) {
        const draggableElements = [...container.querySelectorAll('.swim-lane:not(.is-dragging-lane)')];

        return draggableElements.reduce((closest, child) => {
            const box = child.getBoundingClientRect();
            const offset = x - box.left - box.width / 2;

            if (offset < 0 && offset > closest.offset) {
                return { offset: offset, element: child };
            } else {
                return closest;
            }
        }, { offset: Number.NEGATIVE_INFINITY }).element;
    }


    const overlay = document.getElementById('overlay');
    const addUserToCardForm = document.getElementById('addUserToCardForm');
    const overlayContent = document.getElementById('overlay-content');
    const editForm = document.getElementById('edit-card-form');
    const saveButton = document.getElementById('save-button');
    let isModified = false;

    function showOverlay() {
        overlay.removeAttribute('hidden');
    }

    function hideOverlay() {
        overlay.setAttribute('hidden', true);
    }

    overlayContent.addEventListener('input', function() {
        isModified = true;
        saveButton.classList.add('button-save-modified');
    });

    editForm.addEventListener('submit', function(event) {
        if (!isModified) {
            event.preventDefault();
            return false;
        }
    });

    // Event listener para mostrar o overlay quando um card é clicado
    const cards = document.querySelectorAll('.task');
    // const cards = document.querySelectorAll('.card-link');
    cards.forEach(card => {
        card.addEventListener('click', function(event) {
            const cardId = card.dataset.id;
            const cardName = card.dataset.name;
            const cardDescription = card.dataset.description;
            const cardDate = card.dataset.date;
            let cardTags = card.dataset.tags;
            let cardUser = card.dataset.user;
            let cardCheckboxes = card.dataset.checkboxes;

            try {
                cardTags = JSON.parse(cardTags);
            } catch (error) {
                cardTags = [];
            }
            try {
                cardUser = JSON.parse(cardUser);
            } catch (error) {
                cardUser = null;
            }
            try {
                cardCheckboxes = JSON.parse(cardCheckboxes);
            } catch (error) {
                cardCheckboxes = [];
            }
            console.log(cardCheckboxes);
            const [day, month, year] = cardDate.split('/');
            const formattedDate = `${year}-${month}-${day}`;
            const dateObject = new Date(formattedDate);

            document.getElementById('cardId').value = cardId;
            document.getElementById('tagCardId').value = cardId;
            document.getElementById('userCardId').value = cardId;
            document.getElementById('name').value = cardName;
            document.getElementById('deleteCardId').value = cardId;
            document.getElementById('description').value = cardDescription;
            document.getElementById('date').valueAsDate = dateObject;

            const tagsContainer = document.getElementById('tags');
            tagsContainer.innerHTML = '';
            cardTags.forEach(tag => {
                const tagElement = document.createElement('span');
                tagElement.textContent = tag.Name;
                tagElement.style.backgroundColor = tag.Color;
                tagElement.style.color = isDarkColor(tag.Color) ? 'white' : 'black';
                tagElement.style.padding = '2px 4px';
                tagElement.style.borderRadius = '4px';
                tagElement.style.marginRight = '4px';
                tagsContainer.appendChild(tagElement);
            });
            const checkboxesContainer = document.getElementById('checkbox-list');
            cardCheckboxes.forEach(checkbox => {
                console.log(checkbox.Name);
                const checkboxElement = document.createElement('p');
                checkboxElement.textContent = checkbox.Name;
                checkboxElement.style.color = 'white';
                checkboxElement.style.backgroundColor = '#272727';
                checkboxElement.style.borderRadius = 8;
                checkboxElement.classList.add('px-3', 'py-1', 'my-1', 'align-content-center');
                checkboxesContainer.appendChild(checkboxElement);
            });

            const userContainer = document.getElementById('user');
            userContainer.innerHTML = '';
            if (cardUser) {
                const userImageElement = document.createElement('img');
                if(cardUser.Picture) {
                    userImageElement.src = cardUser.Picture;
                }
                else {
                    userImageElement.src = "/default-images/default-avatar.jpg";
                }
                userImageElement.style.width = '15%';

                const userElement = document.createElement('h4');
                userElement.textContent = cardUser.LastName ? `${cardUser.FirstName} ${cardUser.LastName}` : cardUser.FirstName;

                userElement.style.color = 'white';
                userElement.style.textWrap = 'nowrap';
                userElement.style.overflow = 'hidden';
                userElement.style.textOverflow = 'ellipsis'
                userElement.style.padding = '2px 4px';
                userElement.style.borderRadius = '4px';
                userElement.style.marginRight = '4px';
                userElement.classList.add('px-3', 'm-0', 'align-content-center');

                userContainer.appendChild(userImageElement);
                userContainer.appendChild(userElement);
                addUserToCardForm.setAttribute('hidden', true);
            }

            showOverlay();
        });
    });

    overlay.addEventListener('click', function(event) {
        if (event.target === overlay || event.target.classList.contains('close-button')) {
            hideOverlay();
        }
    });

// cardForms.addEventListener('submit', function(event) {
//         event.preventDefault()
//         cardForms.querySelector('input[name=IsMemberSideBarActive]').value = IsMemberSideBarActive.value;
//         cardForms.submit();
//     });

function isDarkColor(color) {
    let r, g, b;
    if (color.length === 7) {
        r = parseInt(color.slice(1, 3), 16);
        g = parseInt(color.slice(3, 5), 16);
        b = parseInt(color.slice(5, 7), 16);
    } else if (color.length === 4) {
        r = parseInt(color[1] + color[1], 16);
        g = parseInt(color[2] + color[2], 16);
        b = parseInt(color[3] + color[3], 16);
    }}

const addLaneButton = document.querySelector('.new-lane-btn');
const laneForm = document.querySelector('.create-lane-form');
const laneInput = document.querySelector('.lane-input');

addLaneButton.addEventListener('click', function() {
    laneForm.removeAttribute('hidden');
    laneInput.focus();
});

laneInput.setAttribute('autocomplete', 'off');

document.addEventListener('click', function(event) {
    cardForms.forEach((form, index) => {
        if (!event.target.closest('.create-card-form') && !event.target.closest('.add-card-button')) {
            form.setAttribute('hidden', true);
        }})

    if (!event.target.closest('.create-lane-form') && !event.target.closest('.new-lane-btn')) {
        laneForm.setAttribute('hidden', true);
    }
});

    const addCardButtons = document.querySelectorAll('.add-card-button');
    const cardForms = document.querySelectorAll('.create-card-form');
    const cardInputs = document.querySelectorAll('.card-input');

    addCardButtons.forEach((button, index) => {
        button.addEventListener('click', function() {
            cardForms[index].removeAttribute('hidden');
            cardInputs[index].focus();
        });
    });

    cardInputs.forEach((input) => {
        input.setAttribute('autocomplete', 'off');
    });


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

    const columns = document.querySelectorAll(".card-col");

    document.addEventListener("dragstart", (e) => {
        e.target.classList.add("dragging");
    });

    document.addEventListener("dragend", (e) => {
        e.target.classList.remove("dragging");
        saveCardOrder();
    });

    columns.forEach((item) => {
        item.addEventListener("dragover", (e) => {
            e.preventDefault();
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

    const addCheckboxButton = document.getElementById('add-checkbox-button');
    const saveCheckboxesButton = document.getElementById('save-checkboxes-button');
    const checkboxList = document.getElementById('checkbox-list');

    addCheckboxButton.addEventListener('click', function() {
        const checkboxDiv = document.createElement('div');
        checkboxDiv.className = 'd-flex mb-3';

        const checkboxInput = document.createElement('input');
        checkboxInput.type = 'checkbox';

        const textInput = document.createElement('input');
        textInput.type = 'text';
        textInput.name = 'CheckboxNames';
        textInput.className = 'form-input';

        checkboxDiv.appendChild(checkboxInput);
        checkboxDiv.appendChild(textInput);

        checkboxList.appendChild(checkboxDiv);
    });

    saveCheckboxesButton.addEventListener('click', function() {
        const cardId = document.getElementById('cardId').value;
        const checkboxNames = Array.from(document.getElementsByName('CheckboxNames')).map(input => input.value);

        if (checkboxNames.length === 0) {
            alert('Por favor, adicione ao menos um checkbox.');
            return;
        }

        console.log('Iniciando salvamento dos checkboxes:', checkboxNames);

        Promise.all(checkboxNames.map(name => {
            return fetch('/Checkbox/CreateCheckbox', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ cardId: parseInt(cardId), name })
            }).then(response => response.json())
                .then(data => {
                    console.log('Resposta do servidor para checkbox', name, data);
                    return data;
                })
                .catch(error => {
                    console.error('Erro ao salvar checkbox', name, error);
                    return { success: false };
                });
        })).then(results => {
            const allSuccessful = results.every(result => result.success);
            if (allSuccessful) {
                alert('Checkboxes salvos com sucesso!');
                checkboxList.innerHTML = '';
            } else {
                alert('Falha ao salvar um ou mais checkboxes.');
                console.error('Detalhes do erro:', results);
            }
        }).catch(error => {
            console.error('Erro ao salvar checkboxes:', error);
        });
    });
});