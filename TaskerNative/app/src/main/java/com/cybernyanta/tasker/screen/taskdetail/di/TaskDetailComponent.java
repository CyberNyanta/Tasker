package com.cybernyanta.tasker.screen.taskdetail.di;

import com.cybernyanta.tasker.screen.taskdetail.TaskDetailActivity;

import javax.inject.Singleton;

import dagger.Component;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */
@Singleton
@Component(modules = {TaskDetailModule.class})
public interface TaskDetailComponent {
    void injectTaskDetailActivity(TaskDetailActivity taskDetailActivity);
}
