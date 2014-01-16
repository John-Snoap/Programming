package john.snoap.router.support;

import android.app.Activity;

/**
 * Factory
 * @author john.snoap
 */
public class Factory
{
	// private variables
	private Activity parentActivity;
	private UIManager uiManager;
	private NetworkConstants networkConstants;
	// end private variables
	
	// getters and setters
	public Activity GetParentActivityReference()
	{
		return parentActivity;
	} // end getter GetParentActivity
	
	public UIManager GetUIManager()
	{
		return uiManager.GetUIManager();
	} // end getter GetUIManager
	// end getters and setters
	
	// constructors
	// default constructor
	public Factory (Activity callingActivity)
	{
		parentActivity = callingActivity;
		networkConstants = new NetworkConstants(parentActivity);
		uiManager = new UIManager(parentActivity); // this constructor call may have to change to simply giving it the parentActivity
		
		uiManager.UpdateObjectReferences(this);
	} // end default constructor
	// end constructors
} // end class Factory