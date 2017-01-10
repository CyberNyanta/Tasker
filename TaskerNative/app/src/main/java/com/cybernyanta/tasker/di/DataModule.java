package com.cybernyanta.tasker.di;

/**
 * Created by evgeniy.siyanko on 10.01.2017.
 */

import com.cybernyanta.core.database.Datasource;
import com.cybernyanta.core.database.FirebaseDatasource;
import com.cybernyanta.core.manager.TaskManager;
import com.cybernyanta.core.model.Task;
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
        return new FirebaseDatasource<>(Task.class,databaseReference.child(TASKS_CHILD));
    }

}
