window.onload = InitializeFunctions;

function InitializeFunctions() {

    let addBtn = document.getElementById("information-add");
    let deleteBtn = document.getElementById("information-remove");

    let inputContainer = document.getElementById("inputs-container");


    addBtn.addEventListener("click", e => {
        let child = CreateInput();
        inputContainer.appendChild(child);
    })

    deleteBtn.addEventListener("click", e => {
        if (inputContainer.children.length != 1) {
            inputContainer.lastChild.remove();
        }
    })
    
}

function CreateInput() {
    let element = document.createElement("input");
    element.setAttribute("name", INPUTNAME);
    element.setAttribute("type", "text");
    element.classList.add("aditional-information-input");
    return element;
}