package john.snoap.router.support;

public class LL2Demon
{
	// private data
	private UIManager uiManager;
	private LL1Demon ll1demon;
	// end private data
	
	// getters and setters
	// end getters and setters
	
	// constructors
	// default constructor
	public LL2Demon()
	{
		
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		uiManager = factory.getUIManager();
		ll1demon = factory.getDemon1();
	} // end public method updateObjectReferences
	
	public void sendLL2PFrame(LL2P frame)
	{
		ll1demon.sendLL2Pframe(frame);
	} // end public method sendLL2PFrame
	
	public void receiveLL2PFrame(byte[] frameBytes)
	{
		LL2P newReceivedFrame = new LL2P(frameBytes);
		receiveLL2PFrame(newReceivedFrame);
	} // end public method receiveLL2PFrame
	// end public methods
	
	// private methods
	private void receiveLL2PFrame(LL2P frame)
	{
		if (!(frame.getDestinationLL2P_MACaddressString().equalsIgnoreCase(NetworkConstants.MY_LL2P_ADDRESS)))
		{
			uiManager.raiseToast("Received LL2P Frame not for me!");
		} // end if
		else
		{
			switch (frame.getTypeFieldString())
			{
				
			} // end switch
		} // end else
	} // end private method receiveLL2PFrame
	// end private methods
} // end public class LL2Demon
