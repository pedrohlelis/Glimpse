document.addEventListener('DOMContentLoaded', function() {
    const sectionButtons = document.querySelectorAll('.section-btn');

    sectionButtons.forEach(sectionBtn => {
        sectionBtn.addEventListener('click', function() {
            let sectionId = sectionBtn.id.replace('-btn', '');

            const activeSection = document.getElementsByClassName('active-section')[0];
            activeSection.classList.remove('active-section');
            activeSection.classList.add('inactive-section');
            const sectionToShow = document.getElementById('section-' + sectionId);

            sectionToShow.classList.add('active-section');
            sectionToShow.classList.remove('inactive-section');
        });
    });
});

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

document.getElementById('project-roles-btn').addEventListener('click', openRolesModal);

let memberSideMenuBtn = document.querySelector('#member-side-menu-btn')
let memberSideMenu = document.querySelector('.memberSideMenu')
let mainContent = document.querySelector(".main-content")
let memberDivs = document.querySelectorAll(".member-div")
let rolesDivs = document.querySelectorAll(".role-container")
let invButton = document.querySelector(".invite-btn")
let IsMemberSideBarActive = document.querySelector(".IsMemberSideBarActive")

if (memberDivs.length === 0) {
console.error('No memberDiv elements found');
}

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
    } else {
        IsMemberSideBarActive.value = 'false'
    }
}

let toggleMemberBarDiv = document.querySelector('.toggleMemberBarDiv');
if (toggleMemberBarDiv != null)
{
    ToggleMemberSideBar()
};

document.addEventListener('DOMContentLoaded', function() {

    const board = document.querySelector(".lanes");

    const saveCardOrderForm = document.querySelector('.save-card-order-form');
    const cardOrderInput = saveCardOrderForm.querySelector('input[name="taskIndexDictionary"]');

    const saveLaneOrderForm = document.querySelector('.save-lane-order-form');
    const laneOrderInput = saveLaneOrderForm.querySelector('input[name="laneIndexDictionary"]');

    saveCardOrderForm.addEventListener('submit', function(event) {
        event.preventDefault();
        
        const formData = new FormData(saveCardOrderForm);

        fetch(saveCardOrderForm.action, {
            method: 'POST',
            body: formData
        })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                console.error('Failed to save card order.');
            }
        })
        .catch(error => {
            console.error('An error occurred:', error);
        });
    });

    saveLaneOrderForm.addEventListener('submit', function(event) {
        event.preventDefault();
        
        const formData = new FormData(saveLaneOrderForm);

        fetch(saveLaneOrderForm.action, {
            method: 'POST',
            body: formData
        })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                console.error('Failed to save card order.');
            }
        })
        .catch(error => {
            console.error('An error occurred:', error);
        });
    });

    function updateCardOrderInput() {
        const swimLanes = document.querySelectorAll('.swim-lane');
        let isMemberSideBarActive = saveCardOrderForm.querySelector('input[name=IsMemberSideBarActive]')
        let taskIndexDictionary = {};

        swimLanes.forEach((lane) => {
            const laneId = lane.dataset.id;
            const cards = lane.querySelectorAll('.task');

            cards.forEach((card, index) => {
                const taskId = card.dataset.id;
                const indexPosition = index + 1;
                
                if (!taskIndexDictionary[taskId]) {
                    taskIndexDictionary[taskId] = [];
                }

                taskIndexDictionary[taskId].push(String(laneId), String(indexPosition));
            });
        });

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
        let laneIndexDictionary = {};

        lanes.forEach((lane) => {
            const swimLanes = lane.querySelectorAll('.swim-lane');

            swimLanes.forEach((swimLane, index) => {
                const swimLaneId = swimLane.dataset.id;
                const indexPosition = index + 1;

                laneIndexDictionary[swimLaneId] = indexPosition;
            });
        });
        if(memberSideMenu.classList.contains('active')){
            isMemberSideBarActive.value = true;
        }
        else{
            isMemberSideBarActive.value = false;
        }
        laneOrderInput.value = JSON.stringify(laneIndexDictionary);
    }

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

const lanes = document.querySelectorAll(".swim-lane");

