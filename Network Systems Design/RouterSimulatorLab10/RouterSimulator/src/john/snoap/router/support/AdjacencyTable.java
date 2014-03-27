package john.snoap.router.support;

import java.net.InetAddress;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;
import java.util.TreeSet;

public class AdjacencyTable
{
	// private data
	private Set<AdjacencyTableEntry> table;
	// end private data
	
	// getters and setters
	public InetAddress getIPAddressForMAC(Integer LL2PAddress)
	{
		InetAddress inetAddr = null;
		
		for (AdjacencyTableEntry entry : table)
		{
			if (entry.getLL2Paddress().equals(LL2PAddress))
			{
				inetAddr = entry.getIPaddress(); // this is the one
				break; // exit the for each loop because we have what we came here for
			} // end if
		} // end for each loop
		
		return inetAddr;
	} // end getter getIPAddressForMAC
	
	public List<AdjacencyTableEntry> getList()
	{
		return new ArrayList<AdjacencyTableEntry>(table);
	} // end getter getList
	// end getters and setters
	
	// constructors
	// default constructor
	public AdjacencyTable()
	{
		table = new TreeSet<AdjacencyTableEntry>();
	} // end default constructor
	// end constructors
	
	// public methods
	public void addEntry(Integer LL2PAddress, String IPAddress)
	{
		table.add(new AdjacencyTableEntry(LL2PAddress, IPAddress));
	} // end public method addEntry
	
	public void removeEntry(Integer LL2PAddress)
	{
		for (AdjacencyTableEntry entry : table)
		{
			if (entry.getLL2Paddress().equals(LL2PAddress))
			{
				table.remove(entry); // remove the entry
				break; // exit the for each loop because we have done what we came to do
			} // end if
		} // end for each loop
	} // end public method removeEntry
	// end public methods
} // end class AdjacencyTable
