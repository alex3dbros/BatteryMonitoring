/*
 Name:    BatMon.ino
 Created: 2/29/2020 1:04:17 PM
 Author:  Me
*/
#include <Arduino.h>
#include <bluefruit.h>
#include <Adafruit_ADS1015.h>

typedef volatile uint32_t REG32;
#define pREG32 (REG32 *)

#define DEVICE_ID_HIGH    (*(pREG32 (0x10000060)))
#define DEVICE_ID_LOW     (*(pREG32 (0x10000064)))

BLEService        bleService = BLEService(0xfff0);

BLECharacteristic c1Char = BLECharacteristic(0xfff1);
BLECharacteristic c2Char = BLECharacteristic(0xfff2);
BLECharacteristic c3Char = BLECharacteristic(0xfff3);
BLECharacteristic c4Char = BLECharacteristic(0xfff4);

BLECharacteristic c5Char = BLECharacteristic(0xfff5);
BLECharacteristic c6Char = BLECharacteristic(0xfff6);
BLECharacteristic c7Char = BLECharacteristic(0xfff7);
BLECharacteristic c8Char = BLECharacteristic(0xfff8);

BLECharacteristic temp1 = BLECharacteristic(0xff01);
BLECharacteristic temp2 = BLECharacteristic(0xff02);

unsigned long long               lastSent = 0;
unsigned long long               lastSentLoop = 0;
int val;

uint16_t output;
Adafruit_ADS1115 ads0(0x48);
Adafruit_ADS1115 ads1(0x49);

BLEDis bledis;    // DIS (Device Information Service) helper class instance
//BLEBas blebas;    // BAS (Battery Service) helper class instance

uint8_t  bps = 0;

String uuid;
int64_t cellsVoltage[] = { 0, 0, 0, 0, 0, 0, 0, 0 };
int64_t temps[] = { 0, 0};

void setup()
{
  Serial.begin(115200);
  while (!Serial) delay(10);   // for nrf52840 with native usb

  // Initialise the Bluefruit module
  Bluefruit.begin();

  // Set the advertised device name
  uuid = String(DEVICE_ID_HIGH, HEX) + String(DEVICE_ID_LOW, HEX);
  String name = "BM-" + uuid;
  Bluefruit.setName(name.c_str());

  // Set the connect/disconnect callback handlers
  Bluefruit.Periph.setConnectCallback(connect_callback);
  Bluefruit.Periph.setDisconnectCallback(disconnect_callback);

  // Configure and Start the Device Information Service

  bledis.setManufacturer("3D Brothers");
  bledis.setModel("Battery Monitor V 1.0");
  bledis.begin();

  // Start the BLE Battery Service and set it to 100%
  //blebas.begin();
  //blebas.write(100);

  // Setup the Heart Rate Monitor service using
  // BLEService and BLECharacteristic classes
  Serial.println("Configuring the Heart Rate Monitor Service");

  setupCellChars();

  // Setup the advertising packet(s)
  startAdv();
  ads0.begin();
  ads1.begin();


}

void startAdv(void)
{
  // Advertising packet
  Bluefruit.Advertising.addFlags(BLE_GAP_ADV_FLAGS_LE_ONLY_GENERAL_DISC_MODE);
  Bluefruit.Advertising.addTxPower();

  // Include HRM Service UUID
  Bluefruit.Advertising.addService(bleService);

  // Include Name
  Bluefruit.Advertising.addName();

  Bluefruit.Advertising.restartOnDisconnect(true);
  Bluefruit.Advertising.setInterval(32, 244);    // in unit of 0.625 ms
  Bluefruit.Advertising.setFastTimeout(30);      // number of seconds in fast mode
  Bluefruit.Advertising.start(0);                // 0 = Don't stop advertising after n seconds  
}

