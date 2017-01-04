package com.cybernyanta.tasker.ui.activities;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.cybernyanta.data.model.Task;
import com.cybernyanta.tasker.R;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.cybernyanta.tasker.constants.FirebaseConstants.TASKS_CHILD;
import static com.cybernyanta.tasker.constants.FirebaseConstants.USERS_CHILD;
import static com.cybernyanta.tasker.constants.IntentExtraConstants.TASK_ID_EXTRA;

/**
 * Created by evgeniy.siyanko on 15.12.2016.
 */

public class TaskEditCreateActivity extends BaseActivity {


    //region Properties
    @BindView(R.id.task_title)
    protected EditText taskTitle;
    @BindView(R.id.task_description)
    protected EditText taskDescription;
    @BindView(R.id.task_project)
    protected TextView taskProject;
    @BindView(R.id.task_dueDate)
    protected TextView taskDueDate;
    @BindView(R.id.task_remindDate)
    protected TextView taskRemindDate;
    @BindView(R.id.color_name)
    protected TextView colorName;
    @BindView(R.id.color_container)
    protected LinearLayout colorContainer;
    @BindView(R.id.color_shape)
    protected ImageView colorShape;

    //endregion
    public DatabaseReference mFirebaseDatabaseReference;
    protected FirebaseUser mFirebaseUser;
    protected String mUsername;
    protected FirebaseAuth mFirebaseAuth;

    Task task;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.task_edit_create);
        ButterKnife.bind(this);

        String taskUID = getIntent().getStringExtra(TASK_ID_EXTRA);

        mFirebaseAuth = FirebaseAuth.getInstance();
        mFirebaseUser = mFirebaseAuth.getCurrentUser();
        mFirebaseDatabaseReference = FirebaseDatabase.getInstance().getReference().child(USERS_CHILD).child(mFirebaseUser.getUid());

        mFirebaseDatabaseReference.child(TASKS_CHILD).child(taskUID).addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot dataSnapshot) {
                Task task = dataSnapshot.getValue(Task.class);

            }

            @Override
            public void onCancelled(DatabaseError databaseError) {

            }
        });


    }

    @Override
    protected void onResume() {
        super.onResume();
    }
}
