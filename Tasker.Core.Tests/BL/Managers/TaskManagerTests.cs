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
            taskList[0].IsSolved = true;
            taskList[1].IsSolved = false;
            unitOfWork.TasksRepository.Stub(m => m.GetAll()).Return(taskList);
            return unitOfWork;
        }

        [Test]
        public void GetAll_IsRepositoryCalled()
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
        public void Get_IsRepositoryCalled()
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
        public void GetProjectTasks_IsRepositoryCalled()
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
            var taskList = manager.GetAllSolve();
            //assert
            var countOfOpen = taskList.Find(x => !x.IsSolved);
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
            var countOfSolve = taskList.Find(x => x.IsSolved);
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
            var list = manager.GetProjectSolveTasks(projectId);
            //assert
            var anyOpenTask = list.Find(x => !x.IsSolved);
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
            var anySolveTask = list.Find(x => x.IsSolved);
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
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetAll());
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
            var countOfSolve = taskList.Find(x => x.IsSolved);
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
            var anyWrong = taskList.Find(x => x.IsSolved || x.DueDate >= DateTime.Today.AddDays(1));
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
            var anySolve = taskList.Find(x => x.IsSolved);
            Assert.IsNull(anySolve);
        }

        [Test]
        public void GetForTomorrow_IsDateCorrect()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForToday();
            //assert
            var anyWrong = taskList.Find(x => x.IsSolved || x.DueDate >= DateTime.Today.AddDays(2) && x.DueDate < DateTime.Today.AddDays(1));
            Assert.IsNull(anyWrong);
        }

        [Test]
        public void GetForNextWeek_IsAllOpen()
        {
            //arrange
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new TaskManager(unitOfWork);
            //act
            var taskList = manager.GetForTomorrow();
            //assert
            var anySolve = taskList.Find(x => x.IsSolved);
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
            var anyWrong = taskList.Find(x => x.IsSolved || x.DueDate >= DateTime.Today.AddDays(1));
            Assert.IsNull(anyWrong);
        }
    }
}