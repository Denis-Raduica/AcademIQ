// Get the elements
const toggleButton = document.getElementById('toggleAnnouncementForm');
const announcementForm = document.getElementById('announcementForm');
const form = document.getElementById('announcementFormId');

// Toggle the form when the button is clicked
toggleButton.addEventListener('click', function () {
    if (announcementForm.style.display === 'none' || announcementForm.style.display === '') {
        announcementForm.style.display = 'block';  // Show the form
    } else {
        announcementForm.style.display = 'none';  // Hide the form
    }
});

// Handle the form submission
form.addEventListener('submit', async function (event) {
    event.preventDefault(); // Prevent full page reload

    const title = document.getElementById('Title').value;
    const description = document.getElementById('Description').value;
    const teacherId = document.getElementById('TeacherId').value;
    const courseId = document.querySelector('input[name="CourseId"]').value;

    const announcement = {
        Title: title,
        Description: description,
        TeacherId: teacherId.toString(),   // 🧠 Ensure it's a string
        CourseId: courseId.toString(),     // 🧠 Ensure it's a string
        // CreatedDate and IsActive handled server-side
    };

    try {
        const response = await fetch('/Announcements/CreateAnnouncementFromCoursePage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(announcement)
        });

        if (response.ok) {
            alert('Announcement created successfully!');
            location.reload(); // Reload page to show new announcement
        } else {
            const errorData = await response.json();
            alert('Error creating announcement: ' + JSON.stringify(errorData));
        }
    } catch (error) {
        alert('Error: ' + error.message);
    }
});
