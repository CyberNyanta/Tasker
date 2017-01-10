package com.cybernyanta.tasker.ui.adapters;

import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;

import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.ui.adapters.firebase.FirebaseRecyclerAdapter;
import com.google.firebase.database.Query;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * Created by evgeniy.siyanko on 15.12.2016.
 */

public class TaskListAdapter extends FirebaseRecyclerAdapter<Task, TaskListAdapter.TaskViewHolder> {
    protected Query queryRef;

    /**
     * @param modelClass      Firebase will marshall the data at a location into an instance of a class that you provide
     * @param modelLayout     This is the layout used to represent a single item in the list. You will be responsible for populating an
     *                        instance of the corresponding view with the data from an instance of modelClass.
     * @param viewHolderClass The class that hold references to all sub-views in an instance modelLayout.
     * @param ref             The Firebase location to watch for data changes. Can also be a slice of a location, using some
     *                        combination of {@code limit()}, {@code startAt()}, and {@code endAt()}.
     */
    public TaskListAdapter(Class<Task> modelClass, int modelLayout, Class<TaskViewHolder> viewHolderClass, Query ref) {
        super(modelClass, modelLayout, viewHolderClass, ref);
        queryRef = ref;
    }

    @Override
    protected void populateViewHolder(TaskViewHolder viewHolder, Task model, int position) {

    }

    public static class TaskViewHolder extends RecyclerView.ViewHolder {

        @BindView(R.id.task_title)
        public TextView titleTextView;
        @BindView(R.id.task_dueDate)
        public TextView dueDateTextView;
        @BindView(R.id.task_project)
        public TextView projectTextView;
        @BindView(R.id.task_color_border)
        public View colorView;

        public TaskViewHolder(View v) {
            super(v);
            ButterKnife.bind(this, v);
        }
    }

}
