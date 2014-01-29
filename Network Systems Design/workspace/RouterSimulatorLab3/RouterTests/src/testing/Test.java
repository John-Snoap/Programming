package testing;

public class Test
{
	// private data
	public int destinationLL2P_MACaddress;
	public String destinationLL2P_MACaddressString;
	public byte[] payload;
	public String payloadString;
	// end private data

	/**
	 * @param args
	 */
	public static void main(String[] args)
	{
		/*
		Test test = new Test();
		Test test2 = new Test();
		System.out.println(test.destinationLL2P_MACaddressString);
		System.out.println(test.destinationLL2P_MACaddress);
		System.out.println(test.payloadString);
		System.out.println(test.payload);
		
		test.setPayload("Watch This!");
		test2.setPayload(test.payload);
		System.out.println(test2.payloadString);
		*/
		
		byte[] one = new byte[] {0, 1, 2};
		byte[] two = new byte[] {3, 4, 5};
		byte[] both = Test.concat(one, two);
		
		System.out.println("\nFirst time showing both:  ");
		for (byte b : both)
		{
			System.out.println(b);
		} // end for each loop
		
		both[0] = 5;
		
		System.out.println("\nSecond time showing both:  ");
		for (byte b : both)
		{
			System.out.println(b);
		} // end for each loop
		
		System.out.println("\nShowing one:  ");
		for (byte b : one)
		{
			System.out.println(b);
		} // end for each loop
	} // end main
	
	public Test()
	{
		byte[] dumb = new byte[] {1, 2, 3, 4};
		setDestinationLL2P_MACaddress("");
		//setPayload(dumb);
		setPayload("");
	} // end constructor
	
	public static byte[] concat(byte[]...arrays)
	{
	    // Determine the length of the result array
	    int totalLength = 0;
	    for (int i = 0; i < arrays.length; i++)
	    {
	        totalLength += arrays[i].length;
	    } // end for loop

	    // create the result array
	    byte[] result = new byte[totalLength];

	    // copy the source arrays into the result array
	    int currentIndex = 0;
	    for (int i = 0; i < arrays.length; i++)
	    {
	        System.arraycopy(arrays[i], 0, result, currentIndex, arrays[i].length);
	        currentIndex += arrays[i].length;
	    } // end for loop

	    return result;
	} // end public static method concat
	
	
	
	
	
	
	
	public byte[] getPayload()
	{
		// we must return a copy (not a reference) so that client code cannot mess up the private data variable
		byte[] copyOfPayload = new byte[payload.length];
		
		// loop to copy the byte values
		for (int index = 0; index < payload.length; index++)
		{
			copyOfPayload[index] = payload[index];
		} // end for loop
		
		return copyOfPayload;
	} // end getter getPayload
	
	public String getPayloadString()
	{
		return payloadString;
	} // end getter getPayloadString
	
	public void setPayload(byte[] value)
	{
		// we must keep a copy (not a reference) so that client code cannot mess up the private data variable
		payload = new byte[value.length];
		
		// loop to copy the byte values
		for (int index = 0; index < payload.length; index++)
		{
			payload[index] = value[index];
		} // end for loop
		
		payloadString = new String(payload);
	} // end setter setPayload
	
	public void setPayload(String value)
	{
//		if (value.length() > 0)
//		{
//			payloadString = value;
//			payload = payloadString.getBytes();
//		} // end if
//		else
//		{
//			payloadString = "";
//			payload = new byte[] {};
//		} // end else
		
		payloadString = value;
		payload = payloadString.getBytes();
	} // end setter setPayload
	
	
	
	
	
	
	
	
	
	
	
	
	public void setDestinationLL2P_MACaddress(int value)
	{
		try
		{
			if (value > 0xFFFFFF || value < 0)
			{
				System.out.println("Dest MAC Addr int is wrong!");
				destinationLL2P_MACaddressString = "";
			} // end if
			else
			{
				destinationLL2P_MACaddress = value;
				destinationLL2P_MACaddressString = padHexString(Integer.toHexString(destinationLL2P_MACaddress), LL2P_ADDRESS_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from an int to a hex string\nwhile setting the destination MAC Address (int).\n\n");
			destinationLL2P_MACaddress = 0;
			destinationLL2P_MACaddressString = "";
		} // end catch
	} // end setter setDestinationMACaddress
	
	public void setDestinationLL2P_MACaddress(String value)
	{
		try
		{
			if (value.length() > (2*LL2P_ADDRESS_LENGTH))
			{
				System.out.println("Dest MAC Addr string greater than 6 characters!");
				destinationLL2P_MACaddressString = "";
			} // end if
			else
			{
				destinationLL2P_MACaddressString = padHexString(value, LL2P_ADDRESS_LENGTH);
				destinationLL2P_MACaddress = Integer.valueOf(destinationLL2P_MACaddressString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from a hex string to an int\nwhile setting the destination MAC Address (String).\n\n");
			destinationLL2P_MACaddressString = "";
			destinationLL2P_MACaddress = 0;
		} // end catch
	} // end setter setDestinationMACaddressString
	
	// Pat Smith's padding methods
	public static String padHexString(String hexString, int length)
	{
		String paddedString = new String(hexString);
		/*
		 * here we run the loop starting at the current length (which has two characters for each byte.  
		 * We run to 2*length because length is in bytes, but there are two characters per byte.
		 */
		for (int i = paddedString.length(); i < length*2; i++)
			paddedString = "0" + paddedString;
		return paddedString;
	} // end public static method padHexString
	
	/**
	 * PrependString(String, Int) - given a string, prepend spaces to it and extend
	 *   it to the given length.  
	 * @param s
	 * @param length
	 * @return
	 */
	public static String prependString(String s, int length)
	{
		String paddedString = new String(s);
		for (int i=paddedString.length(); i<length;i++)
			paddedString = " " + paddedString;
		return paddedString;
	} // end public static method prependString
	
	// my MAC Address
	public static final String MY_LL2P_ADDRESS = "f001ed";
	
	// LL2P type fields
	public static final String LL3P_PACKET_PAYLOAD = "8001";
	public static final String ARP_PACKET_PAYLOAD = "8002";
	public static final String LRP_PACKET_PAYLOAD = "8003";
	
	// LL2P field lengths
	public static final int LL2P_ADDRESS_LENGTH = 3;
	public static final int CRC_LENGTH = 2;
	public static final int LL2P_TYPE_LENGTH = 2;
} // end class Test
