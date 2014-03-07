package john.snoap.router.support;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;
import java.nio.channels.IllegalBlockingModeException;
import java.util.List;

import android.os.Handler;
import android.os.Message;
import android.util.Log;

public class LL1Demon // because Daemon is just too long... and Demon is funnier
{
	// constants
	private static final int MESSAGE_WRITE = 1;
	private static final int MESSAGE_READ = 2;
	private static final int MESSAGE_ERROR = 3;
	private static final int TRANSMIT_PORT = 34999;
	private static final int RECEIVE_PORT = 35000;
	// end constants
	
	// private data
	private DatagramSocket sendSocket;
	private DatagramSocket receiveSocket;
	private AdjacencyTable addressTable;
	private List<LL2P> ll2pList;
	private static LL2Demon ll2demon;
	private static UIManager uiManager;
	private ReceivePacketsOverUDP receivePackets;
	// end private data
	
	// getters and setters
	public List<AdjacencyTableEntry> getAdjacencyList()
	{
		return addressTable.getList();
	} // end getter getAdjacencyList
	// end getters and setters
	
	// constructors
	// default constructor
	public LL1Demon()
	{
		addressTable = new AdjacencyTable();
		openUDPport();
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		ll2pList = factory.getLL2P();
		uiManager = factory.getUIManager();
		ll2demon = factory.getDemon2();
		receivePackets = new ReceivePacketsOverUDP(receiveSocket);
		new Thread(receivePackets).start();
	} // end public method updateObjectReferences
	
	public void addAdjacency(int LL2PAddress, String IPAddress)
	{
		addressTable.addEntry(LL2PAddress, IPAddress);
		uiManager.resetAdjacencyListAdapter();
		ll2demon.sendARPUpdate(LL2PAddress); // when an adjacency is added, send an ARP update
	} // end public method addAdjacency
	
	public void removeAdjacency(int LL2PAddress)
	{
		addressTable.removeEntry(LL2PAddress);
		uiManager.resetAdjacencyListAdapter();
	} // end public method removeAdjacency
	
	public void sendLL2Pframe()
	{
		sendLL2Pframe(ll2pList.get(ll2pList.size() - 1));
	} // end public method sendLL2Pframe
	
	public void sendLL2Pframe(LL2P frame)
	{
		byte[] frameBytes = frame.getFramebytes();
		InetAddress IPaddress = null;
		
		// get the IP address for the ll2P frame from the adjacency table and use it 
		// to fetch the actual Internet address data structure for use in opening the port
		try
		{
			IPaddress = addressTable.getIPAddressForMAC(frame.getDestinationLL2P_MACaddress());
		} // end try
		catch (Exception e)
		{
			IPaddress = null;
		} // end catch
		
		if (IPaddress != null)
		{
			DatagramPacket packetToTransmit = new DatagramPacket(frameBytes, frameBytes.length, IPaddress, RECEIVE_PORT);
			
			SendPacketOverUDP sendPacket = new SendPacketOverUDP(sendSocket, packetToTransmit);
			new Thread(sendPacket).start(); // send the packet!
		} // end if
		else
		{
			uiManager.raiseToast("Attempt to send to unknown LL2P:  " + frame.getDestinationLL2P_MACaddressString());
		} // end else
	} // end public method sendLL2Pframe
	
	// call this in an activity's onDestroy method to ensure the sockets are taken care of
	public void cancel()
	{
		try
		{
			sendSocket.close();
			receiveSocket.close();
			receivePackets.cancel();
		} // end try
		catch (Exception e)
		{
			
		} // end catch
	} // end public method cancel
	// end public methods
	
	// private methods
	private void openUDPport()
	{
		sendSocket = null; // in case the open port method fails
		receiveSocket = null; // in case the open port method fails
		
		try
		{
			sendSocket = new DatagramSocket(TRANSMIT_PORT); // just so it doesn't randomly pick 9999
		} // end try
		catch (SocketException e)
		{
			e.printStackTrace();
		} // end catch
		
		try
		{
			receiveSocket = new DatagramSocket(RECEIVE_PORT);
		} // end try
		catch (SocketException e)
		{
			e.printStackTrace();
		} // end catch
	} // end private method openUDPport
	// end private methods
	
	// the Handler that gets information back from the socket
	private static final Handler mHandler = new Handler()
	{
		@Override
		public void handleMessage(Message msg)
		{
			switch (msg.what)
			{
				case MESSAGE_WRITE:
					DatagramPacket sentPacket = (DatagramPacket) msg.obj;
					int bytesSent = msg.arg1;
					byte[] txData = new String(sentPacket.getData()).substring(0, bytesSent).getBytes();
					String tempTx = new String(txData);
					uiManager.raiseToast("Transmitted:  " + tempTx);
					//uiManager.raiseToast("Successfully Transmitted Packet!");
					break;
				case MESSAGE_READ:
					DatagramPacket receivePacket = (DatagramPacket) msg.obj;
					int bytesReceived = msg.arg1;
					byte[] rxData = new String(receivePacket.getData()).substring(0, bytesReceived).getBytes();
					String tempRx = new String(rxData);
					ll2demon.receiveLL2PFrame(rxData, receivePacket.getAddress());
					uiManager.raiseToast("Received:  " + tempRx);
					break;
				default : // Error occurred
					uiManager.raiseToast((String) msg.obj);
					break;
			} // end switch
		} // end override public method handleMessage
	}; // end Handler
	
