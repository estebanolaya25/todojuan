﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using todojuan.common.Models;
using todojuan.Functions.Functions;
using todojuan.Tests.Helpers;
using Xunit;

namespace todojuan.Tests.Tests
{
    public class TodoApiTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();
       
        [Fact]
        public async void CreateTodo_Shoud_Return_200()
        {
            //Arrange
            MockCloudTableTodo mockTodos = new MockCloudTableTodo(new Uri("http://127.0.0.1:10002/devstoreaccount1/reports"));
            Todo todoRequest = TestFactory.GetTodoRequest();
            DefaultHttpRequest request = TestFactory.CreateHttpRequest(todoRequest);
            //Act
             IActionResult response = await TodoApi.CreateTodo(request,mockTodos,logger);
            //Assert

            OkObjectResult result = (OkObjectResult)response;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        
        }

        [Fact]
        public async void UpdateTodo_Shoud_Return_200()
        {
            //Arrange
            MockCloudTableTodo mockTodos = new MockCloudTableTodo(new Uri("http://127.0.0.1:10002/devstoreaccount1/reports"));
            Todo todoRequest = TestFactory.GetTodoRequest();
            Guid todoId = Guid.NewGuid();
            DefaultHttpRequest request = TestFactory.CreateHttpRequest(todoId,todoRequest);
            //Act
            IActionResult response = await TodoApi.UpdateTodo(request, mockTodos, todoId.ToString(), logger);
            //Assert
            OkObjectResult result = (OkObjectResult)response;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

        }
    }
}
