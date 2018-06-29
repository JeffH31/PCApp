function myDeleteDBObjectFunction(ID) {
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Home/DeleteDeck/" + ID, true);
    xhttp.send();
}

function myRemoveElementFunction(elementID) {
    var element = document.getElementById(elementID);
    element.parentNode.removeChild(element);
}