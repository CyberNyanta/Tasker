package com.cybernyanta.tasker.screen.tasks.recycler;

import android.content.Context;
import android.graphics.Color;
import android.graphics.Paint;
import android.support.annotation.ColorInt;
import android.support.annotation.ColorRes;
import android.support.annotation.NonNull;
import android.support.v4.content.ContextCompat;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;

import com.cybernyanta.tasker.TaskerApplication;
import com.cybernyanta.tasker.constants.TaskConstants;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.util.ColorUtil;
import com.cybernyanta.tasker.util.DateUtil;
import com.cybernyanta.tasker.enums.TaskColor;

import butterknife.BindView;
import butterknife.ButterKnife;

import static android.graphics.Paint.EMBEDDED_BITMAP_TEXT_FLAG;
import static android.graphics.Paint.ANTI_ALIAS_FLAG;
import static android.graphics.Paint.STRIKE_THRU_TEXT_FLAG;
import static com.cybernyanta.tasker.constants.TaskConstants.COMPLETED_TASK_BACKGROUND_ALPHA;
import static com.cybernyanta.tasker.constants.TaskConstants.TASK_BACKGROUND_ALPHA;


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

    public void bind(@NonNull Task task) {
        titleTextView.setText(task.getTitle());
        
        Context context = TaskerApplication.getContext();
        
        if (task.isCompleted()) {
            //Set Task due date
//            dueDateTextView.setText(context.getString(R.string.completed_on, DateUtil.dateToString(task.getDueDate(), true)));
            titleTextView.setPaintFlags(STRIKE_THRU_TEXT_FLAG | ANTI_ALIAS_FLAG | EMBEDDED_BITMAP_TEXT_FLAG);
            itemView.setAlpha(COMPLETED_TASK_BACKGROUND_ALPHA);
        } else {
            //Set Task due date
            dueDateTextView.setText(DateUtil.dateToString(task.getDueDate(), true));
            titleTextView.setPaintFlags(Paint.ANTI_ALIAS_FLAG | Paint.EMBEDDED_BITMAP_TEXT_FLAG);
//            itemView.getBackground().setAlpha(255);
            itemView.setAlpha(TASK_BACKGROUND_ALPHA);
            dueDateTextView.setTextColor(DateUtil.isDateOverdue(task.getDueDate())
                    ? ContextCompat.getColor(context, R.color.light_red)
                    : ContextCompat.getColor(context, R.color.black));
        }

        if (task.getColor() == 0)
            colorView.setBackgroundColor(itemView.getDrawingCacheBackgroundColor());
        else {
            int taskColor = ColorUtil.getTaskColor(task.getColor());
            int noneColor = ColorUtil.getTaskColor(TaskColor.NONE);
            colorView.setBackgroundColor(taskColor != 0 && taskColor != noneColor
                    ? taskColor : itemView.getDrawingCacheBackgroundColor());
        }

    }


}
