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

public class Johny_GameOfLife_Endless_Small extends PApplet {

//change to change size of the squares
int pixelSize = 8;
//change to change time it takes for one frame
int frameTime = 2;
//change to change the rate at which colour change happens
int changeSpeed = 1;
//change to change the length of the trail the squares leave behind
int trail = 32;

int screenX = round(1920 / pixelSize) + 2;
int screenY = round(1080 / pixelSize) + 2;
boolean[][] field = new boolean[screenX][];
boolean[][] oldField = new boolean[screenX][];
int xCord = 0;
int yCord = 0;
int neighbours = 0;
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
          field[i][j] = random(1) > 0.85f;
        }
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
    fill(0, 0, 0, trail);
    square(0, 0, 5000);
    fill(redC, greenC, blueC);
    xCord = 0;
    
    for (int i = 1; i < field.length - 1; i++)
    {
      yCord = 0;
      for (int j = 1; j < field[i].length - 1; j++)
      {
        if (field[i][j])
        {
          square(xCord, yCord, pixelSize);
        }
        yCord += pixelSize;
      }
      xCord += pixelSize;
    }
      for (int i = 1; i < field.length - 1; i++)
      {
         for (int j = 1; j < field[i].length - 1; j++)
         {
           if (random(1) > 0.9999f)
           {
             field[i][j] = !field[i][j];
           }
         }
      }
    
    timer = frameTime;
  }
  timer--;
  
}
  public void settings() {  fullScreen(); }
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "--present", "--window-color=#666666", "--stop-color=#cccccc", "Johny_GameOfLife_Endless_Small" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
