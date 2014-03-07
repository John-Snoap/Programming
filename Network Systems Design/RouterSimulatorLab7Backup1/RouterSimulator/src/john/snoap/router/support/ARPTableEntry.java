package john.snoap.router.support;

import java.util.Calendar;

public class ARPTableEntry implements Comparable<ARPTableEntry>
{
	// private data
	private Integer LL2Paddress;
	private Integer LL3Paddress;
	private long lastTimeTouched;
	// end private data
	
	// getters and setters
	public Integer getLL2Paddress()
	{
		return LL2Paddress;
	} // end getter getLL2Paddress
	
	public Integer getLL3Paddress()
	{
		return LL3Paddress;
	} // end getter getLL3Paddress
	
	public long getCurrentAgeInSeconds()
	{
		return (Calendar.getInstance().getTimeInMillis()/1000) - lastTimeTouched;
	} // end getter getCurrentAgeInSeconds
	// end getters and setters
	
	// constructors
	// constructor with Integer and Integer
	public ARPTableEntry(Integer ll2pAddr, Integer ll3pAddr)
	{
		LL2Paddress = ll2pAddr;
		LL3Paddress = ll3pAddr;
		updateTime();
	} // end constructor with Integer and Integer
	// end constructors
	
	// public methods
	public void updateTime()
	{
		lastTimeTouched = Calendar.getInstance().getTimeInMillis()/1000;
	} // end public method updateTime
	
	// override methods
	@Override
	public int compareTo(ARPTableEntry another)
	{
		return LL3Paddress.compareTo(another.LL3Paddress);
	} // end override public method compareTo
	// end override methods
	// end public methods
} // end public class ARPTableEntry
