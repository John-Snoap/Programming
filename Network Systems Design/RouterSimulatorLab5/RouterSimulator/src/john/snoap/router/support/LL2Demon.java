package john.snoap.router.support;

import java.net.InetAddress;
import java.util.List;

public class LL2Demon
{
	// private data
	private UIManager uiManager;
	private LL1Demon ll1demon;
	private List<LL2P> ll2pList;
	private String myLL2PAddress;
	//private LL2P ll2p;
	// end private data
	
	// getters and setters
	public void setLocalLL2PAddress(String myAddress)
	{
		myLL2PAddress = myAddress;
	} // end setter setLocalLL2PAddress
	// end getters and setters
	
	// constructors
	// default constructor
	public LL2Demon()
	{
		myLL2PAddress = NetworkConstants.MY_LL2P_ADDRESS;
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		uiManager = factory.getUIManager();
		ll2pList = factory.getLL2P();
		ll1demon = factory.getDemon1();
	} // end public method updateObjectReferences
	
	public void sendLL2PFrame(LL2P frame)
	{
		ll1demon.sendLL2Pframe(frame);
	} // end public method sendLL2PFrame
	
	public void sendLL2PFrame(String payload, String destinationLL2PAddress, String LL2PType)
	{
		LL2P frame = new LL2P(destinationLL2PAddress, myLL2PAddress, LL2PType, payload);
		
		sendLL2PFrame(frame);
	} // end public method sendLL2PFrame
	
	public void sendLL2PEchoRequest(String payload, String LL2PAddress)
	{
		sendLL2PFrame(payload, LL2PAddress, NetworkConstants.LL2P_ECHO_REQUEST);
	} // end public method sendLL2PEchoRequest
	
	public void receiveLL2PFrame(byte[] frameBytes, InetAddress address)
	{
		LL2P newReceivedFrame = new LL2P(frameBytes);
		receiveLL2PFrame(newReceivedFrame, address);
	} // end public method receiveLL2PFrame
	// end public methods
	
	// private methods
	private void receiveLL2PFrame(LL2P frame, InetAddress address)
	{
		ll2pList.add(frame);
		uiManager.updateLL2PDisplay(); // updates the screen's current LL2P section
		String destLL2PADDR = frame.getDestinationLL2P_MACaddressString();
		
		if (!(destLL2PADDR.equalsIgnoreCase(NetworkConstants.MY_LL2P_ADDRESS)) && !(destLL2PADDR.equalsIgnoreCase(myLL2PAddress)))
		{
			uiManager.raiseToast("Received LL2P Frame not for me!");
		} // end if
		else
		{
			String typeFieldString = frame.getTypeFieldString();
			
			if (typeFieldString.equals(NetworkConstants.LL3P_PACKET_PAYLOAD))
			{
				// pass frame to layer 3 demon
			} // end if
			else if (typeFieldString.equals(NetworkConstants.ARP_PACKET_PAYLOAD))
			{
				
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.LRP_PACKET_PAYLOAD))
			{
				// pass frame to lrp demon
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.LL2P_ECHO_REQUEST))
			{
				replyToEchoRequest(frame, address);// deal with it here
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.LL2P_ECHO_REPLY))
			{
				// deal with it here
			} // end else if
			else
			{
				uiManager.raiseToast("Type field is messed up.");
			} // end else
		} // end else
	} // end private method receiveLL2PFrame
	
	private void replyToEchoRequest(LL2P frame, InetAddress address)
	{	
		ll1demon.addAdjacency(frame.getSourceLL2P_MACaddress(), address.getHostAddress());
		
		LL2P newFrame = new LL2P(frame.getSourceLL2P_MACaddressString(), 
				myLL2PAddress, 
				NetworkConstants.LL2P_ECHO_REPLY, 
				frame.getPayloadString());
		
		sendLL2PFrame(newFrame);
	} // end private method replyToEchoRequest
	// end private methods
} // end public class LL2Demon
