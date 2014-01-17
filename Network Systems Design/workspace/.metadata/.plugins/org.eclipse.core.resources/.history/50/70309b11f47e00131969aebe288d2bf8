package john.snoap.routersimulator;

import john.snoap.router.support.Factory;
import john.snoap.router.support.UIManager;
import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;

public class MainActivity extends Activity
{
	// private variables
    public Factory myFactory = new Factory(this); // my factory to manage everything
    public UIManager uiManager = new UIManager(this); // manages the user interface
    // end private variables
	
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
        // my stuff
        uiManager.RaiseToast("all done");
    } // end override method onCreate

    @Override
    public boolean onCreateOptionsMenu(Menu menu)
    {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    } // end override method onCreateOptionsMenu
} // end class MainActivity
