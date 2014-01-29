// John Snoap UIManager.java

package john.snoap.router.support;

import john.snoap.routersimulator.R;
import android.app.Activity;
import android.content.Context;
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
	//private TextView IPAddressLabel;
	//private TextView IPAddress;
	private SoundPlayer soundPlayer;
	private LL2P ll2p;

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
		setupMainScreenWidgets();
		//IPAddress.setText(NetworkConstants.IP_ADDRESS);
	} // end default constructor
	// end constructors

	// public methods
	public void updateObjectReferences(Factory factory)
	{
		this.factory = factory;
		soundPlayer = this.factory.getSoundPlayer();
		ll2p = this.factory.getLL2P();
	} // end public method updateObjectReferences

	/**
	 * raiseToast message : String to display
	 * 
	 * @param message
	 */
	public void raiseToast(String message)
	{
		Toast.makeText(context, message, Toast.LENGTH_LONG).show();
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
	// end public methods

	// private methods
	private void setupMainScreenWidgets()
	{
		destinationLL2P = (TextView) parentActivity.findViewById(R.id.textViewDestinationLL2P);
		sourceLL2P = (TextView) parentActivity.findViewById(R.id.textViewSourceLL2P);
		typeFieldLL2P = (TextView) parentActivity.findViewById(R.id.textViewTypeFieldLL2P);
		crc16ccitt = (TextView) parentActivity.findViewById(R.id.textViewCRC16CCITT);
		payloadLL2P = (TextView) parentActivity.findViewById(R.id.textViewPayloadLL2P);
	} // end private method setupMainScreenWidgets
	// end private methods
} // end class UIManager
