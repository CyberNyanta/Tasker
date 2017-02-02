package com.cybernyanta.tasker.screen.taskdetail;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.cybernyanta.core.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.screen.taskdetail.di.DaggerTaskDetailComponent;
import com.cybernyanta.tasker.screen.taskdetail.di.TaskDetailModule;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.cybernyanta.tasker.constants.IntentExtraConstants.TASK_EXTRA;
import static com.cybernyanta.tasker.constants.IntentExtraConstants.TASK_ID_EXTRA;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */

public class TaskDetailActivity extends AppCompatActivity implements TaskDetailContract.TaskDetailView {

    //region view
    @BindView(R.id.task_title)
    EditText title;
    @BindView(R.id.task_description)
    EditText description;
    @BindView(R.id.task_dueDate)
    TextView dueDate;
    @BindView(R.id.task_remindDate)
    TextView remindDate;
    @BindView(R.id.task_project)
    TextView taskProject;
    @BindView(R.id.color_container)
    LinearLayout colorContainer;
    @BindView(R.id.color_shape)
    ImageView colorShape;
    @BindView(R.id.color_name)
    TextView colorName;
    //endregion
    private Task task;
    @Inject
    TaskDetailContract.TaskDetailPresenter presenter;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.task_edit_create);
        setSupportActionBar((Toolbar)findViewById(R.id.toolbar));
        getSupportActionBar().setTitle(getString(R.string.task_edit_title));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);
        ButterKnife.bind(this);
        DaggerTaskDetailComponent.builder()
                .taskDetailModule(new TaskDetailModule())
                .build()
                .injectTaskDetailActivity(this);
        initFields();
    }

    @Override
    public void initFields() {
        task = (Task) getIntent().getSerializableExtra(TASK_EXTRA);
        if(task!=null){
            title.setText(task.getTitle());
            description.setText(task.getDescription());
            dueDate.setText(task.getDueDate().toString());
            remindDate.setText(task.getRemindDate().toString());
        }
    }

    @Override
    public void setProject() {

    }

    @Override
    public void setColor() {

    }

    @Override
    public void setReminder() {

    }

    @Override
    public void setTitleError() {

    }



    @Override
    public void setTitle(String title) {

    }


}
