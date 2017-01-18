package com.cybernyanta.tasker.screen.base;

/**
 * Created by evgeniy.siyanko on 18.01.2017.
 */

public interface BasePresenter<V extends BaseView> {
    void bindView(V view);

    void unbindView();
}
