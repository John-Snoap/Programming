package john.snoap.router.support;

import java.util.List;

public class LL3Demon
{
	// private data
	private ARPTable arpTable;
	private UIManager uiManager;
	private List<LL3P> ll3pList;
	private LL2Demon ll2demon;
	private ForwardingTable forwardingTable;
	private int identifier;
	// end private data
	
	// constructors
	// default constructor
	public LL3Demon()
	{
		arpTable = new ARPTable();
		identifier = 0;
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		uiManager = factory.getUIManager();
		ll2demon = factory.getDemon2();
		ll3pList = factory.getLL3P();
		forwardingTable = factory.getForwardingTable();
	} // end public method updateObjectReferences
	
	public void receiveLL3Ppacket(byte[] packetBytes, Integer ll2pSourceAddress)
	{
		ll3pList.add(new LL3P(packetBytes)); // build an LL3P Packet and add to list
		LL3P ll3pPacket = ll3pList.get(ll3pList.size() - 1);
		
		// update the screen
		uiManager.updateLL3PDisplay();
		
		// If the ll3pPacket was sourced by an adjacent node it should also 
		// touch the appropriate ARP entry
		// (use the TTL field to determine if this ll3pPacket was sourced from an adjacent node)
		if (ll3pPacket.getTTL() == 255) // I don't know if they will have 255 or 255 - 1
		{
			/*Integer ll2pAddress = arpTable.getLL2PAddressFor(ll3pPacket.getSourceLL3P_IPaddress());
			
			if (ll2pAddress != null)
			{
				arpTable.addOrUpdate(ll2pAddress, ll3pPacket.getSourceLL3P_IPaddress());
			} // end if*/
			
			arpTable.addOrUpdate(ll2pSourceAddress, ll3pPacket.getSourceLL3P_IPaddress());
		} // end if
		
		// determine if ll3pPacket should be forwarded or delivered locally
		if (ll3pPacket.getDestinationLL3P_IPaddressString().equalsIgnoreCase(NetworkConstants.MY_LL3P_ADDRESS))
		{
			uiManager.raiseToast(ll3pPacket.toString()); // deliver locally
		} // end if
		else
		{
			sendLL3Ppacket(ll3pPacket); // forward
		} // end else
		
		// If your router is the destination of this LL3P ll3pPacket, 
		// update the UI display with the fields from this LL3P ll3pPacket.
	} // end public method receiveLL3Ppacket
	
	public void sendLL3Ppacket(LL3P packet)
	{
		// if from me, do not decrement TTL
		if (packet.getSourceLL3P_IPaddressString().equalsIgnoreCase(NetworkConstants.MY_LL3P_ADDRESS))
		{
			// do not decrement the TTL because I originated it
		} // end if
		else
		{
			packet.setTTL(packet.getTTL() - 1); // decrement the TTL
		} // end else
		
		if (packet.getTTL() < 0)
		{
			// do nothing, ll3pPacket is too old
		} // end if
		else
		{
			// find next hop according to forwarding table
			Integer nextHopAddress = forwardingTable.getNextHopAddress(packet.getDestinationLL3P_IPaddress());
			
			if (nextHopAddress != null)
			{
				// the below lines are just to get the destination string 
				// from the destination integer in a properly formatted way
				
				Integer ll2pAddress = arpTable.getLL2PAddressFor(nextHopAddress);
				
				if (ll2pAddress != null)
				{
					LL2P ll2p = new LL2P();
					ll2p.setDestinationLL2P_MACaddress(ll2pAddress);
					ll2p.setPayload(packet.getPacketBytes()); // this calculates the checksum
					
					ll2demon.sendLL2PFrame(packet.toString(), 
							ll2p.getDestinationLL2P_MACaddressString(), 
							NetworkConstants.LL3P_PACKET_PAYLOAD);
				} // end if
			} // end else
		} // end else
	} // end public method sendLL3Ppacket
	
	public void sendPayloadToLL3Pdestination(Integer LL3Paddress, byte[] payload)
	{
		LL3P ll3pPacket = new LL3P();
		
		ll3pPacket.setSourceLL3P_IPaddress(NetworkConstants.MY_LL3P_ADDRESS);
		ll3pPacket.setDestinationLL3P_IPaddress(LL3Paddress);
		ll3pPacket.setTypeField(NetworkConstants.LL3P_PACKET_PAYLOAD);
		ll3pPacket.setIdentifier(identifier);
		ll3pPacket.setTTL(225);
		ll3pPacket.setPayload(payload);
		
		sendLL3Ppacket(ll3pPacket);
		
		identifier = (identifier + 1) % 0xFFFF;
	} // end public method sendPayloadToLL3Pdestination
	// end public methods
	
	// private methods
	// end private methods
} // end public class LL3Demon
