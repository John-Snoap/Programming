package testing;

public class CRC16CCITT
{
	/**
	 * @param args
	 */
	public static void main(String[] args)
	{
		int crc = 0x0; // initial value
        int polywhirl = 0x1021; // P(x) = 2^16 + 2^12 + 2^5 + 2^0 which is technically 0x11021
    							// but 0x1021 works too and is therefore probably more efficient
        						// polywhirl is just my cute name for polynomial
        						// Polywhirl is a 1st generation Pokemon
        boolean bit; // each bit for each byte as we calculate the CRC
        boolean crc15bit; // 2^15 place bit in the CRC
        int tempResult;
        int tempCRC = 0;
        String testString = "123456789";
        int count = 0;
        final int WORD_SIZE = 8; // 8 bits equals 1 byte equals 1 word

        byte[] testBytes = testString.getBytes();
        //testString.getBytes("ASCII");

        //byte[] bytes = args[0].getBytes();
        byte[] bytes = testBytes;
/*
        for (byte word : bytes) // go through one word (byte) at a time
        {
        	System.out.println("word:  " + Integer.toHexString(word << WORD_SIZE));
            for (int i = 0; i < WORD_SIZE; i++) // go through one bit at a time
            {
            	// get the specific bit we want from the byte starting with MSbit down to LSbit
            	// 2^7 place bit down to 2^0 place bit
            	// shift the bit we want down to 2^0 and mask it with 1, then compare it to 1
                bit = (((word >>> (7-i)) & 1) == 1);
                
                // get the 2^15 place bit in the CRC
                crc15bit = (((crc >>> 15) & 1) == 1);
                
                // shift the CRC one bit to the left
                crc <<= 1;
                
                if (crc15bit ^ bit)
            	{
                	crc ^= polywhirl;
            	} // end if
                
                //crc &= 0xFFFF;
                
                count++;
                System.out.println("CRC After:  " + Integer.toHexString(crc) + " Count:  " + count);
            } // end for loop
        } // end for each loop
*/
///*
        for (byte word : bytes) // go through one word (byte) at a time
        {
        	//word <<= 8;
        	tempResult = (word << WORD_SIZE) ^ tempCRC;
        	crc ^= (word << WORD_SIZE);
        	System.out.println("crc after shift for a byte:  " + Integer.toHexString(crc));
            for (int i = 0; i < WORD_SIZE; i++) // go through one bit at a time
            {
            	// get the specific bit we want from the byte starting with MSbit down to LSbit
            	// 2^7 place bit down to 2^0 place bit
            	// shift the bit we want down to 2^0 and mask it with 1, then compare it to 1
                //bit = (((word >>> (7-i)) & 1) == 1);
                
                // get the 2^15 place bit in the CRC
                //crc15bit = (((crc >>> 15) & 1) == 1);
                
                // shift the CRC one bit to the left
                //crc <<= 1;
                
                tempResult <<= 1;
                crc <<= 1;
                
                if (((crc >>> (2*WORD_SIZE)) & 1) == 1)
                {
                	tempResult ^= polywhirl;
                	crc ^= polywhirl;
                } // end if
                
                /*
                if (crc15bit ^ bit)
            	{
                	crc ^= polywhirl;
            	} // end if
                */
                
                //crc &= 0xFFFF;
                
                
                count++;
                System.out.println("CRC:\t\t" + Integer.toHexString(crc) + " Count:  " + count);
                System.out.println("tempResult:\t" + Integer.toHexString(tempResult) + " Count:  " + count);
            } // end for loop
            tempCRC = tempResult;
        } // end for each loop
//*/
        crc &= 0xFFFF; // we just want four hex characters
        System.out.println("CRC16-CCITT = " + Integer.toHexString(crc).toUpperCase());
	} // end main
} // end class CRC16CCITT
