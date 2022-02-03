int screenX = 194;
int screenY = 110;

boolean[][] field = new boolean[screenX][];
boolean[][] oldField = new boolean[screenX][];
boolean[][] olderField = new boolean[screenX][];
boolean[][] oldererField = new boolean[screenX][];
int xCord = 0;
int yCord = 0;
int neighbours = 0;
int timer = 0;
boolean theSame;
boolean inverted = false;

int redC = 255;
int greenC = 0;
int blueC = 0;
int changeSpeed = 1;

void setup()
{
  fullScreen();
  background(0, 0, 0);
  noStroke();
  generateField();
}

void generateField()
{
    for (int i = 0; i < field.length; i++)
  {
    field[i] = new boolean[screenY];
    oldField[i] =  new boolean[screenY];
    olderField[i] =  new boolean[screenY];
    oldererField[i] =  new boolean[screenY];
    if (i == 0 || i == field.length -1)
    {
      for (int j = 0; j < field[i].length; j++)
      {
        field[i][j] = false;
      }
    }
    else
    {
      for (int j = 0; j < field[i].length; j++)
      {
        if (j == 0 || j == field[i].length - 1)
        {
          field[i][j] = false;
        }
        else
        {
          field[i][j] = random(1) > 0.85;
        }
      }
    }
    
  }
}

void generateColour() 
{
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
}

void draw()
{
  //if (timer == 0)
  {
    for (int i = 0; i < field.length; i++)
    {
       for (int j = 0; j < field[i].length; j++)
       {
         oldererField[i][j] = olderField[i][j];
         olderField[i][j] = oldField[i][j];
         oldField[i][j] = field[i][j];
       }
    }
    for (int i = 1; i < field.length - 1; i++)
    {
      for (int j = 1; j < field[i].length - 1; j++)
      {
        neighbours = 0;
        if (oldField[i-1][j-1]) { neighbours++; }
        if (oldField[i][j-1]) { neighbours++; }
        if (oldField[i+1][j-1]) { neighbours++; }
        if (oldField[i-1][j]) { neighbours++; }
        if (oldField[i+1][j]) { neighbours++; }
        if (oldField[i-1][j+1]) { neighbours++; }
        if (oldField[i][j+1]) { neighbours++; }
        if (oldField[i+1][j+1]) { neighbours++; }
      
        if (neighbours < 2) { field[i][j] = false; }
        if (neighbours == 3) { field[i][j] = true; }
        if (neighbours > 3) { field[i][j] = false; }
      }
    }
    
    generateColour();
    if (!inverted)
    {
       fill(0, 0, 0, 32);
       square(0, 0, 5000);
       fill(redC, greenC, blueC);
    }
    else
    {
       fill(redC, greenC, blueC, 32);
       square(0, 0, 5000);
       fill(blueC, redC, greenC);
    }
    xCord = 0;
    
    for (int i = 1; i < field.length - 1; i++)
    {
      yCord = 0;
      for (int j = 1; j < field[i].length - 1; j++)
      {
        if (field[i][j])
        {
          square(xCord, yCord, 10);
        }
        yCord += 10;
      }
      xCord += 10;
    }
    theSame = true;
     for (int i = 0; i < field.length; i++)
    {
       for (int j = 0; j < field[i].length; j++)
       {
         if (!(olderField[i][j] == field[i][j]) && !(oldererField[i][j] == field[i][j]))
         {
           theSame = false;
         }
       }
    }
    if (theSame)
    {
      inverted = !inverted;
      generateField();
    }
    
    timer = 3;
  }
  timer--;
  
}
