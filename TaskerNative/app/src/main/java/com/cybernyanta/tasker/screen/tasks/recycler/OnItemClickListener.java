package com.cybernyanta.tasker.screen.tasks.recycler;

import android.support.annotation.NonNull;
import android.view.View;

import com.cybernyanta.core.model.Task;

/**
 * Created by evgeniy.siyanko on 02.02.2017.
 */

public interface OnItemClickListener {
    void onItemClick(@NonNull View view, @NonNull Task task);
}
