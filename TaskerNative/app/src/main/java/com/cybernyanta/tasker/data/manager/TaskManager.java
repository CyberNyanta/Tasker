package com.cybernyanta.tasker.data.manager;

import android.support.annotation.Nullable;

import com.cybernyanta.tasker.data.database.Datasource;
import com.cybernyanta.tasker.data.database.OnChangedListener;
import com.cybernyanta.tasker.data.manager.contract.TaskManagerContract;
import com.cybernyanta.tasker.data.model.Task;
import com.cybernyanta.tasker.data.util.DateUtil;
import com.google.android.gms.tasks.OnCompleteListener;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by evgeniy.siyanko on 04.01.2017.
 */

public class TaskManager implements TaskManagerContract {

    private Datasource<Task> taskDatasource;

    public TaskManager(Datasource<Task> taskDatasource) {
        this.taskDatasource = taskDatasource;
    }

    public void addOnChangedListener(OnChangedListener listener) {
        taskDatasource.addOnChangedListener(listener);
    }

    public void cleanup() {
        taskDatasource.cleanup();
    }

    public List<Task> getAllOpen() {
        List<Task> result = new ArrayList<>();
        for (Task task : taskDatasource) {
            if (!task.isCompleted()) {
                result.add(task);
            }
        }
        return result;
    }

    public List<Task> getAllCompleted() {
        List<Task> result = new ArrayList<>();
        for (Task task : taskDatasource) {
            if (task.isCompleted()) {
                result.add(task);
            }
        }

        return result;
    }

    public List<Task> getForToday() {
        List<Task> result = new ArrayList<>();
        for (Task task : getAllOpen()) {
            if (task.getDueDate() < DateUtil.addDays(DateUtil.getTodayEpochDate(), 1)) {
                result.add(task);
            }
        }
        return result;
    }

    public List<Task> getForTomorrow() {
        List<Task> result = new ArrayList<>();
        for (Task task : getAllOpen()) {
            if (task.getDueDate() < DateUtil.addDays(DateUtil.getTodayDate(), 2).getTime()
                    && task.getDueDate() >= DateUtil.addDays(DateUtil.getTodayEpochDate(), 1)) {
                result.add(task);
            }
        }
        return result;
    }

    public List<Task> getForNextWeek() {
        List<Task> result = new ArrayList<>();
        for (Task task : getAllOpen()) {
            if (task.getDueDate() >DateUtil.addDays(DateUtil.getTodayEpochDate(), 8)) {
                result.add(task);
            }
        }
        return result;
    }

    public void changeCompletedStatus(Task task) {
        task.setCompleted(!task.isCompleted());
        taskDatasource.set(task);
    }

    public void addTask(Task task) {
        taskDatasource.add(task);
    }

    public void setTask(Task task) {
        taskDatasource.set(task);
    }

    public void addTask(Task task, OnCompleteListener<Void> onCompleteListener) {
        taskDatasource.add(task, onCompleteListener);
    }

    public void setTask(Task task, OnCompleteListener<Void> onCompleteListener) {
        taskDatasource.set(task, onCompleteListener);
    }

    public void deleteTask(String id) {
        taskDatasource.remove(id);
    }

    @Nullable
    public Task getTask(String id) {
        return taskDatasource.get(id);
    }


    /* public List<Task> GetProjectTasks(String projectId){
    }

    public List<Task> GetProjectOpenTasks(String projectId){
    }

    public List<Task> GetProjectCompletedTasks(String projectId){

    }*/

    /*    public List<Task> GetWhere(Predicate<Task> predicate){

    }*/

   /* public List<Project> GetProjects(){

    }*/
}
