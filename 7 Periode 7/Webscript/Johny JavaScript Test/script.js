function Plane(image, name, speed, chairs) {
    this.image = image;
    this.name = name;
    this.speed = speed;
    this.chairs = chairs;
}

var buttonManiaNumber = 0;
var planets = ["Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune"];
var planetIndex = 0;
var boeing737 = new Plane("737.PNG", "Boeing 737", 850, 186)
var boeing747 = new Plane("747.PNG", "Boeing 747", 920, 408)
var boeing777 = new Plane("777.PNG", "Boeing 777", 905, 408)

function dog(isVisible) {

    if (isVisible) {
        document.getElementById("dog").style.display = "initial";
    }
    else {
        document.getElementById("dog").style.display = "none";
    }
}

function buttonMania(mode) {
    switch (mode) {
        case 0:
            buttonManiaNumber = 0;
            break;
        case 1:
            buttonManiaNumber += 1;
            break;
        case 2:
            buttonManiaNumber *= 3;
            break;
    }
    document.getElementById("button-mania-number").innerText = buttonManiaNumber;
}

function displayPlanes() {
    displayPlane(boeing737, "boeing-737");
    displayPlane(boeing747, "boeing-747");
    displayPlane(boeing777, "boeing-777");
}

function displayPlane(plane, id)
{
    document.getElementById(id).innerHTML = "<img src='img/" + plane.image + "' width=125px><br><p style='font-weight: bold;'>" + plane.name + "</p><p>speed: " + plane.speed + "<br>chairs:" + plane.chairs + "</p>"
}

function changePlanetIndex(forward) {
    if (forward && planetIndex < planets.length - 1) {
        planetIndex++;
    }
    else if (!forward && planetIndex > 0) {
        planetIndex--;
    }

    if (planets.length == 0) {
        document.getElementById("current-planet").innerText = "All planets are destroyed!";
    }
    else {
        document.getElementById("current-planet").innerText = planets[planetIndex];
    }
}

function destroyPlanet() {
    if (planets.length > 0) {
        planets.shift();
        changePlanetIndex(false);
    }
}

function addPlanet(isNamed) {
    if (isNamed) {
        planets.push(document.getElementById("planet-name").value);
    }
    else {
        planets.push('planet');
    }
    if (planets.length == 1) {
        changePlanetIndex(false);
    }
}

function showPlanets() {
    var planetList = "";
    for (i = 0; i < planets.length; i++) {
        planetList += planets[i] + " ";
    }
    document.getElementById("planet-viewer").innerText = planetList;
}