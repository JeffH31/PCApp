function deleteDeck(ID) {
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Home/DeleteDeck/" + ID, true);
    xhttp.send();
}

function removeElement(elementID) {
    var el = document.getElementById(elementID);
    el.parentNode.removeChild(el);
}

function addToDeckList(cardName) {
    var el;
    var Id;    
    for (i = 0; i < 10; i++) {
        Id = "DL" + [i];        
        el = document.getElementById(Id);
        if (el.innerHTML.trim() != cardName) {
            if (el.innerHTML.trim() === "") {
                el.textContent = cardName;
                i = 10;//end loop
            } else if (document.getElementById("DL9").innerHTML.trim() != "") {
                alert("DECK HAS MAX NUMBER OF CARDS");
                i = 10;//end loop
            }
        } else {
            alert("THIS CARD HAS ALREADY BEEN ADDED");
            i = 10;//end loop
        }
    }
}

function removeText() {
    var Id;
    var el;
    for (i = 9; i > -1; i--) {
        Id = "DL" + [i];
        el = document.getElementById(Id);
        if (el.innerHTML.trim() != "") {
            el.textContent = "";
            i = -1;//end loop
        } else if (document.getElementById("DL0").innerHTML.trim() === "") {
            alert("NO CARD TO REMOVE");
            i = -1;//end loop
        }
    }
}

function sendArray() {
    var Id;
    var stringArray = new Array();
    //fill array with CardNames
    for (i = 0; i < 10; i++) {
        Id = "DL" + [i];
        stringArray[i] = document.getElementById(Id).textContent.trim();
    }    
    //Add DeckName to array
    stringArray[i] = document.getElementById("DeckName").value.trim();
    //send array with ajax to CreateDeck
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Home/CreateDeck",
        traditional: true,
        data: {
            DeckName: stringArray[10],
            Cards: [ stringArray[0], stringArray[1], stringArray[2], stringArray[3], stringArray[4],
                stringArray[5], stringArray[6], stringArray[7], stringArray[8], stringArray[9] ]
        },        
        success: function (data) {
            alert(data.Result);
            //Redirect to User Profile
            if (data.Result === "DECK ADDED") {
                window.location = data.url;
            }
        }
    });
}

function rollDie() {
    var die = document.getElementById("Die");
    var tracker = document.getElementById("Tracker");
    var number = tracker.innerHTML;
    var result = Math.floor(Math.random() * 6);
    //var sound
    if (result < 4) {
        //change die face
        die.src = "../../Content/PlanechasePics/Die/white.png";
        //increase tracker
        number++;
        tracker.innerHTML = number;
    } else if (result === 4) {
        //change die face
        die.src = "../../Content/PlanechasePics/Die/chaos.png";
        //pulse image
        //play sound clip
        //sound = new sound("chaos.mp3");
        //sound.play()
        //increase tracker
        number++;
        tracker.innerHTML = number;
    } else {
        //change die face
        die.src = "../../Content/PlanechasePics/Die/planeswalker.png";
        //pulse image
        //play sound clip
        //sound = new sound("chaos.mp3");
        //sound.play()
        //increase tracker
        number++;
        tracker.innerHTML = number;
    }
}

function resetTracker() {
    var tracker = document.getElementById("Tracker");
    tracker.innerHTML = "0";
}