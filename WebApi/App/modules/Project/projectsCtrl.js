var app = angular.module("app", [])
.controller("projectsCtrl", function($scope, $http)
{
    $scope.projects = [];
    $scope.editProjects = [];

    $scope.search = function() {
        $http({
            url: 'http://localhost:49834/projects/list',
            method: 'GET',
            params: { name: $scope.name, skip: 0, take:1000 }
        })
        .then(function(response) {
            $scope.projects = response.data;
        },
        function(error) {
            console.error(error);
        });
    }

    $scope.search();

    $scope.createProject = function() {
        $http({
            url: 'http://localhost:49834/projects/create/' + newGuid(),
            method: 'POST',
            data: JSON.stringify({ name: $scope.newName.name })
        })
        .then(function(response) {
            $scope.projects.push(response.data);
        },
        function(error) {
            console.error(error);
        });
    }

    $scope.deleteProject = function(project)
    {
        $http({
            url:'http://localhost:49834/projects/delete/' + project.id,
            method: 'DELETE'
        })
        .then(function(response) {
            $scope.projects.removeById(project.id);
        }),
        function(error) {
            console.error(error);
        }
    }

    var updateProjectItem = function (project, projectCleanup) {
       $http({
            url: 'http://localhost:49834/projects/update/' + project.id,
            method: 'POST',
            data: JSON.stringify({ name: project.name })
        })
        .then(function(response) {
            var project = $scope.projects.getById(response.data.id);
            if (project) {
                project.name = response.data.name;
            }
            projectCleanup(project);
            alert('Update successfully!!');
        }, function(error) {
            console.error(error);
        });
    }

    $scope.updateProject = function() {
        updateProjectItem($scope.editProject, function () {
            $scope.editProject = null;
        });
    }

    $scope.updateProjects = function () {
        var projects = angular.copy($scope.editProjects);
        for (var i = 0; i < projects.length; i++) {
            updateProjectItem(projects[i], $scope.removeEditProject);
        }
    }

    $scope.getProjectsEdit = function (project) {
        var itemProject = $scope.editProjects.getById(project.id);
        if (!itemProject) {
            $scope.editProjects.push(angular.copy(project));
        }
   } 

    $scope.setProjectForEdit = function(project) {
        $scope.editProject = angular.copy(project);
    }

    $scope.removeEditProject = function (project) {
        $scope.editProjects.removeById(project.id);
    }

});