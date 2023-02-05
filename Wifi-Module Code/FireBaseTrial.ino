#include <ESP8266WiFi.h>
#include <Firebase_ESP_Client.h>
#include <NTPClient.h>
#include <Wire.h>
#include <WiFiUdp.h>


WiFiUDP ntpUDP;
NTPClient timeClient(ntpUDP, "pool.ntp.org", 7200);
// Provide the token generation process info.
#include "addons/TokenHelper.h"
// Provide the RTDB payload printing info and other helper functions.
#include "addons/RTDBHelper.h"

#define WIFI_SSID "slmn"
#define WIFI_PASSWORD "00000000"

// Insert Firebase project API Key
#define API_KEY "AIzaSyBFK_-7e58wK6mws72ZP24tmWqVy9He728"

// Insert Authorized Email and Corresponding Password
#define USER_EMAIL "zyadhamedashour@gmail.com"
#define USER_PASSWORD "#Ziadzoz2022"

// Insert RTDB URLefine the RTDB URL
#define DATABASE_URL "https://capstone327masterbase-default-rtdb.europe-west1.firebasedatabase.app/"

// Define Firebase objects
FirebaseData fbdo;
FirebaseAuth auth;
FirebaseConfig config;

unsigned long sendDataPrevMillis = 0;
unsigned long timerDelay = 3000;

// Variable to save USER UID
String uid;

// Database main path (to be updated in setup with the user UID)
String databasePath;
// Database child nodes
String dataPath = "/data";

// Parent Node (to be updated in every loop)
String parentPath;

FirebaseJson json;

void initWiFi() {
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  Serial.print("Connecting to WiFi ..");
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print('.');
    delay(1000);
  }
  Serial.println(WiFi.localIP());
  Serial.println();
}

void setup()
{
  Serial.begin(115200);
  initWiFi();
   // Assign the api key (required)
  config.api_key = API_KEY;

  // Assign the user sign in credentials
  auth.user.email = USER_EMAIL;
  auth.user.password = USER_PASSWORD;

  // Assign the RTDB URL (required)
  config.database_url = DATABASE_URL;

  Firebase.reconnectWiFi(true);
  fbdo.setResponseSize(4096);

  // Assign the callback function for the long running token generation task */
  config.token_status_callback = tokenStatusCallback; //see addons/TokenHelper.h

  // Assign the maximum retry of token generation
  config.max_token_generation_retry = 5;

  // Initialize the library with the Firebase authen and config
  Firebase.begin(&config, &auth);

  // Getting the user UID might take a few seconds
  Serial.println("Getting User UID");
  while ((auth.token.uid) == "") {
    Serial.print('.');
    delay(1000);
  }
  // Print user UID
  uid = auth.token.uid.c_str();
  Serial.print("User UID: ");
  Serial.println(uid);

  // Update database path
  databasePath = "/UsersData/" + uid + "/readings";
  timeClient.begin();
}

void loop()
{
  if(Serial.available() > 0 && Firebase.ready())
  {
    String data = Serial.readString();
    timeClient.update();
    parentPath= databasePath + "/" + String(timeClient.getFormattedTime());
    
    sendDataPrevMillis = millis();
    json.set(dataPath.c_str(), data);
    Firebase.RTDB.setJSON(&fbdo, parentPath.c_str(), &json);
  }
}
