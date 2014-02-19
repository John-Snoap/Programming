// John Snoap UIManager.java

package john.snoap.router.support;

import java.util.List;

import john.snoap.routersimulator.R;
import john.snoap.router.support.*;
import android.app.Activity;
import android.bluetooth.BluetoothDevice;
import android.content.Context;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.View.OnLongClickListener;
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
	private ListView listViewForLL1Demon;
	private Button buttonAddAdjacency;
	private EditText editTextForIPaddress;
	private EditText editTextForLL2PMAC;
	private List<AdjacencyTableEntry> listOfAdjacencyTableEntries;
	private ArrayAdapter<AdjacencyTableEntry> arrayAdapterForAdjacencyTable;
	private SoundPlayer soundPlayer;
	private LL2P ll2p;
	private LL1Demon ll1demon;
	// end private variables

	// getters and setters
	public UIManager getUIManager()
	{
		return this;
	} // end getter getUIManager
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
		ll2p = this.factory.getLL2P();
		ll1demon = this.factory.getDemon();
		setupMainScreenWidgets();
	} // end public method updateObjectReferences

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
		ll2p.getFramebytes(); // this makes it calculate the CRC, this function call will be moved later
		destinationLL2P.setText(ll2p.getDestinationLL2P_MACaddressString());
		sourceLL2P.setText(ll2p.getSourceLL2P_MACaddressString());
		typeFieldLL2P.setText(ll2p.getTypeFieldString());
		crc16ccitt.setText(ll2p.getCRCString());
		payloadLL2P.setText(ll2p.getPayloadString());
	} // end public method updateLL2PDisplay
	
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
					
					resetAdjacencyListAdapter();
				} // end if
			} // end try
			catch (Exception e)
			{
				Log.e("Add Adjacency Button", "Did not work");
			} // end catch
		} // end override public method onClick
	}; // end OnClickListener addAdjacency
	// end public methods

	// private methods
	private void setupMainScreenWidgets()
	{
		destinationLL2P = (TextView) parentActivity.findViewById(R.id.textViewDestinationLL2P);
		sourceLL2P = (TextView) parentActivity.findViewById(R.id.textViewSourceLL2P);
		typeFieldLL2P = (TextView) parentActivity.findViewById(R.id.textViewTypeFieldLL2P);
		crc16ccitt = (TextView) parentActivity.findViewById(R.id.textViewCRC16CCITT);
		payloadLL2P = (TextView) parentActivity.findViewById(R.id.textViewPayloadLL2P);
		
		listViewForLL1Demon = (ListView) parentActivity.findViewById(R.id.listViewAdjacencyTable);
		
		buttonAddAdjacency = (Button) parentActivity.findViewById(R.id.buttonAddAdjacency);
		
		buttonAddAdjacency.setOnClickListener(addAdjacency);
		
		editTextForIPaddress = (EditText) parentActivity.findViewById(R.id.editTextIPaddress);
		editTextForLL2PMAC = (EditText) parentActivity.findViewById(R.id.editTextLL2PMAC);
		
		listOfAdjacencyTableEntries = ll1demon.getAdjacencyList();
		
		arrayAdapterForAdjacencyTable = new ArrayAdapter<AdjacencyTableEntry>(parentActivity, 
				android.R.layout.simple_list_item_1, 
				listOfAdjacencyTableEntries);
		
		resetAdjacencyListAdapter();
	} // end private method setupMainScreenWidgets
	
	private void resetAdjacencyListAdapter()
	{
		// get the current list
		//adjacencyList = layer1Daemon.getAdjacencyList();
		listOfAdjacencyTableEntries = ll1demon.getAdjacencyList();
		// clear the adjacency list
		//adjacencyListAdapter.clear();
		arrayAdapterForAdjacencyTable.clear();
		// load the list items in the adapter, this updates the screen too.
		for (AdjacencyTableEntry tableEntry : listOfAdjacencyTableEntries)
	    {
	        // Add the name and address to an array adapter to show in a ListView
			arrayAdapterForAdjacencyTable.add(tableEntry);
	    } // end for each loop
	    
		listViewForLL1Demon.setAdapter(arrayAdapterForAdjacencyTable);
		listViewForLL1Demon.setOnItemClickListener(sendToLL2P);
		listViewForLL1Demon.setOnItemLongClickListener(removeFromList);
	    //list.setAdapter(mArrayAdapter);
	    //list.setOnItemClickListener(mMessageClickedHandler);
		
		//Iterator<AdjacencyTableEntry> listIterator = adjacencyList.iterator();
		//while (listIterator.hasNext())
			//adjacencyListAdapter.add(listIterator.next());
	} // end private method resetAdjacencyListAdapter
	
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
					NetworkConstants.LL3P_PACKET_PAYLOAD, 
					ll2p.getPayloadString());
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
			resetAdjacencyListAdapter();
			
			return false;
		} // end public override method onItemLongClick
	}; // end OnItemLongClickListener
} // end class UIManager
