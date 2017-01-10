package com.cybernyanta.core.database;

import android.support.annotation.NonNull;

import com.cybernyanta.core.model.BaseModel;
import com.cybernyanta.core.model.Task;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 06.01.2017.
 */

public interface Datasource<M extends BaseModel> extends List<M> {

    void cleanup();

    void addOnChangedListener(@NonNull OnChangedListener listener);

    void set(@NonNull M task);

    M get(String id);

    M remove(int index);
}