package com.cybernyanta.tasker.screen.taskdetail.di;

import com.cybernyanta.tasker.data.database.Datasource;
import com.cybernyanta.tasker.data.database.FirebaseDatasource;
import com.cybernyanta.tasker.data.manager.ProjectManager;
import com.cybernyanta.tasker.data.manager.TaskManager;
import com.cybernyanta.tasker.data.model.Project;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.screen.taskdetail.TaskDetailContract;
import com.cybernyanta.tasker.screen.taskdetail.TaskDetailPresenter;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.database.FirebaseDatabase;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;

import static com.cybernyanta.tasker.constants.FirebaseConstants.PROJECT_CHILD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.TASKS_CHILD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.USERS_CHILD;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */
@Module
public class TaskDetailModule {

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
    ProjectManager provideProjectManager(Datasource<Project> projectDatasource){
        return new ProjectManager(projectDatasource);
    }

    @Provides
    @Singleton
    Datasource<Project> provideProjectDatasource(){
        return new FirebaseDatasource<>(FirebaseDatabase.getInstance()
                .getReference().child(USERS_CHILD)
                .child(FirebaseAuth.getInstance().getCurrentUser().getUid())
                .child(PROJECT_CHILD), Project.class);
    }
    @Provides
    @Singleton
    TaskDetailContract.TaskDetailPresenter provideTasksPresenter(TaskManager taskManager){
        return new TaskDetailPresenter(taskManager);
    }
}
