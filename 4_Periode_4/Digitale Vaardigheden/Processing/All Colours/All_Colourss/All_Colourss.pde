int red = 0;
int green = 0;
int blue = 0;
int xcor = 0;
int ycor = 0;
int ypos = 0;
int increaseAmount = 4;
int blueAmount = 0;

void setup()
{
  fullScreen();
  noStroke();
  background(0, 0, 0);
  
  
   while (blue <= 255)
  {
        red = 0;
        blue += increaseAmount;
    if (blueAmount >= 256/increaseAmount && blue != 0)
    {
      ypos += 256 / increaseAmount;
      xcor = 0;
      blueAmount = increaseAmount;
    }

    blueAmount += increaseAmount;
    while (red <= 255)
    {
      while (green <= 255)
      {
        fill(red, green, blue);
        square(xcor, ycor + ypos, 1);
        green += increaseAmount;
        ycor++;
      }
      red += increaseAmount;
      ycor = 0;
      green = 0;
      xcor++;
    }
  }
}
