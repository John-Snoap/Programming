package john.snoap.router.support;

import java.util.List;

//import android.app.Activity;

public class LRPDemon
{
	// private data
	private ARPTable arpTable;
	private RouteTable routeTable;
	private ForwardingTable forwardingTable;
	//private UIManager uiManager;
	// I do not need the UIManager because 
	// I update the tables in the RouteTable class and in the
	// ForwardingTable class
	// I have not used the below two classes yet
	// so I have commented them out as well
	//private Activity parentActivity;
	//private LL2Demon ll2demon;
	// end private data
	
	// getters and setters
	public RouteTable getRouteTable()
	{
		return routeTable;
	} // end getter getRouteTable
	
	public List<RouteTableEntry> getRoutingTableAsList()
	{
		return routeTable.getList();
	} // end getter getRoutingTableAsList
	
	public ForwardingTable getForwardingTable()
	{
		return forwardingTable;
	} // end getter getForwardingTable
	
	public List<RouteTableEntry> getForwardingTableAsList()
	{
		return forwardingTable.getList();
	} // end getter getForwardingTableAsList
	// end getters and setters
	
	// constructors
	public LRPDemon()
	{
		
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		routeTable = factory.getRouteTable();
		forwardingTable = factory.getForwardingTable();
		
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS), Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS), 0, Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS));
		forwardingTable.addRouteList(routeTable.getList());
		
		arpTable = factory.getARPTable();
		//uiManager = factory.getUIManager();
		//parentActivity = factory.getParentActivityReference();
		//ll2demon = factory.getDemon2();
	} // end public method updateObjectReferences
	
	public void receiveNewLRP(byte[] LRPPacket, Integer LL2PSource)
	{
		LRP lrp = new LRP(LRPPacket);
		int lrpSource = lrp.getSourceAddress();
		
		arpTable.addOrUpdate(LL2PSource, lrpSource);
		
		for (NetworkDistancePair netDistPair : lrp.getPairList())
		{
			routeTable.addEntry(lrpSource, netDistPair, lrpSource);
		} // end for each loop
		
		forwardingTable.addRouteList(routeTable.getList());
	} // end public method receiveNewLRP
	// end public methods
	
	// private methods
	// end private methods
} // end public class LRPDemon
