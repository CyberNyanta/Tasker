package com.cybernyanta.tasker.screen.tasks.recycler;

import android.content.Context;
import android.graphics.Paint;
import android.support.v4.content.ContextCompat;
import android.support.v7.util.DiffUtil;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.cybernyanta.tasker.TaskerApplication;
import com.cybernyanta.tasker.constants.TaskConstants;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.util.DateUtil;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksRecyclerAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    protected List<Task> tasks;
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
        List<Task> oldTasks =  this.tasks;
        this.tasks = tasks;
        DiffUtil.calculateDiff(new TasksDiffCallback(oldTasks, tasks), false).dispatchUpdatesTo(this);
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        return new TaskViewHolder(LayoutInflater.from(parent.getContext())
                .inflate(R.layout.task_list_item, parent, false));
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        Task task = tasks.get(position);
        holder.itemView.setOnClickListener(internalListener);
        holder.itemView.setTag(task);

        TaskViewHolder taskViewHolder = (TaskViewHolder)holder;
        taskViewHolder.bind(task);
    }

    @Override
    public int getItemCount() {
        return tasks.size();
    }

}
