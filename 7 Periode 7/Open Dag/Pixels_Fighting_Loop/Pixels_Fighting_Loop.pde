//change to change size of the squares
int pixelSize = 8;
//change to change time it takes for one frame
int frameTime = 2;
//change to change the rate at which colour change happens
int changeSpeed = 1;
//change to change the length of the trail the squares leave behind
int trail = 32;

int screenX = round(1920 / pixelSize);
int screenY = round(1080 / pixelSize);
boolean[][] field = new boolean[screenX][];
boolean[][] oldField = new boolean[screenX][];
int xCord = 0;
int yCord = 0;
int timer = 3;

int redC = 255;
int greenC = 0;
int blueC = 0;

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
    if (i > (field.length - 1) / 2)
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
        field[i][j] = true;
      }
    }
  }
}

void generateColour() 
{
      //red to yellow
    if (greenC < 255 && redC >= 255 && blueC <= 0)
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
  if (timer == 0)
  {
    for (int i = 0; i < field.length; i++)
    {
       for (int j = 0; j < field[i].length; j++)
       {
         oldField[i][j] = field[i][j];
       }
    }
    for (int i = 0; i < field.length; i++)
    {
      for (int j = 0; j < field[i].length; j++)
      {
        int team1 = 0;
        int team2 = 0;
        int minusI = i, maxI = i, minusJ = j, maxJ = j;
        
        if (i == 0)
        {
          minusI = field.length;
        }
        if (i == field.length - 1)
        {
          maxI = -1;
        }
        if (j == 0)
        {
          minusJ = field[i].length;
        }
        if (j == field[i].length - 1)
        {
          maxJ = -1;
        }
        
        if (oldField[minusI-1][minusJ-1]) { team1++; } else { team2++; }
        if (oldField[minusI-1][j]) { team1++; } else { team2++; }
        if (oldField[minusI-1][maxJ+1]) { team1++; } else { team2++; }
        if (oldField[i][minusJ-1]) { team1++; } else { team2++; }
        if (oldField[i][j]) { team1++; } else { team2++; }
        if (oldField[i][maxJ+1]) { team1++; } else { team2++; }
        if (oldField[maxI+1][minusJ-1]) { team1++; } else { team2++; }
        if (oldField[maxI+1][j]) { team1++; } else { team2++; }
        if (oldField[maxI+1][maxJ+1]) { team1++; } else { team2++; }
        
       
        
        
        float ratio = 1 / (float(team1) + float(team2)) * float(team1);
        if (ratio > random(1)) { field[i][j] = true; } else { field[i][j] = false; }
      }
    }
    
    generateColour();
    fill(255 - redC, 255 - greenC, 255 - blueC);
    square(0, 0, 5000);
    fill(redC, greenC, blueC);
    xCord = 0;
    
    for (int i = 0; i < field.length; i++)
    {
      yCord = 0;
      for (int j = 0; j < field[i].length; j++)
      {
        if (field[i][j])
        {
          square(xCord, yCord, pixelSize);
        }
        
        yCord += pixelSize;
      }
      xCord += pixelSize;
    }
    
    timer = frameTime;
  }
  timer--;
  
}
