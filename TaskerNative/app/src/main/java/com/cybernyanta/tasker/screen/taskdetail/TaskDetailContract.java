package com.cybernyanta.tasker.screen.taskdetail;

import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.screen.base.BasePresenter;
import com.cybernyanta.tasker.screen.base.BaseView;
import com.google.android.gms.tasks.OnCompleteListener;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */

public interface TaskDetailContract {
    interface TaskDetailView extends BaseView<TaskDetailPresenter> {
        void setProject();
        void setColor();
        void setReminder();
        void setDueDate();
        void setTitleError();
        void saveTask();
        void deleteTask();
        void initFields();
    }

    interface TaskDetailPresenter extends BasePresenter<TaskDetailView> {
        Task getTask(String id);
        void saveTask(Task task);
        void saveTask(Task task, OnCompleteListener<Void> onCompleteListener);
        void deleteTask(String id);
        void changeStatus(Task task);
    }
}
