import java.util.*;
String brushColor = "black";
List<int[]> paints = new ArrayList<int[]>();
int brushSize = 30;
int oldMouseX;
int oldMouseY;
int previousPaint;
int isMousePressed = 0;
int R = 0;
int G = 0;
int B = 0;
void setup() {
  size(1920, 1080);
  noFill();
}



void draw() 
{
  
  background(255, 255, 255);
  if (mousePressed)
  {
    isMousePressed = 1;
    int[] temp = { mouseX, mouseY, brushSize, isMousePressed, R, G, B };
    paints.add(temp);
  }
  else if (isMousePressed == 1)
  {
    isMousePressed = 0;
    int[] temp = { mouseX, mouseY, brushSize, isMousePressed, R, G, B };
    paints.add(temp);
  }
  previousPaint = 0;
  for (int[] paint : paints) 
  {
    if (paint[3] == 1)
    {
      if (previousPaint == 1)
      {
        strokeWeight(paint[2]);
        stroke(paint[4], paint[5], paint[6]);
        line(paint[0], paint[1], oldMouseX, oldMouseY);
      }
      oldMouseX = paint[0];
      oldMouseY = paint[1];
      previousPaint = 1;
    }
    else
    {
      previousPaint = 0;
    }
  }
  strokeWeight(1);
  stroke(R, G, B);
  ellipse(mouseX, mouseY, brushSize, brushSize);
}

void mouseWheel (MouseEvent event) 
{
  if (isMousePressed == 0)
  {
    brushSize -= event.getCount() * 10;
    if (brushSize < 10)
    {
      brushSize = 10;
    }
  }

}

void keyPressed()
{
  //if (key == 'u' && paints.size() > 5)
  //{
  //        exit();
   // paints.remove(paints.get(paints.size()-1));
  //  while (paints.get(paints.size()-1)[3] == 1)
   // {
   //   paints.remove(paints.get(paints.size()-1));
  //  }
  //}
    switch (key)
  {
    case '1': R = 0; G = 0; B = 0;
    break;
    case '2': R = 127; G = 127; B = 127;
    break;
    case '3': R = 136; G = 0; B = 21;
    break;
    case '4': R = 237; G = 28; B = 36;
    break;
    case '5': R = 255; G = 127; B = 39;
    break;
    case '6': R = 255; G = 242; B = 0;
    break;
    case '7': R = 34; G = 177; B = 76;
    break;
    case '8': R = 0; G = 162; B = 232;
    break;
    case '9': R = 63; G = 72; B = 204;
    break;
    case '0': R = 163; G = 73; B = 164;
    break;
    default:
    break;
  }
    
}
