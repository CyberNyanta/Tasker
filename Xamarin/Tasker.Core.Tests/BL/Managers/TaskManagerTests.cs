using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Ploeh.AutoFixture.NUnit3;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.BL.Managers;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.Tests.BL.Managers
{
    [TestFixture]
    public class TaskManagerTests
    {
        private IFixture fixture;
        private const int LIST_SIZE = 20;
        private List<Task> taskList;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
        }

        private IUnitOfWork GetFakeUnitOfWork()
        {
            var unitOfWork = fixture.Create<IUnitOfWork>();
            var projectRepository = fixture.Create<IRepository<Project>>();
            var taskRepository = fixture.Create<IRepository<Task>>();
            unitOfWork.Stub(u =>u.ProjectsRepository).Return(projectRepository);
            unitOfWork.Stub(u => u.TasksRepository).Return(taskRepository);
            taskList = fixture.CreateMany<Task>(LIST_SIZE).ToList();
            for (int i = 1; i < 3; i++)
            {
                taskList[1 * i].DueDate = DateTime.Today;
                taskList[2 * i].DueDate = DateTime.Today.AddDays(1);
                taskList[3 * i].DueDate = DateTime.Today.AddDays(4);
                taskList[4 * i].DueDate = DateTime.Today.AddDays(10);
                taskList[5 * i].DueDate = DateTime.Today.AddDays(-3);
                for(int ii = 1; ii < 6; ii++)
                {
                    if (i == 1)
                    {
                        taskList[ii * i].IsCompleted = true;
                    }
                    else
                    {
                        taskList[ii * i].IsCompleted = false;
                    }
                }
            
            }           
            unitOfWork.TasksRepository.Stub(m => m.GetAll()).Return(taskList);
            unitOfWork.TasksRepository.Stub(m => m.GetById(Arg<int>.Is.GreaterThan(0))).Return(fixture.Create<Task>());
            unitOfWork.ProjectsRepository.Stub(m => m.GetById(Arg<int>.Is.GreaterThan(0))).Return(fixture.Create<Project>());
            return unitOfWork;
        }

        [Test]
        public void GetAll_IsTaskRepositoryCalled()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var list = manager.GetAll();
            //assert
            unitOfWork.TasksRepository.AssertWasCalled(u => u.GetAll());
        }

        [Test]
        public void Get_IsTaskRepositoryCalled()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            var id = fixture.Create<int>();
            //act
            var returnId = manager.Get(id);
            //assert
            unitOfWork.TasksRepository.AssertWasCalled(u => u.GetById(id));
        }

        [Test]
        [TestCase(0)]
        public void Get_ZeroParameter_IsNullReturn(int id)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var item = manager.Get(id);
            //assert
            Assert.IsNull(item);
        }

        [Test]
        public void GetProjectTasks_IsTaskRepositoryCalled()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);            
            var id = fixture.Create<int>();
            //act
            var taskList = manager.GetProjectTasks(id);
            //assert
            unitOfWork.TasksRepository.AssertWasCalled(u => u.Find(Arg<Func<Task, bool>>.Is.Anything));
        }

        [Test]
        public void GetAllSolve_IsAllSolve()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetAllCompleted();
            //assert
            var countOfOpen = taskList.Find(x => !x.IsCompleted);
            Assert.AreEqual(countOfOpen, null);
        }

        [Test]
        public void GetAllOpen_IsAllOpen()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetAllOpen();
            //assert
            var countOfSolve = taskList.Find(x => x.IsCompleted);
            Assert.AreEqual(countOfSolve, null);
        }

        [Test]
        public void GetProjectSolveTasks_IsAllSolve()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            var projectId = fixture.Create<int>();
            unitOfWork.TasksRepository.Stub(m => m.Find(Arg<Func<Task, bool>>.Is.Anything)).Return(taskList);
            //act
            var list = manager.GetProjectCompletedTasks(projectId);
            //assert
            var anyOpenTask = list.Find(x => !x.IsCompleted);
            Assert.IsNull(anyOpenTask);
        }

        [Test]
        public void GetProjectSolveTasks_IsAllOpen()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            var projectId = fixture.Create<int>();
            unitOfWork.TasksRepository.Stub(m => m.Find(Arg<Func<Task, bool>>.Is.Anything)).Return(taskList);
            //act
            var list = manager.GetProjectOpenTasks(projectId);
            //assert
            var anySolveTask = list.Find(x => x.IsCompleted);
            Assert.IsNull(anySolveTask);
        }

        [Test]
        public void GetProjects_IsProjectRepositoryCalled()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var list = manager.GetProjects();
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetAll(),o=> { var t = o.Repeat; });
        }

        [Test]
        public void GetForToday_IsAllOpen()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForToday();
            //assert
            var countOfSolve = taskList.Find(x => x.IsCompleted);
            Assert.AreEqual(countOfSolve, null);
        }

        [Test]
        public void GetForToday_IsDateCorrect()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForToday();
            //assert
            var anyWrong = taskList.Find(x => x.DueDate >= DateTime.Today.AddDays(1));
            Assert.IsNull(anyWrong);
        }

        [Test]
        public void GetForTomorrow_IsAllOpen()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForTomorrow();
            //assert
            var anySolve = taskList.Find(x => x.IsCompleted);
            Assert.IsNull(anySolve);
        }

        [Test]
        public void GetForTomorrow_IsDateCorrect()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForTomorrow();
            //assert
            var anyWrong = taskList.Find(x => x.DueDate >= DateTime.Today.AddDays(2)
                        || x.DueDate < DateTime.Today.AddDays(1));
            Assert.IsNull(anyWrong);
        }

        [Test]
        public void GetForNextWeek_IsAllOpen()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForNextWeek();
            //assert
            var anySolve = taskList.Find(x => x.IsCompleted);
            Assert.IsNull(anySolve);
        }

        [Test]
        public void GetForNextWeek_IsDateCorrect()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForNextWeek();
            //assert
            var anyWrong = taskList.Find(x=> x.DueDate >= DateTime.Today.AddDays(8));
            Assert.IsNull(anyWrong);
        }

        [Test, AutoData]
        public void SaveItem_NotZeroID_IsTaskRepositoryGetByIdCalled(Task task)
        {
            //arrange
            task.ID = 1;
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);            
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.TasksRepository.AssertWasCalled(u => u.GetById(1));
        }

        [Test, AutoData]
        public void SaveItem_ZeroID_IsTaskRepositoryGetByIdNotCalled(Task task)
        {
            //arrange
            task.ID = 0;
            var unitOfWork = GetFakeUnitOfWork();           
            var manager = new TaskManager(unitOfWork);
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.TasksRepository.AssertWasNotCalled(u => u.GetById(Arg<int>.Is.Anything));
        }

        [Test, AutoData]
        public void SaveItem_ZeroID_IsProjectRepositoryGetByIdCalled(Task task)
        {
            //arrange
            task.ID = 0;
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetById(task.ProjectID), options => options.Repeat.AtLeastOnce());
        }

        [Test, AutoData]
        public void SaveItem_NotZeroID_IsProjectRepositoryGetByIdCalled(Task task)
        {
            //arrange
            task.ID = 1;
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetById(task.ProjectID), options => options.Repeat.AtLeastOnce());
        }

        [Test, AutoData]
        public void SaveItem_ZeroId_IsProjectRepositorySaveCalled(Task task)
        {
            //arrange
            task.ID = 0;
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.Save(Arg<Project>.Is.Anything), options => options.Repeat.AtLeastOnce());
        }

        [Test, AutoData]
        public void SaveItem_NotZeroId_IsProjectRepositorySaveCalled(Task task)
        {
            //arrange
            task.ID = 1;
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.Save(Arg<Project>.Is.Anything), options => options.Repeat.AtLeastOnce());
        }

        [Test, AutoData]
        public void ChangeStatus_IsTaskRepositoryGetByIdCalled(Task task)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.TasksRepository.AssertWasCalled(u => u.GetById(Arg<int>.Is.Anything));
        }
        [Test, AutoData]
        public void ChangeStatus_IsTaskRepositorySaveCalled(Task task)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var returnId = manager.SaveItem(task);
            //assert
            unitOfWork.TasksRepository.AssertWasCalled(u => u.Save(Arg<Task>.Is.Anything));
        }
        [Test, AutoData]
        public void ChangeStatus_IsProjectRepositoryGetByIdCalled(Task task)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            manager.ChangeStatus(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetById(task.ProjectID), options => options.Repeat.AtLeastOnce());
        }

        [Test, AutoData]
        public void ChangeStatus_IsProjectRepositorySaveCalled(Task task)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            manager.ChangeStatus(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.Save(Arg<Project>.Is.Anything), options => options.Repeat.AtLeastOnce());
        }

        [Test, AutoData]
        public void Delete_IsTaskRepositoryDeleteCalled(Task task)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            manager.Delete(task);
            //assert
            unitOfWork.TasksRepository.AssertWasCalled(u => u.Delete(Arg<int>.Is.Anything), options => options.Repeat.AtLeastOnce());
        }
        [Test, AutoData]
        public void Delete_IsProjectRepositoryGetByIdCalled(Task task)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            manager.Delete(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetById(Arg<int>.Is.Anything), options => options.Repeat.AtLeastOnce());
        }

        [Test, AutoData]
        public void Delete_IsProjectRepositorySaveCalled(Task task)
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            manager.ChangeStatus(task);
            //assert
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.Save(Arg<Project>.Is.Anything), options => options.Repeat.AtLeastOnce());
        }
    }
}