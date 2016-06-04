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

const int deviceId = 1;

//Target Rest URL
const char* restHost = "192.168.1.191";
const int restPort = 36604;
const String restUrl = "/api/measurements";

Ticker ledBlinking;
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

	Serial.print("Connection to ");
	Serial.print(restHost);
	WiFiClient client;
	if (!client.connect(restHost, restPort)) {
		Serial.println(" failed!");
		endBlinking();
		return;
	}
	Serial.println(" succeeded!");

	String header = String("POST http://" + String(restHost) + ":" + restPort + restUrl + " HTTP/1.1\r\n");
	header = String(header + "Host: " + WiFi.localIP() + "\r\n");
	header = String(header + "Connection: close\r\n");
	header = String(header + "Content-Type: application/json\r\n");
	header = String(header + "User-Agent: ESP8266\r\n");
	header = String(header + "Accept: */*\r\n");

	String payload = "[\r\n";

	for (int i = 0; i < numberOfNetworks; i++) {
		payload = String(payload + "{");
		payload = String(payload + "\"DeviceId\":" + deviceId + ",");
		payload = String(payload + "\"SSID\":\"" + WiFi.SSID(i) + "\",");
		payload = String(payload + "\"BSSID\":\"" + WiFi.BSSIDstr(i) + "\",");
		payload = String(payload + "\"SignalStrength\":" + WiFi.RSSI(i) + ",");
		payload = String(payload + "\"Channel\":" + WiFi.channel(i) + ",");
		payload = String(payload + "\"Encryption\":" + WiFi.encryptionType(i) + ",");
		payload = String(payload + "\"Hidden\":" + WiFi.isHidden(i));

		if (i < numberOfNetworks - 1) {
			payload = String(payload + "},\r\n");
		}
		else {
			payload = String(payload + "}\r\n");
		}
	}

	payload = String(payload + "]");

	header = String(header + "Content-Length: " + payload.length() + "\r\n");

	String data = String(header + "\r\n" + payload + "\r\n");

	//Command
	//client.print("POST http://");
	//client.print(restHost);
	//client.print(":");
	//client.print(restPort);
	//client.print(restUrl);
	//client.print(" HTTP/1.1");
	//client.println();

	//Header
	//client.println("Connection: close");
	//client.println("Content-Type: application/json");
	//client.println("User-Agent: ESP8266");
	//client.println("Accept: */*");

	/*client.println("[");

	for (int i = 0; i < numberOfNetworks; i++) {

		client.println("{");
		client.print("\"DeviceId\":");
		client.print(deviceId);
		client.println(",");
		client.print("\"SSID\":");
		client.print(WiFi.SSID(i));
		client.println(",");
		client.print("\"BSSID\":");
		client.print(WiFi.BSSIDstr(i));
		client.println(",");
		client.print("\"SignalStrength\":");
		client.print(WiFi.RSSI(i));
		client.println(",");
		client.print("\"Encryption\":");
		client.print(WiFi.encryptionType(i));
		client.println(",");
		client.print("\"Channel\":");
		client.print(WiFi.channel(i));
		client.println(",");
		client.print("\"Hidden\":");
		client.print(WiFi.isHidden(i));
		client.println(",");

		if (i < numberOfNetworks - 1) {
			client.println("},");
		}
		else {
			client.println("}");
		}*/

		/*
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
			}*/
	Serial.println("Data:");
	Serial.println(data);
	Serial.println();

	client.println(data);

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