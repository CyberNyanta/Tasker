package com.cybernyanta.tasker.screen.taskdetail;

import com.cybernyanta.core.manager.TaskManager;
import com.cybernyanta.core.model.Task;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */

public class TaskDetailPresenter implements TaskDetailContract.TaskDetailPresenter {

    private TaskDetailContract.TaskDetailView view;
    private TaskManager taskManager;

    public TaskDetailPresenter(TaskManager taskManager) {
        this.taskManager = taskManager;
    }

    @Override
    public Task getTask(String id) {
        return taskManager.getTask(id);
    }

    @Override
    public void saveTask(Task task) {

    }

    @Override
    public void deleteTask(String id) {

    }

    @Override
    public void changeStatus(Task task) {

    }

    @Override
    public void bindView(TaskDetailContract.TaskDetailView view) {
        this.view = view;
    }

    @Override
    public void unbindView() {
        this.view = null;
    }
}
