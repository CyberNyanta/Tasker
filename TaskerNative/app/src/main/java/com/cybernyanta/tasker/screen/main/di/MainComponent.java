package com.cybernyanta.tasker.screen.main.di;

import com.cybernyanta.tasker.screen.main.MainActivity;
import com.cybernyanta.tasker.screen.main.MainContract;

import javax.inject.Singleton;

import dagger.Component;

/**
 * Created by evgeniy.siyanko on 18.01.2017.
 */
@Singleton
@Component(modules = {MainModule.class})
public interface MainComponent {
    void injectMainActivity(MainContract.MainPresenter taskListFragment);
}
