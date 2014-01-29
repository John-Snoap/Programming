package crc16;

public class Command
{
	// class Command tester
	public static void main(String[] args)
	{
		// simulate a user entering the code via a copy and paste from the internet
		Command appleRemoteVolumeDown = new Command("0000 006e 0024 0000 0156 00ac 0016 0016 0016 0041 0016 0040 0016 0040 0016 0016 0016 0041 0016 0041 0016 0041 0016 0041 0016 0041 0016 0040 0016 0016 0016 0016 0016 0016 0016 0016 0016 0041 0016 0016 0016 0016 0016 0040 0016 0041 0016 0016 0016 0016 0016 0016 0016 0016 0016 0016 0016 0041 0016 0041 0016 0016 0016 0040 0016 0040 0016 0040 0016 0016 0016 058d 0156 0055 0016 00ac");
		
		
		
		System.out.println(appleRemoteVolumeDown);
		
		byte[] commandBytes = appleRemoteVolumeDown.getCommand();
		
		// check to see if there are the correct number of bytes in this command
		// there should be 76*2  or 152 bytes
		for (int number = 0; number < commandBytes.length; number++)
		{
			System.out.println("Byte number " + (number+1) + " is " + commandBytes[number]);
		} // end for each loop
		
		System.out.println();
		// perform an error check
		if (commandBytes[0] != -1)
		{
			// simulate getting a code from the microprocessor
			Command appleRemoteVolumeDown2 = new Command(commandBytes);
			
			// see if they give the same numbers
			System.out.println(appleRemoteVolumeDown2);
			System.out.println(appleRemoteVolumeDown);
		} // end if
		else
		{
			System.out.println("The command for appleRemoteVolumeDown is not a valid command.");
		} // end else
		
		System.out.println();
		// simulate getting an erroneous code from the microprocessor
		commandBytes[0] = (byte) -0xFFF;
		Command appleRemoteVolumeDown2 = new Command(commandBytes);
		
		// see if they give the same numbers
		System.out.println(appleRemoteVolumeDown2);
		System.out.println(appleRemoteVolumeDown);
		
		byte[] bytes = appleRemoteVolumeDown.getCommand();
		
		System.out.println("\nLet's see what the bytes are!");
		// test to see what the bytes are
		for (byte B : bytes)
		{
			System.out.print(B + ", ");
		} // end for loop
		
		System.out.println();
		System.out.println(appleRemoteVolumeDown);
	} // end class Command tester
	
	
	// private data
	private String prontoString = ""; // default value "" to let client code know it is not set
	//private int[] prontoInts = {-1}; // default value -1 to let client code know it is not set
	//private byte[] prontoBytes = {-1}; // default value -1 to let client code know it is not set
	private byte[] prontoBytes;
	
	
	// constructors
	// default constructor
	public Command(String prontoCode)
	{
		setCommand(prontoCode);
	} // end default constructor
	
	// secondary constructor
	public Command(byte[] prontoCode)
	{
		setCommand(prontoCode);
	} // end secondary constructor
	
	
	// public methods
	@Override public String toString()
	{
		return prontoString;
	} // end property GetProntoString
	
	public void setCommand(String prontoCode)
	{
		prontoString = prontoCode; // so the client code can always see what string was input
		CreateProntoBytesFromProntoString();
		
		if (prontoBytes[0] != -1) // if the command bytes are valid
		{
			CreateProntoStringFromProntoBytes(); // make the string look nice
		} // end if
	} // end property SetProntoString
	
	public byte[] getCommand()
	{
		// we must return a copy (not a reference) so that client code cannot mess up the private data variable
		byte[] copyOfProntoBytes = new byte[prontoBytes.length];
		
		// loop to copy the byte values
		for (int index = 0; index < prontoBytes.length; index++)
		{
			copyOfProntoBytes[index] = prontoBytes[index];
		} // end for loop
		
		return copyOfProntoBytes;
	} // end property GetProntoBytes
	
	public void setCommand(byte[] prontoCode)
	{
		prontoBytes = prontoCode;
		CreateProntoStringFromProntoBytes();
	} // end property SetProntoBytes
	
	
	// private methods
	private void CreateProntoBytesFromProntoString()
	{
		try
		{
			//prontoBytes = javax.xml.bind.DatatypeConverter.parseHexBinary(prontoString.replaceAll("\\s+", ""));
			prontoBytes = parseHexBinary(prontoString.replaceAll("\\s+", ""));
		} // end try
		catch (java.lang.IllegalArgumentException e)
		{
			prontoBytes = new byte[] {-1};
		} // end catch
	} // end method CreateProntoBytesFromProntoString
	
	private void CreateProntoStringFromProntoBytes()
	{
		//prontoString = (javax.xml.bind.DatatypeConverter.printHexBinary(prontoBytes)).replaceAll("....(?!$)", "$0 ");
		//prontoString = (printHexBinary(prontoBytes)).replaceAll("....(?!$)", "$0 ");
		prontoString = (printHexBinary(prontoBytes));//.replaceAll("..(?!$)", "$0");
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
} // end class Command
