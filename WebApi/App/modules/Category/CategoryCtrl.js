var app = angular.module("app", [])
    .controller("categoryCtrl", function ($scope, $http) {
        $scope.categories = [];

        $scope.search = function () {
            $http({
                url: 'http://localhost:49834/categories/list',
                method: 'GET',
                params: {
                    name: $scope.name,
                    active: $scope.active,
                    skip: 0,
                    take: 1000
                }
            })
                .then(function (response) {
                    $scope.categories = response.data;
                },
                    function (error) {
                        console.error(error);
                    });
        }

        $scope.resetFilterCategory = function () {
            $scope.name = null;
            $scope.active = null;
            $scope.search();
        }
        
        $scope.resetFilterCategory();

        $scope.deleteCategory = function (category) {
            $http({
                url: 'http://localhost:49834/categories/delete/' + category.id,
                method: 'DELETE'
            })
                .then(function (response) {
                    $scope.categories.removeById(category.id);
                }),
                function (error) {
                    console.error(error);
                }
        }

        $scope.createCategory = function () {
            $http({
                url: 'http://localhost:49834/categories/create/' + newGuid(),
                method: 'POST',
                data: JSON.stringify({
                    name: $scope.newCategory.name,
                    active: $scope.newCategory.active
                })
            })
                .then(function (response) {
                    $scope.categories.push(response.data);
                },
                    function (error) {
                        console.error(error);
                    });
        }

        var updateCategoryItem = function (category, categoryCleanup) {
            $http({
                url: 'http://localhost:49834/categories/update/' + category.id,
                method: 'POST',
                data: JSON.stringify({
                    name: category.name,
                    active: category.active
                })
            })
                .then(function (response) {
                    var categoryUpdate = $scope.categories.getById(response.data.id);
                    if (categoryUpdate) {
                        categoryUpdate.name = response.data.name;
                        categoryUpdate.active = response.data.active;
                    }
                    categoryCleanup(category);
                },
                    function (error) {
                        console.error(error);
                    });
        }

        $scope.updateCategory = function () {
            updateCategoryItem($scope.editCategory, function () {
                $scope.editCategory = null;
            });
        }

        $scope.setCategoryForEdit = function (category) {
            $scope.editCategory = angular.copy(category);
        }

        

    });