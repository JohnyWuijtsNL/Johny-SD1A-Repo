void setup() {
  size(1000, 1000);
}

float frames = 0;
float hover;
float hover2;

void draw() {
  frames += 0.05f;
  hover = 7 * sin(frames);
  hover2 = 7 * sin(frames - 0.7f);
  
  clear();
  background(128, 128, 128);
  
  strokeWeight(24);
  stroke(0, 0, 0);
  line(385, 894 + hover2, 385, 624 + hover2);
  line(615, 894 + hover2, 615, 624 + hover2);
  strokeWeight(32);
  curve(390, 909 + hover2, 381, 909 + hover2, 382, 893 + hover2, 395, 893 + hover2);
  curve(615, 909 + hover2, 621, 909 + hover2, 618, 893 + hover2, 605, 893 + hover2);
  strokeWeight(17);
  curve(395, 912 + hover2, 405, 907 + hover2, 396, 891 + hover2, 370, 891 + hover2);
  curve(612, 905 + hover2, 597, 907 + hover2, 604, 891 + hover2, 634, 891 + hover2);
  
  strokeWeight(28);
  noFill();
  curve(477, 795 + hover2, 477, 795 + hover2, 482, 708 + hover2, 435, 708 + hover2);
  curve(482, 708 + hover2, 482, 708 + hover2, 426, 638 + hover2, 526, -700 + hover2);
  
  curve(572, 830 + hover2, 552, 830 + hover2, 540, 752 + hover2, 620, 752 + hover2);
  curve(200, 252 + hover2, 540, 752 + hover2, 575, 645 + hover2, 575, 645 + hover2);
  
  strokeWeight(9);
  fill(242, 235, 23);
  triangle(500, 235 + hover, 750, 663 + hover, 250, 663 + hover);
  strokeWeight(5);
  line(447, 355 + hover, 460, 378 + hover);
  line(484, 371 + hover, 480, 344 + hover);
  line(553, 355 + hover, 540, 378 + hover);
  line(516, 371 + hover, 520, 344 + hover);
  
  line(447, 507 + hover, 460, 484 + hover);
  line(484, 491 + hover, 480, 518 + hover);
  line(553, 507 + hover, 540, 484 + hover);
  line(516, 491 + hover, 520, 518 + hover);
  

 
  fill(255, 255, 255);
  beginShape();
  curveVertex(410, 900 + hover); // the first control point
  curveVertex(410, 431 + hover); // is also the start point of curve
  curveVertex(590, 431 + hover); // the last point of curve
  curveVertex(590, 900 + hover); // is also the last control point
  endShape();
  beginShape();
  curveVertex(410, -40 + hover); // the first control point
  curveVertex(410, 429 + hover); // is also the start point of curve
  curveVertex(590, 429 + hover); // the last point of curve
  curveVertex(590, -40 + hover); // is also the last control point
  endShape();
  
  strokeWeight(4);
  stroke(160, 152, 5);
  
  line(325, 542 + hover, 675, 542 + hover);
  line(305, 579 + hover, 695, 579 + hover);
  line(280, 620 + hover, 720, 620 + hover);
  
  line(367, 543 + hover, 367, 578 + hover);
  line(500, 543 + hover, 500, 578 + hover);
  line(636, 543 + hover, 636, 578 + hover);
  
  line(433, 580 + hover, 433, 619 + hover);
  line(567, 580 + hover, 567, 619 + hover);
  
  line(367, 621 + hover, 367, 662 + hover);
  line(500, 621 + hover, 500, 662 + hover);
  line(636, 621 + hover, 636, 662 + hover);
  
  noStroke();
  fill(0, 0, 0);
  ellipse(500, 430 + hover, 12, 92);
  
  noFill();
  stroke(0, 0, 0);
  strokeWeight(9);
  triangle(500, 235 + hover, 750, 663 + hover, 250, 663 + hover);
  
  noStroke();
  fill(0, 0, 0);
  triangle(449, 548 + hover, 449, 604 + hover, 503, 579 + hover);
  triangle(550, 548 + hover, 550, 604 + hover, 497, 579 + hover);
  
  beginShape();
  vertex(470, 77 + hover);
  vertex(530, 77 + hover);
  vertex(523, 209 + hover);
  vertex(561, 209 + hover);
  vertex(561, 222 + hover);
  vertex(436, 222 + hover);
  vertex(436, 209 + hover);
  vertex(477, 209 + hover);
  endShape(CLOSE);
  

}
