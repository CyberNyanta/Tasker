package com.cybernyanta.tasker.screen.tasks.recycler;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.ViewGroup;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.TaskerApplication;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.util.DateUtil;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import static com.cybernyanta.tasker.constants.TaskConstants.HEADER_ID;
import static com.cybernyanta.tasker.constants.TaskConstants.TASK_VIEW_TYPE;
import static com.cybernyanta.tasker.constants.TaskConstants.HEADER_VIEW_TYPE;

/**
 * Created by evgeniy.siyanko on 10.02.2017.
 */

public class AllTasksRecyclerAdapter extends TasksRecyclerAdapter {

    public AllTasksRecyclerAdapter(List<Task> tasks, OnItemClickListener onItemClickListener) {
        super(tasks, onItemClickListener);
    }

    //TODO move somewhere, create some kind of HeaderProvider
    private List<Task> getTaskCategories() {
        Context context = TaskerApplication.getContext();
        List<Task> headers = new ArrayList<>();
        //Today header
        headers.add(new Task(context.getString(R.string.today),
                DateUtil.dateToString(DateUtil.getTodayEpochDate(), "E d MMM"),
                Long.MIN_VALUE));
        // Tomorrow
        headers.add(new Task(context.getString(R.string.tomorrow),
                DateUtil.dateToString(DateUtil.addDays(DateUtil.getTodayEpochDate(), 1), "E d MMM"),
                DateUtil.addDays(DateUtil.getTodayEpochDate(), 1) - 1));
        // Next 7 days
        headers.add(new Task(context.getString(R.string.next7days),
                DateUtil.dateToString(DateUtil.addDays(DateUtil.getTodayEpochDate(), 2), "d MMM")
                        + " - " + DateUtil.dateToString(DateUtil.addDays(DateUtil.getTodayEpochDate(), 8), "d MMM"),
                DateUtil.addDays(DateUtil.getTodayEpochDate(), 2) - 1));
        // rest
        headers.add(new Task(context.getString(R.string.rest),
                "",
                DateUtil.addDays(DateUtil.getTodayEpochDate(), 9) - 1));
        for (Task header : headers) header.setProject(HEADER_ID);
        return headers;
    }

    @Override
    public void setTasks(List<Task> tasks) {
        tasks.addAll(getTaskCategories());
        Collections.sort(tasks, new Comparator<Task>() {
            @Override
            public int compare(Task o1, Task o2) {
                return Long.compare(o1.getDueDate(), o2.getDueDate());
            }
        });
        super.setTasks(tasks);
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        if (viewType == TASK_VIEW_TYPE) return super.onCreateViewHolder(parent, viewType);
        else return new TaskCategoryViewHolder(LayoutInflater.from(parent.getContext())
                .inflate(R.layout.task_list_header, parent, false));
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position, List<Object> payloads) {
        if (!tasks.get(position).getProject().equals(HEADER_ID))
            super.onBindViewHolder(holder, position, payloads);
        else if (position + 1 != tasks.size() && !tasks.get(position + 1).getProject().equals(HEADER_ID)) {
            TaskCategoryViewHolder header = (TaskCategoryViewHolder) holder;
            header.show();
            header.bind(tasks.get(position));
        } else {
            ((TaskCategoryViewHolder) holder).hide();
        }
    }

    @Override
    public int getItemViewType(int position) {
        return tasks.get(position).getProject().equals(HEADER_ID) ? HEADER_VIEW_TYPE : TASK_VIEW_TYPE;
    }
}
