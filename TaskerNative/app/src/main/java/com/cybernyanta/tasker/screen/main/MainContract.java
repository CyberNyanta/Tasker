package com.cybernyanta.tasker.screen.main;

import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.base.BasePresenter;
import com.cybernyanta.tasker.screen.base.BaseView;

/**
 * Created by evgeniy.siyanko on 18.01.2017.
 */

public interface MainContract{
    interface MainView extends BaseView<MainPresenter>{
        void showAuthScreen();
        void showTaskFragment(TasksScreenType tasksScreenType);
    }

    interface MainPresenter extends BasePresenter<MainView>{
        void checkAuth();
        TasksScreenType getStartTasksScreenType();
    }
}
