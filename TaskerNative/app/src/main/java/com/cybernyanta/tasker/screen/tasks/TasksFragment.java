package com.cybernyanta.tasker.screen.tasks;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.constants.IntentExtraConstants;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.tasks.di.DaggerTasksComponent;
import com.cybernyanta.tasker.screen.tasks.di.TasksModule;
import com.google.firebase.database.DatabaseError;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksFragment extends Fragment implements TasksContract.TasksView{

    @Inject
    TasksPresenter tasksPresenter;

    @BindView(R.id.tasks_recycleView)
    RecyclerView recyclerView;

    TasksScreenType tasksScreenType;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        DaggerTasksComponent.builder()
                .tasksModule(new TasksModule())
                .build().injectTasksFragment(this);
        tasksScreenType = (TasksScreenType)getActivity().getIntent().getSerializableExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.task_list, container,false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);
        final TasksRecycleAdapter adapter = new TasksRecycleAdapter(tasksPresenter.getTasks(getTasksScreenType()));
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getActivity().getApplicationContext()));
        tasksPresenter.addOnDataSetChanged(new OnChangedListener() {
            @Override
            public void onChanged(EventType type, int index, int oldIndex) {
                adapter.setTasks(tasksPresenter.getTasks(tasksScreenType));
            }

            @Override
            public void onCancelled(DatabaseError databaseError) {

            }
        });
    }


    @Override
    public void setTitle(String titlle) {

    }

    @Override
    public TasksScreenType getTasksScreenType() {
        return  tasksScreenType;
    }
}
