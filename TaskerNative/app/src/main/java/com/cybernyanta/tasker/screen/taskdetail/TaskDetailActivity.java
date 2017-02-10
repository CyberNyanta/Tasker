package com.cybernyanta.tasker.screen.taskdetail;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.data.util.DateUtil;
import com.cybernyanta.tasker.enums.TaskDueDate;
import com.cybernyanta.tasker.screen.taskdetail.adapter.DueDateListAdapter;
import com.cybernyanta.tasker.screen.taskdetail.di.DaggerTaskDetailComponent;
import com.cybernyanta.tasker.screen.taskdetail.di.TaskDetailModule;

import java.util.Date;

import javax.inject.Inject;

import butterknife.BindView;
import butterknife.ButterKnife;
import us.bridgeses.slidedatetimepicker.SlideDateTimeListener;
import us.bridgeses.slidedatetimepicker.SlideDateTimePicker;

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
        dueDate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setDueDate();
            }
        });
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
            dueDate.setText(DateUtil.dateToString(task.getDueDate(), true));
            remindDate.setText(DateUtil.dateToString(task.getRemindDate(), true));
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
/*        new SlideDateTimePicker.Builder(getSupportFragmentManager())
                .setListener(listener)
                .setInitialDate(new Date())
                .build()
                .show()*/
    }
    AlertDialog dialog;
    @Override
    public void setDueDate() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        dialog = builder.setCancelable(true)
                .setAdapter(new DueDateListAdapter(this, task.getDueDate(),
                        new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                dialog.dismiss();
                                SetDueDate((TaskDueDate)v.getTag());
                                //InitRemindDate();
                            }
                        }), null).show();

    }
/*    private void SetDueDate()
    {

    }*/

   private void SetDueDate(TaskDueDate type)
    {
        switch (type)
        {
            case TODAY:
                task.setDueDate(DateUtil.getTodayEpochDate());
                dueDate.setText(getString(R.string.due_dates_today));
                break;
            case TOMORROW:
                task.setDueDate(DateUtil.addDays(DateUtil.getTodayEpochDate(),1));
                dueDate.setText(getString(R.string.due_dates_tomorrow));
                break;
            case NEXT_WEEK:
                task.setDueDate(DateUtil.addDays(DateUtil.getTodayEpochDate(),8));
                dueDate.setText(DateUtil.dateToString(task.getDueDate(),true));
                break;
            case REMOVED:
                task.setDueDate(Long.MAX_VALUE);
                dueDate.setText("");
                break;
            case CUSTOM:
                SlideDateTimePicker.Builder dateTimePicker = new SlideDateTimePicker.Builder(getFragmentManager());
                dateTimePicker.setInitialDate(new Date())
                        .setMinDate(new Date())
                        .setListener(new SlideDateTimeListener() {
                            @Override
                            public void onDateTimeSet(Date date) {
                                task.setDueDate(date.getTime());
                                dueDate.setText(DateUtil.dateToString(date.getTime(),true));
                                //InitRemindDate();
                            }
                        })
                        .setTheme(0)
                        .setIs24HourTime(true)
                        .build()
                        .show();
                break;
        }
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
