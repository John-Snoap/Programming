package john.snoap.router.support;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Set;
import java.util.TreeSet;

import android.app.Activity;

public class RouteTable
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
		table = new TreeSet<RouteTableEntry>();
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
	} // end public method AddEntry
	
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
	} // end public method RemoveOldRoutes
	
	public void removeEntry(int source, int net)
	{ // this is utilizing my compareTo method, only the source and network number matter
		table.remove(new RouteTableEntry(source, new NetworkDistancePair(net, 0), 0));
	} // end public method removeEntry
	// end public methods
	
	// private methods
	// end private methods
} // end public class RouteTable

