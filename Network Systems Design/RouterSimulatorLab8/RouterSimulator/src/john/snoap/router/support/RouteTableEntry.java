package john.snoap.router.support;

import java.util.Calendar;

public class RouteTableEntry implements Comparable<RouteTableEntry>
{
	// private data
	Integer sourceLL3P;
	String sourceLL3PString;
	NetworkDistancePair networkDistancePair;
	Integer nextHop;
	String nextHopString;
	long lastTimeTouched;
	// end private data
	
	// getters and setters
	public Integer getSourceLL3P()
	{
		return sourceLL3P;
	} // end getter getsourceLL3P
	
	public void setSourceLL3P(Integer value)
	{
		if (value > 0xFFFF || value < 0)
		{
			sourceLL3P = 0;
			sourceLL3PString = "0000";
		} // end if
		else
		{
			sourceLL3P = value; // can only be two byte long
			sourceLL3PString = Utilities.padHexString(Integer.toHexString(sourceLL3P), 2); // two because 2 bytes long
		} // end else
	} // end setter setSourceLL3P
	
	public NetworkDistancePair getNetworkDistancePair()
	{
		return networkDistancePair;
	} // end getter getNetworkDistancePair
	
	public void setNetworkDistancePair(NetworkDistancePair value)
	{
		networkDistancePair = value;
	} // end setter setNetworkDistancePair
	
	public long getCurrentAgeInSeconds()
	{
		return (Calendar.getInstance().getTimeInMillis()/1000) - lastTimeTouched;
	} // end getter getCurrentAgeInSeconds
	
	public Integer getNextHop()
	{
		return nextHop;
	} // end getter getNextHop
	
	public void setNextHop(Integer value)
	{
		if (value > 0xFFFF || value < 0)
		{
			nextHop = 0;
			nextHopString = "0000";
		} // end if
		else
		{
			nextHop = value; // can only be two byte long
			nextHopString = Utilities.padHexString(Integer.toHexString(nextHop), 2); // two because 2 bytes long
		} // end else
	} // end setter setNextHop
	// end getters and setters
	
	// constructors
	// default constructor
	public RouteTableEntry()
	{
		this(0, new NetworkDistancePair(), 0);
	} // end default constructor
	
	// constructor with source, distance pair, and next hop
	public RouteTableEntry(Integer source, NetworkDistancePair netDistPairValue, Integer nextHop)
	{
		setSourceLL3P(source);
		setNetworkDistancePair(netDistPairValue);
		setNextHop(nextHop);
		updateTime();
	} // end constructor with source, distance pair, and next hop
	// end constructors
	
	// public methods
	public void updateTime()
	{
		lastTimeTouched = Calendar.getInstance().getTimeInMillis()/1000;
	} // end public method updateTime
	
	// override methods
	@Override
	public String toString()
	{
		return sourceLL3PString + "\t\t" + 
				networkDistancePair.getNetworkNumberString() + "\t\t" + 
				networkDistancePair.getDistanceString() + "\t\t" + 
				nextHopString + "\t\t" + 
				lastTimeTouched;
	} // end override public method toString
	
	@Override
	public int compareTo(RouteTableEntry another)
	{
		int finalAnswer; // we must look for both ll3p source and network number
		
		if (sourceLL3P.equals(another.getSourceLL3P()))
		{
			finalAnswer = (networkDistancePair.getNetworkNumber()).compareTo((another.getNetworkDistancePair()).getNetworkNumber());
		} // end if
		else
		{
			finalAnswer = sourceLL3P.compareTo(another.getSourceLL3P());
		} // end else
		
		return finalAnswer;
	} // end override public method compareTo
	// end override methods
	// end public methods
} // end public class RouteTableEntry
