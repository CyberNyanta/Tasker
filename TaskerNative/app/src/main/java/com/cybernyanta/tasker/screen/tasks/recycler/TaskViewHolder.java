package com.cybernyanta.tasker.screen.tasks.recycler;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;

import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.data.util.DateUtil;

import java.util.Date;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */
public class TaskViewHolder extends RecyclerView.ViewHolder {

    @BindView(R.id.task_title)
    TextView titleTextView;
    @BindView(R.id.task_dueDate)
    TextView dueDateTextView;
    @BindView(R.id.task_project)
    TextView projectTextView;
    @BindView(R.id.task_color_border)
    View colorView;

    public TaskViewHolder(View v) {
        super(v);
        ButterKnife.bind(this, v);
    }

    public void bind(@NonNull Task task){
        titleTextView.setText(task.getTitle());
        dueDateTextView.setText(DateUtil.dueDateToString(task.getDueDate(),true));
    }
}
