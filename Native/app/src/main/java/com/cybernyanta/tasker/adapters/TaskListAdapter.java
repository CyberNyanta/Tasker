package com.cybernyanta.tasker.adapters;

import android.app.Activity;
import android.database.DataSetObserver;
import android.support.annotation.LayoutRes;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.models.Task;
import com.firebase.ui.database.FirebaseListAdapter;
import com.google.firebase.database.Query;

/**
 * Created by evgeniy.siyanko on 15.12.2016.
 */

public class TaskListAdapter extends FirebaseListAdapter<Task>{



    /**
     * @param activity    The activity containing the ListView
     * @param modelClass  Firebase will marshall the data at a location into an instance of a class that you provide
     * @param modelLayout This is the layout used to represent a single list item. You will be responsible for populating an
     *                    instance of the corresponding view with the data from an instance of modelClass.
     * @param ref         The Firebase location to watch for data changes. Can also be a slice of a location, using some
     *                    combination of {@code limit()}, {@code startAt()}, and {@code endAt()}.
     */
    public TaskListAdapter(Activity activity, Class<Task> modelClass, int modelLayout, Query ref) {
        super(activity, modelClass, modelLayout, ref);
    }

    @Override
    protected void populateView(View view, Task model, int position) {

        if (view==null){
            view = View.inflate(mActivity, R.layout.task_list_item, null);
        }
        TextView title = (TextView)view.findViewById(R.id.task_title);
        title.setText(model.getTitle());
    }
}