void setupCellChars() {
  bleService.begin();

  c1Char.setProperties(CHR_PROPS_READ);
  c1Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c1Char.setFixedLen(2);
  c1Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c1Char.begin();
  c1Char.write16(0);

  c2Char.setProperties(CHR_PROPS_READ);
  c2Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c2Char.setFixedLen(2);
  c2Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c2Char.begin();
  c2Char.write16(0);

  c3Char.setProperties(CHR_PROPS_READ);
  c3Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c3Char.setFixedLen(2);
  c3Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c3Char.begin();
  c3Char.write16(0);

  c4Char.setProperties(CHR_PROPS_READ);
  c4Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c4Char.setFixedLen(2);
  c4Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c4Char.begin();
  c4Char.write16(0);

  c5Char.setProperties(CHR_PROPS_READ);
  c5Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c5Char.setFixedLen(2);
  c5Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c5Char.begin();
  c5Char.write16(0);

  c6Char.setProperties(CHR_PROPS_READ);
  c6Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c6Char.setFixedLen(2);
  c6Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c6Char.begin();
  c6Char.write16(0);

  c7Char.setProperties(CHR_PROPS_READ);
  c7Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c7Char.setFixedLen(2);
  c7Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c7Char.begin();
  c7Char.write16(0);

  c8Char.setProperties(CHR_PROPS_READ);
  c8Char.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  c8Char.setFixedLen(2);
  c8Char.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  c8Char.begin();
  c8Char.write16(0);

  temp1.setProperties(CHR_PROPS_READ);
  temp1.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  temp1.setFixedLen(2);
  temp1.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  temp1.begin();
  temp1.write16(0);

  temp2.setProperties(CHR_PROPS_READ);
  temp2.setPermission(SECMODE_OPEN, SECMODE_NO_ACCESS);
  temp2.setFixedLen(2);
  temp2.setCccdWriteCallback(cccd_callback);  // Optionally capture CCCD updates
  temp2.begin();
  temp2.write16(0);

}


void connect_callback(uint16_t conn_handle)
{
  // Get the reference to current connection
  BLEConnection* connection = Bluefruit.Connection(conn_handle);

  char central_name[32] = { 0 };
  connection->getPeerName(central_name, sizeof(central_name));

  Serial.print("Connected to ");
  Serial.println(central_name);

  c1Char.write16(0);
  c2Char.write16(0);
  c3Char.write16(0);
  c4Char.write16(0);

  c5Char.write16(0);
  c6Char.write16(0);
  c7Char.write16(0);
  c8Char.write16(0);

}

/**
 * Callback invoked when a connection is dropped
 * @param conn_handle connection where this event happens
 * @param reason is a BLE_HCI_STATUS_CODE which can be found in ble_hci.h
 */
void disconnect_callback(uint16_t conn_handle, uint8_t reason)
{
  (void)conn_handle;
  (void)reason;

  Serial.print("Disconnected, reason = 0x"); Serial.println(reason, HEX);
  Serial.println("Advertising!");
}

void cccd_callback(uint16_t conn_hdl, BLECharacteristic* chr, uint16_t cccd_value)
{
  // Display the raw request packet
  Serial.print("CCCD Updated: ");
  //Serial.printBuffer(request->data, request->len);
  Serial.print(cccd_value);
  Serial.println("");

  // Check the characteristic this CCCD update is associated with in case
  // this handler is used for multiple CCCD records.

}

void loop()
{
  digitalToggle(LED_RED);
  

  //if (millis() > 1000 && (millis() - 1000) > lastSentLoop)
  //{
	 // //Serial.println("looping");
	 // lastSentLoop = millis();

	 // // Unique Device ID
	 // Serial.print("Device ID  : ");
	 // Serial.print(DEVICE_ID_HIGH, HEX);
	 // Serial.println(DEVICE_ID_LOW, HEX);

	 //

	 // //sprintf(data, "Device: %s", String(DEVICE_ID_HIGH, HEX) + String(DEVICE_ID_LOW, HEX));


	 // Serial.println(uuid);


  //}

  
  if (Bluefruit.connected()) {

	c1Char.write16(0);
	c2Char.write16(0);
	c3Char.write16(0);
	c4Char.write16(0);

	c5Char.write16(0);
	c6Char.write16(0);
	c7Char.write16(0);
	c8Char.write16(0);

	temp1.write16(0);
	temp2.write16(0);


    while (Bluefruit.connected()) {


		if (millis() > 1000 && (millis() - 1000) > lastSentLoop)
		{
			//Serial.println("looping");
			lastSentLoop = millis();

		}



      if (millis() > 100 && (millis() - 100) > lastSent) {


		lastSent = millis();

		readVoltage();
		readTemp();

		c1Char.write16(cellsVoltage[0]);
		c2Char.write16(cellsVoltage[1]);
		c3Char.write16(cellsVoltage[2]);
		c4Char.write16(cellsVoltage[3]);

		c5Char.write16(cellsVoltage[4]);
		c6Char.write16(cellsVoltage[5]);
		c7Char.write16(cellsVoltage[6]);
		c8Char.write16(cellsVoltage[7]);

		temp1.write16(temps[0]);
		temp2.write16(temps[1]);


        //Serial.print(F("counter = "));
        //Serial.println(cell1Characteristic.value(), DEC);
      }

    }
  }


  //if (Bluefruit.connected()) {
  //  uint8_t hrmdata[2] = { 0b00000110, bps++ };           // Sensor connected, increment BPS value


  //  c1Char.write16(4000);

  //}

  // Only send update once per second
  //delay(1000);
}
