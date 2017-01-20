package com.cybernyanta.tasker.screen.main;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.auth.SignInActivity;

import javax.inject.Inject;

public class MainActivity extends AppCompatActivity
        implements MainContract.MainView, NavigationView.OnNavigationItemSelectedListener{

    @Inject
    MainContract.MainPresenter presenter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        /*AppComponent build = DaggerAppComponent.builder()
                .dataModule(new DataModule())
                .build();
        build.injectTaskListFragment(this);*/
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

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

    @Override
    public void showAuthScreen() {
        startActivity(new Intent(this, SignInActivity.class));
        finish();
    }

    @Override
    public void showTaskFragment(TasksScreenType tasksScreenType) {

    }

    @Override
    public void setPresenter(MainContract.MainPresenter presenter) {

    }

    @Override
    public void setTitle(String titlle) {

    }
}
