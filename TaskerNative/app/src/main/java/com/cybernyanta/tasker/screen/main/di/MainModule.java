package com.cybernyanta.tasker.screen.main.di;

import android.content.SharedPreferences;

import com.cybernyanta.tasker.PreferenceHelper;
import com.cybernyanta.tasker.screen.main.MainContract;
import com.cybernyanta.tasker.screen.main.MainPresenter;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;

/**
 * Created by evgeniy.siyanko on 20.01.2017.
 */
@Module
public class MainModule {
    private SharedPreferences sharedPreferences;

    public MainModule(SharedPreferences sharedPreferences){
        this.sharedPreferences = sharedPreferences;
    }

    @Provides
    @Singleton
    PreferenceHelper providePreferenceHelper(){
        return new PreferenceHelper(sharedPreferences);
    }

    @Provides
    @Singleton
    MainContract.MainPresenter provideMainPresenter(PreferenceHelper preferenceHelper){
        return new MainPresenter(preferenceHelper);

    }
}
