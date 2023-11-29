using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.Service.Extensions;
using TodoList.Service.Schema;
using TodoList.Service.Services.Database;
using TodoList.Service.Services.Transformers;
using Xunit;
using TaskStatus = TodoList.Service.Schema.TaskStatus;


public class DatabaseServiceTests
{
    public TaskItemConfig GetTaskConfigMock()
    {
        var statusName = "inprogress";
        var state = default(TaskStatus);

        if (Enum.TryParse<TaskStatus>(statusName, out TaskStatus statusValue))
        {
            state = statusValue;
        }

        return new TaskItemConfig
        {
            
            UniqueId = "1234",
            Name = "123",
            Description = "hi",
            DueDate = DateTime.Now,
            Status = state,
        };

    }

    public FirestoreTaskItemConfig GetFirestoreTaskItemCOnfig()
    {
        var statusName = "inprogress";

        return new FirestoreTaskItemConfig
        {
            DocumentId = "123",
            UniqueId = "123",
            Name = "1231",
            Description = "12312",
            DueDate = DateTime.Now,
            Owner = new List<string>
            {
                "owner1", "owner2" 

            },
            Status = "inprogress",
        };

    }
    public IDatabaseService GetService()
    {
        var transformerMock = new Mock<ITransformerService>();
        var firestoreDbMock = new Mock<IFirestoreService>();
        var request = new Mock<HttpRequest>();
        var targetClaimType = "userId";
        var taskItem = GetTaskConfigMock();
        var user = "testUser";
        var status = "InProgress";
        var name = "TaskName";
        var order = "ascending";
        var firestoreTaskItemList = new List<FirestoreTaskItemConfig> { GetFirestoreTaskItemCOnfig() };

        var firestoreTaskItem = GetFirestoreTaskItemCOnfig();
        transformerMock.Setup(x => x.TransformToFireStoreConfig(taskItem, It.IsAny<List<string>>()))
            .Returns(firestoreTaskItem);

        firestoreDbMock.Setup(x => x.AddAsync<FirestoreTaskItemConfig>(firestoreTaskItem))
            .Returns((Task<FirestoreTaskItemConfig>)Task.CompletedTask);

        firestoreDbMock.Setup(x => x.GetAllAsync<FirestoreTaskItemConfig>(user))
            .ReturnsAsync(new List<FirestoreTaskItemConfig>());

        transformerMock.Setup(x => x.GetUserFromClaims(request.Object, targetClaimType))
            .Returns("testUser");


        firestoreDbMock.Setup(x => x.GetAsync<FirestoreTaskItemConfig>(taskItem.UniqueId))
            .ReturnsAsync(firestoreTaskItem);


        
        firestoreDbMock.Setup(x => x.GetAllByQueryAsync<FirestoreTaskItemConfig>(user, status, name))
            .ReturnsAsync(firestoreTaskItemList);

        transformerMock.Setup(x => x.TransformToTaskItemConfig(It.IsAny<FirestoreTaskItemConfig>()))
            .Returns(new TaskItemConfig { /* initialize with data */ });


        return new DatabaseService(transformerMock.Object, firestoreDbMock.Object);
    }



    [Fact]
    public void GetUserFromAuth_ReturnsUserFromClaims()
    {
        // Arrange
        var service = GetService();
        var request = new Mock<HttpRequest>();
        var targetClaimType = "userId";


        // Act
        var result = service.GetUserFromAuth(request.Object, targetClaimType);

        // Assert
        Assert.Equal("testUser", result);
    }

    [Fact]
    public async Task CreateNewItem_SuccessfullyCreatesItem()
    {
        // Arrange
        var service = GetService();

        var taskItem = GetTaskConfigMock();
        var user = "testUser";

        // Act
        var result = await service.CreateNewItem(taskItem, user);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var taskItemList = Assert.IsType<List<TaskItemConfig>>(okResult.Value);
        Assert.Single(taskItemList); // Assuming one item was created
        // Add more assertions based on your specific logic
    }

    [Fact]
    public async Task GetTaskItems_ReturnsTaskItems()
    {
        // Arrange

        var service = GetService();

        var taskItem = GetTaskConfigMock();
        var user = "testUser";
        var firestoreTaskItemList = new List<FirestoreTaskItemConfig> { /* initialize with data */ };
     
        //Act
        var result = await service.GetTaskItems(user);

        // Assert
        var taskItemList = Assert.IsType<List<TaskItemConfig>>(result);
    }

    [Fact]
    public async Task GetCurrentTaskOwner_ReturnsTaskOwners()
    {
        // Arrange
        var service = GetService();
        var taskItem = GetTaskConfigMock();

        // Act
        var result = await service.GetCurrentTaskOwner(taskItem);

        // Assert
        Assert.Equal(new List<string>
        {
            "owner1", "owner2"

        }, result);
    }

}