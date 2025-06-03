document.addEventListener("DOMContentLoaded", function () {
    const tabs = document.querySelectorAll(".tab-button");
    const forms = document.querySelectorAll(".form-section");
    const tables = document.querySelectorAll(".table-section");
    
    tabs.forEach(tab => {
        tab.addEventListener("click", () => {
            tabs.forEach(t => t.classList.remove("active-tab"));
            forms.forEach(f => f.classList.remove("active-form"));
            tables.forEach(f => f.classList.remove("active-table"));
            tab.classList.add("active-tab");
            const targetId = tab.getAttribute("data-target");
            document.getElementById(targetId).classList.add("active-form");
            document.getElementById(targetId).classList.add("active-table");
        });
    });
});
