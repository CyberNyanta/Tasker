package com.cybernyanta.core.database;

import android.support.annotation.NonNull;

import com.cybernyanta.core.model.BaseModel;
import com.cybernyanta.core.model.Task;
import com.google.android.gms.tasks.OnCompleteListener;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 06.01.2017.
 */

public interface Datasource<M extends BaseModel> extends List<M> {

    void cleanup();

    void addOnChangedListener(@NonNull OnChangedListener listener);

    void set(@NonNull M element);

    M get(String id);

    M remove(int index);

    void add(@NonNull M element, OnCompleteListener<Void> onCompleteListener);

    void set(M element, OnCompleteListener<Void> onCompleteListener);
}