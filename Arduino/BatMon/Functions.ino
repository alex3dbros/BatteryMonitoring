
int64_t vValid(int64_t val) {

	if (val < 50) {

		return 0;

	}
	return val;

}


void readVoltage() {

	int16_t adc0, adc1, adc2, adc3, adc4, adc5, adc6, adc7;
	int16_t vadc0, vadc1, vadc2, vadc3, vadc4, vadc5, vadc6, vadc7;
	int16_t vCell0, vCell1, vCell2, vCell3, vCell4, vCell5, vCell6, vCell7;

	adc0 = ads0.readADC_SingleEnded(0);
	adc1 = ads0.readADC_SingleEnded(1);
	adc2 = ads0.readADC_SingleEnded(2);
	adc3 = ads0.readADC_SingleEnded(3);
	adc4 = ads1.readADC_SingleEnded(0);
	adc5 = ads1.readADC_SingleEnded(1);
	adc6 = ads1.readADC_SingleEnded(2);
	adc7 = ads1.readADC_SingleEnded(3);

	vadc0 = adc0 * (98215.60 / 32767);
	vadc1 = adc1 * (98215.60 / 32767);
	vadc2 = adc2 * (98215.60 / 32767);
	vadc3 = adc3 * (98215.60 / 32767);
	vadc4 = adc4 * (98215.60 / 32767);
	vadc5 = adc5 * (98215.60 / 32767);
	vadc6 = adc6 * (98215.60 / 32767);
	vadc7 = adc7 * (98215.60 / 32767);

	cellsVoltage[0] = vValid(vadc0);
	cellsVoltage[1] = vValid(vadc1 - vadc0);
	cellsVoltage[2] = vValid(vadc2 - vadc1);
	cellsVoltage[3] = vValid(vadc3 - vadc2);

	cellsVoltage[4] = vValid(vadc4 - vadc3);
	cellsVoltage[5] = vValid(vadc5 - vadc4);
	cellsVoltage[6] = vValid(vadc6 - vadc5);
	cellsVoltage[7] = vValid(vadc7 - vadc6);

}

void readTemp() {

	float tDivider = 21.36;

	int t1 = analogRead(A0);
	int t2 = analogRead(A1);

	temps[0] = t1 / tDivider;
	temps[1] = t2 / tDivider;

}