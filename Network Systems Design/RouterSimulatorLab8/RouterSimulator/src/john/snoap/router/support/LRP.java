package john.snoap.router.support;

import java.util.ArrayList;
import java.util.List;

import android.util.Log;

public class LRP
{
	// private data
	private int sourceAddress;
	private String sourceAddressString;
	private static int sequenceNumber = 0;
	private int mySequenceNumber;
	private int totalNumberOfSequenceNumbers;
	private int routeCount;
	private List<NetworkDistancePair> pairList;
	private List<List<NetworkDistancePair>> splitUpPairList;
	// end private data
	
	// getters and setters
	public int getSourceAddress()
	{
		return sourceAddress;
	} // end getter getSourceAddress
	
	public void setSourceAddress(int value)
	{
		try
		{
			if (value > 0xFFFF || value < 0)
			{
				Log.i("LRP", "Addr int is wrong!");
				sourceAddress = 0;
				sourceAddressString = "0000";
			} // end if
			else
			{
				sourceAddress = value;
				sourceAddressString = Utilities.padHexString(Integer.toHexString(sourceAddress), NetworkConstants.LRP_ADDRESS_LENGTH);
			} // end else
		} // end try
		catch (Exception e)
		{
			Log.i("LRP", "There was an error converting from an int to a hex string\nwhile setting the Address (int).\n\n");
			sourceAddress = 0;
			sourceAddressString = "0000";
		} // end catch
	} // end setter setSourceAddress
	
	public List<NetworkDistancePair> getPairList()
	{
		return pairList;
	} // end getter getPairList
	
	public void setPairList(ForwardingTable myForwardingTable, int destinyLL3PAddress)
	{
		ArrayList<RouteTableEntry> list = myForwardingTable.getFIBExcludingLL3PSourceAddress(destinyLL3PAddress);
		
		for (RouteTableEntry entry : list)
		{
			pairList.add(entry.getNetworkDistancePair());
		} // end for each loop
		
		routeCount = pairList.size();
		totalNumberOfSequenceNumbers = (int) Math.ceil(((float) routeCount) / ((float) NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT));
		sequenceNumber = (sequenceNumber + totalNumberOfSequenceNumbers) % (NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT + 1);
		mySequenceNumber = sequenceNumber;
		
		if (totalNumberOfSequenceNumbers > 0)
		{
			splitUpPairList = new ArrayList<List<NetworkDistancePair>>(totalNumberOfSequenceNumbers);
		} // end if
		
		int routeCountModNumOfNetDistPairsPerPkt = routeCount % NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT;
		
		for (int num = 0; num < totalNumberOfSequenceNumbers; num++)
		{	
			if ((num + 1) < totalNumberOfSequenceNumbers || routeCountModNumOfNetDistPairsPerPkt == 0)
			{
				splitUpPairList.add(new ArrayList<NetworkDistancePair>(NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT));
				
				for (int count = 0; count < NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT; count ++)
				{
					splitUpPairList.get(num).add(pairList.get(num * NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT + count));
				} // end for loop
			} // end if
			else
			{
				splitUpPairList.add(new ArrayList<NetworkDistancePair>(routeCountModNumOfNetDistPairsPerPkt));
				
				for (int count = 0; count < routeCountModNumOfNetDistPairsPerPkt; count++)
				{
					splitUpPairList.get(num).add(pairList.get(num * NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT + count));
				} // end for loop
			} // end else
		} // end for loop
	} // end setter setPairList
	
	public int getRouteCount()
	{
		return routeCount;
	} // end getter getRouteCount
	
	public byte[][] getBytes()
	{
		byte[][] value = null;
		
		if (totalNumberOfSequenceNumbers > 0)
		{
			value = new byte[totalNumberOfSequenceNumbers][];
		} // end if
		
		for (int sequenceNum = 0; sequenceNum < totalNumberOfSequenceNumbers; sequenceNum++)
		{
			StringBuilder valueString = new StringBuilder();
			
			valueString.append(sourceAddressString);
			valueString.append(Integer.toHexString((mySequenceNumber - totalNumberOfSequenceNumbers + sequenceNum) % (NetworkConstants.NUMBER_OF_NET_DIST_PAIRS_PER_PKT + 1)));
			valueString.append(Integer.toHexString(splitUpPairList.get(sequenceNum).size()));
			
			for (NetworkDistancePair entry : splitUpPairList.get(sequenceNum))
			{
				valueString.append(entry.toString());
			} // end for each loop
			
			value[sequenceNum] = valueString.toString().getBytes();
		} // end for loop
		
		return value;
	} // end getter getBytes
	// end getters and setters
	
	// constructors
	public LRP(int sourceLL3PAddress, ForwardingTable myForwardingTable, int destinyLL3PAddress)
	{
		setSourceAddress(sourceLL3PAddress);
		setPairList(myForwardingTable, destinyLL3PAddress);
	} // end constructor with integer, ForwardingTable, and integer
	
	public LRP(byte[] inCommingBytes)
	{
		buildFromInCommingBytes(inCommingBytes);
	} // end constructor with byte array
	// end constructors
	
	// public methods
	// override methods
	@Override
	public String toString()
	{
		StringBuilder returnString = new StringBuilder();
		
		returnString.append(sourceAddressString);
		returnString.append(Integer.toHexString(mySequenceNumber));
		returnString.append(Integer.toHexString(routeCount));
		
		for (NetworkDistancePair entry : pairList)
		{
			returnString.append(entry.toString());
		} // end for each loop
		
		return returnString.toString();
	} // end override public method toString
	// end override methods
	// end public methods
	
	// private methods
	private void buildFromInCommingBytes(byte[] inCommingBytes)
	{
		String inCommingBytesString = new String(inCommingBytes);
		
		String source = inCommingBytesString.substring(0, 4);
		String sequenceNum = inCommingBytesString.substring(4, 5);
		String count = inCommingBytesString.substring(5, 6);
		String data = inCommingBytesString.substring(6, inCommingBytesString.length());
		
		sourceAddressString = Utilities.padHexString(source, NetworkConstants.LRP_ADDRESS_LENGTH);
		sourceAddress = Integer.valueOf(sourceAddressString, 16);
		mySequenceNumber = Integer.valueOf(sequenceNum, 16);
		routeCount = Integer.valueOf(count, 16);
		
		for (int num = 0; num < routeCount; num++)
		{
			String networkNumberString = data.substring(num * 4, num * 4 + 2);
			String distanceString = data.substring(num * 4 + 2, num * 4 + 4);
			
			int networkNumber = Integer.valueOf(networkNumberString, 16);
			int distance = Integer.valueOf(distanceString, 16);
			
			pairList.add(new NetworkDistancePair(networkNumber, distance));
		} // end for loop
	} // end private method buildFromInCommingBytes
	// end private methods
} // end public class LRP
