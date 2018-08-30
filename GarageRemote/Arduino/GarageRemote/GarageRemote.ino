//Code based on https://create.arduino.cc/projecthub/user206876468/arduino-bluetooth-basic-tutorial-d8b737
/*
* Bluetooh Garage remote
* Shashi Sadasivan
* The app sends a signal to toggle the garage remote. Which is opening the relay for a few seconds
*/
String data = "";            //Variable for storing received data
const String key = "ToggleMyGarageDoor"; // Secret Key: This is the string that is sent by the App

void setup()
{
    Serial.begin(9600);   //Sets the baud for serial data transmission
    pinMode(10, OUTPUT);  //Sets digital pin 10 as output pin
    pinMode(4, OUTPUT);   //Shows if the signal was received
    pinMode(11, INPUT);   //External button
}
void loop()
{
   //Check if external button was pressed
   
   if(digitalRead(11) == HIGH)
   {
      Serial.println("11 high");
      openGarage();
   }
   

   if(Serial.available() > 0)      // Send data only when you receive data:
   {
      //data = Serial.read();        //Read the incoming data & store into data
      data = Serial.readString();
      // Serial.print(data);          //Print Value inside data in Serial monitor
      // Serial.print("\n");
      if(data == key)
      {
         Serial.println("Bluetooth " + data);
         openGarage();
         /*
         digitalWrite(4, HIGH);
         digitalWrite(10, HIGH);   //Set the Value to High. This should trigger the Relay
         delay(1000);              //delay by 1 second to allow the
         digitalWrite(10, LOW);    //Set the Value to LOW, to end the Relay
         digitalWrite(4, LOW);
         */
      }
      else
      {
        Serial.println("Bluetooth FALSE - " + data);
      }
   }
}

void openGarage()
{
   Serial.println("openGarage");
   digitalWrite(4, HIGH);
   digitalWrite(10, HIGH);   //Set the Value to High. This should trigger the Relay
   delay(500);              //delay by 1 second to allow the
   digitalWrite(10, LOW);    //Set the Value to LOW, to end the Relay
   digitalWrite(4, LOW);
}

