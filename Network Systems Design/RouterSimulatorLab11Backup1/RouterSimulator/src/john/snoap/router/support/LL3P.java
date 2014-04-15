package john.snoap.router.support;

import android.util.Log;

public class LL3P
{
	// private data
	private int destinationLL3P_IPaddress;
	private String destinationLL3P_IPaddressString;
	private int sourceLL3P_IPaddress;
	private String sourceLL3P_IPaddressString;
	private int typeField;
	private String typeFieldString;
	private int identifier;
	private String identifierString;
	private int timeToLive;
	private String timeToLiveString;
	private byte[] payload;
	private String payloadString;
	private SixteenBitOnesComplementChecksum checksum;
	// end private data
	
	// getters and setters
	public int getDestinationLL3P_IPaddress()
	{
		return destinationLL3P_IPaddress;
	} // end getter getDestinationMACaddress

	public String getDestinationLL3P_IPaddressString()
	{
		return destinationLL3P_IPaddressString;
	} // end getter getDestinationMACaddressString

	public void setDestinationLL3P_IPaddress(int value)
	{
		try
		{
			if (value > 0xFFFF || value < 0)
			{
				Log.i("LL3P", "Dest IP Addr int is wrong!");
				destinationLL3P_IPaddress = 0;
				destinationLL3P_IPaddressString = "0000";
			} // end if
			else
			{
				destinationLL3P_IPaddress = value;
				destinationLL3P_IPaddressString = Utilities.padHexString(Integer.toHexString(destinationLL3P_IPaddress), NetworkConstants.LL3P_ADDRESS_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from an int to a hex string\nwhile setting the destination IP Address (int).\n\n");
			destinationLL3P_IPaddress = 0;
			destinationLL3P_IPaddressString = "0000";
		} // end catch
	} // end setter setDestinationMACaddress

	public void setDestinationLL3P_IPaddress(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL3P_ADDRESS_LENGTH))
			{
				Log.i("LL3P", "Dest IP Addr string greater than 4 characters!");
				destinationLL3P_IPaddressString = "0000";
				destinationLL3P_IPaddress = 0;
			} // end if
			else
			{
				destinationLL3P_IPaddressString = Utilities.padHexString(value, NetworkConstants.LL3P_ADDRESS_LENGTH);
				destinationLL3P_IPaddress = Integer.valueOf(destinationLL3P_IPaddressString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from a hex string to an int\nwhile setting the destination IP Address (String).\n\n");
			destinationLL3P_IPaddressString = "0000";
			destinationLL3P_IPaddress = 0;
		} // end catch
	} // end setter setDestinationMACaddressString

	public int getSourceLL3P_IPaddress()
	{
		return sourceLL3P_IPaddress;
	} // end getter getSourceMACaddress

	public String getSourceLL3P_IPaddressString()
	{
		return sourceLL3P_IPaddressString;
	} // end getter getSourceMACaddressString

	public void setSourceLL3P_IPaddress(int value)
	{
		try
		{
			if (value > 0xFFFF || value < 0)
			{
				Log.i("LL3P", "Source IP Addr int is wrong!");
				sourceLL3P_IPaddress = 0;
				sourceLL3P_IPaddressString = "0000";
			} // end if
			else
			{
				sourceLL3P_IPaddress = value;
				sourceLL3P_IPaddressString = Utilities.padHexString(Integer.toHexString(sourceLL3P_IPaddress), NetworkConstants.LL3P_ADDRESS_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from an int to a hex string\nwhile setting the source IP Address (int).\n\n");
			sourceLL3P_IPaddress = 0;
			sourceLL3P_IPaddressString = "0000";
		} // end catch
	} // end setter setSourceMACaddress

	public void setSourceLL3P_IPaddress(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL3P_ADDRESS_LENGTH))
			{
				Log.i("LL3P", "Source IP Addr string greater than 4 characters!");
				sourceLL3P_IPaddressString = "0000";
				sourceLL3P_IPaddress = 0;
			} // end if
			else
			{
				sourceLL3P_IPaddressString = Utilities.padHexString(value, NetworkConstants.LL3P_ADDRESS_LENGTH);
				sourceLL3P_IPaddress = Integer.valueOf(sourceLL3P_IPaddressString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from a hex string to an int\nwhile setting the source IP Address (String).\n\n");
			sourceLL3P_IPaddressString = "0000";
			sourceLL3P_IPaddress = 0;
		} // end catch
	} // end setter setSourceMACaddress

	public int getTypeField()
	{
		return typeField;
	} // end getter getTypeField

	public String getTypeFieldString()
	{
		return typeFieldString;
	} // end getter getTypeFieldString

	public void setTypeField(int value)
	{
		try
		{
			if (value > 0xFFFF || value < 0)
			{
				Log.i("LL3P", "Type Field int is wrong!");
				typeField = 0;
				typeFieldString = "0000";
			} // end if
			else
			{
				typeField = value;
				typeFieldString = Utilities.padHexString(Integer.toHexString(typeField),
				NetworkConstants.LL3P_TYPE_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from an int to a hex string\nwhile setting the type field (int).\n\n");
			typeField = 0;
			typeFieldString = "0000";
		} // end catch
	} // end setter setTypeField

	public void setTypeField(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL3P_TYPE_LENGTH))
			{
				Log.i("LL3P", "Type Field string greater than 4 characters!");
				typeFieldString = "0000";
				typeField = 0;
			} // end if
			else
			{
				typeFieldString = Utilities.padHexString(value, NetworkConstants.LL3P_TYPE_LENGTH);
				typeField = Integer.valueOf(typeFieldString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from a hex string to an int\nwhile setting the type field (String).\n\n");
			typeFieldString = "0000";
			typeField = 0;
		} // end catch
	} // end setter setTypeField
	
	public int getIdentifier()
	{
		return identifier;
	} // end getter getIdentifier

	public String getIdentifierString()
	{
		return identifierString;
	} // end getter getIdentifierString

	public void setIdentifier(int value)
	{
		try
		{
			if (value > 0xFFFF || value < 0)
			{
				Log.i("LL3P", "Identifier int is wrong!");
				identifier = 0;
				identifierString = "0000";
			} // end if
			else
			{
				identifier = value;
				identifierString = Utilities.padHexString(Integer.toHexString(identifier),
				NetworkConstants.LL3P_IDENTIFIER_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from an int to a hex string\nwhile setting the identifier (int).\n\n");
			identifier = 0;
			identifierString = "0000";
		} // end catch
	} // end setter setIdentifier

	public void setIdentifier(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL3P_IDENTIFIER_LENGTH))
			{
				Log.i("LL3P", "Identifier string greater than 4 characters!");
				identifierString = "0000";
				identifier = 0;
			} // end if
			else
			{
				identifierString = Utilities.padHexString(value, NetworkConstants.LL3P_IDENTIFIER_LENGTH);
				identifier = Integer.valueOf(identifierString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from a hex string to an int\nwhile setting the identifier (String).\n\n");
			identifierString = "0000";
			identifier = 0;
		} // end catch
	} // end setter setIdentifier
	
	public int getTTL()
	{
		return timeToLive;
	} // end getter getIdentifier

	public String getTTLstring()
	{
		return timeToLiveString;
	} // end getter getIdentifierString

	public void setTTL(int value)
	{
		try
		{
			if (value > 0xFF || value < 0)
			{
				Log.i("LL3P", "TTL int is wrong!");
				timeToLive = 0;
				timeToLiveString = "00";
			} // end if
			else
			{
				timeToLive = value;
				timeToLiveString = Utilities.padHexString(Integer.toHexString(timeToLive),
				NetworkConstants.LL3P_TTL_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from an int to a hex string\nwhile setting the TTL (int).\n\n");
			timeToLive = 0;
			timeToLiveString = "00";
		} // end catch
	} // end setter setIdentifier

	public void setTTL(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL3P_TTL_LENGTH))
			{
				Log.i("LL3P", "TTL string greater than 2 characters!");
				timeToLiveString = "00";
				timeToLive = 0;
			} // end if
			else
			{
				timeToLiveString = Utilities.padHexString(value, NetworkConstants.LL3P_TTL_LENGTH);
				timeToLive = Integer.valueOf(timeToLiveString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LL3P", "There was an error converting from a hex string to an int\nwhile setting the TTL (String).\n\n");
			timeToLiveString = "00";
			timeToLive = 0;
		} // end catch
	} // end setter setIdentifier

	public byte[] getPayload()
	{
		// we must return a copy (not a reference) so that client code cannot
		// mess up the private data variable
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
		// we must keep a copy (not a reference) so that client code cannot mess
		// up the private data variable
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
		payloadString = value;
		payload = payloadString.getBytes();
	} // end setter setPayload

	public byte[] getPacketBytes()
	{
		byte[] transmitBytes = Utilities.concat(sourceLL3P_IPaddressString.getBytes(), 
				destinationLL3P_IPaddressString.getBytes(), typeFieldString.getBytes(), 
				identifierString.getBytes(), timeToLiveString.getBytes(), 
				payloadString.getBytes());
		checksum.calculateChecksum(transmitBytes); // this is the only time I ever calculate the checksum before sending
		return Utilities.concat(transmitBytes, checksum.getChecksumHexString().getBytes());
	} // end getter getPacketBytes

	public String getChecksumString()
	{
		return checksum.getChecksumHexString();
	} // end getter getCRC
	// end getters and setters
	
	// constructors
	// default constructor
	public LL3P()
	{
		zeroEverything();
	} // end default constructor

	// constructor with strings
	public LL3P(String sourceAddress, String destinationAddress, String type, String identity, String ttl, String payload)
	{
		setEverything(sourceAddress, destinationAddress, type, identity, ttl, payload);
	} // end constructor with strings

	// constructor with byte array
	public LL3P(byte[] inCommingBytes)
	{
		try
		{
			String inCommingBytesString = new String(inCommingBytes);
			String inCommingChecksum = (inCommingBytesString.substring(inCommingBytesString.length() - 4, inCommingBytesString.length()));
			checksum = new SixteenBitOnesComplementChecksum();
			checksum.calculateChecksum((inCommingBytesString.substring(0, inCommingBytesString.length() - 4)).getBytes());

			if (!(checksum.getChecksumHexString().equalsIgnoreCase(inCommingChecksum)))
			{
				//uiMangaer.raiseToast("CRC did not match, therefore there are errors in the in-comming bits!");
				Log.i("LL3P", "Checksum did not match, therefore there are errors in the in-comming bits!");
				// for now I will ignore a mismatched checksum
				//zeroEverything();
				//crc.resetCRC();
				// later these below lines will be blown away
				String source = inCommingBytesString.substring(0, 4);
				String destination = inCommingBytesString.substring(4, 8);
				String type = inCommingBytesString.substring(8, 12);
				String identity = inCommingBytesString.substring(12, 16);
				String ttl = inCommingBytesString.substring(16, 18);
				String data = inCommingBytesString.substring(18, inCommingBytesString.length() - 4);

				setEverything(source, destination, type, identity, ttl, data);
			} // end if
			else
			{
				String source = inCommingBytesString.substring(0, 4);
				String destination = inCommingBytesString.substring(4, 8);
				String type = inCommingBytesString.substring(8, 12);
				String identity = inCommingBytesString.substring(12, 16);
				String ttl = inCommingBytesString.substring(16, 18);
				String data = inCommingBytesString.substring(18, inCommingBytesString.length() - 4);

				setEverything(source, destination, type, identity, ttl, data);
			} // end else
		} // end try
		catch (Exception e)
		{
			//uiMangaer.raiseToast("Incoming bytes are wrong!");
			Log.i("LL3P", "In-coming bytes are wrong!");
			zeroEverything();
		} // end catch
	} // end constructor with byte array
	// end constructors
	
	// public methods
	// override methods
	@Override
	public String toString()
	{
		return (sourceLL3P_IPaddressString + destinationLL3P_IPaddressString + 
				typeFieldString + identifierString + timeToLiveString + 
				payloadString + checksum.getChecksumHexString());
	} // end override public method toString
	// end override methods
	// end public methods
	
	// private methods
	private void zeroEverything()
	{
		setEverything("", "", "", "", "", "");
	} // end private method zeroEverything

	private void setEverything(String sourceAddress, String destinationAddress, String type, String identity, String ttl, String payload)
	{
		setSourceLL3P_IPaddress(sourceAddress);
		setDestinationLL3P_IPaddress(destinationAddress);
		setTypeField(type);
		setIdentifier(identity);
		setTTL(ttl);
		setPayload(payload);
		checksum = new SixteenBitOnesComplementChecksum();
	} // end private method setEverything
	// end private methods
} // end public class LL3P
