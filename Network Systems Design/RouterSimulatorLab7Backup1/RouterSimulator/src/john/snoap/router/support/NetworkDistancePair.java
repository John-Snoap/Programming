package john.snoap.router.support;

public class NetworkDistancePair
{
	// private data
	private Integer networkNumber;
	private String networkNumberString;
	private int distance;
	private String distanceString;
	// end private data
	
	// getters and setters
	public Integer getNetworkNumber()
	{
		return networkNumber;
	} // end getter getNetworkNumber
	
	public String getNetworkNumberString()
	{
		return networkNumberString;
	} // end getter getNetworkNumberString
	
	public void setNetworkNumber(int value)
	{	
		if (value > 0xFF || value < 0)
		{
			networkNumber = 0;
			networkNumberString = "00";
		} // end if
		else
		{
			networkNumber = value; // can only be one byte long
			networkNumberString = Utilities.padHexString(Integer.toHexString(networkNumber), 1); // one because 1 byte long
		} // end else
	} // end setter setNetworkNumber
	
	public int getDistance()
	{
		return distance;
	} // end getter getDistance
	
	public String getDistanceString()
	{
		return distanceString;
	} // end getter getDistanceString
	
	public void setDistance(int value)
	{
		if (value > 0xFF || value < 0)
		{
			distance = 0;
			distanceString = "00";
		} // end if
		else
		{
			distance = value; // can only be one byte long
			distanceString = Utilities.padHexString(Integer.toHexString(distance), 1); // one because 1 byte long
		} // end else
	} // end setter setDistance
	// end getters and setters
	
	// constructors
	// default constructor
	public NetworkDistancePair()
	{
		this(0, 0); // call the other constructor with zeros
	} // end default constructor
	
	// constructor with network number and distance supplied
	public NetworkDistancePair(int networkNumber, int distance)
	{
		setNetworkNumber(networkNumber);
		setDistance(distance);
	} // end constructor with network number and distance supplied
	// end constructors
	
	// public methods
	// override methods
	@Override
	public String toString()
	{
		return networkNumberString + distanceString;
	} // end override public method toString
	// end override methods
	// end public methods
} // end public class NetworkDistancePair
