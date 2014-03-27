package john.snoap.router.support;

import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public class Scheduler
{
	// private data
	ScheduledThreadPoolExecutor threadPoolManager;
	ARPTable arpTable;
	//ARPDemon arpDemon;
	RouteTable routeTable;
	ForwardingTable forwardingTable;
	LRPDemon lerpDemon;
	// end private data
	
	// constructors
	// default constructor
	public Scheduler()
	{
		threadPoolManager = new ScheduledThreadPoolExecutor(4); // 4 threads
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		arpTable = factory.getARPTable();
		//arpDemon = factory.getARPDemon();
		routeTable = factory.getRouteTable();
		forwardingTable = factory.getForwardingTable();
		lerpDemon = factory.getLRPDemon();
		
		threadPoolManager.scheduleAtFixedRate(arpTable, 
				NetworkConstants.ROUTER_BOOT_TIME, // time to wait after startup
				NetworkConstants.ARP_CHECK_INTERVAL, 
				TimeUnit.SECONDS);
		
		/*threadPoolManager.scheduleAtFixedRate(arpDemon, 
				NetworkConstants.ROUTER_BOOT_TIME, // time to wait after startup
				NetworkConstants.ARP_CHECK_INTERVAL/2, 
				TimeUnit.SECONDS);*/
		
		threadPoolManager.scheduleAtFixedRate(routeTable, 
				NetworkConstants.ROUTER_BOOT_TIME, // time to wait after startup
				NetworkConstants.ROUTE_TABLE_CHECK_INTERVAL, 
				TimeUnit.SECONDS);
		
		threadPoolManager.scheduleAtFixedRate(forwardingTable, 
				NetworkConstants.ROUTER_BOOT_TIME, // time to wait after startup
				NetworkConstants.ROUTE_TABLE_CHECK_INTERVAL, 
				TimeUnit.SECONDS);
		
		threadPoolManager.scheduleWithFixedDelay(lerpDemon, 
				NetworkConstants.ROUTER_BOOT_TIME, // time to wait after startup
				NetworkConstants.ROUTE_TABLE_CHECK_INTERVAL, 
				TimeUnit.SECONDS);
	} // end public method updateObjectReferences
	// end public methods
} // end public class Scheduler
