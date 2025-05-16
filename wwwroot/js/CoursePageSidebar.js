document.addEventListener('DOMContentLoaded', function () {
    const toggleSidebarButton = document.getElementById('toggleSidebarButton');
    const sidebar = document.getElementById('sidebar');

    toggleSidebarButton.addEventListener('click', function () {
        sidebar.classList.toggle('active');

        if (sidebar.classList.contains('active')) {
            toggleSidebarButton.textContent = 'Hide Participants';
        } else {
            toggleSidebarButton.textContent = 'Show Participants';
        }
    });
});
