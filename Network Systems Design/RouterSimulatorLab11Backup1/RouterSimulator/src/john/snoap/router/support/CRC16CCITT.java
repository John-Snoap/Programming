// John Snoap CRC16CCITT.java
//
// use the below link to check CRC values:
// http://depa.usst.edu.cn/chenjq/www2/SDesign/JavaScript/CRCcalculation.htm
//
// I used the following link to help me understand how a CRC16CCITT can be done in java:
// http://introcs.cs.princeton.edu/java/51data/CRC16CCITT.java.html
//
// the above link helped me understand bit shifting in java, but
// ultimately I implemented the CRC16CCITT using professor Pat Smith's method
//
// Binary to Decimal to Hexadecimal Converter:
// http://www.mathsisfun.com/binary-decimal-hexadecimal-converter.html

package john.snoap.router.support;

public class CRC16CCITT
{
	// private data
	private int crc; // stores the CRC
	private int crcResetValue; // stores the CRC reset value
	private final int POLYWHIRL = 0x1021; // P(x) = 2^16 + 2^12 + 2^5 + 2^0
											// which is technically 0x11021
											// but 0x1021 works too because we only have 16 bits
											// POLYWHIRL is just my cute name for polynomial
											// Polywhirl is a 1st generation Pokemon
	private final int WORD_SIZE = 8; // 8 bits equals 1 byte equals 1 word
	private final int LOWER_16_BIT_MASK = 0xFFFF; // our CRC is only 16 bits
	private final int ZERO_CRC = 0x0; // for zeroing the CRC
	// end private data

	// getters and setters
	public int getCRC()
	{
		return crc; // return the CRC
	} // end getter getCRC

	public String getCRCHexString()
	{
		return Utilities.padHexString(Integer.toHexString(crc), 2);// .toUpperCase(Locale.US); // return
										// the CRC in a hex string
	} // end getter getCRCHexString
	// end getters and setters

	// constructors
	// default constructor
	public CRC16CCITT()
	{
		setCRCResetValue(ZERO_CRC); // set the reset CRC value to zero
		resetCRC(); // set the CRC to zero
	} // end default constructor

	// set CRC constructor
	public CRC16CCITT(int desiredCRC)
	{
		setCRCResetValue(desiredCRC); // set the reset CRC value to the desired
										// CRC value
		crc = desiredCRC; // set the CRC to the desired CRC value
	} // end set CRC constructor
	// end constructors

	// public methods
	public void setCRCResetValue(int desiredResetCRC)
	{
		crcResetValue = desiredResetCRC; // set the reset value to the desired
											// reset CRC
	} // end public method setCRCResetValue

	public void resetCRC()
	{
		crc = crcResetValue; // reset the CRC
	} // end public method resetCRC

	public void setCRC(int desiredCRC)
	{
		crc = desiredCRC; // set the CRC to the desired CRC
	} // end public method setCRC

	public void update(final byte[] bytes)
	{
		for (byte word : bytes) // go through one word (byte) at a time
		{
			crc ^= (word << WORD_SIZE); // XOR the word shifted over 1 byte with
										// the CRC

			for (int i = 0; i < WORD_SIZE; i++) // go through one bit at a time
			{
				crc <<= 1; // shift the CRC one bit to the left

				// this comparison is comparing the bit in the 2^16 place to 1
				// shift the bit we want down to 2^0 and mask it with 1, then
				// compare it to 1
				if (((crc >>> (2 * WORD_SIZE)) & 1) == 1) {
					crc ^= POLYWHIRL;
				} // end if
			} // end for loop
		} // end for each loop

		crc &= LOWER_16_BIT_MASK; // we just want four hex characters
	} // end public method update

	// override methods
	@Override
	public String toString()
	{
		return Integer.toString(crc); // return the decimal representation of
										// the CRC in a string
	} // end override public method toString
	// end override methods
	// end public methods
} // end class CRC16CCITT
