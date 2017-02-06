package com.cybernyanta.tasker.screen.tasks;

import com.cybernyanta.tasker.data.database.OnChangedListener;
import com.cybernyanta.tasker.data.manager.TaskManager;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.enums.TasksScreenType;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksPresenter implements TasksContract.TasksPresenter {

    private TaskManager taskManager;
    private TasksContract.TasksView tasksView;

    public TasksPresenter(TaskManager taskManager) {
        this.taskManager = taskManager;
    }

    @Override
    public List<Task> getTasks(TasksScreenType tasksScreenType) {
        switch (tasksScreenType){
            case ALL:
                return taskManager.getAllOpen();
            case TODAY:
                return taskManager.getForToday();
            case TOMORROW:
                return taskManager.getForTomorrow();
            case INBOX:
                break;
            case NEXT_WEEK:
                return taskManager.getForNextWeek();
            case PROJECT_TASKS:
                break;
        }
        return null;
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
        taskManager.cleanup();
    }
}
