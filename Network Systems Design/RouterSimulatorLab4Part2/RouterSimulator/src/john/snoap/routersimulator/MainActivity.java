// John Snoap MainActivity.java

package john.snoap.routersimulator;

import john.snoap.router.support.*;
import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;
import android.view.MenuItem;

public class MainActivity extends Activity
{
	// private variables
	public Factory myFactory; // my factory to manage everything
	public UIManager uiManager; // manages the user interface
	public LL1Demon demon; // unleashes pure evil upon unsuspecting programmers
	// end private variables

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		// my stuff
		myFactory = new Factory(this);
		uiManager = myFactory.getUIManager();
		demon = myFactory.getDemon();
		uiManager.raiseToast(NetworkConstants.IP_ADDRESS);
		uiManager.updateLL2PDisplay();
	} // end override protected method onCreate

	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	} // end override public method onCreateOptionsMenu

	/**
	 * Here we respond to the menu selection. The menu is created in res/menu.
	 * Any item created there can be checked for here this method will then
	 * handle all of these.
	 */
	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		if (item.getItemId() == R.id.showIPAddress)
		{
			uiManager.raiseToast(NetworkConstants.IP_ADDRESS);
			//uiManager.setIPAddressTextView();
		} // end if
		else if (item.getItemId() == R.id.sendLL2Pframe)
		{
			demon.sendLL2Pframe();
		} // end else if

		return true;
	} // end override public method onOptionsItemSelected
} // end class MainActivity