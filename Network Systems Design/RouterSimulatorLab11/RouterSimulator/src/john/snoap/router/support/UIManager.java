// John Snoap UIManager.java

package john.snoap.router.support;

import java.util.List;

import john.snoap.routersimulator.R;
//import john.snoap.router.support.*;
import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemLongClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

/**
 * UIManager
 * 
 * @author john.snoap
 */
public class UIManager
{
	// private variables
	private Factory factory;
	private Activity parentActivity;
	private Context context;
	private TextView destinationLL2P;
	private TextView sourceLL2P;
	private TextView typeFieldLL2P;
	private TextView crc16ccitt;
	private TextView payloadLL2P;
	private TextView sourceLL3P;
	private TextView destinationLL3P;
	private TextView typeFieldLL3P;
	private TextView id;
	private TextView ttl;
	private TextView checksum;
	private TextView payloadLL3P;
	private ListView listViewForLL1Demon;
	private ListView listViewForRoutingTable;
	private ListView listViewForForwardingTable;
	private Button buttonAddAdjacency;
	private EditText editTextForIPaddress;
	private EditText editTextForLL2PMAC;
	private EditText editTextForEchoPayload;
	private List<AdjacencyTableEntry> listOfAdjacencyTableEntries;
	private ArrayAdapter<AdjacencyTableEntry> arrayAdapterForAdjacencyTable;
	private List<RouteTableEntry> listOfRoutingTableEntries;
	private ArrayAdapter<RouteTableEntry> arrayAdapterForRoutingTable;
	private List<RouteTableEntry> listOfForwardingTableEntries;
	private ArrayAdapter<RouteTableEntry> arrayAdapterForForwardingTable;
	private SoundPlayer soundPlayer;
	private List<LL2P> ll2pList;
	private LL2P ll2p;
	private List<LL3P> ll3pList;
	private LL3P ll3p;
	private LL1Demon ll1demon;
	private RouteTable routingTable;
	private ForwardingTable forwardingTable;
	private Messenger messenger;
	// end private variables

	// getters and setters
	public UIManager getUIManager()
	{
		return this;
	} // end getter getUIManager
	
	public Messenger getMessenger()
	{
		return messenger;
	} // end getter getMessenger
	// end getters and setters

	// constructors
	// default constructor
	public UIManager(Activity callingActivity)
	{
		parentActivity = callingActivity;
		context = parentActivity.getBaseContext();
	} // end default constructor
	// end constructors

	// public methods
	public void updateObjectReferences(Factory factory)
	{
		this.factory = factory;
		soundPlayer = this.factory.getSoundPlayer();
		ll2pList = this.factory.getLL2P();
		ll3pList = this.factory.getLL3P();
		ll1demon = this.factory.getDemon1();
		routingTable = this.factory.getRouteTable();
		forwardingTable = this.factory.getForwardingTable();
		messenger = this.factory.getMessenger();
		setupMainScreenWidgets();
	} // end public method updateObjectReferences
	
	public void receiveMessage(int ll3pSourceAddress, String message)
	{
		messenger.receiveMessage(ll3pSourceAddress, message);
	} // end public method receiveMessage

	/**
	 * raiseToast message : String to display
	 * 
	 * @param message
	 */
	public void raiseToast(String message)
	{
		//Toast.makeText(context, message, Toast.LENGTH_LONG).show();
		Toast myMessage = Toast.makeText(context, message, Toast.LENGTH_SHORT);
		myMessage.setGravity(Gravity.CENTER, 0, 0);
		myMessage.show();
	} // end public method raiseToast

	public void setIPAddressTextView()
	{
		setupMainScreenWidgets();
		soundPlayer.playButtonPressSound();
	} // end public method setIPAddressTextView
	
	public void updateLL2PDisplay()
	{
		ll2p = ll2pList.get(ll2pList.size() - 1);
		ll2p.getFramebytes(); // this makes it calculate the CRC, this function call will be moved later
		destinationLL2P.setText(ll2p.getDestinationLL2P_MACaddressString());
		sourceLL2P.setText(ll2p.getSourceLL2P_MACaddressString());
		typeFieldLL2P.setText(ll2p.getTypeFieldString());
		crc16ccitt.setText(ll2p.getCRCString());
		payloadLL2P.setText(ll2p.getPayloadString());
	} // end public method updateLL2PDisplay
	
