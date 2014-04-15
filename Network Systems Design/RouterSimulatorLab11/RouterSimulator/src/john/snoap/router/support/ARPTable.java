package john.snoap.router.support;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Set;
import java.util.TreeSet;

public class ARPTable implements Runnable
{
	// private data
	private Set<ARPTableEntry> table;
	// end private data
	
	// getters and setters
	public Integer getLL2PAddressFor(Integer LL3PAddress)
	{
		Integer ll2pAddress = null;
		//ARPTableEntry arp = new ARPTableEntry(0, LL3PAddress);
		
		for (ARPTableEntry entry : table)
		{
			if ((entry.getLL3Paddress()).equals(LL3PAddress))
			{
				ll2pAddress = entry.getLL2Paddress(); // this is the one
				break; // exit the for each loop because we have what we came here for
			} // end if
		} // end for each loop
		
		return ll2pAddress;
	} // end getter getLL2PAddressFor
	
	public ArrayList<ARPTableEntry> getList()
	{
		return new ArrayList<ARPTableEntry>(table);
	} // end getter getList
	// end getters and setters
	
	// constructors
	// default constructor
	public ARPTable()
	{
		table = new TreeSet<ARPTableEntry>();
	} // end default constructor
	// end constructors
	
	// public methods
	public void addEntry(Integer LL2PAddress, Integer LL3PAddress)
	{
		table.add(new ARPTableEntry(LL2PAddress, LL3PAddress));
	} // end public method addEntry
	
	public void removeEntryWithLL2P(Integer LL2PAddress)
	{
		for (ARPTableEntry entry : table)
		{
			if (entry.getLL2Paddress().equals(LL2PAddress))
			{
				table.remove(entry); // remove the entry
				break; // exit the for each loop because we have done what we came to do
			} // end if
		} // end for each loop
	} // end public method removeEntry
	
	public void removeEntryWithLL3P(Integer LL3PAddress)
	{
		for (ARPTableEntry entry : table)
		{
			if (entry.getLL3Paddress().equals(LL3PAddress))
			{
				table.remove(entry); // remove the entry
				break; // exit the for each loop because we have done what we came to do
			} // end if
		} // end for each loop
	} // end public method removeEntry
	
	public void expireAndRemove()
	{
		List<ARPTableEntry> safeList = Collections.synchronizedList(getList());
		
		table.clear();
		
		for (ARPTableEntry entry : safeList)
		{
			if (entry.getCurrentAgeInSeconds() > NetworkConstants.ARP_TIMEOUT)
			{
				// do nothing, the entry expired so leave it out of the table
			} // end if
			else
			{
				table.add(entry); // put it back in the table
			} // end else
		} // end for each loop
	} // end public method expireAndRemove
	
	public void addOrUpdate(Integer LL2PAddress, Integer LL3PAddress)
	{
		addEntry(LL2PAddress, LL3PAddress); // add if it does not exist
		
		for (ARPTableEntry entry : table)
		{
			if (entry.getLL3Paddress().equals(LL3PAddress))
			{
				entry.updateTime(); // update the time for the entry
				break; // exit the for each loop because we have done what we came to do
			} // end if
		} // end for each loop
	} // end public method addOrUpdate
	
	// override methods
	@Override
	public void run()
	{
		expireAndRemove();
	} // end override public method run
	// end override methods
	// end public methods
} // end public class ARPTable
