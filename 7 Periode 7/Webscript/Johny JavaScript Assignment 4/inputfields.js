let inputs = [];
let fibonacci = [1, 1];
let fibonacciString = "1, 1";

function initialize() {
  for (let i = 0; i < 9; i++) {
    inputs.push(document.getElementById("input" + i));
    inputs[i].addEventListener("keyup", function (event) {
      if (event.keyCode === 13) {
        changeText(i, this.value);
      }
    });
  }
}

function changeText(textNumber, inputText) {
  if (textNumber == 4) {
    text = document.getElementById("p3");
  } else if (textNumber == 8) {
    text = document.getElementById("p7");
  } else {
    text = document.getElementById("p" + textNumber);
  }
  switch (textNumber) {
    case 0:
      text.innerText = inputText;
      break;
    case 1:
      text.innerText = inputText * 2;
      break;
    case 2:
      text.innerText = "€" + (inputText * 0.901026).toFixed(2);
      break;
    case 3:
      var inputText2 = document.getElementById("input4").value;
      text.innerText = inputText * inputText2;
      break;
    case 4:
      var inputText2 = document.getElementById("input3").value;
      text.innerText = inputText * inputText2;
      break;
    case 5:
      text.innerText = (inputText * 1.8 + 32).toFixed(0) + "°F";
      break;
    case 6:
      text.innerText = Math.PI * inputText * inputText;
      break;
    case 7:
      var inputText2 = document.getElementById("input8").value;
      text.innerText = (parseInt(inputText) + parseInt(inputText2)) / 2;
      break;
    case 8:
      var inputText2 = document.getElementById("input7").value;
      text.innerText = (parseInt(inputText) + parseInt(inputText2)) / 2;
      break;
    default:
      text.innerText = "Something went horribly wrong!";
      break;
  }
}

function buttonClick(buttonNumber) {
  switch (buttonNumber) {
    case 0:
      document.getElementById("p0").innerText =
        document.getElementById("input0").value;
      break;
    case 1:
      generateFibonacci();
      break;
  }
}

function generateFibonacci() {
  fibonacci.push(
    fibonacci[fibonacci.length - 2] + fibonacci[fibonacci.length - 1]
  );
  fibonacciString += ", ";
  fibonacciString += fibonacci[fibonacci.length - 1];
  document.getElementById("fibonacci").innerText = fibonacciString;
}
