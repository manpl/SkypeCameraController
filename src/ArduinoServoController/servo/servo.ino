#include <Servo.h>
Servo tiltServo,
      panServo;
 
int panMax = 140,
    panMin = 20,
    tiltMax = 180,
    tiltMin = 20,
    tiltAngle = tiltMin, // 20 - 180
    panAngle = panMin;// 0 - 140 excluding
    
void setup() {
  Serial.begin(9600);
  panServo.attach(9);
  tiltServo.attach(10);  // attaches the servo on pin 9 to the servo object

  tiltServo.write(tiltAngle);
  panServo.write(panAngle);
}

void loop() {

  while(Serial.available()) {
    String operation = Serial.readString();
    char operationName = operation.charAt(0);
    int angle = operation.substring(1).toInt();
    
    switch(operationName){
      case 'p':
        pan(angle);
        break;
      case 't':
        tilt(angle);
        break;
    }

    Serial.println(operation + " tilt:" + tiltAngle + " pan:" + panAngle);
  } 
  delay(10);  
}

void tilt(int angle){
  tiltAngle = sanitizeAngles(tiltAngle, angle, tiltMin, tiltMax);
  tiltAngle += angle;
  tiltServo.write(tiltAngle);
}

void pan(int angle){
  panAngle = sanitizeAngles(panAngle, angle, panMin, panMax);
  panServo.write(panAngle);
}

int sanitizeAngles(int current, int change, int min, int max){
  int afterChange = current + change;
  if(afterChange > panMax) return panMax;
  if(afterChange < panMin) return panMin;
  return afterChange; 
}
