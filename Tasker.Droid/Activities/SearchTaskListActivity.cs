using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views.Animations;
using ListView = Android.Widget.ListView;
using SearchView = Android.Support.V7.Widget.SearchView;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Graphics;
using Android.Widget;

using TinyIoC;
using Com.Github.Jjobes.Slidedatetimepicker;
using FAB = Clans.Fab.FloatingActionButton;

using Tasker.Core.BL.Managers;
using Tasker.Core.DAL.Entities;
using Tasker.Core.AL.Utils;
using Tasker.Core.AL.ViewModels.Contracts;
using Tasker.Droid.Fragments;


namespace Tasker.Droid.Activities
{
    [Activity]
    [IntentFilter(new string[] { "android.intent.action.SEARCH" })]
    [MetaData(("android.app.searchable"), Resource = "@xml/searchable")]
    public class SearchTaskListActivity : AppCompatActivity, SearchView.IOnQueryTextListener
    {
        private Adapters.TaskListAdapter _taskListAdapter;
        private List<Task> _tasks;
        private List<Project> _projects;
        private List<Task> _foundTasks = new List<Task>();
        private ITaskListViewModel _viewModel;
        private ListView _listView;
        private bool _isProjectTaskSearch;
        private int _projectId;
        private string _lastQuery;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.search_list);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            _viewModel = TinyIoCContainer.Current.Resolve<ITaskListViewModel>();
            _listView = FindViewById<ListView>(Resource.Id.task_list);
            _listView.ItemClick += ItemClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.search_task_list_menu, menu);

            // Associate searchable configuration with the SearchView

            var searchManager = (SearchManager)GetSystemService(Context.SearchService);
            var searchView = (SearchView)menu.FindItem(Resource.Id.menu_search_widget).ActionView;
            searchView.SetSearchableInfo(searchManager.GetSearchableInfo(ComponentName));
            searchView.SetIconifiedByDefault(false);
            searchView.SetOnQueryTextListener(this);

            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            base.OnResume();

            _isProjectTaskSearch = Intent.GetBooleanExtra("IsProjectTaskSearch", false);
            if (!_isProjectTaskSearch)
            {
                _tasks = _viewModel.GetAll();
            }
            else
            {
                _projectId = Intent.GetIntExtra("ProjectId", 0);
                _tasks = _viewModel.GetProjectTasks(_projectId);
            }
            _projects = _viewModel.GetAllProjects();
            _taskListAdapter = new Adapters.TaskListAdapter(this, _foundTasks, _projects);
            _listView.Adapter = _taskListAdapter;

            OnQueryTextChange(_lastQuery);
        }


        private void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int id = (int)e.Id;
            Intent intent = new Intent(this, typeof(TaskEditCreateActivity));
            intent.PutExtra("TaskId", id);
            StartActivity(intent);
        }

        #region SearchView.IOnQueryTextListener
        public bool OnQueryTextChange(string newText)
        {
            _lastQuery = newText?.ToLower();
            if (!string.IsNullOrWhiteSpace(_lastQuery))
            {
                _foundTasks = _tasks.FindAll(task => task.Title.ToLower().Contains(_lastQuery));
                _taskListAdapter.ChangeDataSet(_foundTasks);
            }
            else
            {
                _taskListAdapter.ChangeDataSet(new List<Task>());
            }
            return true;
        }

        public bool OnQueryTextSubmit(string query)
        {
            return true;
        }
        #endregion
    }
}