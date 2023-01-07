window.onload = InitializeBasicFunctions;

function InitializeBasicFunctions() {
    InitializeConfirmationWindow();
    InputRange();
    FormManagement();
    EnableProfileModal();
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

function InputRange() {
    let input = document.getElementById("product-max-price");
    let output = document.getElementById("current-price");
    output.innerHTML = parseFloat(input.value).toFixed(2);
    input.addEventListener("input", e => {
        output.innerHTML = parseFloat(e.target.value).toFixed(2);
    })
}

function EnableProfileModal() {
    let openBtn = document.getElementById("open-profile-modal");
    let modal = document.getElementById("profile-modal");
    let closeBtn = document.getElementById("close-profile-modal");

    openBtn.addEventListener("click", e => {
        console.log("LOL");
        modal.classList.add("user-profile-modal-opened");
    });

    closeBtn.addEventListener("click", e => {
        modal.classList.remove("user-profile-modal-opened");
    });

}

function FormManagement() {

    let select = document.getElementById("sort-by-select");
    let order = document.getElementById("sort-order");

    let sortByInput = document.getElementById("sort-by");
    let orderInput = document.getElementById("sorting-order");

    let form = document.getElementById("filtering-form");
    let btn = document.getElementById("apply-sorting");

    let pageOutput = document.getElementById("page-number");
    let pageBtns = document.querySelectorAll(".change-page-button");

    pageBtns.forEach(b => {
        b.addEventListener("click", e => {
            pageOutput.value = e.target.value;
            form.submit();
        })
    })

    sortByInput.value = select.value;
    orderInput.value = order.value;

    btn.addEventListener("click", e => {
        sortByInput.value = select.value;
        orderInput.value = order.value;
        form.submit();
    })

}

function ShowConfirmationWindow(name, targetUrl) {

    let window = document.getElementById("confirmation-window");
    let closebutton = document.getElementById("confirmation-window-close");
    let input = document.getElementById("confirmation-window-confirm");
    let windowData = document.getElementById("confirmation-window-text");

    input.href = targetUrl;
    windowData.innerHTML = "Usunąć: <br/> <strong>" + name + "</strong> ?";
    window.style.display = "block";
    closebutton.addEventListener("click", e => {
        window.style.display = "none";
    });
}



