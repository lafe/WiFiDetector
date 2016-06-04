/*
 Name:		WiFiDetector.ino
 Created:	5/28/2016 6:30:10 PM
 Author:	Lars Fernhomberg
*/

#include "WiFiCredentials.h"
#include <Ticker.h>
#include <WiFiUdp.h>
#include <WiFiServer.h>
#include <WiFiClientSecure.h>
#include <WiFiClient.h>
#include <ESP8266WiFiType.h>
#include <ESP8266WiFiSTA.h>
#include <ESP8266WiFiScan.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266WiFiGeneric.h>
#include <ESP8266WiFiAP.h>
#include <ESP8266WiFi.h>

#define LED D4
#define SECOND 1000

Ticker ledBlinking;
Ticker wlanDetection;
bool ledOn = false;

// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
	pinMode(LED, OUTPUT);
	
	//Connect to Wifi
	startBlinking(0.05);
	Serial.print("Connecting to ");
	Serial.println(WiFiSSID);

	WiFi.begin(WiFiSSID, WiFiPassword);

	while (WiFi.status() != WL_CONNECTED) {
		delay(0.5 * SECOND);
		Serial.print(".");
	}
	Serial.println();
	Serial.println();
	Serial.print("Connected to ");
	Serial.println(WiFiSSID);
	Serial.print("IP address: ");
	Serial.println(WiFi.localIP());

	endBlinking();
}

// the loop function runs over and over again until power down or reset
void loop() {
	checkWlan();
	delay(5 * SECOND);
}

void checkWlan() {
	startBlinking(0.1);

	int numberOfNetworks = WiFi.scanNetworks(false, true);
	if (numberOfNetworks == -1) {
		Serial.println("Could not found networks");
		return;
	}

	Serial.print("Found ");
	Serial.print(numberOfNetworks);
	Serial.println(" networks");

	for (int i = 0; i < numberOfNetworks; i++) {
		Serial.print(i);
		Serial.print(") ");
		Serial.print(WiFi.SSID(i));
		Serial.print(", BSSID: ");
		Serial.print(WiFi.BSSIDstr(i));
		Serial.print(", Signal: ");
		Serial.print(WiFi.RSSI(i));
		Serial.print(" dBm");
		Serial.print(", Encryption: ");
		printEncryptionType(WiFi.encryptionType(i));
		Serial.print(", Channel: ");
		Serial.print(WiFi.channel(i));
		Serial.print(", Hidden: ");
		Serial.print(WiFi.isHidden(i));
		
		Serial.println();
	}
	Serial.println();
	
	endBlinking();
}

void printEncryptionType(int thisType)
{
	// read the encryption type and print out the name:
	switch (thisType) {
	case ENC_TYPE_WEP:
		Serial.print("WEP");
		break;
	case ENC_TYPE_TKIP:
		Serial.print("WPA");
		break;
	case ENC_TYPE_CCMP:
		Serial.print("WPA2");
		break;
	case ENC_TYPE_NONE:
		Serial.print("None");
		break;
	case ENC_TYPE_AUTO:
		Serial.print("Auto");
		break;
	}
}

void startBlinking(float interval) {
	ledBlinking.attach(interval, blinkLed);
}

void endBlinking() {
	ledBlinking.detach();

	ledOn = false;
	switchLed();
}

void blinkLed() {

	switchLed();
	ledOn = !ledOn;
}

void switchLed() {
	if (!ledOn) {
		digitalWrite(LED, HIGH);
	}
	else {
		digitalWrite(LED, LOW);
	}
}