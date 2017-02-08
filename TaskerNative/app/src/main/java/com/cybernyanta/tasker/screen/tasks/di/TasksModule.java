package com.cybernyanta.tasker.screen.tasks.di;

import com.cybernyanta.tasker.data.database.Datasource;
import com.cybernyanta.tasker.data.database.FirebaseDatasource;
import com.cybernyanta.tasker.data.manager.TaskManager;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.data.util.DateUtil;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.tasks.TasksPresenter;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.Query;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;

import static com.cybernyanta.tasker.constants.FirebaseConstants.INBOX_ID;
import static com.cybernyanta.tasker.constants.FirebaseConstants.TASKS_CHILD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.TASK_COMPLETED_FIELD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.TASK_DUE_DATE_FIELD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.TASK_PROJECT_FIELD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.USERS_CHILD;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */
@Module
public class TasksModule {

    TasksScreenType tasksScreenType;
    String projectId;


    public TasksModule(TasksScreenType tasksScreenType, String projectId) {
        this.tasksScreenType = tasksScreenType;
        this.projectId = projectId;
    }

    @Provides
    @Singleton
    TaskManager provideTaskManager(Datasource<Task> taskDatasource){
        return new TaskManager(taskDatasource);
    }
    @Provides
    @Singleton
    Datasource<Task> provideTaskDatasource(){
        Query query = FirebaseDatabase.getInstance()
                .getReference().child(USERS_CHILD)
                .child(FirebaseAuth.getInstance().getCurrentUser().getUid())
                .child(TASKS_CHILD); //startAt(1482152555009.0).endAt(1482152555012.0);
        switch (tasksScreenType) {
            case TODAY:
                query.orderByChild(TASK_DUE_DATE_FIELD).endAt((double) DateUtil.addDays(DateUtil.getTodayEpochDate(), 1));
                break;
            case TOMORROW:
                query.orderByChild(TASK_DUE_DATE_FIELD).startAt((double) DateUtil.addDays(DateUtil.getTodayEpochDate(), 2))
                        .endAt((double) DateUtil.addDays(DateUtil.getTodayEpochDate(), 2));
                break;
            case NEXT_WEEK:
                query.orderByChild(TASK_DUE_DATE_FIELD).endAt((double) DateUtil.addDays(DateUtil.getTodayEpochDate(), 8));
                break;
            case INBOX:
                query.orderByChild(TASK_PROJECT_FIELD).equalTo(INBOX_ID);
                break;
            case PROJECT_TASKS:
                query.orderByChild(TASK_PROJECT_FIELD).equalTo(projectId);
                break;
            case PROJECT_COMPLETED_TASKS:
                query.orderByChild(TASK_PROJECT_FIELD).equalTo(projectId);
                break;
            case COMPLETED_TASKS:
                query.orderByChild(TASK_COMPLETED_FIELD).equalTo(true);
                break;
        }
        return new FirebaseDatasource<>(query, Task.class);
    }
    @Provides
    @Singleton
    TasksPresenter provideTasksPresenter(TaskManager taskManager){
        return new TasksPresenter(taskManager);
    }
}
