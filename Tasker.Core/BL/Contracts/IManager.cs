﻿using System.Collections.Generic;
using Tasker.Core.DAL.Contracts;

namespace Tasker.Core.BL.Contracts
{
    public interface IManager<T> where T : IBusinessEntity
    {
        T Get(int id);

        List<T> GetAll();

        int SaveItem(T item);

        int Delete(int id);

    }
}
