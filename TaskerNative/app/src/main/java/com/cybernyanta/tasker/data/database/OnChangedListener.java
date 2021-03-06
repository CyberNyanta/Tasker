package com.cybernyanta.tasker.data.database;

import com.google.firebase.database.DatabaseError;

/**
 * Created by evgeniy.siyanko on 04.01.2017.
 */

public interface OnChangedListener{
    enum EventType {ADDED, CHANGED, REMOVED, MOVED}

    void onChanged(OnChangedListener.EventType type, int index, int oldIndex);

    void onCancelled(DatabaseError databaseError); //// TODO: 10.01.2017 change DatabaseError type on another
}