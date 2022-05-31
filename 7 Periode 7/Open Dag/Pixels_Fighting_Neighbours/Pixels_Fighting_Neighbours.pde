//change to change size of the squares
int pixelSize = 8;
//change to change time it takes for one frame
int frameTime = 3;

//number of teams. if equal to 2 or 4, divided equally. otherwise, randomised.
int numberOfTeams = 2;
//amount of effort it takes to change a pixel. wildly unstable if under 1.
int defense = 1;
//alpha channel of pixels. causes a fading effect
int fade = 0;



int screenX = round(1920 / pixelSize);
int screenY = round(1080 / pixelSize);
int[][] field = new int[screenX][];
int[][] oldField = new int[screenX][];
int xCord = 0;
int yCord = 0;
int timer = 1;

int[] redC;
int[] greenC;
int[] blueC;
int numberOfColors = numberOfTeams;
boolean isEmpty = true;
boolean startFilled = false;
boolean randomColors = true;
boolean isFilled = false;


void setup()
{
  fullScreen();
  background(0, 0, 0);
  noStroke();
  generateField();
  
  if (randomColors)
  {
    redC = new int[numberOfTeams];
    greenC = new int[numberOfTeams];
    blueC = new int[numberOfTeams];
    
    for (int i = 0; i < numberOfTeams; i++)
    {
      redC[i] = floor(random(256));
      greenC[i] = floor(random(256));
      blueC[i] = floor(random(256));
    }
  }
}

void generateField()
{
  for (int i = 0; i < field.length; i++)
  {
    field[i] = new int[screenY];
    oldField[i] =  new int[screenY];
    if (numberOfTeams != 2 && numberOfTeams != 4)
    {
      for (int j = 0; j < field[i].length; j++)
      {
        field[i][j] = floor(random(numberOfTeams));
      }
    }
    else
    {
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
            if (numberOfTeams == 4) field[i][j] = 2;
            else field[i][j] = 0;
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
            if (numberOfTeams == 4) field[i][j] = 3;
            else field[i][j] = 1;
          }
        }
      }
    }
  }
}

void draw()
{
  timer--;
  if (timer == 0)
  {
     GenerateGeneration();
     GenerateCanvas();
     timer = frameTime;
  }
}

void GenerateCanvas()
{
  xCord = 0;
    
  for (int i = 0; i < field.length; i++)
  {
    yCord = 0;
    for (int j = 0; j < field[i].length; j++)
    {
      if (randomColors)
      {
        fill(redC[field[i][j]], greenC[field[i][j]], blueC[field[i][j]], fade);
      }
      else
      {
        switch (field[i][j])
      {
          case 0:
            fill(255, 0, 0);
          break;
          case 1:
            fill(0, 0, 255);
          break;
          case 2:
            fill(0, 255, 0);
          break;
          case 3:
            fill(255, 255, 0);
          break;
          case 4:
            fill(255, 0, 255);
          break;
          case 5:
            fill(0, 255, 255);
          break;
          default:
          if (isEmpty)
          {
            fill(0, 0, 0);
          }
          else
          {
            int isBlack = floor(random(2)) * 255;
            fill(isBlack, isBlack, isBlack);
          }

          break;
        }
      }
      
      square(xCord, yCord, pixelSize);
      yCord += pixelSize;
    }
    xCord += pixelSize;
  }
}

void GenerateGeneration()
{
  isFilled = false;
  while (!isFilled)
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
        
        int neighbours = 0;
        if (!iLow)
        {
          teams[oldField[i-1][j]]++;
          neighbours++;
          if (!jLow)
          {
            //teams[oldField[i-1][j-1]]++;
            //neighbours++;
          }
          if (!jHigh)
          {
            //teams[oldField[i-1][j+1]]++;
            //neighbours++;
          }
        }
        if (!iHigh)
        {
          teams[oldField[i+1][j]]++;
          neighbours++;
          if (!jLow)
          {
            //teams[oldField[i+1][j-1]]++;
            //neighbours++;
          }
          if (!jHigh)
          {
            //teams[oldField[i+1][j+1]]++;
            //neighbours++;
          }
        }
        if (!jLow)
        {
          teams[oldField[i][j-1]]++;
          neighbours++;
        }
        if (!jHigh)
        {
          teams[oldField[i][j+1]]++;
          neighbours++;
        }
        if ((!(oldField[i][j] >= numberOfColors)))
        {
          for (int x = 0; x < defense; x++)
          {
            teams[oldField[i][j]]++;
            neighbours++;
          }
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
        int chosenTeam = ratios[floor(random(ratios.length))];
        if (chosenTeam >= numberOfColors && isEmpty)
        {
          chosenTeam = oldField[i][j];
        }
        field[i][j] = chosenTeam;
      }
    }
    
    isFilled = true;
    if (startFilled)
    {
      for (int i = 0; i < field.length; i++)
      {
        for (int j = 0; j < field[i].length; j++)
        {
          if (field[i][j] >= numberOfColors) { isFilled = false; };
        }
      }
    }
  }
}
