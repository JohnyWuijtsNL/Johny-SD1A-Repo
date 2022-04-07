import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class Pixels_Fighting extends PApplet {

//change to change size of the squares
int pixelSize = 16;
//change to change time it takes for one frame
int frameTime = 2;
//change to change the rate at which colour change happens
int changeSpeed = 1;
//change to change the length of the trail the squares leave behind
int trail = 32;

int screenX = round(3840 / pixelSize);
int screenY = round(2160 / pixelSize);
boolean[][] field = new boolean[screenX][];
boolean[][] oldField = new boolean[screenX][];
int xCord = 0;
int yCord = 0;
int timer = 3;

int redC = 255;
int greenC = 0;
int blueC = 0;

public void setup()
{
  
  background(0, 0, 0);
  noStroke();
  generateField();
}

public void generateField()
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

public void generateColour() 
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

public void draw()
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
          if (oldField[i-1][j]) { team1++; } else { team2++; }
          if (!jLow)
          {
            if (oldField[i-1][j-1]) { team1++; } else { team2++; }
          }
          if (!jHigh)
          {
            if (oldField[i-1][j+1]) { team1++; } else { team2++; }
          }
        }
        if (!iHigh)
        {
          if (oldField[i+1][j]) { team1++; } else { team2++; }
          if (!jLow)
          {
            if (oldField[i+1][j-1]) { team1++; } else { team2++; }
          }
          if (!jHigh)
          {
            if (oldField[i+1][j+1]) { team1++; } else { team2++; }
          }
        }
        if (!jLow)
        {
          if (oldField[i][j-1]) { team1++; } else { team2++; }
        }
        if (!jHigh)
        {
          if (oldField[i][j+1]) { team1++; } else { team2++; }
        }
        if (oldField[i][j]) { team1++; } else { team2++; }
        
       
        
        
        float ratio = 1 / (PApplet.parseFloat(team1) + PApplet.parseFloat(team2)) * PApplet.parseFloat(team1);
        if (ratio > random(1)) { field[i][j] = true; } else { field[i][j] = false; }
      }
    }
    
    clear();
    xCord = 0;
    
    for (int i = 0; i < field.length; i++)
    {
      yCord = 0;
      for (int j = 0; j < field[i].length; j++)
      {
        if (field[i][j])
        {
          fill(255, 0, 255);
        }
        else
        {
          fill(0, 255, 0);
        }
        square(xCord, yCord, pixelSize);
        yCord += pixelSize;
      }
      xCord += pixelSize;
    }
    
    timer = frameTime;
  }
  timer--;
  
}
  public void settings() {  fullScreen(); }
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "--present", "--window-color=#666666", "--stop-color=#cccccc", "Pixels_Fighting" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
