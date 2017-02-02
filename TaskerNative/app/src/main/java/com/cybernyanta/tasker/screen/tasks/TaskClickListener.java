package com.cybernyanta.tasker.screen.tasks;

import android.support.annotation.NonNull;
import android.view.View;

import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.screen.tasks.recycler.OnItemClickListener;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */

public class TaskClickListener implements com.cybernyanta.tasker.screen.tasks.recycler.OnItemClickListener {
    TasksFragment tasksFragment;
    
    public TaskClickListener(TasksFragment tasksFragment){
        this.tasksFragment = tasksFragment;
    }
    @Override
    public void onItemClick(@NonNull View view, @NonNull Task task) {

    }
}
