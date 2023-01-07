window.onload += EnableProfileModal;

function EnableProfileModal() {
    console.info("Sdad");
    let openBtn = document.getElementById("open-profile-modal");
    let modal = document.getElementById("profile-modal");
    let closeBtn = document.getElementById("close-profile-modal");

    openBtn.addEventListener("click", e => {
        console.log("LOL");;
        modal.classList.add("user-profile-modal-opened");
    });

    closeBtn.addEventListener("click", e => {
        modal.classList.remove("user-profile-modal-opened");
    });

}