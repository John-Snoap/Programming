// John Snoap UIManager.java

package john.snoap.router.support;

import john.snoap.routersimulator.R;
import android.app.Activity;
import android.content.Context;
import android.widget.TextView;
import android.widget.Toast;

/**
 * UIManager
 * @author john.snoap
 */
public class UIManager
{
	// private variables
	private Factory factory;
	private Activity parentActivity;
	private Context context;
	private TextView IPAddressLabel;
	private TextView IPAddress;
	private SoundPlayer soundPlayer;
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
		IPAddress.setText(NetworkConstants.IP_ADDRESS);
	} // end default constructor
	// end constructors
	
	// public methods
	public void updateObjectReferences(Factory factory)
	{
		this.factory = factory;
		soundPlayer = this.factory.getSoundPlayer();
	} // end public method updateObjectReferences
	
	/**
	 * raiseToast
	 * message : String to display
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
	// end public methods
	
	// private methods
	private void setupMainScreenWidgets()
	{
		IPAddressLabel = (TextView) parentActivity.findViewById(R.id.textView2);
		IPAddress = (TextView) parentActivity.findViewById(R.id.textView1);
	} // end private method setupMainScreenWidgets
	// end private methods
} // end class UIManager
