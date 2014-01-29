package crc16;

public class HexBinary
{
	/**
	 * @param args
	 */
	public static void main(String[] args)
	{

	} // end main
	
	public byte[] getHexBytesFromHexString(String hexString)
	{
		byte[] hexBytes;
		
		try
		{
			//prontoBytes = javax.xml.bind.DatatypeConverter.parseHexBinary(prontoString.replaceAll("\\s+", ""));
			hexBytes = parseHexBinary(hexString.replaceAll("\\s+", ""));
		} // end try
		catch (java.lang.IllegalArgumentException e)
		{
			hexBytes = new byte[] {-1};
		} // end catch
		
		return hexBytes;
	} // end method CreateProntoBytesFromProntoString
	
	public String getHexStringFromHexBytes(byte[] hexByte)
	{
		//prontoString = (javax.xml.bind.DatatypeConverter.printHexBinary(prontoBytes)).replaceAll("....(?!$)", "$0 ");
		//prontoString = (printHexBinary(prontoBytes)).replaceAll("....(?!$)", "$0 ");
		return printHexBinary(hexByte);
	} // end method CreateProntoStringFromProntoBytes
	
	private byte[] parseHexBinary(String s)
	{
		final int len = s.length();
		
		// "111" is not a valid hex encoding.
		if( len%2 != 0 )
			throw new IllegalArgumentException("hexBinary needs to be even-length: "+s);
		
		byte[] out = new byte[len/2];
		
		for( int i=0; i<len; i+=2 )
		{
			int h = hexToBin(s.charAt(i  ));
			int l = hexToBin(s.charAt(i+1));
			if( h==-1 || l==-1 )
				throw new IllegalArgumentException("contains illegal character for hexBinary: "+s);
			
			out[i/2] = (byte)(h*16+l);
		} // end for loop
		
		return out;
	} // end method parseHexBinary
	
	private String printHexBinary(byte[] data)
	{
		StringBuilder r = new StringBuilder(data.length*2);
		
		for ( byte b : data)
		{
			r.append(hexCode[(b >> 4) & 0xF]);
			r.append(hexCode[(b & 0xF)]);
		} // end for loop
		
		return r.toString();
	} // end method printHexBinary
	
	private static int hexToBin( char ch )
	{
		if( '0'<=ch && ch<='9' )    return ch-'0';
		if( 'A'<=ch && ch<='F' )    return ch-'A'+10;
		if( 'a'<=ch && ch<='f' )    return ch-'a'+10;
		return -1;
	} // end static method hexToBin
	
	private static final char[] hexCode = "0123456789ABCDEF".toCharArray();
} // end class HexBinary