	// forget asynchronous tasks, i'm going to program like a man!
	private class ReceivePacketsOverUDP implements Runnable
	{
		private DatagramSocket datagramSocketReceiveUDP;
		String TAG = "ReceivePacketsOverUDP";

		public ReceivePacketsOverUDP(DatagramSocket rxSocket)
		{
			try
			{
				datagramSocketReceiveUDP = rxSocket;
			} // end try
			catch (Exception streamError)
			{
				mHandler.obtainMessage(MESSAGE_ERROR, -1, -1, "Error when getting input socket");
				Log.e(TAG, "Error when getting input socket");
			} // end catch
		} // end constructor with BluetoothSocket
		
		public void run()
		{
			byte[] receiveData = new byte[1024]; // buffer store for the stream
			DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);
			int bytesReceived; // number of bytes in the received packet

			// Keep listening to the InputStream until an exception occurs
			while (true)
			{
				try
				{
					datagramSocketReceiveUDP.receive(receivePacket);
					bytesReceived = receivePacket.getLength();
					mHandler.obtainMessage(MESSAGE_READ, bytesReceived, -1, receivePacket).sendToTarget(); // Send the obtained bytes to the UI activity
				} // end try
				catch (IOException e)
				{
					mHandler.obtainMessage(MESSAGE_ERROR, -1, -1, "Error reading from Receive Socket");
					Log.e(TAG, "Error reading from Receive Socket");
					break;
				} // end I/O catch
				catch (IllegalBlockingModeException e)
				{
					mHandler.obtainMessage(MESSAGE_ERROR, -1, -1, "Channel is in Non-Blocking Mode");
					Log.e(TAG, "Channel is in Non-Blocking Mode");
					break;
				} // end non-blocking mode catch
				catch (Exception e)
				{
					mHandler.obtainMessage(MESSAGE_ERROR, -1, -1, "Something horribly wrong happened");
					Log.e(TAG, "Something horribly wrong happened");
					break;
				} // end catch
			} // end infinite while loop
		} // end public method run
		
		// Will cancel an in-progress connection, and close the socket
		public void cancel()
		{
			try
			{
				datagramSocketReceiveUDP.close();
			} // end try
			catch (Exception e)
			{
				
			} // end catch
		} // end public method cancel
	} // end private class ReceivePacketsOverUDP
	
	// it feels good to program like a man.  i'm going to do it again!
	private class SendPacketOverUDP implements Runnable
	{
		private DatagramSocket datagramSocketSendUDP;
		private DatagramPacket datagramPacketSendUDP;
		String TAG = "SendPacketOverUDP";

		public SendPacketOverUDP(DatagramSocket xmitSocket, DatagramPacket xmitPacket)
		{
			try
			{
				datagramSocketSendUDP = xmitSocket;
				datagramPacketSendUDP = xmitPacket;
			} // end try
			catch (Exception streamError)
			{
				mHandler.obtainMessage(MESSAGE_ERROR, -1, -1, "Error when getting output socket");
				Log.e(TAG, "Error when getting output socket");
			} // end catch
		} // end constructor with BluetoothSocket
		
		public void run()
		{
			try
			{
				datagramSocketSendUDP.send(datagramPacketSendUDP);
				int bytesSent = datagramPacketSendUDP.getLength();
				mHandler.obtainMessage(MESSAGE_WRITE, bytesSent, -1, datagramPacketSendUDP).sendToTarget(); // show that we sent the packet
			} // end try
			catch (IOException e)
			{
				mHandler.obtainMessage(MESSAGE_ERROR, -1, -1, "Error sending to Send Socket");
				Log.e(TAG, "Error sending to Send Socket");
			} // end catch
			catch (Exception e)
			{
				mHandler.obtainMessage(MESSAGE_ERROR, -1, -1, "Error sending to Send Socket");
				cancel();
				Log.e(TAG, "Something horrible happened");
			} // end catch
		} // end public method run
		
		// Will cancel an in-progress connection, and close the socket
		public void cancel()
		{
			try
			{
				datagramSocketSendUDP.close();
			} // end try
			catch (Exception e)
			{
				
			} // end catch
		} // end public method cancel
	} // end private class SendPacketsOverUDP
} // end public class LL1Demon
