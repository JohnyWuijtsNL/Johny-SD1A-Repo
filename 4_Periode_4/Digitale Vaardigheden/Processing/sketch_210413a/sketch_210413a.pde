boolean isBlack = false;
int redC = 255;
int greenC = 0;
int blueC = 0;
int changeSpeed = 5;

void setup() {
  size(1000, 1000);
}



void draw() {

  if (mousePressed) {

    if (!isBlack)
    {
       fill(255);
       stroke(255);
       isBlack = !isBlack;
    }
    else
    {
      fill(0);
      stroke(0);
      isBlack = !isBlack;
    }
    
  } else {
    //red to yellow
    if (greenC < 255 && redC >= 255 && blueC == 0)
    {
      greenC += changeSpeed;
      blueC = 0;
    }
    //yellow to green
    else if (redC > 0 && greenC >= 255)
    {
      redC -= changeSpeed;
      greenC = 255;
    }
    //green to cyan
    else if (blueC < 255 && greenC >= 255)
    {
      redC = 0;
      blueC += changeSpeed;
    }
    //cyan to blue
    else if (greenC > 0 && blueC >= 255)
    {
      greenC -= changeSpeed;
      blueC = 255;
    }
    //blue to magenta
    else if (redC < 255 && blueC >= 255)
    {
      redC += changeSpeed;
      greenC = 0;
    }
    //magenta to red
    else if (blueC > 0 && redC >= 255)
    {
      redC = 255;
      blueC -= changeSpeed;
    }
    fill(redC, greenC, blueC);
    stroke(redC, greenC, blueC);

  }
  ellipse(mouseX, mouseY, 80, 80);
    ellipse(-mouseX+1000, mouseY, 80, 80);
      ellipse(mouseX, -mouseY+1000, 80, 80);
        ellipse(-mouseX+1000, -mouseY+1000, 80, 80);
}
