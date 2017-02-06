package com.cybernyanta.tasker.screen.tasks;

import com.cybernyanta.tasker.data.database.OnChangedListener;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.base.BasePresenter;
import com.cybernyanta.tasker.screen.base.BaseView;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public interface TasksContract {
    interface TasksView extends BaseView<TasksPresenter> {
        TasksScreenType getTasksScreenType();
    }

    interface TasksPresenter extends BasePresenter<TasksView> {
        List<Task> getTasks(TasksScreenType tasksScreenType);
        void addOnDataSetChanged(OnChangedListener listener);
    }
}
