package john.snoap.router.support;

import java.util.ArrayList;

public class ForwardingTable extends RouteTable
{
	// getters and setters
	public Integer getNextHopAddress(Integer LL3PNetworkAddress)
	{
		Integer networkNumber = Utilities.getNetworkFromLL3P(LL3PNetworkAddress);
		Integer nextHop = null; // if returns null then it does not exist
		
		for (RouteTableEntry entry : table)
		{
			if (entry.getNetworkDistancePair().getNetworkNumber() == networkNumber)//entry.equals(new RouteTableEntry(0, new NetworkDistancePair(LL3PNetworkAddress, 0), 0)))
			{
				nextHop = entry.getNextHop();
				break; // we have what we came here for, get out
			} // end if
		} // end for each loop
		
		return nextHop;
	} // end getter getNextHopAddress
	
	public ArrayList<RouteTableEntry> getFIBExcludingLL3PSourceAddress(Integer LL3PSourceAddress)
	{
		ArrayList<RouteTableEntry> listExcludingSourceAddress = new ArrayList<RouteTableEntry>();
		
		for (RouteTableEntry entry : table)
		{
			if (entry.getSourceLL3P().equals(LL3PSourceAddress))//entry.equals(new RouteTableEntry(LL3PSourceAddress, new NetworkDistancePair(0, 0), 0)))
			{
				// do nothing, we learned this route from the router we are sending to, leave it out
			} // end if
			else
			{
				listExcludingSourceAddress.add(entry); // put it back in the table
			} // end else
		} // end for each loop
		
		return listExcludingSourceAddress;
	} // end getter getFIBExcludingLL3PSourceAddress
	// end getters and setters
	
	// constructors
	public ForwardingTable()
	{
		super();
	}
	// end constructors
	
	// public methods
	// override methods
	@Override
	public void addEntry(int source, int net, int distance, int nextHop)
	{ // ensure we don't add something dumb
		addFIBEntry(new RouteTableEntry(source, new NetworkDistancePair(net, distance), nextHop));
	} // end override public method addEntry
	// end override methods
	
	public void addFIBEntry(RouteTableEntry newEntry)
	{
		boolean netInTableAlready = false;
		
		for (RouteTableEntry entry : table)
		{
			if (entry.getNetworkDistancePair().getNetworkNumber().equals(newEntry.getNetworkDistancePair().getNetworkNumber()))
			{
				netInTableAlready = true;
				
				if (newEntry.getNetworkDistancePair().getDistance() < entry.getNetworkDistancePair().getDistance())
				{
					table.remove(entry);
					table.add(newEntry);
					
					parentActivity.runOnUiThread(new Runnable()
					{ 
						public void run()
						{
							uiManager.resetForwardingListAdapter();
						} // end public method run
					}); // end runOnUiThread
					
					break; // we have done what we came here to do, now lets leave
				} // end if
			} // end if
		} // end for each loop
		
		if (!netInTableAlready)
		{
			table.add(newEntry);
			
			parentActivity.runOnUiThread(new Runnable()
			{ 
				public void run()
				{
					uiManager.resetForwardingListAdapter();
				} // end public method run
			}); // end runOnUiThread
		} // end if
	} // end public method addFibEntry
	
	public void addRouteList(ArrayList<RouteTableEntry> list)
	{
		for (RouteTableEntry entry : list)
		{
			addFIBEntry(entry);
		} // end for each loop
	} // end public method addRouteList
	// end public methods
} // end public class ForwardingTable
