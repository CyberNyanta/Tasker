package com.cybernyanta.tasker.screen.tasks;

import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.core.manager.TaskManager;
import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.enums.TasksScreenType;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksPresenter implements TasksContract.TasksPresenter {

    private TaskManager taskManager;
    private List<Task> tasks;
    private TasksContract.TasksView tasksView;

    public TasksPresenter(TaskManager taskManager) {
        this.taskManager = taskManager;
    }

    @Override
    public List<Task> getTasks(TasksScreenType tasksScreenType) {
        return tasks;
    }

    @Override
    public void addOnDataSetChanged(OnChangedListener listener) {
        taskManager.addOnChangedListener(listener);
    }

    @Override
    public void bindView(TasksContract.TasksView view) {
        tasksView = view;
    }

    @Override
    public void unbindView() {
        tasksView = null;
    }
}
