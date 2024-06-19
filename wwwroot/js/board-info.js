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

function deleteTag(button) {
    var editlistItem = button.closest('li');
    document.getElementById('tagToDeleteId').value = editlistItem.dataset.id;

    var deleteTagModal = new bootstrap.Modal(document.getElementById('ConfirmDeleteTagModal'));
    deleteTagModal.show();
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

    function submitCheckboxState() {
    var checkboxes = overlay.querySelectorAll('input[type="checkbox"]');
    var checkboxStates = {};

    checkboxes.forEach(function(checkbox) {
        var checkboxToEditId = checkbox.id.replace("checkbox_", "");;
        var checkboxState = checkbox.checked;
        console.log(checkboxToEditId + ":" + checkboxState);
        checkboxStates[checkboxToEditId] = checkboxState;
    });

    var editFormData = new FormData();
    editFormData.append("boardId", 2);
    editFormData.append("checkboxesStatus", JSON.stringify(checkboxStates));
    console.log(JSON.stringify(checkboxStates))
    
    fetch('/Checkbox/EditCheckbox', {
        method: 'POST',
        body: editFormData
    })
    .then(response => {
        if (response.ok) {
            console.log('Checkbox states updated successfully!');
            // Optionally handle UI update or feedback
            // location.reload();
        } else {
            console.error('Failed to update checkbox states.');
            // Handle error case if needed
        }
    })
    .catch(error => {
        console.error('Error occurred while updating checkbox states:', error);
    });
}

    function showOverlay() {
        overlay.removeAttribute('hidden');
    }

async function hideOverlay() {
        await submitCheckboxState();
        location.reload();
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

    const cards = document.querySelectorAll('.task');
    cards.forEach(card => {
        card.addEventListener('click', function(event) {
            const boardId = card.dataset.boardid;
            const cardId = card.dataset.id;
            const cardName = card.dataset.name;
            const cardDescription = card.dataset.description;
            const cardStartDate = card.dataset.startdate;
            const cardDueDate = card.dataset.duedate;
            
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

            const [day, month, year] = cardStartDate.split('/');

            // Splitting cardDueDate into day, month, year, hour, and minute
            const [dueDay, dueMonth, dueYearTime] = cardDueDate.split('/');
            const [dueYear, time] = dueYearTime.split(' ');
            const [hour, minute] = time.split(':');

            // Formatting startDate for input element (if needed)
            const formattedStartDate = `${year}-${month}-${day}`;
            const formattedDueDateTime = `${dueYear}-${dueMonth}-${dueDay}T${hour}:${minute}`;

            // Creating Date objects
            const startDateObject = new Date(formattedStartDate);
            const dueDateTimeObject = new Date(formattedDueDateTime);


            // Setting values to HTML elements
            document.getElementById('cardId').value = cardId;
            document.getElementById('tagCardId').value = cardId;
            document.getElementById('userCardId').value = cardId;
            document.getElementById('name').value = cardName;
            document.getElementById('deleteCardId').value = cardId;
            document.getElementById('description').value = cardDescription;

            // Setting startDate input element
            document.getElementById('startDate').valueAsDate = startDateObject;

            // Setting dueDate input element (assuming it accepts date and time)
            document.getElementById('dueDate').value = formattedDueDateTime;

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
            const checkboxListNew = document.createElement('div');
            checkboxListNew.style.display = 'flex';
            checkboxListNew.style.alignContent = 'center';
            cardCheckboxes.forEach(checkbox => {
                // Create checkbox element
                var checkboxElement = document.createElement('input');
                checkboxElement.classList.add('task-checkbox');
                checkboxElement.checked = checkbox.Finished;
                checkboxElement.type = 'checkbox';
                checkboxElement.name = 'checkbox_' + checkbox.Id; // Set name attribute if needed
                checkboxElement.id = 'checkbox_' + checkbox.Id; // Set id attribute if needed

                // Create label for checkbox
                var labelElement = document.createElement('label');
                labelElement.textContent = checkbox.Name;
                labelElement.setAttribute('for', 'checkbox_' + checkbox.Id); // Set for attribute

                // Hidden input for checkbox Id
                var checkboxId = document.createElement('input');
                checkboxId.type = 'hidden';
                checkboxId.name = 'checkboxId';
                checkboxId.value = checkbox.Id;

                // Hidden input for board Id
                var checkboxCardId = document.createElement('input');
                checkboxCardId.type = 'hidden';
                checkboxCardId.name = 'cardId';
                checkboxCardId.value = cardId;
                
                var checkboxBoardId = document.createElement('input');
                checkboxBoardId.type = 'hidden';
                checkboxBoardId.name = 'boardId';
                checkboxBoardId.value = boardId;

                // Create delete button (x)
                var deleteButton = document.createElement('button');
                deleteButton.innerHTML = '<box-icon name="x" color="white"></box-icon>'; // Example with box-icon
                deleteButton.style.backgroundColor = '#131313';
                deleteButton.style.border = 'none';
                deleteButton.addEventListener('click', function() {
                    // Handle delete button click event here
                    // checkboxContainer.remove();
                    deleteForm.dispatchEvent(new Event('submit')); // Manually trigger form submission
                });

                // Create form for delete button
                var deleteForm = document.createElement('form');
                deleteForm.classList.add('ms-5'); // Add custom class if needed
                deleteForm.id = 'checkbox-delete-form'; // Set id attribute if needed
                deleteForm.addEventListener('submit', function(event) {
                    event.preventDefault(); // Prevent default form submission behavior

                    // Construct FormData object with form data
                    var formData = new FormData(deleteForm);
                    formData.append('checkboxId', checkbox.Id);
                    formData.append('boardId', boardId); // Assuming boardId is defined

                    // Perform fetch request to delete checkbox
                    fetch('/Checkbox/DeleteCheckbox', {
                        method: 'POST',
                        body: formData
                    })
                    .then(response => {
                        if (response.ok) {
                            console.log('Checkbox deleted successfully!');
                            // Optionally, update UI or reload page after successful deletion
                            location.reload();
                        } else {
                            console.error('Failed to delete checkbox.');
                        }
                    })
                    .catch(error => {
                        console.error('Error occurred while deleting checkbox:', error);
                    });
                });

                deleteForm.appendChild(deleteButton); // Append delete button to form
                deleteForm.appendChild(checkboxId); // Append checkboxId input to form
                deleteForm.appendChild(checkboxCardId); // Append cardBoardId input to form
                deleteForm.appendChild(checkboxBoardId); // Append cardBoardId input to form

                // Create div to hold checkbox, label, and form
                var checkboxContainer = document.createElement('div');
                checkboxContainer.classList.add('d-flex', 'justify-content-start'); // Adjust classes as needed
                var checkboxDiv = document.createElement('div');
                checkboxDiv.appendChild(checkboxElement); // Append checkbox to div
                checkboxDiv.appendChild(labelElement); // Append label to div
                checkboxContainer.appendChild(checkboxDiv); // Append checkbox div to container
                checkboxContainer.appendChild(deleteForm); // Append form to container

                checkboxesContainer.appendChild(checkboxContainer); // Append complete structure to main container
            });

            const checkboxes = document.querySelectorAll('.task-checkbox');
            const progressBar = document.getElementById('progress-bar');
            const progressLabel = document.getElementById('progress-label');
            const totalTasks = checkboxes.length;
            let animationInterval = null; // Variable to store animation interval

            checkboxes.forEach(checkbox => {
                checkbox.addEventListener('change', updateProgress);
            });

            if(checkboxes.length > 0){updateProgress();}

            function updateProgress() {
                let checked = 0;

                checkboxes.forEach(checkbox => {
                    if (checkbox.checked) {
                        checked++;
                    }
                });

                const percentage = (checked / totalTasks) * 100;
                const currentPercentage = parseFloat(progressLabel.textContent) || 0;

                // Clear existing animation interval if it exists
                if (animationInterval) {
                    clearInterval(animationInterval);
                }

                // Animate the progress label incrementally
                animateProgress(currentPercentage, percentage);

                progressBar.style.width = `${percentage}%`;
                progressBar.setAttribute('aria-valuenow', percentage);

                if (percentage >= 99.5) {
                    progressBar.style.backgroundColor = '#3BE73B'; // Green
                    progressBar.style.boxShadow = '0 0 10px rgba(59, 231, 59, 0.7)'; // Green shadow
                    progressBar.classList.add('completed');
                } else {
                    progressBar.style.backgroundColor = 'rgb(109, 19, 255)'; // Purple
                    progressBar.style.boxShadow = '0 0 10px rgba(109, 19, 255, 0.7)'; // Purple shadow
                    progressBar.classList.remove('completed');
                }
            }

            function animateProgress(current, target) {
                const increment = target > current ? 1 : -1;
                animationInterval = setInterval(() => {
                    if ((increment > 0 && current >= target) || (increment < 0 && current <= target)) {
                        clearInterval(animationInterval);
                        animationInterval = null; // Reset animation interval variable
                    } else {
                        current += increment;
                        progressLabel.textContent = `${current.toFixed(1)}%`;
                        progressLabel.style.color = current >= 99.5 ? '#3BE73B' : 'white';
                    }
                }, 10);
            }

            const userContainer = document.getElementById('user');
            userContainer.innerHTML = '';
            if (cardUser) {
                const userImageElement = document.createElement('img');
                if (cardUser.Picture) {
                    userImageElement.src = cardUser.Picture;
                } else {
                    userImageElement.src = "/default-images/default-avatar.jpg";
                }
                userImageElement.style.width = '15%';

                const userElement = document.createElement('h4');
                userElement.textContent = cardUser.LastName ? `${cardUser.FirstName} ${cardUser.LastName}` : cardUser.FirstName;
                userElement.style.color = 'white';
                userElement.style.textWrap = 'nowrap';
                userElement.style.overflow = 'hidden';
                userElement.style.textOverflow = 'ellipsis';
                userElement.style.padding = '2px 4px';
                userElement.style.borderRadius = '4px';
                userElement.style.marginRight = '4px';
                userElement.classList.add('px-3', 'm-0', 'align-content-center');

                // Encontrar o formulário existente
                const form = document.getElementById('removeUserForm');

                const inputBoardId = document.createElement('input');
                inputBoardId.type = 'hidden';
                inputBoardId.name = 'boardId';
                inputBoardId.value = boardId;

                const inputCardId = document.createElement('input');
                inputCardId.type = 'hidden';
                inputCardId.name = 'userCardId';
                inputCardId.value = cardId;

                form.appendChild(inputBoardId);
                form.appendChild(inputCardId);

                const removeButton = document.createElement('button');

                const removeImageElement = document.createElement('img');
                removeImageElement.src = "/Icons/default-avatar.jpg";

                userImageElement.style.width = '15%';
                removeButton.type = 'submit';
                removeButton.style.display = 'flex';
                removeButton.textContent = 'Remover'
                removeButton.style.marginLeft = '10px';
                removeButton.classList.add('m-0', 'mt-2', 'p-1', 'btn-danger', 'rounded-3');

                userContainer.appendChild(userImageElement);
                userContainer.appendChild(userElement);
                form.appendChild(removeButton);
            }
            
            showOverlay();
        });
    });

    overlay.addEventListener('click', function(event) {
        if (event.target === overlay || event.target.classList.contains('close-button')) {
            hideOverlay();
            location.reload();
        }
    });

function isDarkColor(color) {
    // Remove o símbolo # se estiver presente
    if (color.startsWith('#')) {
        color = color.slice(1);
    }

    // Converte cores hexadecimais de 3 caracteres para 6 caracteres
    if (color.length === 3) {
        color = color.split('').map(char => char + char).join('');
    }

    // Converte a cor de hexadecimal para RGB
    const r = parseInt(color.substring(0, 2), 16);
    const g = parseInt(color.substring(2, 4), 16);
    const b = parseInt(color.substring(4, 6), 16);

    // Calcula a luminância relativa
    const luminance = (0.299 * r + 0.587 * g + 0.114 * b) / 255;

    // Retorna true se a cor for escura, false se for clara
    return luminance < 0.5;
}

const addLaneButton = document.querySelector('.new-lane-btn');
const laneForm = document.querySelector('.create-lane-form');
const laneInput = document.querySelector('.lane-input');
const cardForms = document.querySelectorAll('.create-card-form'); // Assuming this is defined elsewhere

addLaneButton.addEventListener('click', function(event) {
    event.stopPropagation(); // Prevent propagation to the document
    laneForm.removeAttribute('hidden');
    laneInput.focus();
});

laneInput.setAttribute('autocomplete', 'off');

document.addEventListener('click', function(event) {
    // Hide card forms if clicking outside
    cardForms.forEach((form) => {
        if (!event.target.closest('.create-card-form') && !event.target.closest('.add-card-button')) {
            form.setAttribute('hidden', true);
        }
    });

    // Check if clicking outside the lane form and the new lane button
    if (!event.target.closest('.create-lane-form') && !event.target.closest('.new-lane-btn')) {
        laneForm.setAttribute('hidden', true);
    }
});

    const addCardButtons = document.querySelectorAll('.add-card-button');
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
        checkboxDiv.className = 'd-flex flex-column mb-3';

        const checkboxRow = document.createElement('div');
        checkboxRow.className = 'd-flex';

        const checkboxInput = document.createElement('input');
        checkboxInput.type = 'checkbox';
        checkboxInput.name = 'CheckboxFinished';
        checkboxInput.className = 'form-input';

        const textInput = document.createElement('input');
        textInput.type = 'text';
        textInput.name = 'CheckboxName';
        textInput.className = 'form-input';

        const errorMessage = document.createElement('div');
        errorMessage.className = 'text-danger mt-1';
        errorMessage.style.display = 'none';

        checkboxRow.appendChild(checkboxInput);
        checkboxRow.appendChild(textInput);

        checkboxDiv.appendChild(checkboxRow);
        checkboxDiv.appendChild(errorMessage);

        checkboxList.appendChild(checkboxDiv);
    });

    saveCheckboxesButton.addEventListener('click', function() {
        const cardId = document.getElementById('cardId').value;
        const checkboxFinished = Array.from(document.getElementsByName('CheckboxFinished')).map(input => input.checked);
        const checkboxNames = Array.from(document.getElementsByName('CheckboxName')).map(input => input.value);
        const errorMessages = checkboxList.getElementsByClassName('text-danger');

        // Reset error messages
        Array.from(errorMessages).forEach(error => error.style.display = 'none');

        // Verificar se todos os nomes dos checkboxes não estão vazios
        let hasEmptyName = false;
        checkboxNames.forEach((name, index) => {
            if (name.trim() === '') {
                errorMessages[index].textContent = 'Por favor, preencha o nome do checkbox.';
                errorMessages[index].style.display = 'block';
                hasEmptyName = true;
            }
        });

        if (hasEmptyName) {
            return;
        }

        if (checkboxNames.length === 0) {
            alert('Por favor, adicione ao menos um checkbox.');
            return;
        }

        console.log('Iniciando salvamento dos checkboxes:', checkboxNames);

        Promise.all(checkboxNames.map((name, index) => {
            return fetch('/Checkbox/CreateCheckbox', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 
                    cardId: parseInt(cardId), 
                    name, 
                    finished: checkboxFinished[index] // Adicionando o valor booleano correspondente
                })
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
                location.reload();
            } else {
                alert('Falha ao salvar um ou mais checkboxes.');
                console.error('Detalhes do erro:', results);
            }
        }).catch(error => {
            console.error('Erro ao salvar checkboxes:', error);
        });
    });

});