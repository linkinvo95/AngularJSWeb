var app = angular.module("app", [])
    .controller("orderCtrl", function ($scope, $http) {
        
        $scope.orders = [];
        $scope.users = [];
        $scope.products = [];
        $scope.items = [];

        $scope.orderBy = 'number';

        $scope.search = function () {
            $http({
                url: 'http://localhost:49834/orders/list',
                method: 'GET',
                params: {
                    userId: $scope.user,
                    number: $scope.number,
                    dateFrom: $scope.dateFrom ? $scope.dateFrom.toISOString() : null,
                    dateTo: $scope.dateTo ? $scope.dateTo.toISOString() : null,
                    skip: 0,
                    take: 1000
                }
            })
                .then(function (response) {
                    $scope.orders = response.data;
                },
                    function (error) {
                        console.error(error);
                    });
        }

        $scope.resetFilter = function () {
            $scope.user = null;
            $scope.number = null;
            $scope.dateFrom = null;
            $scope.dateTo = null;
            $scope.search();
        }
        $scope.resetFilter();

         $http({
                    url: 'http://localhost:49834/users/list',
                    method: 'GET',
                    params: { name: $scope.name,
                              skip:0,
                              take:1000}
            })
             .then(function(response) {
               $scope.users = response.data;
             },
               function(error) {
               console.error(error);
             });

        $http({
                url: 'http://localhost:49834/products/list',
                method: 'GET',
                params: {
                    name: $scope.name,
                    skip: 0,
                    take: 1000
                    }
        })
            .then(function (response) {
                $scope.products = response.data;
            },
                function (error) {
                    console.error(error);
               });


        $scope.deleteOrder = function (order) {
            $http({
                url: 'http://localhost:49834/orders/delete/' + order.id,
                method: 'DELETE',
            })
                .then(function (response) {
                    $scope.orders.removeById(order.id);
                }),
                function (error) {
                    console.error(error);
                }
        }

            //var model = {
            //        user: $scope.user,
            //        items: [
            //            {
            //                id: newGuid(),
            //                product: $scope.items.product,
            //                quantity: $scope.items.quantity
            //            }
            //            ]
            //   };


            //var items = [];
            //angular.forEach($scope.items, function (item) {
            //    items.push({
            //            id: item.id,
            //            product: item.product.id,
            //            quantity: item.quantity
            //        });
            //});
            // var model = {
            //        user: $scope.user,
            //        items: items
            //   };

            //console.log('model:', model);

           
        //}
    });