// John Snoap Factory.java

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
	private SoundPlayer soundPlayer;
	// end private variables
	
	// getters and setters
	public Activity getParentActivityReference()
	{
		return parentActivity;
	} // end getter getParentActivity
	
	public UIManager getUIManager()
	{
		return uiManager.getUIManager();
	} // end getter getUIManager
	
	public SoundPlayer getSoundPlayer()
	{
		return soundPlayer;
	} // end getter getSoundPlayer
	// end getters and setters
	
	// constructors
	// default constructor
	public Factory (Activity callingActivity)
	{
		parentActivity = callingActivity;
		networkConstants = new NetworkConstants(parentActivity);
		soundPlayer = new SoundPlayer(parentActivity);
		uiManager = new UIManager(parentActivity);
		
		uiManager.updateObjectReferences(this);
	} // end default constructor
	// end constructors
} // end class Factory
