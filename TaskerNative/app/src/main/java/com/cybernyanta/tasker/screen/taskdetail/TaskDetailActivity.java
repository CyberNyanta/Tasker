package com.cybernyanta.tasker.screen.taskdetail;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.screen.taskdetail.di.DaggerTaskDetailComponent;
import com.cybernyanta.tasker.screen.taskdetail.di.TaskDetailModule;

import java.util.Date;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.cybernyanta.tasker.constants.IntentExtraConstants.TASK_EXTRA;

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
    public boolean onCreateOptionsMenu(Menu menu) {
        if (task.getId()!=null)
        {
            getMenuInflater().inflate(R.menu.task_edit_menu, menu);
            MenuItem item = menu.findItem(R.id.menu_complete);
            item.setTitle(task.isCompleted() ? R.string.uncomplete_task :  R.string.complete_task);
        }
        else
        {
            getMenuInflater().inflate(R.menu.task_create_menu, menu);
        }
        
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId())
        {
            case android.R.id.home:
                onBackPressed();
                break;
            case R.id.menu_save:
                saveTask();
                break;
            case R.id.menu_delete:
                deleteTask();
                break;
            case R.id.menu_complete:
                item.setTitle(task.isCompleted() ? R.string.complete_task : R.string.uncomplete_task);
                presenter.changeStatus(task);
                break;
        }
        return super.onOptionsItemSelected(item);
    }

    @Override
    public void initFields() {
        task = (Task) getIntent().getSerializableExtra(TASK_EXTRA);
        if(task!=null){
            title.setText(task.getTitle());
            description.setText(task.getDescription());
            dueDate.setText(new Date(task.getDueDate()).toString());
            remindDate.setText(new Date(task.getRemindDate()).toString());
            getSupportActionBar().setTitle(getString(R.string.task_edit_title));
        } else
            task = new Task();
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
    public void saveTask() {
        boolean error = false;
        if(title.getTextSize()==0){
            title.setError(getString(R.string.title_error));
        }else {
            task.setTitle(title.getText().toString());
            task.setDescription(description.getText().toString());
            presenter.saveTask(task);
            finish();
        }
    }

    @Override
    public void deleteTask() {
        presenter.deleteTask(task.getId());
        finish();
    }



}
