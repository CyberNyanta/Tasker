package com.cybernyanta.tasker;

import android.app.Application;
import android.content.Context;

import com.google.firebase.database.FirebaseDatabase;


/**
 * Created by evgeniy.siyanko on 10.01.2017.
 */

public class TaskerApplication extends Application {

    private static Context context;

    public static Context getContext() {
        return context;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        context = this;
        FirebaseDatabase.getInstance().setPersistenceEnabled(true);
    }
}
