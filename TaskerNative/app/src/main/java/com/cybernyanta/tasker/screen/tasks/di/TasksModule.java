package com.cybernyanta.tasker.screen.tasks.di;

import com.cybernyanta.core.database.Datasource;
import com.cybernyanta.core.database.FirebaseDatasource;
import com.cybernyanta.core.manager.TaskManager;
import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.screen.tasks.TasksPresenter;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.FirebaseDatabase;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;

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
        return new FirebaseDatasource<>(Task.class, FirebaseDatabase.getInstance().getReference().child(USERS_CHILD).child(FirebaseAuth.getInstance().getCurrentUser().getUid()));
    }
    @Provides
    @Singleton
    TasksPresenter provideTasksPresenter(TaskManager taskManager){
        return new TasksPresenter(taskManager);
    }
}
