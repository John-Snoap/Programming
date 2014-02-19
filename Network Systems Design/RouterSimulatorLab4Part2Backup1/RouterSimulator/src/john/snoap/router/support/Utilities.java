package john.snoap.router.support;

import java.util.Calendar;

public class Utilities
{

	/**
	 * This static variable is the value for the number of seconds in the
	 * current time since some time back in the 70's. It's used to calculate the
	 * number of seconds since the program began by the method which follows.
	 */
	public static long baseDateSeconds = Calendar.getInstance().getTimeInMillis() / 1000;

	/**
	 * This method returns the number of seconds since the program began.
	 * 
	 * @return
	 */
	public static int getTimeInSeconds()
	{
		return (int) (Calendar.getInstance().getTimeInMillis() / 1000 - baseDateSeconds);
	} // end public static method getTimeInSeconds

	// Pat Smith's padding methods
	public static String padHexString(String hexString, int length)
	{
		String paddedString = new String(hexString);
		/*
		 * here we run the loop starting at the current length (which has two
		 * characters for each byte. We run to 2*length because length is in
		 * bytes, but there are two characters per byte.
		 */
		for (int i = paddedString.length(); i < length * 2; i++)
			paddedString = "0" + paddedString;
		return paddedString;
	} // end public static method padHexString

	/**
	 * PrependString(String, Int) - given a string, prepend spaces to it and
	 * extend it to the given length.
	 * 
	 * @param s
	 * @param length
	 * @return
	 */
	public static String prependString(String s, int length)
	{
		String paddedString = new String(s);
		for (int i = paddedString.length(); i < length; i++)
			paddedString = " " + paddedString;
		return paddedString;
	} // end public static method prependString

	// my methods that I might use
	// I stole this method from online:
	// http://stackoverflow.com/questions/5513152/easy-way-to-concatenate-two-byte-arrays
	public static byte[] concat(byte[]... arrays)
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
			System.arraycopy(arrays[i], 0, result, currentIndex,
					arrays[i].length);
			currentIndex += arrays[i].length;
		} // end for loop

		return result;
	} // end public static method concat

	public byte[] getHexBytesFromHexString(String hexString)
	{
		byte[] hexBytes;

		try
		{
			hexBytes = parseHexBinary(hexString.replaceAll("\\s+", ""));
		} // end try
		catch (java.lang.IllegalArgumentException e)
		{
			hexBytes = new byte[] { -1 };
		} // end catch

		return hexBytes;
	} // end public method getHexBytesFromHexString

	public String getHexStringFromHexBytes(byte[] hexByte)
	{
		return printHexBinary(hexByte);
	} // end public method getHexStringFromHexBytes

	private byte[] parseHexBinary(String s)
	{
		final int len = s.length();

		// "111" is not a valid hex encoding.
		if (len % 2 != 0)
			throw new IllegalArgumentException("hexBinary needs to be even-length: " + s);

		byte[] out = new byte[len / 2];

		for (int i = 0; i < len; i += 2)
		{
			int h = hexToBin(s.charAt(i));
			int l = hexToBin(s.charAt(i + 1));
			if (h == -1 || l == -1)
				throw new IllegalArgumentException("contains illegal character for hexBinary: " + s);

			out[i / 2] = (byte) (h * 16 + l);
		} // end for loop

		return out;
	} // end private method parseHexBinary

	private String printHexBinary(byte[] data)
	{
		StringBuilder r = new StringBuilder(data.length * 2);

		for (byte b : data)
		{
			r.append(hexCode[(b >> 4) & 0xF]);
			r.append(hexCode[(b & 0xF)]);
		} // end for loop

		return r.toString();
	} // end private method printHexBinary

	private static int hexToBin(char ch)
	{
		if ('0' <= ch && ch <= '9')
			return ch - '0';
		if ('A' <= ch && ch <= 'F')
			return ch - 'A' + 10;
		if ('a' <= ch && ch <= 'f')
			return ch - 'a' + 10;
		return -1;
	} // end private static method hexToBin

	private static final char[] hexCode = "0123456789ABCDEF".toCharArray();
} // end class Utilities
