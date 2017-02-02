package com.cybernyanta.tasker.screen.tasks.recycler;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.R;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksRecyclerAdapter extends RecyclerView.Adapter<TaskViewHolder> {
    private List<Task> tasks;
    private final OnItemClickListener onItemClickListener;

    private final View.OnClickListener internalListener = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Task task = (Task) view.getTag();
            onItemClickListener.onItemClick(view, task);
        }
    };

    public TasksRecyclerAdapter(List<Task> tasks, OnItemClickListener onItemClickListener) {
        this.onItemClickListener = onItemClickListener;
        this.tasks = tasks;
    }

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
        holder.bind(task);
        holder.itemView.setOnClickListener(internalListener);
        holder.itemView.setTag(task);
    }

    @Override
    public int getItemCount() {
        return tasks.size();
    }

}
