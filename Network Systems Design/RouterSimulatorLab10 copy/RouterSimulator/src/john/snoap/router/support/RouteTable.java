package john.snoap.router.support;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Set;
import java.util.TreeSet;

import android.app.Activity;

public class RouteTable implements Runnable
{
	// protected data
	protected Set<RouteTableEntry> table;
	protected UIManager uiManager;
	protected Activity parentActivity;
	// end protected data
	
	// getters and setters
	public ArrayList<RouteTableEntry> getList()
	{
		return new ArrayList<RouteTableEntry>(table);
	} // end getter getRouteList
	// end getters and setters
	
	// constructors
	// default constructor
	public RouteTable()
	{
		// synchronizedSet makes sure the table is thread safe
		table = Collections.synchronizedSet(new TreeSet<RouteTableEntry>());
		//table = new TreeSet<RouteTableEntry>();
	} // end default constructor
	// end constructors
	
	// public methods
	// final methods
	public final void updateObjectReferences(Factory factory)
	{
		uiManager = factory.getUIManager();
		parentActivity = factory.getParentActivityReference();
	} // end public final method updateObjectReferences
	// end final methods
	
	public void addEntry(int source, int net, int distance, int nextHop)
	{
		table.add(new RouteTableEntry(source, new NetworkDistancePair(net, distance), nextHop));
		
		parentActivity.runOnUiThread(new Runnable()
		{ 
			public void run()
			{
				uiManager.resetRoutingListAdapter();
			} // end public method run
		}); // end runOnUiThread
	} // end public method AddEntry
	
	public void addEntry(int source, NetworkDistancePair netDist, int nextHop)
	{
		table.add(new RouteTableEntry(source, netDist, nextHop));
		
		parentActivity.runOnUiThread(new Runnable()
		{ 
			public void run()
			{
				uiManager.resetRoutingListAdapter();
			} // end public method run
		}); // end runOnUiThread
	} // end public method AddEntry
	
	public void addEntry(RouteTableEntry RTE)
	{
		table.add(RTE);
		
		parentActivity.runOnUiThread(new Runnable()
		{ 
			public void run()
			{
				uiManager.resetRoutingListAdapter();
			} // end public method run
		}); // end runOnUiThread
	} // end public method addEntry
	
	public void addOrUpdate(int source, NetworkDistancePair netDist, int nextHop)
	{
		RouteTableEntry RTE = new RouteTableEntry(source, netDist, nextHop);
		addEntry(RTE);  // add if it does not exist
		
		for (RouteTableEntry entry : table)
		{
			if (entry.equals(RTE))
			{
				entry.updateTime(); // update the time for the entry
				break; // exit the for each loop because we have done what we came to do
			} // end if
		} // end for each loop
	} // end public method addOrUpdate
	
	public void removeOldRoutes()
	{
		List<RouteTableEntry> safeList = Collections.synchronizedList(getList());
		
		table.clear(); // clear the table
		
		for (RouteTableEntry entry : safeList)
		{
			if (entry.getCurrentAgeInSeconds() > NetworkConstants.ROUTE_TABLE_TIMEOUT)
			{
				// do nothing, the entry expired so leave it out of the table
			} // end if
			else
			{
				table.add(entry); // put it back in the table
			} // end else
		} // end for each loop
		
		/*addOrUpdate(Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS), 
				new NetworkDistancePair(Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS), 
				0), // I am zero away from myself
				Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS));*/
		
		parentActivity.runOnUiThread(new Runnable()
		{ 
			public void run()
			{
				uiManager.resetRoutingListAdapter();
				uiManager.resetForwardingListAdapter();
			} // end public method run
		}); // end runOnUiThread
	} // end public method RemoveOldRoutes
	
	public void removeEntry(int source, int net)
	{ // this is utilizing my compareTo method, only the source and network number matter
		table.remove(new RouteTableEntry(source, new NetworkDistancePair(net, 0), 0));
		
		parentActivity.runOnUiThread(new Runnable()
		{ 
			public void run()
			{
				uiManager.resetRoutingListAdapter();
				uiManager.resetForwardingListAdapter();
			} // end public method run
		}); // end runOnUiThread
	} // end public method removeEntry
	
	// override methods
	@Override
	public void run()
	{
		removeOldRoutes();
	} // end override public method run
	// end override methods
	// end public methods
	
	// private methods
	// end private methods
} // end public class RouteTable
