package john.snoap.router.support;

import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public class Scheduler
{
	// private data
	ScheduledThreadPoolExecutor threadPoolManager;
	ARPTable arpTable;
	//routeTable will be added later
	//lrpDemon will be added later
	// end private data
	
	// constructors
	// default constructor
	public Scheduler()
	{
		threadPoolManager = new ScheduledThreadPoolExecutor(3); // 3 threads
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		arpTable = factory.getARPTable();
		
		// wait 10 seconds after start up and then begin scheduling the ARP table
		threadPoolManager.scheduleAtFixedRate(arpTable, 10, NetworkConstants.ARP_CHECK_INTERVAL, TimeUnit.SECONDS);
	} // end public method updateObjectReferences
	// end public methods
} // end public class Scheduler
