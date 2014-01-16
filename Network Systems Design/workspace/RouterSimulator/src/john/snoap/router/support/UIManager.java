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
	// end private variables
	
	// getters and setters
	public UIManager GetUIManager()
	{
		return this;
	} // end getter GetUIManager
	// end getters and setters
	
	// constructors
	// default constructor
	public UIManager(Activity callingActivity)
	{
		// I am only doing the below line of code so I do not re-write code in this class.
		// In most of our classes calling UpdateObjectReferences in the constructor
		// will not be a good way for us to do things because we will need a two part
		// birth process.
		//UpdateObjectReferences(factory);
		// Maybe I should only pass the constructor an Activity.
		// Then I would be forced to call UpdateObjectReferences later.
		parentActivity = callingActivity;//this.factory.GetParentActivityReference();
		context = parentActivity.getBaseContext();
		setupMainScreenWidgets();
	} // end default constructor
	// end constructors
	
	// public methods
	public void UpdateObjectReferences(Factory factory)
	{
		this.factory = factory;
	} // end public method GetObjectReferences
	
	/**
	 * RaiseToast
	 * message : String to display
	 * @param message
	 */
	public void RaiseToast(String message)
	{
		Toast.makeText(context, message, Toast.LENGTH_LONG).show();
	} // end public method raiseToast
	// end public methods
	
	// private methods
	private void setupMainScreenWidgets()
	{
		//IPAddressLabel = (TextView) parentActivity.findViewById(R.id.IPAddressLabel);
	} // end private method setupMainScreenWidgets
	// end private methods
} // end class UIManager