// John Snoap LL2P.java
// Lab Layer 2 Protocol

package john.snoap.router.support;

import john.snoap.router.support.Utilities;

public class LL2P
{
	// private data
	private int destinationLL2P_MACaddress;
	private String destinationLL2P_MACaddressString;
	private int sourceLL2P_MACaddress;
	private String sourceLL2P_MACaddressString;
	private int typeField;
	private String typeFieldString;
	private byte[] payload;
	private String payloadString;
	private CRC16CCITT crc;
	private UIManager uiMangaer;
	// end private data

	// getters and setters
	public int getDestinationLL2P_MACaddress()
	{
		return destinationLL2P_MACaddress;
	} // end getter getDestinationMACaddress

	public String getDestinationLL2P_MACaddressString()
	{
		return destinationLL2P_MACaddressString;
	} // end getter getDestinationMACaddressString

	public void setDestinationLL2P_MACaddress(int value)
	{
		try
		{
			if (value > 0xFFFFFF || value < 0)
			{
				System.out.println("Dest MAC Addr int is wrong!");
				destinationLL2P_MACaddressString = "000000";
			} // end if
			else
			{
				destinationLL2P_MACaddress = value;
				destinationLL2P_MACaddressString = Utilities.padHexString(
				Integer.toHexString(destinationLL2P_MACaddress),
				NetworkConstants.LL2P_ADDRESS_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from an int to a hex string\nwhile setting the destination MAC Address (int).\n\n");
			destinationLL2P_MACaddress = 0;
			destinationLL2P_MACaddressString = "000000";
		} // end catch
	} // end setter setDestinationMACaddress

	public void setDestinationLL2P_MACaddress(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL2P_ADDRESS_LENGTH))
			{
				System.out.println("Dest MAC Addr string greater than 6 characters!");
				destinationLL2P_MACaddressString = "000000";
			} // end if
			else
			{
				destinationLL2P_MACaddressString = Utilities.padHexString(value, NetworkConstants.LL2P_ADDRESS_LENGTH);
				destinationLL2P_MACaddress = Integer.valueOf(destinationLL2P_MACaddressString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from a hex string to an int\nwhile setting the destination MAC Address (String).\n\n");
			destinationLL2P_MACaddressString = "000000";
			destinationLL2P_MACaddress = 0;
		} // end catch
	} // end setter setDestinationMACaddressString

	public int getSourceLL2P_MACaddress()
	{
		return sourceLL2P_MACaddress;
	} // end getter getSourceMACaddress

	public String getSourceLL2P_MACaddressString()
	{
		return sourceLL2P_MACaddressString;
	} // end getter getSourceMACaddressString

	public void setSourceLL2P_MACaddress(int value)
	{
		try
		{
			if (value > 0xFFFFFF || value < 0)
			{
				System.out.println("Source MAC Addr int is wrong!");
				sourceLL2P_MACaddressString = "000000";
			} // end if
			else
			{
				sourceLL2P_MACaddress = value;
				sourceLL2P_MACaddressString = Utilities.padHexString(Integer.toHexString(sourceLL2P_MACaddress), NetworkConstants.LL2P_ADDRESS_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from an int to a hex string\nwhile setting the source MAC Address (int).\n\n");
			sourceLL2P_MACaddress = 0;
			sourceLL2P_MACaddressString = "000000";
		} // end catch
	} // end setter setSourceMACaddress

	public void setSourceLL2P_MACaddress(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL2P_ADDRESS_LENGTH))
			{
				System.out.println("Source MAC Addr string greater than 6 characters!");
				sourceLL2P_MACaddressString = "000000";
			} // end if
			else
			{
				sourceLL2P_MACaddressString = Utilities.padHexString(value, NetworkConstants.LL2P_ADDRESS_LENGTH);
				sourceLL2P_MACaddress = Integer.valueOf(sourceLL2P_MACaddressString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from a hex string to an int\nwhile setting the source MAC Address (String).\n\n");
			sourceLL2P_MACaddressString = "000000";
			sourceLL2P_MACaddress = 0;
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
				System.out.println("Type Field int is wrong!");
				typeFieldString = "0000";
			} // end if
			else
			{
				typeField = value;
				typeFieldString = Utilities.padHexString(Integer.toHexString(typeField),
				NetworkConstants.LL2P_TYPE_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from an int to a hex string\nwhile setting the type field (int).\n\n");
			typeField = 0;
			typeFieldString = "0000";
		} // end catch
	} // end setter setTypeField

	public void setTypeField(String value)
	{
		try
		{
			if (value.length() > (2 * NetworkConstants.LL2P_TYPE_LENGTH))
			{
				System.out.println("Type Field string greater than 4 characters!");
				typeFieldString = "0000";
			} // end if
			else
			{
				typeFieldString = Utilities.padHexString(value, NetworkConstants.LL2P_TYPE_LENGTH);
				typeField = Integer.valueOf(typeFieldString, 16);
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from a hex string to an int\nwhile setting the type field (String).\n\n");
			typeFieldString = "0000";
			typeField = 0;
		} // end catch
	} // end setter setTypeField

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

	public byte[] getFramebytes()
	{
		byte[] transmitBytes = Utilities.concat(destinationLL2P_MACaddressString.getBytes(), sourceLL2P_MACaddressString.getBytes(), typeFieldString.getBytes(), payloadString.getBytes());
		crc.resetCRC();
		crc.update(transmitBytes); // this is the only time I ever calculate the CRC before sending
		return Utilities.concat(transmitBytes, crc.getCRCHexString().getBytes());
	} // end getter getPayloadbytes

	public String getCRCString()
	{
		return crc.getCRCHexString();
	} // end getter getCRC
	// end getters and setters

	// constructors
	// default constructor
	public LL2P()
	{
		zeroEverything();
	} // end default constructor

	// constructor with strings
	public LL2P(String destinationAddress, String sourceAddress, String type, String payload)
	{
		setEverything(destinationAddress, sourceAddress, type, payload);
	} // end constructor with strings

	// constructor with byte array
	public LL2P(byte[] inCommingBytes)
	{
		try
		{
			String inCommingBytesString = new String(inCommingBytes);
			String inCommingCRC = (inCommingBytesString.substring(inCommingBytesString.length() - 4, inCommingBytesString.length()));
			crc = new CRC16CCITT();
			crc.update((inCommingBytesString.substring(0, inCommingBytesString.length() - 4)).getBytes());

			if (!(crc.getCRCHexString().equalsIgnoreCase(inCommingCRC)))
			{
				System.out.println("CRC did not match, therefore there are errors in the in-comming bits!");
				zeroEverything();
			} // end if
			else
			{
				String destination = inCommingBytesString.substring(0, 6);
				String source = inCommingBytesString.substring(6, 12);
				String type = inCommingBytesString.substring(12, 16);
				String data = inCommingBytesString.substring(16, inCommingBytesString.length() - 4);

				setEverything(destination, source, type, data);
				// setEverything(inCommingBytesString.substring(0, 6),
				// inCommingBytesString.substring(6, 12),
				// inCommingBytesString.substring(12, 16),
				// inCommingBytesString.substring(16,
				// inCommingBytesString.length() - 4));
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("In-comming bytes are wrong!");
			zeroEverything();
		} // end catch
	} // end constructor with byte array
	// end constructors

	// public methods
	public void updateObjectReferences(Factory factory)
	{
		uiMangaer = factory.getUIManager();
	} // end public method updateObjectReferences

	// override methods
	@Override
	public String toString()
	{
		return (destinationLL2P_MACaddressString + sourceLL2P_MACaddressString + typeFieldString + payloadString + crc.getCRCHexString());
	} // end override public method toString
	// end override methods
	// end public methods

	// private methods
	private void zeroEverything()
	{
		setEverything("", "", "", "");
	} // end private method zeroEverything

	private void setEverything(String destinationAddress, String sourceAddress, String type, String payload)
	{
		setDestinationLL2P_MACaddress(destinationAddress);
		setSourceLL2P_MACaddress(sourceAddress);
		setTypeField(type);
		setPayload(payload);
		crc = new CRC16CCITT();
	} // end private method setEverything
	// end private methods
} // end class LL2P
