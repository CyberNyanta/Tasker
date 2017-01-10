package com.cybernyanta.core.manager;

import com.cybernyanta.core.database.Datasource;
import com.cybernyanta.core.database.FirebaseDatasource;
import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.core.model.Task;
import com.cybernyanta.core.util.DateUtil;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by evgeniy.siyanko on 04.01.2017.
 */

public class TaskManager{

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

    public List<Task> getAllOpen(){
        List<Task> result = new ArrayList<>();
        for(Task task: taskDatasource){
            if(!task.isCompleted()){
                result.add(task);
            }
        }
        return  result;
    }

    public List<Task> getAllCompleted(){
        List<Task> result = new ArrayList<>();
        for(Task task: taskDatasource){
            if(task.isCompleted()){
                result.add(task);
            }
        }

        return result;
    }

    public List<Task> getForToday(){
        List<Task> result = getAllOpen();
        for(Task task: taskDatasource){
            if(task.getDueDate().compareTo(DateUtil.addDays(DateUtil.getTodayDate(),1))<0){
                result.add(task);
            }
        }
        return  result;
    }

    public List<Task> getForTomorrow(){
        List<Task> result = getAllOpen();
        for(Task task: taskDatasource){
            if(task.getDueDate().compareTo(DateUtil.addDays(DateUtil.getTodayDate(),2))<0
                    &&task.getDueDate().compareTo(DateUtil.addDays(DateUtil.getTodayDate(),1))>=0){
                result.add(task);
            }
        }
        return  result;
    }

    public List<Task> getTasksForNextWeek(){
        List<Task> result = getAllOpen();
        for(Task task: taskDatasource){
            if(task.getDueDate().compareTo(DateUtil.addDays(DateUtil.getTodayDate(),8))<0){
                result.add(task);
            }
        }
        return  result;
    }

    public void changeCompletedStatus(Task task){
        task.setCompleted(!task.isCompleted());
        taskDatasource.set(task);
    }

    public void addTask(Task task){
        taskDatasource.add(task);
    }

    public void setTask(Task task){
        taskDatasource.set(task);
    }

    public void deleteTask(String id){
        taskDatasource.remove(id);
    }

    public Task getTask(String id){
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
