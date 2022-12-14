window.onload = InitializeBasicFunctions;

function InitializeBasicFunctions() {
    InitializeConfirmationWindow();
}

function InitializeConfirmationWindow() {
    let actionButtons = document.getElementsByClassName("-confirmation-window");
    Array.prototype.forEach.call(actionButtons, b => {
        b.addEventListener("click", c => {
            if (c.target.attributes.formaction === undefined) {
                ShowConfirmationWindow(c.target.parentElement.dataset.name, c.target.parentElement.attributes.formaction.nodeValue);
            } else {
                ShowConfirmationWindow(c.target.dataset.name, c.target.attributes.formaction.nodeValue);
            }
        });
    });
}

function ShowConfirmationWindow(name, targetUrl) {

    let window = document.getElementById("confirmation-window");
    let closebutton = document.getElementById("confirmation-window-close");
    let input = document.getElementById("confirmation-window-confirm");
    let windowData = document.getElementById("confirmation-window-text");

    input.href = targetUrl;
    windowData.innerHTML = "Usunąć produkt:<br/> <strong>" + name + "</strong> ?";
    window.style.display = "block";
    closebutton.addEventListener("click", e => {
        window.style.display = "none";
    });
}

