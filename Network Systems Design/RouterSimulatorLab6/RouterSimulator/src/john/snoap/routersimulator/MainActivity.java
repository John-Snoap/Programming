// John Snoap MainActivity.java

package john.snoap.routersimulator;

import john.snoap.router.support.*;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.app.Activity;
import android.content.Context;
import android.view.Menu;
import android.view.MenuItem;

/**
 * This Activity displays the screen's UI, creates a TaskFragment
 * to manage the task, and receives progress updates and results 
 * from the TaskFragment when they occur.
 */
public class MainActivity extends Activity
{
	// private variables
	private Factory myFactory; // my factory to manage everything
	private UIManager uiManager; // manages the user interface
	private LL1Demon demon; // unleashes pure evil upon unsuspecting programmers
	private ConnectivityManager connManager; // for getting the WiFi connectivity service
	private NetworkInfo mWifi; // for making sure WiFi is turned on
	private boolean goAhead; // keeps track of if WiFi was turned on at start
	// end private variables

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		// my stuff
		connManager = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
		mWifi = connManager.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
		goAhead = false;

		if (mWifi.isConnected())
		{
			myFactory = new Factory(this);
			uiManager = myFactory.getUIManager();
			demon = myFactory.getDemon1();
			uiManager.updateLL2PDisplay();
			goAhead = true;
		} // end if
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
		if (goAhead)
		{
			if (item.getItemId() == R.id.showIPAddress)
			{
				uiManager.raiseToast(NetworkConstants.IP_ADDRESS);
			} // end if
			else if (item.getItemId() == R.id.sendLL2Pframe)
			{
				demon.sendLL2Pframe();
			} // end else if
		} // end if
		
		return true;
	} // end override public method onOptionsItemSelected
	
	protected void onDestroy()
	{
		super.onDestroy();
		
		if (goAhead)
		{
			demon.cancel();
		} // end if
	} // end protected method onDestroy
} // end class MainActivity