lanes.forEach((lane) => {
    lane.addEventListener("dragstart", (e) => {
        if (e.target.classList.contains("swim-lane")) {
            lane.classList.add("is-dragging-lane");
        }
        e.stopPropagation();
    });

    lane.addEventListener("dragend", (e) => {
        if (e.target.classList.contains("swim-lane")) {
            lane.classList.remove("is-dragging-lane");
            updateLaneOrderInput();
            saveLaneOrderForm.submit();
        }
        e.stopPropagation();
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
            checkboxStates[checkboxToEditId] = checkboxState;
            });

            var editFormData = new FormData();
            editFormData.append("boardId", 2);
            editFormData.append("checkboxesStatus", JSON.stringify(checkboxStates));
            
            fetch('/Checkbox/EditCheckbox', {
                method: 'POST',
                body: editFormData
            })
            .then(response => {
                if (response.ok) {
                } else {
                    console.error('Failed to update checkbox states.');
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
        submitCheckboxState();
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

            const [dueDay, dueMonth, dueYearTime] = cardDueDate.split('/');
            const [dueYear, time] = dueYearTime.split(' ');
            const [hour, minute] = time.split(':');

            const formattedStartDate = `${year}-${month}-${day}`;
            const formattedDueDateTime = `${dueYear}-${dueMonth}-${dueDay}T${hour}:${minute}`;

            const startDateObject = new Date(formattedStartDate);
            const dueDateTimeObject = new Date(formattedDueDateTime);

            document.getElementById('cardId').value = cardId;
            document.getElementById('tagCardId').value = cardId;
            document.getElementById('userCardId').value = cardId;
            document.getElementById('name').value = cardName;
            document.getElementById('deleteCardId').value = cardId;
            document.getElementById('description').value = cardDescription;

            document.getElementById('startDate').valueAsDate = startDateObject;

            document.getElementById('dueDate').value = formattedDueDateTime;
            if (dueDateTimeObject <= Date.now()) {
                document.getElementById('dueDate').style.backgroundColor = '#8a1c2e';
            } else {
                document.getElementById('dueDate').style.backgroundColor = '#23782e';
            }

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
                var divWrapper = document.createElement('div');
                divWrapper.classList.add('checkbox-wrapper-12');

                var divCbx = document.createElement('div');
                divCbx.classList.add('cbx');

                var checkboxElement = document.createElement('input');
                checkboxElement.id = 'cbx-12';
                checkboxElement.type = 'checkbox';
                checkboxElement.classList.add('task-checkbox', 'me-3');
                checkboxElement.checked = checkbox.Finished;
                checkboxElement.name = 'checkbox_' + checkbox.Id;
                checkboxElement.id = 'checkbox_' + checkbox.Id;

                divCbx.appendChild(checkboxElement);

                var labelElement = document.createElement('label');
                labelElement.setAttribute('for', 'cbx-12');
                divCbx.appendChild(labelElement);

                var svgElement = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
                svgElement.setAttribute('width', '15');
                svgElement.setAttribute('height', '14');
                svgElement.setAttribute('viewBox', '0 0 15 14');
                svgElement.setAttribute('fill', 'none');
                var pathElement = document.createElementNS('http://www.w3.org/2000/svg', 'path');
                pathElement.setAttribute('d', 'M2 8.36364L6.23077 12L13 2');
                svgElement.appendChild(pathElement);
                divCbx.appendChild(svgElement);

                divWrapper.appendChild(divCbx);

                var svgDefs = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
                svgDefs.setAttribute('xmlns', 'http://www.w3.org/2000/svg');
                svgDefs.setAttribute('version', '1.1');

                var defsElement = document.createElementNS('http://www.w3.org/2000/svg', 'defs');
                var filterElement = document.createElementNS('http://www.w3.org/2000/svg', 'filter');
                filterElement.setAttribute('id', 'goo-12');

                var feGaussianBlur = document.createElementNS('http://www.w3.org/2000/svg', 'feGaussianBlur');
                feGaussianBlur.setAttribute('in', 'SourceGraphic');
                feGaussianBlur.setAttribute('stdDeviation', '4');
                feGaussianBlur.setAttribute('result', 'blur');
                filterElement.appendChild(feGaussianBlur);

                var feColorMatrix = document.createElementNS('http://www.w3.org/2000/svg', 'feColorMatrix');
                feColorMatrix.setAttribute('in', 'blur');
                feColorMatrix.setAttribute('mode', 'matrix');
                feColorMatrix.setAttribute('values', '1 0 0 0 0 0 1 0 0 0 0 0 1 0 0 0 0 0 22 -7');
                feColorMatrix.setAttribute('result', 'goo-12');
                filterElement.appendChild(feColorMatrix);

                var feBlend = document.createElementNS('http://www.w3.org/2000/svg', 'feBlend');
                feBlend.setAttribute('in', 'SourceGraphic');
                feBlend.setAttribute('in2', 'goo-12');
                filterElement.appendChild(feBlend);

                defsElement.appendChild(filterElement);
                svgDefs.appendChild(defsElement);

                divWrapper.appendChild(svgDefs);

                var labelElement = document.createElement('label');
                labelElement.textContent = checkbox.Name;
                labelElement.setAttribute('for', 'checkbox_' + checkbox.Id);

                var checkboxId = document.createElement('input');
                checkboxId.type = 'hidden';
                checkboxId.name = 'checkboxId';
                checkboxId.value = checkbox.Id;

                var checkboxCardId = document.createElement('input');
                checkboxCardId.type = 'hidden';
                checkboxCardId.name = 'cardId';
                checkboxCardId.value = cardId;
                
                var checkboxBoardId = document.createElement('input');
                checkboxBoardId.type = 'hidden';
                checkboxBoardId.name = 'boardId';
                checkboxBoardId.value = boardId;

                var deleteButton = document.createElement('button');
                deleteButton.innerHTML = '<box-icon name="x" color="white"></box-icon>';
                deleteButton.style.backgroundColor = '#1b1b1b';
                deleteButton.style.border = 'none';
                deleteButton.style.alignContent = 'center';
                deleteButton.addEventListener('click', function() {
                    deleteForm.dispatchEvent(new Event('submit'));
                });

                var deleteForm = document.createElement('form');
                deleteForm.classList.add('ms-5');
                deleteForm.id = 'checkbox-delete-form';
                deleteForm.addEventListener('submit', function(event) {
                    event.preventDefault();

                    var formData = new FormData(deleteForm);
                    formData.append('checkboxId', checkbox.Id);
                    formData.append('boardId', boardId);

                    fetch('/Checkbox/DeleteCheckbox', {
                        method: 'POST',
                        body: formData
                    })
                    .then(response => {
                        if (response.ok) {
                            location.reload();
                        } else {
                            console.error('Failed to delete checkbox.');
                        }
                    })
                    .catch(error => {
                        console.error('Error occurred while deleting checkbox:', error);
                    });
                });

                deleteForm.appendChild(deleteButton);
                deleteForm.appendChild(checkboxId);
                deleteForm.appendChild(checkboxCardId);
                deleteForm.appendChild(checkboxBoardId);

                var checkboxContainer = document.createElement('div');
                checkboxContainer.classList.add('d-flex', 'justify-content-start', 'p-2', 'align-items-center');

                var checkboxDiv = document.createElement('div');
                checkboxContainer.style = 'background-color: #1b1b1b; border-radius: 6px; border: 2px; border-color: #373737; ';
                
                divWrapper.style.marginRight = '3%';
                divWrapper.style.marginLeft = '1%';
                checkboxDiv.appendChild(divWrapper);
                checkboxDiv.appendChild(labelElement);
                checkboxDiv.style.flexGrow = '1';
                checkboxDiv.style.paddingTop = '1%';
                checkboxDiv.style.paddingBottom = '1%';
                checkboxDiv.style.display = 'flex';
                
                checkboxContainer.appendChild(checkboxDiv);
                checkboxContainer.appendChild(deleteForm);

                checkboxesContainer.appendChild(checkboxContainer);
            });

            const checkboxes = document.querySelectorAll('.task-checkbox');
            const progressBar = document.getElementById('progress-bar');
            const progressLabel = document.getElementById('progress-label');
            const totalTasks = checkboxes.length;
            let animationInterval = null;

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

                if (animationInterval) {
                    clearInterval(animationInterval);
                }

                animateProgress(currentPercentage, percentage);

                progressBar.style.width = `${percentage}%`;
                progressBar.setAttribute('aria-valuenow', percentage);

                if (percentage >= 99.5) {
                    progressBar.style.backgroundColor = '#3BE73B';
                    progressBar.style.boxShadow = '0 0 10px rgba(59, 231, 59, 0.7)';
                    progressBar.classList.add('completed');
                } else {
                    progressBar.style.backgroundColor = 'rgb(109, 19, 255)';
                    progressBar.style.boxShadow = '0 0 10px rgba(109, 19, 255, 0.7)';
                    progressBar.classList.remove('completed');
                }
            }

            function animateProgress(current, target) {
                const increment = target > current ? 1 : -1;
                animationInterval = setInterval(() => {
                    if ((increment > 0 && current >= target) || (increment < 0 && current <= target)) {
                        clearInterval(animationInterval);
                        animationInterval = null;
                    } else {
                        current += increment;
                        progressLabel.textContent = `${current.toFixed(1)}%`;
                        progressLabel.style.color = current >= 99.5 ? '#3BE73B' : 'white';
                    }
                }, 10);
            }

            const userContainer = document.getElementById('user');
            userContainer.style.marginTop = '5%';
            userContainer.innerHTML = '';
            if (cardUser) {
                const userImageElement = document.createElement('img');
                if (cardUser.Picture) {
                    userImageElement.src = cardUser.Picture;
                } else {
                    userImageElement.src = "/default-images/default-avatar.jpg";
                }
                userImageElement.style.width = '15%';
            
                const userElement = document.createElement('h5');
                userElement.textContent = cardUser.LastName ? `${cardUser.FirstName} ${cardUser.LastName}` : cardUser.FirstName;
                userElement.style.color = 'white';
                userElement.style.textWrap = 'nowrap';
                userElement.style.overflow = 'hidden';
                userElement.style.textOverflow = 'ellipsis';
                userElement.style.padding = '2px 4px';
                userElement.style.borderRadius = '4px';
                userElement.style.marginRight = '4px';
                userElement.classList.add('px-3', 'm-0', 'align-content-center');

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

    overlay.addEventListener('click', async function(event) {
        if (event.target === overlay || event.target.classList.contains('close-button')) {
            await hideOverlay();
            location.reload();
        }
    });

function isDarkColor(color) {
    if (color.startsWith('#')) {
        color = color.slice(1);
    }

    if (color.length === 3) {
        color = color.split('').map(char => char + char).join('');
    }

    const r = parseInt(color.substring(0, 2), 16);
    const g = parseInt(color.substring(2, 4), 16);
    const b = parseInt(color.substring(4, 6), 16);

    const luminance = (0.299 * r + 0.587 * g + 0.114 * b) / 255;

    return luminance < 0.5;
}

const addLaneButton = document.querySelector('.new-lane-btn');
const laneForm = document.querySelector('.create-lane-form');
const laneInput = document.querySelector('.lane-input');
const cardForms = document.querySelectorAll('.create-card-form');

addLaneButton.addEventListener('click', function(event) {
    event.stopPropagation();
    event.stopPropagation();
    laneForm.removeAttribute('hidden');
    laneInput.focus();
});

laneInput.setAttribute('autocomplete', 'off');

document.addEventListener('click', function(event) {
    cardForms.forEach((form) => {
        if (!event.target.closest('.create-card-form') && !event.target.closest('.add-card-button')) {
            form.setAttribute('hidden', true);
        }
    });

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

        Array.from(errorMessages).forEach(error => error.style.display = 'none');

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

        Promise.all(checkboxNames.map((name, index) => {
            return fetch('/Checkbox/CreateCheckbox', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 
                    cardId: parseInt(cardId), 
                    name, 
                    finished: checkboxFinished[index]
                })
            }).then(response => response.json())
                .then(data => {
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

    const toggleButtons = document.querySelectorAll('.dropdown-glimpse-toggle');

    toggleButtons.forEach(function(toggleButton) {
        toggleButton.addEventListener('click', function(event) {
            document.querySelectorAll('.dropdown-glimpse').forEach(function(dropdown) {
                dropdown.style.display = 'none';
            });

            const dropdownBody = toggleButton.nextElementSibling;
            dropdownBody.style.display = dropdownBody.style.display === 'block' ? 'none' : 'block';

            event.stopPropagation();
        });
    });

    document.addEventListener('click', function(event) {
        if (!event.target.closest('.dropdown-glimpse') && !event.target.closest('.dropdown-glimpse-toggle')) {
            document.querySelectorAll('.dropdown-glimpse').forEach(function(dropdown) {
                dropdown.style.display = 'none';
            });
        }
    });

    const dropdownItems = document.querySelectorAll('.dropdown-glimpse-item');
    dropdownItems.forEach(function(item) {
        item.addEventListener('click', function() {
            item.closest('.dropdown-glimpse').style.display = 'none';
        });
    });
    
});