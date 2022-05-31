//change to change size of the squares
int pixelSize = 30;
//change to change time it takes for one frame
int frameTime = 3;
//change to change the rate at which colour change happens
int changeSpeed = 1;
//change to change the length of the trail the squares leave behind
int trail = 32;

int numberOfTeams = 2;

int screenX = round(1920 / pixelSize);
int screenY = round(1080 / pixelSize);
int[][] field = new int[screenX][];
int[][] oldField = new int[screenX][];
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
    field[i] = new int[screenY];
    oldField[i] =  new int[screenY];
    for (int j = 0; j < field[i].length; j++)
    {
      if (i > (field.length - 1) / 2)
      {
        if (j > (field[i].length - 1) / 2)
        {
          field[i][j] = 0;
        }
        else
        {
          field[i][j] = 0;
        }
      }
      else
      {
        if (j > (field[i].length - 1) / 2)
        {
          field[i][j] = 1;
        }
        else
        {
          field[i][j] = 1;
        }
      }
    }
  }
}

void draw()
{
  if (timer == 0)
  {
        xCord = 0;
    
    for (int i = 0; i < field.length; i++)
    {
      yCord = 0;
      for (int j = 0; j < field[i].length; j++)
      {
        if (field[i][j] == 0)
        {
          fill(255, 0, 0);
        }
        else if (field[i][j] == 1)
        {
          fill(255, 255, 0);
        }
        else if (field[i][j] == 2)
        {
          fill(0, 255, 0);
        }
        else
        {
          fill(0, 0, 255);
        }
        square(xCord, yCord, pixelSize);
        yCord += pixelSize;
      }
      xCord += pixelSize;
    }
    
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
        int[] teams = new int[numberOfTeams];
        boolean iLow = true, iHigh = true, jLow = true, jHigh = true;
        if (i > 0)
        {
          iLow = false;
        }
        if (i < field.length - 1)
        {
          iHigh = false;
        }
        if (j > 0)
        {
          jLow = false;
        }
        if (j < field[i].length - 1)
        {
          jHigh = false;
        }
        
        if (!iLow)
        {
          teams[oldField[i-1][j]]++;
          if (!jLow)
          {
            teams[oldField[i-1][j-1]]++;
          }
          if (!jHigh)
          {
            teams[oldField[i-1][j+1]]++;
          }
        }
        if (!iHigh)
        {
          teams[oldField[i+1][j]]++;
          if (!jLow)
          {
            teams[oldField[i+1][j-1]]++;
          }
          if (!jHigh)
          {
            teams[oldField[i+1][j+1]]++;
          }
        }
        if (!jLow)
        {
          teams[oldField[i][j-1]]++;
        }
        if (!jHigh)
        {
          teams[oldField[i][j+1]]++;
        }
        teams[oldField[i][j]]++;
        
        int neighbours = 0;
        for (int x = 0; x < teams.length; x++)
        {
          neighbours += teams[x];
        }
          
        int[] ratios = new int[neighbours];
        int y = 0;
        for (int x = 0; x < teams.length; x++)
        {
          while (teams[x] > 0)
          {
            ratios[y] = x;
            y++;
            teams[x]--;
          }
        }
        for (int x = 0; x < ratios.length; x++)
        {

          
        }
        field[i][j] = ratios[floor(random(ratios.length))];
      }
    }
    timer = frameTime;
  }
  timer--;
  
}
