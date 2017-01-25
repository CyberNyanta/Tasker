package com.cybernyanta.tasker.screen.tasks;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.constants.IntentExtraConstants;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.tasks.di.DaggerTasksComponent;
import com.cybernyanta.tasker.screen.tasks.di.TasksModule;

import javax.inject.Inject;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksFragment extends Fragment implements TasksContract.TasksView{

    @Inject
    TasksPresenter tasksPresenter;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        DaggerTasksComponent.builder()
                .tasksModule(new TasksModule())
                .build().injectTasksFragment(this);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.task_list, container);
    }

    @Override
    public void setTitle(String titlle) {

    }

    @Override
    public TasksScreenType getTasksScreenType() {
       return  TasksScreenType.values()[getActivity().getIntent().getIntExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA,0)];
    }
}
