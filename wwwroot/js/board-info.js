document.addEventListener('DOMContentLoaded', function() {
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

    const cards = document.querySelectorAll('.card-link');
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
        }

        const brightness = (r * 299 + g * 587 + b * 114) / 1000;
        return brightness < 128;
    }

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

    function saveCardOrder() {
        const lanesData = [];

        columns.forEach((lane) => {
            const laneId = lane.dataset.laneId;
            const cardIds = Array.from(lane.querySelectorAll(".item")).map((card) => card.dataset.cardId);
            lanesData.push({ laneId, cardIds });
        });

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