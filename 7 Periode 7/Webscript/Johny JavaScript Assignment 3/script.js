let rubikAmount = 123;
let cowchAmount = 13;
let seedAmount = 45747;
let keyAmount = 1;
let rubikBought = 0;
let cowchBought = 0;
let seedBought = 0;
let keyBought = 0;

function initialize() {
  document.getElementById("rubikstock").innerText = "Stock: " + rubikAmount;
  document.getElementById("cowchstock").innerText = "Stock: " + cowchAmount;
  document.getElementById("seedstock").innerText = "Stock: " + seedAmount;
  document.getElementById("keystock").innerText = "Stock: " + keyAmount;
  document.getElementById("rubikcart").style.display = "none";
  document.getElementById("cowchcart").style.display = "none";
  document.getElementById("seedcart").style.display = "none";
  document.getElementById("keycart").style.display = "none";
  document.getElementById("total").style.display = "none";
}

function buyItem(item) {
  switch (item) {
    case "rubik":
      rubikAmount--;
      document.getElementById("rubikstock").innerText = "Stock: " + rubikAmount;
      if (rubikAmount == 0)
      {
        document.getElementById("rubikbutton").style.display = "none";
        document.getElementById("srubikbutton").style.display = "none";
        document.getElementById("rubikstock").innerText = "Out of stock!";
      }
      break;
      case "cowch":
      cowchAmount--;

      document.getElementById("cowchstock").innerText = "Stock: " + cowchAmount;
      if (cowchAmount == 0)
      {
        document.getElementById("cowchbutton").style.display = "none";
        document.getElementById("scowchbutton").style.display = "none";
        document.getElementById("cowchstock").innerText = "Out of stock!";
      }
      break;
    case "seed":
      seedAmount--;
      document.getElementById("seedstock").innerText = "Stock: " + seedAmount;
      if (seedAmount == 0)
      {
        document.getElementById("seedbutton").style.display = "none";
        document.getElementById("sseedbutton").style.display = "none";
        document.getElementById("seedstock").innerText = "Out of stock!";
      }
      break;
    case "key":
      keyAmount--;
      document.getElementById("keystock").innerText = "Stock: " + keyAmount;
      if (keyAmount == 0)
      {
        document.getElementById("keybutton").style.display = "none";
        document.getElementById("skeybutton").style.display = "none";
        document.getElementById("keystock").innerText = "Out of stock!";
      }
      break;
  }
  updateCart(item, 1);
}

function unbuyItem(item) {
    switch (item) {
      case "rubik":
        rubikAmount++;
        document.getElementById("rubikstock").innerText = "Stock: " + rubikAmount;
        if (rubikBought != 0)
        {
          document.getElementById("rubikbutton").style.display = "initial";
          document.getElementById("mrubikbutton").style.display = "initial";
        }
        break;
        case "cowch":
        cowchAmount++;
  
        document.getElementById("cowchstock").innerText = "Stock: " + cowchAmount;
        if (cowchAmount != 0)
        {
          document.getElementById("cowchbutton").style.display = "initial";
          document.getElementById("scowchbutton").style.display = "initial";
        }
        break;
      case "seed":
        seedAmount++;
        document.getElementById("seedstock").innerText = "Stock: " + seedAmount;
        if (seedAmount != 0)
        {
          document.getElementById("seedbutton").style.display = "initial";
          document.getElementById("sseedbutton").style.display = "initial";
        }
        break;
      case "key":
        keyAmount++;
        document.getElementById("keystock").innerText = "Stock: " + keyAmount;
        if (keyAmount != 0)
        {
          document.getElementById("keybutton").style.display = "initial";
          document.getElementById("skeybutton").style.display = "initial";
        }
        break;
    }
    updateCart(item, -1);
  }

function updateCart(item, value) {
    switch (item) {
      case "rubik":
        rubikBought += value;
        if (rubikBought == 0)
        {
          document.getElementById("rubikcart").style.display = "none";
        }
        else if (rubikBought == 1)
        {
            document.getElementById("rubikcart").style.display = "initial";
            document.getElementById("rubikbought").innerText = rubikBought + " Rubik's Sandwich";
        }
        else
        {
            document.getElementById("rubikcart").style.display = "initial";
            document.getElementById("rubikbought").innerText = rubikBought + " Rubik's Sandwiches";
        }
        break;
        case "cowch":
        cowchBought += value;
        if (cowchBought == 0)
        {
          document.getElementById("cowchcart").style.display = "none";
        }
        else if (cowchBought == 1)
        {
            document.getElementById("cowchcart").style.display = "initial";
            document.getElementById("cowchbought").innerText = cowchBought + " cowch";
        }
        else
        {
            document.getElementById("cowchcart").style.display = "initial";
            document.getElementById("cowchbought").innerText = cowchBought + " cowches";
        }
        break;
      case "seed":
        seedBought += value;
        if (seedBought == 0)
        {
          document.getElementById("seedcart").style.display = "none";
        }
        else if (seedBought == 1)
        {
            document.getElementById("seedcart").style.display = "initial";
            document.getElementById("seedbought").innerText = seedBought + " bagel seed";
        }
        else
        {
            document.getElementById("seedcart").style.display = "initial";
            document.getElementById("seedbought").innerText = seedBought + " bagel seeds";
        }
        break;
      case "key":
        keyBought += value;
        if (keyBought == 0)
        {
          document.getElementById("keycart").style.display = "none";
        }
        else if (keyBought == 1)
        {
            document.getElementById("keycart").style.display = "initial";
            document.getElementById("keybought").innerText = keyBought + " Mike Wazowskey";
        }
        else
        {
            document.getElementById("keycart").style.display = "initial";
            document.getElementById("keybought").innerText = keyBought + " Mike Wazowskeys (how?)";
        }
        break;
    }

    if (rubikBought + cowchBought + seedBought + keyBought == 0)
    {
        document.getElementById("nothing").style.display = "initial";
        document.getElementById("total").style.display = "none";
    }
    else
    {
        document.getElementById("nothing").style.display = "none";
        document.getElementById("total").style.display = "initial";
    }

    updateTotal()
  }

  function updateTotal()
  {
      let result = (rubikBought * 3.5 + cowchBought * 50 + seedBought * 0.15 + keyBought * 2000).toFixed(2);
      document.getElementById("totalnumber").innerText = "Total: $" + result;
  }