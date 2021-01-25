var app = angular.module("app", [])
    .controller("worksheetCtrl", function ($scope, $http) {
        $scope.worksheets = [];
        $scope.users = [];
        $scope.projects = [];
       

        $scope.search = function () {
            $http({
                url: 'http://localhost:49834/worksheets/list',
                method: 'GET',
                params: {
                    userId: $scope.user,
                    projectId: $scope.project,
                    skip: 0,
                    take: 1000 }
            })
                .then(function (response) {
                    $scope.worksheets = response.data;
                },
                    function (error) {
                        console.error(error);
                    });
              }
                      
        $scope.resetFilter = function () {        
            $scope.user = null;
            $scope.project = null;
            $scope.search();
        };
        $scope.resetFilter();
               

           $http({
                    url: 'http://localhost:49834/users/list',
                    method: 'GET',
                    params: { skip:0, take:1000}
            })
             .then(function(response) {
               $scope.users = response.data;
             },
               function(error) {
               console.error(error);
             });

            $http({
                url: 'http://localhost:49834/projects/list',
                method: 'GET',
                params: { skip: 0, take: 1000 }
            })
                .then(function (response) {
                    $scope.projects = response.data;
                },
                    function (error) {
                        console.error(error);
                    });

        $scope.deleteWorksheet = function (worksheet) {
            $http({
                url: 'http://localhost:49834/worksheets/delete/' + worksheet.id,
                method: 'DELETE'
            })
                .then(function (response) {
                    $scope.worksheets.removeById(worksheet.id);
                }),
                function (error) {
                    console.error(error);
                }
        }

        $scope.createWorksheet = function () {
            $http({
                url: 'http://localhost:49834/worksheets/create/' + newGuid(),
                method: 'POST',
                data: JSON.stringify({
                                        user: $scope.addWorksheet.user,
                                        project: $scope.addWorksheet.project,
                                        time: $scope.addWorksheet.time,
                                        date: $scope.addWorksheet.date,
                                        description: $scope.addWorksheet.description})
            })
                .then(function (response) {
                    $scope.worksheets.push(response.data);
                },
                    function (error) {
                        console.error(error);
                    });
        }

       
        var updateWorksheetItem = function (worksheet) {
            $http({
                url: 'http://localhost:49834/worksheets/update/' + worksheet.id,
                method: 'POST',
                data: JSON.stringify({
                                        user: worksheet.user.id,
                                        project: worksheet.project.id,
                                        time: worksheet.time,
                                        date: worksheet.date,
                                        description: worksheet.description})
            })
                .then(function (response) {
                    var worksheetUpdate = $scope.worksheets.getById(response.data.id);
                    if (worksheetUpdate) {
                        worksheetUpdate.user = response.data.user;
                        worksheetUpdate.project = response.data.project;
                        worksheetUpdate.time = response.data.time;
                        worksheetUpdate.date = response.data.date;
                        worksheetUpdate.description = response.data.description;
                    }
                },
                    function (error) {
                    console.error(error);
                    });
        }
        
        $scope.updateWorksheet = function () {
            updateWorksheetItem($scope.editWorksheet, function () {
                $scope.editWorksheet = null;
            });
        }

        $scope.setWorksheetForEdit = function (worksheet) {
            $scope.editWorksheet = angular.copy(worksheet);
        }
    });