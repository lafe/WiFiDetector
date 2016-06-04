#WiFi Detector
The idea behind this project is to use the capabilities of the [ESP8266](https://en.wikipedia.org/wiki/ESP8266) to monitor the quality 
of the available WiFis in the surroundings. By using multiple chips, it is, in theory, possible to analyze the quality of the WiFi in
(almost) real time and see if there are issues that need to be fixed.

Currently, the project is nothing more than a working proof of concept - the main goal was to play with Arduino and ASP.net Core RC2. 
However, the code is working (on my machine ;)) and maybe someone has interest in it.  

##Requirements
###Hardware
The hardware code targets ESP8266 based devices. The code itself uses the [Arduino core for ESP8266](https://github.com/esp8266/Arduino). 
For development, I used the [NodeMCU](http://nodemcu.com/index_en.html) board but it should be possible to use any ESP8266 based device.
By changing the included headers, it should be possible to run the code on a "normal" Arduino board with the WiFi shield.

###Software
The code has been written using Visual Studio 2015 and targets ASP.net Core RC2, Entity Framework Core with SQL Server and [Arduino core for ESP8266](https://github.com/esp8266/Arduino).
I used the [Arduino IDE for Visual Studio](https://visualstudiogallery.msdn.microsoft.com/069a905d-387d-4415-bc37-665a5ac9caba) to develop and deploy 
the Arduino code.  

##Configuration
###ESP8266
To connect to your local WiFi, you have to provide a `credentials.h` file. 
The project includes a [template](./Arduino/WiFiDetector/WiFiCredentials.h.template) for the credentials file
that shows the contents of the file. Copy the template to `credentials.h` and modify it to include the SSID of your network and the password.

You may also want to change the location (e.g. IP, Port) of the Web Service, so that the device can send the information to the correct location.

###Software
You have to configure the connection string in the file [appsettings.json](/src/WiFiDetectorWebPortal/appsettings.json) so that it matches your
environment. The connection string assumes a local SQL Server and a database called "WiFiDetector". The structure of the database can be recreated 
by using the Entity Framework Core Migrations.

##Limitations
As discussed above, it is a proof of concept, so currently there are some limitations:
* No security
* Hardcoded values in the Arduino code base