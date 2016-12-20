package com.cybernyanta.tasker.ui.activities;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.cybernyanta.tasker.R;

import butterknife.BindView;
import butterknife.ButterKnife;

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

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.task_edit_create);
        ButterKnife.bind(this);

    }
}
