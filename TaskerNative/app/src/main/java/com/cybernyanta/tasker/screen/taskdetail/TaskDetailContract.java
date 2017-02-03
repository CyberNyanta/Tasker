package com.cybernyanta.tasker.screen.taskdetail;

import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.screen.base.BasePresenter;
import com.cybernyanta.tasker.screen.base.BaseView;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */

public interface TaskDetailContract {
    interface TaskDetailView extends BaseView<TaskDetailPresenter> {
        void setProject();
        void setColor();
        void setReminder();
        void setTitleError();
        void saveTask();
        void deleteTask();
        void initFields();
    }

    interface TaskDetailPresenter extends BasePresenter<TaskDetailView> {
        Task getTask(String id);
        void saveTask(Task task);
        void deleteTask(String id);
        void changeStatus(Task task);
    }
}
