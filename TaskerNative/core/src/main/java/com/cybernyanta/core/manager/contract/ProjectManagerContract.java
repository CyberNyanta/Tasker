package com.cybernyanta.core.manager.contract;

import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.core.model.Project;
import com.cybernyanta.core.model.Task;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 03.02.2017.
 */

public interface ProjectManagerContract {

    void addOnChangedListener(OnChangedListener listener);

    void cleanup();

    List<Project> getAll();

    void addProject(Project project);

    void setProject(Project project);

    void deleteProject(String id);

    Project getProject(String id);
}
