package com.cybernyanta.tasker.screen.tasks;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.constants.IntentExtraConstants;
import com.cybernyanta.tasker.enums.TasksScreenType;
import com.cybernyanta.tasker.screen.taskdetail.TaskDetailActivity;
import com.cybernyanta.tasker.screen.taskdetail.TaskDetailContract;
import com.cybernyanta.tasker.screen.tasks.di.DaggerTasksComponent;
import com.cybernyanta.tasker.screen.tasks.di.TasksModule;
import com.cybernyanta.tasker.screen.tasks.recycler.OnItemClickListener;
import com.cybernyanta.tasker.screen.tasks.recycler.TasksRecyclerAdapter;
import com.google.firebase.database.DatabaseError;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.cybernyanta.tasker.constants.IntentExtraConstants.TASK_EXTRA;
import static com.cybernyanta.tasker.constants.IntentExtraConstants.TASK_ID_EXTRA;

/**
 * Created by evgeniy.siyanko on 25.01.2017.
 */

public class TasksFragment extends Fragment implements TasksContract.TasksView, OnItemClickListener {

    @Inject
    TasksPresenter tasksPresenter;

    @BindView(R.id.tasks_recycleView)
    RecyclerView recyclerView;
    @BindView(R.id.fab)
    FloatingActionButton fab;

    TasksScreenType tasksScreenType;

    TasksRecyclerAdapter adapter;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        DaggerTasksComponent.builder()
                .tasksModule(new TasksModule())
                .build().injectTasksFragment(this);

        TasksScreenType typeExtra = (TasksScreenType) getActivity().getIntent().getSerializableExtra(IntentExtraConstants.TASK_LIST_TYPE_EXTRA);
        tasksScreenType = typeExtra != null ? typeExtra : TasksScreenType.ALL;
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.task_list, container, false);
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);
        adapter = new TasksRecyclerAdapter(tasksPresenter.getTasks(getTasksScreenType()), this);
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getActivity().getApplicationContext()));
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startTaskDetailsActivity(null);
            }
        });
        tasksPresenter.bindView(this);
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
    public void onDestroy() {
        tasksPresenter.unbindView();
        super.onDestroy();
    }

    @Override
    public TasksScreenType getTasksScreenType() {
        return tasksScreenType;
    }

    @Override
    public void onItemClick(@NonNull View view, @NonNull Task task) {
        startTaskDetailsActivity(task);
    }

    private void startTaskDetailsActivity(@Nullable Task task){
        Intent intent = new Intent(getContext(), TaskDetailActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION);
        if(task!=null){
            intent.putExtra(TASK_EXTRA, task);
        }
        getActivity().startActivity(intent);
    }
}
