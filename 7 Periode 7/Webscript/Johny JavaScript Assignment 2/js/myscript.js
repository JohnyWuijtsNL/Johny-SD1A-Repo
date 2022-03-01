let size = 100;
let text = ' is an acronym for "PHP Hypertext Processor. The \'PHP\' in "PHP Hypertext Processor"'
let currentText = "PHP"
let currentLetter = 0
let clickerCount = 0;

function loaded() {
    document.getElementById("p1").textContent = "page loaded!";
}

function button1Clicked() {
    document.getElementById("button1").insertAdjacentHTML("afterend", "<p>You clicked!</p>");
}

function button2Clicked() {
    let p1 = document.getElementById("p1");
    p1.style.color = "rgb(" + Math.floor(Math.random() * 256) + ", " + Math.floor(Math.random() * 256) + ", " + Math.floor(Math.random() * 256) + ")";
    size += 10;
    p1.style.fontSize = size + "%";
}

function button3Clicked() {
    document.getElementById("p2").remove();
}

function button4Clicked(animal) {
    let image = document.getElementById("image");
    if (animal == 0)
    {
        image.src = "img/cow.png";
    }
    if (animal == 1)
    {
        image.src = "img/cat.jpg";
    }
    if (animal == 2)
    {
        image.src = "img/dog.jpg";
    }
}

function button5Clicked() {
    let button = document.getElementById("button-hide");
    let image = document.getElementById("image");
    if (button.textContent == "hide images")
    {
        button.textContent = "show images";
        image.style.visibility = "hidden";
    }
    else
    {
        button.textContent = "hide images";
        image.style.visibility = "visible";
    }
}

let generation = setInterval(function() 
{
    let paragraph = document.getElementById("runtime");
    if (currentLetter < text.length)
    {
        currentText += text[currentLetter];
        currentLetter++
        paragraph.textContent = currentText;
    }
    else
    {
        currentLetter = 0;
    }
}, 50);

function coockieClicker() {
    clickerCount++;
    document.getElementById("clicker").textContent = "You clicked this button " + clickerCount + " times!";
}