	public void updateLL3PDisplay()
	{
		ll3p = ll3pList.get(ll3pList.size() - 1);
		ll3p.getPacketBytes(); // this makes it calculate the checksum
		sourceLL3P.setText(ll3p.getSourceLL3P_IPaddressString());
		destinationLL3P.setText(ll3p.getDestinationLL3P_IPaddressString());
		typeFieldLL3P.setText(ll3p.getTypeFieldString());
		id.setText(ll3p.getIdentifierString());
		ttl.setText(ll3p.getTTLstring());
		checksum.setText(ll3p.getChecksumString());
		payloadLL3P.setText(ll3p.getPayloadString());
	} // end public method updateLL3PDisplay
	
	public View.OnClickListener addAdjacency = new View.OnClickListener()
	{
		@Override
		public void onClick(View v)
		{
			try
			{
				String stringLL2P = editTextForLL2PMAC.getText().toString();
				String stringIPaddress = editTextForIPaddress.getText().toString();
				
				if (stringLL2P.length() > 0 && stringIPaddress.length() > 0)
				{
					ll1demon.addAdjacency(Integer.parseInt(stringLL2P, 16), stringIPaddress);
					
					editTextForLL2PMAC.setText("");
					editTextForIPaddress.setText("");
				} // end if
				else
				{
					// remove old routes
					routingTable.removeOldRoutes();
					forwardingTable.removeOldRoutes();
					
					// reset list adapters
					resetRoutingListAdapter();
					resetForwardingListAdapter();
				} // end else
			} // end try
			catch (Exception e)
			{
				Log.e("Add Adjacency Button", "Did not work");
			} // end catch
		} // end override public method onClick
	}; // end OnClickListener addAdjacency
	
	public void resetAdjacencyListAdapter()
	{
		// get the current list
		listOfAdjacencyTableEntries = ll1demon.getAdjacencyList();
		
		arrayAdapterForAdjacencyTable.clear(); // clear the adjacency list
		// load the list items in the adapter, this updates the screen too.
		for (AdjacencyTableEntry tableEntry : listOfAdjacencyTableEntries)
	    {
	        // Add the name and address to an array adapter to show in a ListView
			arrayAdapterForAdjacencyTable.add(tableEntry);
	    } // end for each loop
	} // end public method resetAdjacencyListAdapter
	
	public void resetRoutingListAdapter()
	{
		// get the current list
		listOfRoutingTableEntries = routingTable.getList();
		
		arrayAdapterForRoutingTable.clear(); // clear the adjacency list
		// load the list items in the adapter, this updates the screen too.
		for (RouteTableEntry tableEntry : listOfRoutingTableEntries)
	    {
	        // Add the name and address to an array adapter to show in a ListView
			arrayAdapterForRoutingTable.add(tableEntry);
	    } // end for each loop
	} // end public method resetRoutingListAdapter
	
	public void resetForwardingListAdapter()
	{
		// get the current list
		listOfForwardingTableEntries = forwardingTable.getList();
		
		arrayAdapterForForwardingTable.clear(); // clear the adjacency list
		// load the list items in the adapter, this updates the screen too.
		for (RouteTableEntry tableEntry : listOfForwardingTableEntries)
	    {
	        // Add the name and address to an array adapter to show in a ListView
			arrayAdapterForForwardingTable.add(tableEntry);
	    } // end for each loop
	} // end public method resetRoutingListAdapter
	// end public methods

