document.querySelector('#side-menu-btn').onclick = function () {
    document.querySelector(".sideBar").classList.toggle('active')
};

function leaveProject(button) {
    var listItem = button.closest('li');
    document.getElementById('projectToLeaveId').value = listItem.dataset.id;

    var leaveProjectModal = new bootstrap.Modal(document.getElementById('ConfirmLeaveProjectModal'));
    leaveProjectModal.show();
}

function reveal() {
    var reveals = document.querySelectorAll(".reveal");
    for (var i = 0; i < reveals.length; i++) {
        var windowHeight = window.innerHeight;
        var elementTop = reveals[i].getBoundingClientRect().top;
        var elementVisible = 150;
        if (elementTop < windowHeight - elementVisible) {
        reveals[i].classList.add("active");
        } else {
        reveals[i].classList.remove("active");
        }
    }
}
window.addEventListener("scroll", reveal);

// To check the scroll position on page load
reveal();

// $(document).ajaxStart(function() {
//     $('#loading').show(); // Mostra a tela de carregamento quando uma requisição AJAX é iniciada
// });

// $(document).ajaxStop(function() {
//     $('#loading').hide(); // Oculta a tela de carregamento quando todas as requisições AJAX são concluídas
// });