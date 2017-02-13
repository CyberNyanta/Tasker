package com.cybernyanta.tasker.screen.tasks.recycler;

import android.support.v7.util.DiffUtil;

import com.cybernyanta.tasker.data.model.Task;

import java.util.List;

import static com.cybernyanta.tasker.constants.TaskConstants.HEADER_ID;

/**
 * Created by evgeniy.siyanko on 09.02.2017.
 */

public class TasksDiffCallback extends DiffUtil.Callback {

    List<Task> oldTasks;
    List<Task> newTasks;

    public TasksDiffCallback(List<Task> oldTasks, List<Task> newTasks) {
        this.oldTasks = oldTasks;
        this.newTasks = newTasks;
    }

    @Override
    public int getOldListSize() {
        return oldTasks.size();
    }

    @Override
    public int getNewListSize() {
        return newTasks.size();
    }

    //TODO need test redundant updating
    @Override
    public boolean areItemsTheSame(int oldItemPosition, int newItemPosition) {
        if (oldTasks.get(oldItemPosition).getId().equals(newTasks.get(newItemPosition).getId())
                && (newTasks.get(newItemPosition).getProject().equals(HEADER_ID)
                || oldTasks.get(oldItemPosition).getProject().equals(HEADER_ID))) {
            return !(newItemPosition + 1 == newTasks.size()
                    || newTasks.get(newItemPosition + 1).getProject().equals(HEADER_ID));
        } else {
            return true;
        }

    }

    @Override
    public boolean areContentsTheSame(int oldItemPosition, int newItemPosition) {
        return oldTasks.get(oldItemPosition).equals(newTasks.get(newItemPosition));
    }

}