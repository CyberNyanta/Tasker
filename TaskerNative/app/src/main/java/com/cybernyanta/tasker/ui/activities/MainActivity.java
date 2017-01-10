package com.cybernyanta.tasker.ui.activities;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.ui.fragment.TaskListFragment;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.FirebaseDatabase;

public class MainActivity extends BaseActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    protected FirebaseUser mFirebaseUser;
    protected String mUsername;
    protected String mPhotoUrl;
    protected FirebaseAuth mFirebaseAuth;

    protected SharedPreferences mSharedPreferences;
    protected GoogleApiClient mGoogleApiClient;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        mFirebaseAuth = FirebaseAuth.getInstance();
        mFirebaseUser = mFirebaseAuth.getCurrentUser();
        if (mFirebaseUser == null) {
            // Not signed in, launch the Sign In activity
            startActivity(new Intent(this, SignInActivity.class));
            finish();
            return;
        } else {
            if(savedInstanceState==null){
                FirebaseDatabase.getInstance().setPersistenceEnabled(true);
            }
            mUsername = mFirebaseUser.getDisplayName();
            if (mFirebaseUser.getPhotoUrl() != null) {
                mPhotoUrl = mFirebaseUser.getPhotoUrl().toString();
            }
        }

        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        getSupportFragmentManager().beginTransaction().replace(R.id.list_fragment, new TaskListFragment()).commit();



        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        switch (id)
        {
            case R.id.navigation_all:
//                StartFragment(StartScreens.AllTask);
                break;
            case R.id.navigation_inbox:
//                StartFragment(StartScreens.Inbox);
                break;
            case R.id.navigation_today:
//                StartFragment(StartScreens.Today);
                break;
            case R.id.navigation_tomorrow:
//                StartFragment(StartScreens.Tomorrow);
                break;
            case R.id.navigation_nextWeek:
//                StartFragment(StartScreens.NextWeek);
                break;
            case R.id.navigation_projects:
//                SupportActionBar.Title = GetString(Resource.String.navigation_projects);
//                SupportFragmentManager.BeginTransaction().Replace(R.id.fragment, new ProjectListFragment()).Commit();
                break;
            case R.id.navigation_settings:
//                SupportActionBar.Title = GetString(Resource.String.settings);
//                SupportFragmentManager.BeginTransaction().Replace(R.id.fragment, new SettingsFragment()).Commit();
                break;
            case R.id.navigation_statistics:
//                SupportActionBar.Title = GetString(Resource.String.statistics);
//                SupportFragmentManager.BeginTransaction().Replace(R.id.fragment, new StatisticsFragment()).Commit();
                break;
        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
}
