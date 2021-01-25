var toDo = angular.module("toDo", []);
toDo.controller("firstCtrl", function($scope, $http)
    {
        $scope.todos = [];
        $scope.editTodos = [];
        $scope.completed = [];


        $scope.search = function() {
            $http({
                    url: 'http://localhost:49834/todo/list',
                    method: 'GET',
                    params: { description: $scope.description }
                })
                .then(function(response) {
                        $scope.todos = response.data;
                    },
                    function(error) {
                        console.error(error);
                    });
        }   

        $scope.search();

        $scope.createTodo = function() {
            $http({
                    url: 'http://localhost:49834/todo/create/' + newGuid(),
                    method: 'POST',
                    data: JSON.stringify({ description: $scope.new.description })
                })
                .then(function(response) {                        
                        $scope.todos.push(response.data);
                    },
                    function(error) {
                        console.error(error);
                    });
        }

        $scope.deleteTodo = function(todo)
        {
            $http({
                url: 'http://localhost:49834/todo/delete/' + todo.id,
                method: 'DELETE'
            })
            .then(function(response) {
                $scope.todos.removeById(todo.id);
            }), function(error) {
                console.error(error);
            }
        }


    var updateTodoItem = function (todo, todoCleanup) {
        $http({
                    url: 'http://localhost:49834/todo/update/' + todo.id,
                    method: 'POST',
                    data: JSON.stringify({ description: todo.description })
                })
                .then(function(response) {                     
                    var todo = $scope.todos.getById(response.data.id);
                    if (todo) {
                        todo.description = response.data.description;
                    }
                    todoCleanup(todo);
                }, function(error) {
                    console.error(error);
                });
    }

     $scope.updateTodo = function() {
         updateTodoItem($scope.editTodo, function () {
               $scope.editTodo = null;  
             });
        }

    $scope.getTodosEdit = function (todo) {
        var itemTodo = $scope.editTodos.getById(todo.id);
        if (!itemTodo) {
            $scope.editTodos.push(angular.copy(todo));
        }
    }

        $scope.setTodoForEdit = function(todo) {
            $scope.editTodo = angular.copy(todo);
        }


    $scope.removeEditTodo = function (todo) {
        $scope.editTodos.removeById(todo.id);
    }

    $scope.updateTodos = function () {
        var todos = angular.copy($scope.editTodos);
        for(var i=0; i<todos.length;i++) { 
            updateTodoItem(todos[i], $scope.removeEditTodo);
        };
    }

    $scope.setForCompleted = function (todo) {
        if (!$scope.completed.getById(todo.id)) {
            $scope.completed.push(todo);
        } else {
            $scope.completed.removeById(todo.id);
        }
    }

    $scope.completedTodos = function () {
         var todos = angular.copy($scope.completed);
        console.log('todos: ', todos);
        for(var i=0; i<todos.length;i++) { 
            console.log('todos['+i+']=', todos[i]);
            $http({
                    url: 'http://localhost:49834/todo/completed/' + todos[i].id,
                    method: 'POST',
                    data: JSON.stringify({ completed: true })
                })
                .then(function(response) { 
                    var todo = $scope.todos.getById(response.data.id);
                    if (todo) {
                        todo.completed = response.data.completed;
                    }
                    $scope.completed.removeById(todo.id);
                },
                    function(error) {
                    console.error(error);
                });
        };
        
    }

    
});