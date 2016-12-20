package com.cybernyanta.tasker.ui.fragment;

import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.models.Task;
import com.cybernyanta.tasker.ui.activities.TaskEditCreateActivity;
import com.firebase.ui.database.FirebaseRecyclerAdapter;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * Created by evgeniy.siyanko on 15.12.2016.
 */

public class TaskListFragment extends BaseFragment implements View.OnClickListener {

    public static class TaskViewHolder extends RecyclerView.ViewHolder {

        @BindView(R.id.task_title)
        public TextView titleTextView;
        @BindView(R.id.task_dueDate)
        public TextView dueDateTextView;
        @BindView(R.id.task_project)
        public TextView projectTextView;
        @BindView(R.id.task_color_border)
        public View colorView;

        public TaskViewHolder(View v) {
            super(v);
            ButterKnife.bind(this,v);
        }
    }

    protected FirebaseUser mFirebaseUser;
    protected String mUsername;
    protected FirebaseAuth mFirebaseAuth;
    private DatabaseReference mFirebaseDatabaseReference;
    private FirebaseRecyclerAdapter<Task, TaskViewHolder> mFirebaseAdapter;

    public static final String USERS_CHILD = "users";
    public static final String TASKS_CHILD = "tasks";

    protected SharedPreferences mSharedPreferences;
    protected GoogleApiClient mGoogleApiClient;

    @BindView(R.id.tasks_recycleView)
    protected RecyclerView mRecycleView;
    private LinearLayoutManager mLinearLayoutManager;


    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mFirebaseAuth = FirebaseAuth.getInstance();
        mFirebaseUser = mFirebaseAuth.getCurrentUser();

    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.task_list, container, false);
        ButterKnife.bind(this,view);
        return view;
    }

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        final FloatingActionButton mFAB = (FloatingActionButton)getActivity().findViewById(R.id.fab);
        mFAB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                long count = mFirebaseAdapter.getItemCount();
                long uid = mFirebaseAdapter.getItemId(0);

//                startActivity(new Intent(getActivity(), TaskEditCreateActivity.class));
            }
        });



        mFirebaseDatabaseReference = FirebaseDatabase.getInstance().getReference().child(USERS_CHILD).child(mFirebaseUser.getUid());

        mLinearLayoutManager = new LinearLayoutManager(getContext());
        mLinearLayoutManager.setStackFromEnd(true);
        mRecycleView.setLayoutManager(mLinearLayoutManager);

        mFirebaseAdapter = new FirebaseRecyclerAdapter<Task,
                TaskViewHolder>(
                Task.class,
                R.layout.task_list_item,
                TaskViewHolder.class,
                mFirebaseDatabaseReference.child(TASKS_CHILD)) {

            @Override
            protected void populateViewHolder(TaskViewHolder viewHolder,
                                              Task model, int position) {
                viewHolder.titleTextView.setText(model.getTitle());
                viewHolder.dueDateTextView.setText(model.getDueDate().toString());
                viewHolder.colorView.setBackgroundColor(Color.parseColor("#FF87CEFA"));
            }
        };

        mFirebaseAdapter.registerAdapterDataObserver(new RecyclerView.AdapterDataObserver() {
            @Override
            public void onItemRangeInserted(int positionStart, int itemCount) {
                super.onItemRangeInserted(positionStart, itemCount);
                int taskCount = mFirebaseAdapter.getItemCount();
                int lastVisiblePosition =
                        mLinearLayoutManager.findFirstCompletelyVisibleItemPosition();
                // If the recycler view is initially being loaded or the
                // user is at the bottom of the list, scroll to the bottom
                // of the list to show the newly added message.
                if (lastVisiblePosition == -1 ||
                        (positionStart >= (taskCount - 1) &&
                                lastVisiblePosition == (positionStart - 1))) {
                    mRecycleView.scrollToPosition(positionStart);
                }
            }
        });

        mRecycleView.setLayoutManager(mLinearLayoutManager);
        mRecycleView.setAdapter(mFirebaseAdapter);

    }

    @Override
    public void onResume() {
        super.onResume();
        // mListView.setAdapter();
    }


    @Override
    public void onClick(View v) {
        int id = v.getId();
        switch (id){
            case R.id.fab:

        }
    }
}