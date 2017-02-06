package com.cybernyanta.tasker.di;

/**
 * Created by evgeniy.siyanko on 10.01.2017.
 */

import com.cybernyanta.tasker.data.database.Datasource;
import com.cybernyanta.tasker.data.database.FirebaseDatasource;
import com.cybernyanta.tasker.data.manager.TaskManager;
import com.cybernyanta.tasker.data.model.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;

import static com.cybernyanta.tasker.constants.FirebaseConstants.TASKS_CHILD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.USERS_CHILD;

@Module
public class DataModule {

    @Provides
    @Singleton
    TaskManager provideTaskManager(Datasource<Task> taskDatasource){
        return new TaskManager(taskDatasource);
    }

    @Provides
    FirebaseAuth provideFirebaseAuth(){
        return FirebaseAuth.getInstance();
    }


    @Provides
    FirebaseUser provideFirebaseUser(FirebaseAuth firebaseAuth){
        return firebaseAuth.getCurrentUser();
    }

    @Provides
    DatabaseReference provideUserDatabaseReference(FirebaseUser firebaseUser){
        return FirebaseDatabase.getInstance().getReference().child(USERS_CHILD).child(firebaseUser.getUid());
    }

    @Provides
    @Singleton
    Datasource<Task> provideTaskDatasource(DatabaseReference databaseReference){
        return new FirebaseDatasource<>(databaseReference.child(TASKS_CHILD), Task.class);
    }

}
