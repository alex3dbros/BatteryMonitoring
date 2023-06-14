/* 
	Editor: https://www.visualmicro.com/
			This file is for intellisense purpose only.
			Visual micro (and the arduino ide) ignore this code during compilation. This code is automatically maintained by visualmicro, manual changes to this file will be overwritten
			The contents of the _vm sub folder can be deleted prior to publishing a project
			All non-arduino files created by visual micro and all visual studio project or solution files can be freely deleted and are not required to compile a sketch (do not delete your own code!).
			Note: debugger breakpoints are stored in '.sln' or '.asln' files, knowledge of last uploaded breakpoints is stored in the upload.vmps.xml file. Both files are required to continue a previous debug session without needing to compile and upload again
	
	Hardware: Adafruit Feather nRF52832, Platform=nrf52, Package=adafruit
*/

#if defined(_VMICRO_INTELLISENSE)

#ifndef _VSARDUINO_H_
#define _VSARDUINO_H_
#define F_CPU 64000000
#define ARDUINO 108012
#define ARDUINO_NRF52832_FEATHER
#define ARDUINO_ARCH_NRF52
#define NRF52832_XXAA
#define NRF52
#define SOFTDEVICE_PRESENT
#define ARDUINO_NRF52_ADAFRUIT
#define NRF52_SERIES
#define LFS_NAME_MAX 64
#define CFG_DEBUG 0
#define CFG_LOGGER 1
#define CFG_SYSVIEW 0
#define __cplusplus 201103L
#define __ARM__
#define __arm__
#define __inline__
#define __asm__(x)
#define __attribute__(x)
#define __extension__

typedef int __builtin_va_list;
#define __STATIC_INLINE static inline
#define __ASSEMBLY__
#define __INLINE
#undef _WIN32
#define __GNUC__ 1
//#define __ICCARM__
//define __ARMCC_VERSION 6010050
#define __builtin_offsetof //(TYPE, int) 
#define SVCALL(SD_MUTEX_NEW, uint32_t, nrf_mutex_t);
#define _GCC_LIMITS_H_

//adafruit
typedef long __SIZE_TYPE__;
typedef long __INTPTR_TYPE__;

#include "arduino.h"
#include <variant.h> 
#include <variant.cpp> 
#undef cli
#define cli()
#include "BatMon.ino"
#include "Functions.ino"
#endif
#endif
