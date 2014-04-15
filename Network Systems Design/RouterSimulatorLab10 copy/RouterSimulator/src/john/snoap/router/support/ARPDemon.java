package john.snoap.router.support;

public class ARPDemon implements Runnable
{
	// private data
	private ARPTable arpTable;
	private LL2Demon ll2demon;
	// end private data
	
	// getters and setters
	public ARPTable getARPTable()
	{
		return arpTable;
	} // end getter getARPTable
	// end getters and setters
	
	// constructors
	// default constructor
	public ARPDemon()
	{
		arpTable = new ARPTable();
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		ll2demon = factory.getDemon2();
	} // end public method updateObjectReferences
	
	public void addOrUpdate(int LL2PAddress, int LL3PAddress)
	{
		arpTable.addOrUpdate(LL2PAddress, LL3PAddress);
	} // end public method addOrUpdate
	
	public void updateAllARPBuddies()
	{
		for (ARPTableEntry entry : arpTable.getList())
		{
			ll2demon.sendARPUpdate(entry.getLL2Paddress());
		} // end for each loop
	} // end public method sendToAllARPBuddies
	
	// override methods
	@Override
	public void run()
	{
		updateAllARPBuddies();
	} // end override public method run
	// end override methods
	// end public methods
} // end public class ARPDemon
