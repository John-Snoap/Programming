package john.snoap.router.support;

import java.util.ArrayList;
import java.util.List;

public class LRPDemon implements Runnable
{
	// private data
	private ARPTable arpTable;
	private RouteTable routeTable;
	private ForwardingTable forwardingTable;
	//private UIManager uiManager; // I am not actually using these here
	//private Activity parentActivity; // I use them at lower levels
	private LL2Demon ll2demon;
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
		arpTable = factory.getARPTable();
		//uiManager = factory.getUIManager();
		//parentActivity = factory.getParentActivityReference();
		ll2demon = factory.getDemon2();
	} // end public method updateObjectReferences
	
	public void receiveNewLRP(byte[] LRPPacket, Integer LL2PSource)
	{
		handleReceivedLRP HDLRP = new handleReceivedLRP(LRPPacket, LL2PSource, arpTable, routeTable, forwardingTable);
		
		Thread handleReceivedLRPThread = new Thread(HDLRP);
		handleReceivedLRPThread.start();
	} // end public method receiveNewLRP
	
	// override methods
	@Override
	public void run()
	{
		routeTable.addOrUpdate(Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS), 
				new NetworkDistancePair(Utilities.getNetworkFromLL3P(Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS)), 
				0), // I am zero away from myself
				Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS));
		
		final ArrayList<ARPTableEntry> arpList = arpTable.getList();
		
		for (ARPTableEntry entry : arpList)
		{
			routeTable.addOrUpdate(entry.getLL3Paddress(), 
					new NetworkDistancePair(Utilities.getNetworkFromLL3P(entry.getLL3Paddress()), 1), 
					entry.getLL3Paddress());
		} // end for each loop
		
		forwardingTable.addRouteList(routeTable.getList());
		
		for (ARPTableEntry entry : arpList)
		{	
			if (entry.getLL3Paddress().equals(Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS)))
			{
				// do not send a routing update to myself
			} // end if
			else
			{ // send a routing update
				LRP lrp = new LRP(Utilities.getLL3PIntFromLL3PString(NetworkConstants.MY_LL3P_ADDRESS), 
						forwardingTable, 
						entry.getLL3Paddress());
				
				for (byte[] bytes : lrp.getBytes())
				{
					ll2demon.sendLL2PFrame(new String(bytes), 
							Utilities.padHexString(Integer.toHexString(entry.getLL2Paddress()), 
									NetworkConstants.LL2P_ADDRESS_LENGTH), 
							NetworkConstants.LRP_PACKET_PAYLOAD);
				} // end for each loop
			} // end else
		} // end for each loop
	} // end override public method run
	// end override methods
	// end public methods
	
	// private methods
	// end private methods
	
	// private classes
	private class handleReceivedLRP implements Runnable
	{
		// private data
		LRP lrp;
		int lrpSource;
		int LL2PSource;
		ARPTable arpTable;
		RouteTable routeTable;
		ForwardingTable forwardingTable;
		// end private data
		
		// constructors
		public handleReceivedLRP(byte[] LRPPacket, Integer LL2PSource, ARPTable arpTable, RouteTable routeTable, ForwardingTable forwardingTable)
		{
			lrp = new LRP(LRPPacket);
			lrpSource = lrp.getSourceAddress();
			
			this.LL2PSource = LL2PSource;
			
			this.arpTable = arpTable;
			this.routeTable = routeTable;
			this.forwardingTable = forwardingTable;
		} // end constructor
		// end constructors
		
		// public methods
		// override methods
		@Override
		public void run()
		{
			arpTable.addOrUpdate(LL2PSource, lrpSource);
			
			for (NetworkDistancePair netDistPair : lrp.getPairList())
			{
				// add one to the distance
				netDistPair.setDistance(netDistPair.getDistance() + 1);
				
				routeTable.addOrUpdate(lrpSource, netDistPair, lrpSource);
			} // end for each loop
			
			forwardingTable.addRouteList(routeTable.getList());
		} // end override public method run
		// end override methods
		// end public methods
	} // end private class handleReceivedLRP
	// end private classes
} // end public class LRPDemon
