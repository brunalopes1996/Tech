// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Function to update logo visibility based on current theme
function updateLogoVisibility() {
    const htmlElement = document.documentElement;
    const logoDark = document.querySelector('.logo-dark');
    const logoLight = document.querySelector('.logo-light');

    if (htmlElement.classList.contains('light-theme')) {
        // Light theme: show light logo, hide dark logo
        if (logoDark) logoDark.style.display = 'none';
        if (logoLight) logoLight.style.display = 'inline';
    } else {
        // Dark theme: show dark logo, hide light logo
        if (logoDark) logoDark.style.display = 'inline';
        if (logoLight) logoLight.style.display = 'none';
    }
}

// Theme Toggle Logic
document.addEventListener("DOMContentLoaded", () => {
    console.log("DOM fully loaded and parsed");
    const themeToggleButton = document.getElementById("theme-toggle-button");
    const htmlElement = document.documentElement;

    if (!themeToggleButton) {
        console.error("Theme toggle button not found!");
        return;
    }
    console.log("Theme toggle button found:", themeToggleButton);

    // Get saved theme from localStorage
    const currentTheme = localStorage.getItem("theme") ? localStorage.getItem("theme") : null;
    console.log("Current theme from localStorage:", currentTheme);

    // Apply saved theme on initial load
    if (currentTheme === "light") {
        htmlElement.classList.add("light-theme");
        console.log("Applied light theme on load");
    } else {
        htmlElement.classList.remove("light-theme");
        console.log("Applied dark theme (default) on load");
    }

    // Update logo visibility after applying initial theme
    updateLogoVisibility();

    // Add click listener to the theme toggle button
    /*themeToggleButton.addEventListener("click", () => {
        console.log("Theme toggle button clicked!");
        
        // Toggle the .light-theme class on the <html> element
        htmlElement.classList.toggle("light-theme");
        console.log("Toggled light-theme class. Current classes:", htmlElement.className);

        // Determine current theme and save to localStorage
        let theme = "dark"; // Default to dark
        if (htmlElement.classList.contains("light-theme")) {
            theme = "light";
        }
        localStorage.setItem("theme", theme);
        console.log("Saved theme to localStorage:", theme);

        // Update logo visibility after theme change
        updateLogoVisibility();
    });*/
});

