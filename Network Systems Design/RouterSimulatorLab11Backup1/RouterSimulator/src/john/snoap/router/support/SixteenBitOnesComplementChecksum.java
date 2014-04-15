package john.snoap.router.support;

public class SixteenBitOnesComplementChecksum
{
	// private data
	private int checksum; // stores the checksum
	private final int ZERO_CHECKSUM = 0x0; // for zeroing the checksum
	// end private data
	
	// getters and setters
	public int getChecksum()
	{
		return checksum; // return the CRC
	} // end getter getCRC

	public String getChecksumHexString()
	{
		return Utilities.padHexString(Integer.toHexString(checksum), 2);// .toUpperCase(Locale.US); // return
											 // the checksum in a hex string
	} // end getter getCRCHexString
	// end getters and setters
	
	// constructors
	// default constructor
	public SixteenBitOnesComplementChecksum()
	{
		resetChecksum(); // set the checksum to zero
	} // end default constructor
	
	// public methods
	public void resetChecksum()
	{
		checksum = ZERO_CHECKSUM; // reset the checksum to zero
	} // end public method resetChecksum
	
	public void calculateChecksum(byte[] buf)
	{
		int length = buf.length;
	    int i = 0;
	
	    checksum = 0;
	    long data;
	
	    // Handle all pairs
	    while (length > 1)
	    {
	    	// Corrected to include @Andy's edits and various comments on Stack Overflow
	    	data = (((buf[i] << 8) & 0xFF00) | ((buf[i + 1]) & 0xFF));
	    	checksum += data;
	    	// 1's complement carry bit correction in 16-bits (detecting sign extension)
	    	if ((checksum & 0xFFFF0000) > 0)
	    	{
	    		checksum = checksum & 0xFFFF;
	    		checksum += 1;
    		}
	    	
	    	i += 2;
	    	length -= 2;
    	}
	
	    // Handle remaining byte in odd length buffers
	    if (length > 0)
	    {
	    	// Corrected to include @Andy's edits and various comments on Stack Overflow
	    	checksum += (buf[i] << 8 & 0xFF00);
	    	// 1's complement carry bit correction in 16-bits (detecting sign extension)
	    	if ((checksum & 0xFFFF0000) > 0)
	    	{
	    		checksum = checksum & 0xFFFF;
	    		checksum += 1;
    		} // end if
	    } // end if
	
	    // Final 1's complement value correction to 16-bits
	    checksum = ~checksum;
	    checksum = checksum & 0xFFFF;
    } // end public method calculateChecksum
	
	// override methods
	@Override
	public String toString()
	{
		return Integer.toString(checksum); // return the decimal representation of
										// the checksum in a string
	} // end override public method toString
	// end override methods
	// end public methods
} // end class SixteenBitOnesComplementChecksum
