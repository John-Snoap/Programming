// John Snoap Factory.java

package john.snoap.router.support;

import android.app.Activity;

/**
 * Factory
 * 
 * @author john.snoap
 */
public class Factory
{
	// private variables
	private Activity parentActivity;
	private UIManager uiManager;
	//private NetworkConstants networkConstants;
	private SoundPlayer soundPlayer;
	private LL2P ll2p;
	private LL1Demon demon;
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
	
	public LL2P getLL2P()
	{
		return ll2p;
	} // end getter getLL2P
	
	public LL1Demon getDemon1()
	{
		return demon;
	} // end getter getDemon
	// end getters and setters

	// constructors
	// default constructor
	public Factory(Activity callingActivity)
	{
		parentActivity = callingActivity;
		//networkConstants = new NetworkConstants(parentActivity);
		soundPlayer = new SoundPlayer(parentActivity);
		uiManager = new UIManager(parentActivity);
		ll2p = new LL2P("de1e7e", NetworkConstants.MY_LL2P_ADDRESS, NetworkConstants.LL3P_PACKET_PAYLOAD, "I love you professor Smith!  You are the greatest professor ever!");
		demon = new LL1Demon();

		uiManager.updateObjectReferences(this);
		ll2p.updateObjectReferences(this);
		demon.updateObjectReferences(this);
	} // end default constructor
	// end constructors
} // end class Factory
