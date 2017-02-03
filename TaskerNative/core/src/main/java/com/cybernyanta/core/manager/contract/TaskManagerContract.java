package com.cybernyanta.core.manager.contract;

import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.core.model.Task;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 18.01.2017.
 */

public interface TaskManagerContract {

    void addOnChangedListener(OnChangedListener listener);

    void cleanup();

    List<Task> getAllOpen();

    List<Task> getAllCompleted();

    List<Task> getForToday();

    List<Task> getForTomorrow();

    List<Task> getForNextWeek();

    void changeCompletedStatus(Task task);

    void addTask(Task task);

    void setTask(Task task);

    void deleteTask(String id);

    Task getTask(String id);
}
