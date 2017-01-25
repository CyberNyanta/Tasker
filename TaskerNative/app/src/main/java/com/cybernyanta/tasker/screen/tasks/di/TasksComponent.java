package com.cybernyanta.tasker.screen.tasks.di;

import com.cybernyanta.tasker.screen.tasks.TasksFragment;

import javax.inject.Singleton;

import dagger.Component;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */
@Singleton
@Component(modules = {TasksModule.class})
public interface TasksComponent {
    void injectTasksFragment(TasksFragment tasksFragment);
}
