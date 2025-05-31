// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Theme Toggle Logic
document.addEventListener("DOMContentLoaded", () => {
    console.log("DOM fully loaded and parsed"); // Debug log 1
    const themeToggleButton = document.getElementById("theme-toggle-button");
    const htmlElement = document.documentElement; // Target <html> element

    if (!themeToggleButton) {
        console.error("Theme toggle button not found!"); // Debug log 2
        return; // Exit if button not found
    }
    console.log("Theme toggle button found:", themeToggleButton); // Debug log 3

    const currentTheme = localStorage.getItem("theme") ? localStorage.getItem("theme") : null;
    console.log("Current theme from localStorage:", currentTheme); // Debug log 4

    // Apply saved theme on initial load
    if (currentTheme === "light") {
        htmlElement.classList.add("light-theme");
        console.log("Applied light theme on load"); // Debug log 5
    } else {
        htmlElement.classList.remove("light-theme");
        console.log("Applied dark theme (default) on load"); // Debug log 6
    }

    // Add click listener to the button
    themeToggleButton.addEventListener("click", () => {
        console.log("Theme toggle button clicked!"); // Debug log 7
        // Toggle the .light-theme class on the <html> element
        htmlElement.classList.toggle("light-theme");
        console.log("Toggled light-theme class. Current classes:", htmlElement.className); // Debug log 8

        // Update localStorage
        let theme = "dark"; // Default to dark
        if (htmlElement.classList.contains("light-theme")) {
            theme = "light";
        }
        localStorage.setItem("theme", theme);
        console.log("Saved theme to localStorage:", theme); // Debug log 9
    });
});

