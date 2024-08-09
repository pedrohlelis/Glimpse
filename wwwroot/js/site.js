function leaveProject(button) {
    var listItem = button.closest('li');
    document.getElementById('projectToLeaveId').value = listItem.dataset.id;

    var leaveProjectModal = new bootstrap.Modal(document.getElementById('ConfirmLeaveProjectModal'));
    leaveProjectModal.show();
}
