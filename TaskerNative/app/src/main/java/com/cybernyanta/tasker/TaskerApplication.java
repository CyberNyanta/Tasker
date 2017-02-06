package com.cybernyanta.tasker;

import android.app.Application;

import com.google.firebase.database.FirebaseDatabase;


/**
 * Created by evgeniy.siyanko on 10.01.2017.
 */

public class TaskerApplication extends Application {

    @Override
    public void onCreate() {
        super.onCreate();
        FirebaseDatabase.getInstance().setPersistenceEnabled(true);
    }
}
