package com.cybernyanta.tasker.ui.fragment;

import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.cybernyanta.tasker.constants.IntentExtraConstants;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.models.Task;

import com.cybernyanta.tasker.ui.activities.TaskEditCreateActivity;
import com.cybernyanta.tasker.ui.adapters.firebase.FirebaseRecyclerAdapter;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.cybernyanta.tasker.constants.FirebaseConstants.TASKS_CHILD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.USERS_CHILD;

/**
 * Created by evgeniy.siyanko on 15.12.2016.
 */

public class TaskListFragment extends BaseFragment implements View.OnClickListener {

    private DatabaseReference mTaskReference;

    public static class TaskViewHolder extends RecyclerView.ViewHolder {

        @BindView(R.id.task_title)
        public TextView titleTextView;
        @BindView(R.id.task_dueDate)
        public TextView dueDateTextView;
        @BindView(R.id.task_project)
        public TextView projectTextView;
        @BindView(R.id.task_color_border)
        public View colorView;

        public View root;
        public TaskViewHolder(View v) {
            super(v);
            ButterKnife.bind(this, v);
            root = v;
        }
    }

    protected FirebaseUser mFirebaseUser;
    protected String mUsername;
    protected FirebaseAuth mFirebaseAuth;
    private DatabaseReference mFirebaseDatabaseReference;
    private FirebaseRecyclerAdapter<Task, TaskViewHolder> mFirebaseAdapter;

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

//                startActivity(new Intent(getActivity(), TaskEditCreateActivity.class));
            }
        });



        mFirebaseDatabaseReference = FirebaseDatabase.getInstance().getReference().child(USERS_CHILD).child(mFirebaseUser.getUid());

        mLinearLayoutManager = new LinearLayoutManager(getContext());
        mLinearLayoutManager.setStackFromEnd(true);
        mFirebaseAdapter = new FirebaseRecyclerAdapter<Task,
                TaskViewHolder>(
                Task.class,
                R.layout.task_list_item,
                TaskViewHolder.class,
                mFirebaseDatabaseReference.child(TASKS_CHILD).equalTo("title0","title")) {

            @Override
            protected void populateViewHolder(TaskViewHolder viewHolder,
                                              Task model, final int position) {
                viewHolder.titleTextView.setText(model.getTitle());
                viewHolder.dueDateTextView.setText(model.getDueDate().toString());
                viewHolder.colorView.setBackgroundColor(Color.parseColor("#FF87CEFA"));
                viewHolder.root.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        String uid = mFirebaseAdapter.getFirebaseItemId(position);
                        Intent intent = new Intent(getContext(), TaskEditCreateActivity.class);
                        intent.putExtra(IntentExtraConstants.TASK_ID_EXTRA, uid);
                        startActivity(intent);
                    }
                });
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
