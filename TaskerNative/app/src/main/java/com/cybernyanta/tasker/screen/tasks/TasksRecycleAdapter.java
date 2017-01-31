package com.cybernyanta.tasker.screen.tasks;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.R;

import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksRecycleAdapter extends RecyclerView.Adapter<TasksRecycleAdapter.TaskViewHolder> {
    public TasksRecycleAdapter(List<Task> tasks) {
        this.tasks = tasks;
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

    private List<Task> tasks;

    public void setTasks(List<Task> tasks){
        this.tasks = tasks;
        notifyDataSetChanged();
    }
    @Override
    public TaskViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.task_list_item, parent, false);
        return new TaskViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(TaskViewHolder holder, int position) {
        Task task = tasks.get(position);
        holder.titleTextView.setText(task.getTitle());
        holder.dueDateTextView.setText(task.getDueDate().toString());
    }

    @Override
    public int getItemCount() {
        return tasks.size();
    }


}
