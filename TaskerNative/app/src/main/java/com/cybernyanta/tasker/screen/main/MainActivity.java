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
import com.cybernyanta.tasker.constants.CommonConstants;
import com.cybernyanta.tasker.constants.IntentExtraConstants;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.auth.SignInActivity;
import com.cybernyanta.tasker.screen.main.di.DaggerMainComponent;
import com.cybernyanta.tasker.screen.main.di.MainModule;
import com.cybernyanta.tasker.screen.tasks.TasksFragment;

import javax.inject.Inject;

public class MainActivity extends AppCompatActivity
        implements MainContract.MainView, NavigationView.OnNavigationItemSelectedListener{

    @Inject
    MainContract.MainPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        DaggerMainComponent.builder()
                .mainModule(new MainModule(getSharedPreferences(CommonConstants.SHARED_PREFERENCES_FILE,0)))
                .build().injectMainActivity(this);
        presenter.bindView(this);
        if(presenter.checkAuth()){
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
            showTaskFragment(presenter.getStartTasksScreenType());
        }else {
            presenter.unbindView();
            showAuthScreen();
        }
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
 /*       switch (id){
            case R.id.menu_show_solve_tasks:
                Intent intent = new Intent(this.Activity, typeof(CompleteTaskListActivity));
                intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)(_taskListType == TaskListType.ProjectOpen ? TaskListType.ProjectSolve : TaskListType.AllSolve));
                intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, _projectId);
                StartActivity(intent);
                break;
            case R.id.menu_search:

                var intent2 = new Intent(Activity, typeof(SearchTaskListActivity));
                intent2.PutExtra(IntentExtraConstants.IS_SEARCH_IN_PROJECT_EXTRA, (_taskListType == TaskListType.ProjectOpen ? true : false));
                intent2.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, _projectId);
                StartActivity(intent2);
                break;
        }
*/
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
                showTaskFragment(TasksScreenType.ALL);
                break;
//            case R.id.navigation_inbox:
//                showTaskFragment(TasksScreenType.INBOX);
//                break;
            case R.id.navigation_today:
                showTaskFragment(TasksScreenType.TODAY);
                break;
            case R.id.navigation_tomorrow:
                showTaskFragment(TasksScreenType.TOMORROW);
                break;
            case R.id.navigation_nextWeek:
                showTaskFragment(TasksScreenType.NEXT_WEEK);
                break;
//            case R.id.navigation_projects:
////                SupportActionBar.Title = getString(R.string.navigation_projects);
////                SupportFragmentManager.BeginTransaction().Replace(R.id.fragment, new ProjectListFragment()).Commit();
//                break;
//            case R.id.navigation_settings:
////                SupportActionBar.Title = getString(R.string.settings);
////                SupportFragmentManager.BeginTransaction().Replace(R.id.fragment, new SettingsFragment()).Commit();
//                break;
//            case R.id.navigation_statistics:
////                SupportActionBar.Title = getString(R.string.statistics);
////                SupportFragmentManager.BeginTransaction().Replace(R.id.fragment, new StatisticsFragment()).Commit();
//                break;
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
        switch (tasksScreenType)
        {
            case ALL:
                getIntent().putExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                getIntent().putExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, TasksScreenType.ALL);
                getSupportActionBar().setTitle(getString(R.string.navigation_all));
                getSupportFragmentManager().beginTransaction().replace(R.id.list_fragment, new TasksFragment()).commit();
                break;
            case INBOX:
              /*  getIntent().putExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                getIntent().putExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, TasksScreenType.PROJECT_TASKS);
                getSupportActionBar().setTitle(getString(R.string.navigation_inbox));
                getSupportFragmentManager().beginTransaction().replace(R.id.list_fragment, new TasksFragment()).commit();*/
                break;
            case TODAY:
                getIntent().putExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                getIntent().putExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, TasksScreenType.TODAY);
                getSupportActionBar().setTitle(getString(R.string.navigation_today));
                getSupportFragmentManager().beginTransaction().replace(R.id.list_fragment, new TasksFragment()).commit();
                break;
            case TOMORROW:
                getIntent().putExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                getIntent().putExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, TasksScreenType.TOMORROW);
                getSupportActionBar().setTitle(getString(R.string.navigation_tomorrow));
                getSupportFragmentManager().beginTransaction().replace(R.id.list_fragment, new TasksFragment()).commit();
                break;
            case NEXT_WEEK:
                getIntent().putExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                getIntent().putExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, TasksScreenType.NEXT_WEEK);
                getSupportActionBar().setTitle(getString(R.string.navigation_nextWeek));
                getSupportFragmentManager().beginTransaction().replace(R.id.list_fragment, new TasksFragment()).commit();
                break;
            case PROJECT_TASKS:
                /*var viewModel = TinyIoCContainer.Current.Resolve<IProjectDetailsViewModel>();
    viewModel.Id = _sharedPreferences.GetInt(getString(R.string.project), 0);
    Intent.PutExtra(IntentExtraConstants.PROJECT_ID_EXTRA, viewModel.Id);
    Intent.PutExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, (int)TaskListType.ProjectOpen);
    var project = viewModel.GetItem();
    SupportActionBar.Title = project.Title;
    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment, new TaskListFragment()).Commit();*/

                getIntent().putExtra(IntentExtraConstants.PROJECT_ID_EXTRA, 0);
                getIntent().putExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA, TasksScreenType.ALL);
                getSupportActionBar().setTitle(getString(R.string.navigation_all));
                getSupportFragmentManager().beginTransaction().replace(R.id.fragment, new TasksFragment()).commit();
//
                break;
        }
    }

}
