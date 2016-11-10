using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Tasker.Core.DAL.Entities;

namespace Tasker.Droid.Adapters
{
    public class StartPageDialogAdapter : BaseExpandableListAdapter
    {

        private readonly Activity Context;
        private const int DEFAULT_SCREEN_COUNT = 5;
        private const int DEFAULT_GROUP = 0;
        private const int PROJECT_GROUP = 1;
        protected List<Project> ProjectList { get; set; }

        public StartPageDialogAdapter(Activity newContext, List<Project> projects) : base()
        {
            Context = newContext;
            ProjectList = projects;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            View header = convertView;
            if (header == null)
            {
                header = Context.LayoutInflater.Inflate(Resource.Layout.start_page_dialog_list_item, null);
            }
            var title = header.FindViewById<TextView>(Resource.Id.title);

            switch (groupPosition)
            {
                case DEFAULT_GROUP:
                    title.Text = Context.GetString(Resource.String.settings_default_views);
                    break;
                case PROJECT_GROUP:
                    title.Text = Context.GetString(Resource.String.projects);
                    break;
            } 

            return header;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = Context.LayoutInflater.Inflate(Resource.Layout.start_page_dialog_list_item, null);
            }
            
            var title = row.FindViewById<TextView>(Resource.Id.title);

            switch (groupPosition)
            {
                case DEFAULT_GROUP:
                    switch ((StartScreens)childPosition)
                    {
                        case StartScreens.AllTask:
                            title.Text = Context.GetString(Resource.String.navigation_all);
                            break;
                        case StartScreens.Inbox:
                            title.Text = Context.GetString(Resource.String.navigation_today);
                            break;
                        case StartScreens.Today:
                            title.Text = Context.GetString(Resource.String.navigation_today);
                            break;
                        case StartScreens.Tomorrow:
                            title.Text = Context.GetString(Resource.String.navigation_tomorrow);
                            break;
                        case StartScreens.NextWeek:
                            title.Text = Context.GetString(Resource.String.navigation_nextWeek);
                            break;
                    }
                    title.Tag = childPosition;
                    break;
                case PROJECT_GROUP:
                    title.Text = ProjectList[childPosition].Title;
                    break;
            }
            return row;
        }
       
        public override int GetChildrenCount(int groupPosition)
        {
            if (groupPosition == DEFAULT_GROUP)
                return DEFAULT_SCREEN_COUNT;
            else
                return ProjectList.Count;
        }

        public override int GroupCount
        {
            get
            {
                return ProjectList == null ? 1 : 2;
            }
        }

        #region implemented abstract members of BaseExpandableListAdapter

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            if (groupPosition == PROJECT_GROUP)
            {
                return ProjectList[childPosition].ID;
            }
            else
                return childPosition;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

        #endregion
    }
}