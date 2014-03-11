package john.snoap.router.support;

import java.net.InetAddress;

public class AdjacencyTableEntry implements Comparable<AdjacencyTableEntry>
{
	// private data
	private Integer LL2Paddress;
	private String IPaddressString;
	private InetAddress IPaddress;
	// end private data
	
	// getters and setters
	public Integer getLL2Paddress()
	{
		return LL2Paddress;
	} // end getter getLL2Paddress
	
	public void setLL2Paddress(Integer value)
	{
		try
		{
			if (value > 0xFFFFFF || value < 0)
			{
				System.out.println("LL2P Addr in Adjacency Table Entry is wrong!");
			} // end if
			else
			{
				LL2Paddress = value;
			} // end else
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error setting the LL2P Address in the Adjacency Table Entry.\n\n");
			LL2Paddress = 0;
		} // end catch
	} // end setter setLL2Paddress
	
	public InetAddress getIPaddress()
	{
		return IPaddress;
	} // end getter getIPaddress
	
	public String getIPaddressString()
	{
		return IPaddressString;
	} // end getter getIPaddressString
	
	public void setIPaddress(String value)
	{
		try
		{
			IPaddressString = value;
			IPaddress = InetAddress.getByName(IPaddressString);
		} // end try
		catch (Exception e)
		{
			System.out.println("There was an error converting from an IP address String to an InetAddress in Adjacency Table Entry\n\n");
			IPaddressString = "0.0.0.0";
			try
			{
				IPaddress = InetAddress.getByName(IPaddressString);
			} // end try
			catch (Exception f)
			{
				System.out.println("Ok, I really messed something up when making an InetAddress\n\n");
				IPaddress = null;
			} // end catch
		} // end catch
	} // end setter setIPaddress
	// end getters and setters
	
	// constructors
	// default constructor
	public AdjacencyTableEntry()
	{
		setLL2Paddress(0);
		setIPaddress("0.0.0.0");
	} // end default constructor
	
	// constructor with an integer and a string
	public AdjacencyTableEntry(Integer ll2p, String ip)
	{
		setLL2Paddress(ll2p);
		setIPaddress(ip);
	} // end constructor with an integer and a string
	// end constructors
	
	// public methods
	// override methods
	@Override
	public String toString()
	{
		return "LL2P:  " + Utilities.padHexString(Integer.toHexString(LL2Paddress), 3) + "\t\tIP:  " + IPaddressString;
	} // end override public method toString
	
	@Override
	public int compareTo(AdjacencyTableEntry another)
	{
		return LL2Paddress.compareTo(another.LL2Paddress);
	} // end override public method compareTo
	// end override methods
	// end public methods
} // end class AdjacencyTableEntry