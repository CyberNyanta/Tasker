package com.cybernyanta.tasker.screen.main;

import android.content.Intent;
import android.preference.PreferenceManager;

import com.cybernyanta.tasker.PreferenceHelper;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.auth.SignInActivity;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.FirebaseDatabase;

import javax.inject.Inject;

/**
 * Created by evgeniy.siyanko on 18.01.2017.
 */

public class MainPresenter implements MainContract.MainPresenter {

    private MainContract.MainView mainView;
    private FirebaseUser mFirebaseUser;
    private FirebaseAuth mFirebaseAuth;
    private String mUsername;
    private String mPhotoUrl;
    private PreferenceHelper preferenceHelper;

    public MainPresenter(PreferenceHelper preferenceHelper){
        this.preferenceHelper = preferenceHelper;
    }

    @Override
    public boolean checkAuth() {
        mFirebaseAuth = FirebaseAuth.getInstance();
        mFirebaseUser = mFirebaseAuth.getCurrentUser();
        if (mFirebaseUser == null) {
            // Not signed in, launch the Sign In activity
            return false;
        } else {
            return true;
        }

    }

    @Override
    public TasksScreenType getStartTasksScreenType() {
        return  preferenceHelper.getStartTasksScreenType();
    }

    @Override
    public void bindView(MainContract.MainView view) {
        mainView = view;
    }

    @Override
    public void unbindView() {
        mainView = null;
    }

}
