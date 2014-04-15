// John Snoap Factory.java

package john.snoap.router.support;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;

/**
 * Factory
 * 
 * @author john.snoap
 */
public class Factory
{
	// private variables
	private Activity parentActivity;
	private UIManager uiManager;
	private SoundPlayer soundPlayer;
	private List<LL2P> ll2pList;
	private LL1Demon ll1demon;
	private LL2Demon ll2demon;
	private ARPDemon arpDemon;
	private LRPDemon lrpDemon;
	private RouteTable routeTable;
	private ForwardingTable forwardingTable;
	private Scheduler scheduler;
	// end private variables

	// getters and setters
	public Activity getParentActivityReference()
	{
		return parentActivity;
	} // end getter getParentActivity

	public UIManager getUIManager()
	{
		return uiManager.getUIManager();
	} // end getter getUIManager

	public SoundPlayer getSoundPlayer()
	{
		return soundPlayer;
	} // end getter getSoundPlayer
	
	public List<LL2P> getLL2P()
	{
		return ll2pList;
	} // end getter getLL2P
	
	public LL1Demon getDemon1()
	{
		return ll1demon;
	} // end getter getDemon1
	
	public LL2Demon getDemon2()
	{
		return ll2demon;
	} // end getter getDemon2
	
	public ARPDemon getARPDemon()
	{
		return arpDemon;
	} // end getter getARPDemon
	
	public LRPDemon getLRPDemon()
	{
		return lrpDemon;
	} // end getter getLRPDemon
	
	public ARPTable getARPTable()
	{
		return arpDemon.getARPTable();
	} // end getter getARPTable
	
	public RouteTable getRouteTable()
	{
		return routeTable;
	} // end getter getRouteTable
	
	public ForwardingTable getForwardingTable()
	{
		return forwardingTable;
	} // end getter getForwardingTable
	// end getters and setters

	// constructors
	// default constructor
	public Factory(Activity callingActivity)
	{
		parentActivity = callingActivity;
		soundPlayer = new SoundPlayer(parentActivity);
		uiManager = new UIManager(parentActivity);
		ll2pList = new ArrayList<LL2P>();
		ll2pList.add(new LL2P("de1e7e", NetworkConstants.MY_LL2P_ADDRESS, NetworkConstants.LL3P_PACKET_PAYLOAD, "I love you professor Smith!  You are the greatest professor ever!"));
		ll1demon = new LL1Demon();
		ll2demon = new LL2Demon();
		arpDemon = new ARPDemon();
		routeTable = new RouteTable();
		forwardingTable = new ForwardingTable();
		lrpDemon = new LRPDemon();
		scheduler = new Scheduler();
		
		uiManager.updateObjectReferences(this);
		ll1demon.updateObjectReferences(this);
		ll2demon.updateObjectReferences(this);
		arpDemon.updateObjectReferences(this);
		routeTable.updateObjectReferences(this);
		forwardingTable.updateObjectReferences(this);
		lrpDemon.updateObjectReferences(this);
		scheduler.updateObjectReferences(this);
		
		// add entries to routing table and forwarding table
		/*
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0205"), 1, 1, Utilities.getLL3PIntFromLL3PString("0104"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0205"), 3, 1, Utilities.getLL3PIntFromLL3PString("0307"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0205"), 4, 1, Utilities.getLL3PIntFromLL3PString("0405"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0205"), 2, 0, Utilities.getLL3PIntFromLL3PString("0205"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0405"), 5, 2, Utilities.getLL3PIntFromLL3PString("0405"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0405"), 2, 2, Utilities.getLL3PIntFromLL3PString("0405"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0405"), 4, 1, Utilities.getLL3PIntFromLL3PString("0405"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0307"), 1, 2, Utilities.getLL3PIntFromLL3PString("0307"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0307"), 3, 1, Utilities.getLL3PIntFromLL3PString("0307"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0307"), 2, 2, Utilities.getLL3PIntFromLL3PString("0307"));
		routeTable.addEntry(Utilities.getLL3PIntFromLL3PString("0307"), 5, 2, Utilities.getLL3PIntFromLL3PString("0307"));
		forwardingTable.addRouteList(routeTable.getList());
		*/
	} // end default constructor
	// end constructors
} // end class Factory
