package john.snoap.router.support;

import java.net.InetAddress;
import java.util.List;

public class LL2Demon
{
	// private data
	private UIManager uiManager;
	private LL1Demon ll1demon;
	private LL3Demon ll3demon;
	private ARPDemon arpDemon;
	private LRPDemon lrpDemon;
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
		ll3demon = factory.getDemon3();
		arpDemon = factory.getARPDemon();
		lrpDemon = factory.getLRPDemon();
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
	
	public void sendARPUpdate(int LL2PNode)
	{	
		LL2P newFrame = new LL2P("000000", // fake string so we can put in an integer later
				myLL2PAddress, 
				NetworkConstants.ARP_UPDATE, 
				NetworkConstants.MY_LL3P_ADDRESS);
		
		newFrame.setDestinationLL2P_MACaddress(LL2PNode);
		
		newFrame.setPayload("ARP Buddies!");
		
		sendLL2PFrame(newFrame);
	} // end public method sendARPUpdate
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
				ll3demon.receiveLL3Ppacket(frame.getPayload(), frame.getSourceLL2P_MACaddress());
			} // end if
			else if (typeFieldString.equals(NetworkConstants.ARP_PACKET_PAYLOAD))
			{
				//arpDemon.addOrUpdate(LL2PAddress, LL3PAddress);
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.LRP_PACKET_PAYLOAD))
			{
				// pass the pay-load to the LRP demon
				lrpDemon.receiveNewLRP(frame.getPayload(), frame.getSourceLL2P_MACaddress());
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.LL2P_ECHO_REQUEST))
			{
				replyToEchoRequest(frame, address);
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.LL2P_ECHO_REPLY))
			{
				// deal with it here
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.ARP_UPDATE))
			{
				// the pay-load string should have the LL3P Address
				arpDemon.addOrUpdate(frame.getSourceLL2P_MACaddress(), Utilities.getLL3PIntFromLL3PString(frame.getPayloadString()));
				replyToARPUpdate(frame);
			} // end else if
			else if (typeFieldString.equals(NetworkConstants.ARP_REPLY))
			{
				//arpDemon.addOrUpdate(frame.getDestinationLL2P_MACaddress(), Utilities.getLL3PIntFromLL3PString(frame.getPayloadString()));
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
	
	private void replyToARPUpdate(LL2P frame)
	{
		LL2P newFrame = new LL2P(frame.getSourceLL2P_MACaddressString(), 
				myLL2PAddress, 
				NetworkConstants.ARP_REPLY, 
				frame.getPayloadString());
		
		sendLL2PFrame(newFrame);
	} // end 
	// end private methods
} // end public class LL2Demon
