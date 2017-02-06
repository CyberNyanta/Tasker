package com.cybernyanta.tasker.screen.taskdetail;

import com.cybernyanta.tasker.data.manager.TaskManager;
import com.cybernyanta.tasker.data.model.Task;
import com.google.android.gms.tasks.OnCompleteListener;

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
        if(task.getId()!=null)
        taskManager.setTask(task);
        else
            taskManager.addTask(task);
    }

    @Override
    public void saveTask(Task task, OnCompleteListener<Void> onCompleteListener) {
        if(task.getId()!=null)
            taskManager.setTask(task, onCompleteListener);
        else
            taskManager.addTask(task, onCompleteListener);
    }
    @Override
    public void deleteTask(String id) {
        taskManager.deleteTask(id);
    }

    @Override
    public void changeStatus(Task task) {
        task.setCompleted(!task.isCompleted());
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