	// private methods
	private void setupMainScreenWidgets()
	{
		destinationLL2P = (TextView) parentActivity.findViewById(R.id.textViewDestinationLL2P);
		sourceLL2P = (TextView) parentActivity.findViewById(R.id.textViewSourceLL2P);
		typeFieldLL2P = (TextView) parentActivity.findViewById(R.id.textViewTypeFieldLL2P);
		crc16ccitt = (TextView) parentActivity.findViewById(R.id.textViewCRC16CCITT);
		payloadLL2P = (TextView) parentActivity.findViewById(R.id.textViewPayloadLL2P);
		
		sourceLL3P = (TextView) parentActivity.findViewById(R.id.textViewSourceLL3P);
		destinationLL3P = (TextView) parentActivity.findViewById(R.id.textViewDestinationLL3P);
		typeFieldLL3P = (TextView) parentActivity.findViewById(R.id.textViewTypeFieldLL3P);
		id = (TextView) parentActivity.findViewById(R.id.textViewIDLL3P);
		ttl = (TextView) parentActivity.findViewById(R.id.textViewTimeToLive);
		checksum = (TextView) parentActivity.findViewById(R.id.textViewChecksumResult);
		payloadLL3P = (TextView) parentActivity.findViewById(R.id.textViewPayloadLL3P);
		
		listViewForLL1Demon = (ListView) parentActivity.findViewById(R.id.listViewAdjacencyTable);
		listViewForRoutingTable = (ListView) parentActivity.findViewById(R.id.listViewRoutingTable);
		listViewForForwardingTable = (ListView) parentActivity.findViewById(R.id.listViewForwardingTable);
		
		buttonAddAdjacency = (Button) parentActivity.findViewById(R.id.buttonAddAdjacency);
		
		buttonAddAdjacency.setOnClickListener(addAdjacency);
		
		editTextForIPaddress = (EditText) parentActivity.findViewById(R.id.editTextIPaddress);
		editTextForLL2PMAC = (EditText) parentActivity.findViewById(R.id.editTextLL2PMAC);
		editTextForEchoPayload = (EditText) parentActivity.findViewById(R.id.editTextEchoPayload);
		
		listOfAdjacencyTableEntries = ll1demon.getAdjacencyList();
		listOfRoutingTableEntries = routingTable.getList();
		listOfForwardingTableEntries = forwardingTable.getList();
		
		arrayAdapterForAdjacencyTable = new ArrayAdapter<AdjacencyTableEntry>(parentActivity, 
				android.R.layout.simple_list_item_1, 
				listOfAdjacencyTableEntries);
		
		arrayAdapterForRoutingTable = new ArrayAdapter<RouteTableEntry>(parentActivity, 
				android.R.layout.simple_list_item_1, 
				listOfRoutingTableEntries);
		
		arrayAdapterForForwardingTable = new ArrayAdapter<RouteTableEntry>(parentActivity, 
				android.R.layout.simple_list_item_1, 
				listOfForwardingTableEntries);
		
		resetAdjacencyListAdapter();
		resetRoutingListAdapter();
		resetForwardingListAdapter();
		
		listViewForLL1Demon.setAdapter(arrayAdapterForAdjacencyTable);
		listViewForLL1Demon.setOnItemClickListener(sendToLL2P);
		listViewForLL1Demon.setOnItemLongClickListener(removeFromList);
		
		listViewForRoutingTable.setAdapter(arrayAdapterForRoutingTable);
		listViewForForwardingTable.setAdapter(arrayAdapterForForwardingTable);
	} // end private method setupMainScreenWidgets
	
	private OnItemClickListener sendToLL2P = new AdapterView.OnItemClickListener()
	{
		@Override
		public void onItemClick(AdapterView<?> adapter, View viewObject, 
				int positionTapped, long id)
		{
			// The positionTapped is used by the adjacencyList.get(int) method
			// so get the adjacency entry.  Then that information is used to
			// create a fake LL2P frame…
			AdjacencyTableEntry target = listOfAdjacencyTableEntries.get(positionTapped);
			LL2P newFrame = new LL2P(Integer.toHexString(target.getLL2Paddress()), 
					NetworkConstants.MY_LL2P_ADDRESS, 
					NetworkConstants.LL2P_ECHO_REQUEST, 
					editTextForEchoPayload.getText().toString());
			ll1demon.sendLL2Pframe(newFrame);
		} // end override public method onItemClick
	}; // end OnItemClickListener
	
	private OnItemLongClickListener removeFromList = new AdapterView.OnItemLongClickListener()
	{
		@Override
		public boolean onItemLongClick(AdapterView<?> arg0, View viewObject,
				int positionTapped, long id)
		{
			AdjacencyTableEntry target = listOfAdjacencyTableEntries.get(positionTapped);
			ll1demon.removeAdjacency(target.getLL2Paddress());
			
			return false;
		} // end public override method onItemLongClick
	}; // end OnItemLongClickListener
} // end class UIManager
