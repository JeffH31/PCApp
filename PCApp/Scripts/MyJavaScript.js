function myDeleteDBObjectFunction(ID) {
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Home/DeleteDeck/" + ID, true);
    xhttp.send();
}

function myRemoveElementFunction(elementID) {
    var element = document.getElementById(elementID);
    element.parentNode.removeChild(element);
}

function myChangeElementFunction(cardName) {
    var el = document.getElementById('DL0');
    el.textContent = cardName;
} 