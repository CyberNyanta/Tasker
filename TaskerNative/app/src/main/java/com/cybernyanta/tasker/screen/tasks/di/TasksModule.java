package com.cybernyanta.tasker.screen.tasks.di;

import com.cybernyanta.tasker.data.database.Datasource;
import com.cybernyanta.tasker.data.database.FirebaseDatasource;
import com.cybernyanta.tasker.data.manager.TaskManager;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.screen.tasks.TasksPresenter;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.database.FirebaseDatabase;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;

import static com.cybernyanta.tasker.constants.FirebaseConstants.TASKS_CHILD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.USERS_CHILD;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */
@Module
public class TasksModule {

    @Provides
    @Singleton
    TaskManager provideTaskManager(Datasource<Task> taskDatasource){
        return new TaskManager(taskDatasource);
    }
    @Provides
    @Singleton
    Datasource<Task> provideTaskDatasource(){
        return new FirebaseDatasource<>(FirebaseDatabase.getInstance()
                .getReference().child(USERS_CHILD)
                .child(FirebaseAuth.getInstance().getCurrentUser().getUid())
                .child(TASKS_CHILD), Task.class);
    }
    @Provides
    @Singleton
    TasksPresenter provideTasksPresenter(TaskManager taskManager){
        return new TasksPresenter(taskManager);
    }
}
