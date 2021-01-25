var app = angular.module("app", [])
 .controller("userCtrl", function ($scope, $http) 
{
    $scope.users = [];

     $scope.resetFilterUser = function () {
         $scope.name = null;
         $scope.email = null;
         $scope.userType = null;
     }

    $scope.search = function() {
         $http({
                    url: 'http://localhost:49834/users/list',
                    method: 'GET',
                    params: { name: $scope.name,
                              email: $scope.email,
                              userType: $scope.userType,
                              skip:0,
                              take:1000}
            })
             .then(function(response) {
               $scope.users = response.data;
             },
               function(error) {
               console.error(error);
             });
        } 
    $scope.search();
     
     $scope.createUser = function () {
         $http({
             url: 'http://localhost:49834/users/create/' + newGuid(),
             method: 'POST',
             data: JSON.stringify({ name: $scope.newName.name,
                                    email: $scope.newEmail.email,
                                    type: $scope.newEmail.type})
         })
             .then(function (response) {
                 $scope.users.push(response.data);
             },
                 function (error) {
                 console.error(error);
             });
     }

     $scope.deleteUser = function (user) {
         $http({
             url: 'http://localhost:49834/users/delete/' + user.id,
             method: 'DELETE'
         })
             .then(function (response) {
                 $scope.users.removeById(user.id);
             }),
             function (error) {
                 console.error(error);
             }
     }

     var updateUserItem = function (user) {
         $http({
             url: 'http://localhost:49834/users/update/' + user.id,
             method: 'POST',
             data: JSON.stringify({ name: user.name,
                                    email: user.email,
                                    type: user.type
                 })
         })
             .then(function (response) {
                var userUP = $scope.users.getById(response.data.id);
                 if (userUP) {
                     userUP.name = response.data.name;
                     userUP.email = response.data.email;
                     userUP.type = response.data.type;
                 }
             },
                 function (error) {
                     console.error(error);
                 });
     }

     $scope.updateUser = function () {
         updateUserItem($scope.editUser, function () {
             $scope.editUser = null;
         });
     }

     $scope.setUserForEdit = function (user) {
         $scope.editUser = angular.copy(user);
     }

});