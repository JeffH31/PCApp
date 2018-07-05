function deleteDeck(ID) {
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Home/DeleteDeck/" + ID, true);
    xhttp.send();
}

function removeElement(elementID) {
    var el = document.getElementById(elementID);
    el.parentNode.removeChild(element);
}

function addToDeckList(cardName) {
    var el
    var Id
    for (i = 0; i < 10; i++) {
        Id = "DL" + [i];
        el = document.getElementById(Id);
        if (el.innerHTML.trim() === "") {
            el.textContent = cardName;
            i = 10;
        }
    }
}

function removeText() {
    var Id
    var el
    for (i = 9; i > -1; i--) {
        Id = "DL" + [i];
        el = document.getElementById(Id);
        if (el.innerHTML.trim() != "") {
            el.textContent = "";
            i = -1;
        }
    }
}