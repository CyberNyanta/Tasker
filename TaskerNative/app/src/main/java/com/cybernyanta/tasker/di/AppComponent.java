package com.cybernyanta.tasker.di;

import javax.inject.Singleton;

import dagger.Component;

/**
 * Created by evgeniy.siyanko on 10.01.2017.
 */
@Singleton
@Component(modules = {DataModule.class})
public interface AppComponent {
    //void injectTaskListFragment(TaskListFragment taskListFragment);
}
