package com.cybernyanta.tasker.screen.tasks.recycler;

import android.content.Context;
import android.graphics.Color;
import android.graphics.Paint;
import android.support.v4.content.ContextCompat;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.cybernyanta.tasker.TaskerApplication;
import com.cybernyanta.tasker.constants.TaskConstants;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.data.util.DateUtil;

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
        Context context = TaskerApplication.getContext();
        if (task.isCompleted())
        {
            //Set Task due date
            holder.dueDateTextView.setText(context.getString(R.string.completed_on, DateUtil.dateToString(task.getDueDate(), true)));
            holder.titleTextView.setPaintFlags( Paint.STRIKE_THRU_TEXT_FLAG | Paint.ANTI_ALIAS_FLAG | Paint.EMBEDDED_BITMAP_TEXT_FLAG);
            holder.itemView.setAlpha(TaskConstants.COMPLETED_TASK_BACKGROUND_ALPHA);
        }
        else
        {
            //Set Task due date
            holder.dueDateTextView.setText(DateUtil.dateToString(task.getDueDate(), true));
            holder.titleTextView.setPaintFlags(Paint.ANTI_ALIAS_FLAG | Paint.EMBEDDED_BITMAP_TEXT_FLAG);
            holder.itemView.setAlpha(TaskConstants.TASK_BACKGROUND_ALPHA);
            holder.dueDateTextView.setTextColor(DateUtil.isDateOverdue(task.getDueDate())
                    ? ContextCompat.getColor(context, R.color.light_red)
                    : ContextCompat.getColor(context, R.color.black));
        }
    }

    @Override
    public int getItemCount() {
        return tasks.size();
    }

}
