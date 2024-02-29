/*
  program Game Controller as HID

  Upload this file to your Arduino,
  you can check with Notepad / Word or any other text based program
  the output of the joystick button (space)
*/

#include "Keyboard.h"

// -------
// DEFINES
// -------
#define NR_OF_ARRAY_ELEMENTS( array ) ( sizeof( array ) / sizeof( typeof( *array ) ) )


// ---------
// CONSTANTS
// ---------
enum pinModes {
    UNUSED_PIN     = -1,
    DIGITAL_INPUT  =  0,
    ANALOG_INPUT   =  1,
    DIGITAL_OUTPUT =  2,
    ANALOG_OUTPUT  =  3,
    SERVO          =  4
};

const int JOYSTICK_BUTTON_PIN =  2;
const int     LEFT_BUTTON_PIN =  3;
const int       UP_BUTTON_PIN =  4;
const int     DOWN_BUTTON_PIN =  5;
const int    RIGHT_BUTTON_PIN =  6;
const int    FIRE2_BUTTON_PIN =  7;
const int    FIRE1_BUTTON_PIN =  8;

int PIN_MODES[] {
     UNUSED_PIN,     // pin 00, TX
     UNUSED_PIN,     // pin 01, RX
    DIGITAL_INPUT,   // pin 02, joystick button
    DIGITAL_INPUT,   // pin 03, left button
    DIGITAL_INPUT,   // pin 04, up button
    DIGITAL_INPUT,   // pin 05, down button
    DIGITAL_INPUT,   // pin 06, right button
    DIGITAL_INPUT,   // pin 07, fire 2 button
    DIGITAL_INPUT,   // pin 08, fire 1 button
    DIGITAL_INPUT,     // pin 09, servo, does not properly do PWM if one or more servo pins are used
     ANALOG_OUTPUT,  // pin 10, right led
     ANALOG_OUTPUT,  // pin 11, middle led
     ANALOG_OUTPUT,  // pin 12, left led
    DIGITAL_OUTPUT,  // pin 13, onboard led
     ANALOG_INPUT,   // pin 14, joystick y axis
     ANALOG_INPUT,   // pin 15, joystick x axis
};

const int MAX_PINS = NR_OF_ARRAY_ELEMENTS( PIN_MODES );


// ----------------
// GLOBAL VARIABLES
// ----------------
int   isButtonPressed[MAX_PINS];
int  wasButtonPressed[MAX_PINS];
char button2character[MAX_PINS] = {
    0, 0, 0, 'j', 'w', 'k', 'a', 'd', 0, 0, 0, 0, 0, 0, 0, 0
};


void setup() {
    delay( 3000 );     // to make reprogramming etc. easier
    
    set_all_pinmodes();
    
    Serial.begin( 9600 );
}


void loop() {
    read_all_inputs();
    write_keyboard_outputs();
    delay( 50 );
}


void set_all_pinmodes()
{
    for ( int pin = 0 ; pin < MAX_PINS ; pin++ ) {
        if ( PIN_MODES[pin] != UNUSED_PIN ) {
            pinMode( pin , PIN_MODES[pin] );
        }
    }
}


void read_all_inputs()
{
    for ( int pin = 0 ; pin < MAX_PINS ; pin++ ) {
        if ( PIN_MODES[pin] == DIGITAL_INPUT ) {
            wasButtonPressed[pin] = isButtonPressed[pin];
            isButtonPressed[pin] = digitalRead( pin );
        }
    }
}


void write_keyboard_outputs()
{
    for ( int pin = 0 ; pin < MAX_PINS ; pin++ ) {
        if ( PIN_MODES[pin] == DIGITAL_INPUT ) {
            if ( isButtonPressed[pin] && ! wasButtonPressed[pin] ) {
                Keyboard.press( button2character[ pin ] );
            } else  if ( ! isButtonPressed[pin] && wasButtonPressed[pin] ) {
                Keyboard.release( button2character[ pin ] );   
            }
        }
    }
}
