var app = angular.module("app", [])
.controller("orderCreateCtrl", function ($scope, $http) {

    var orderId = null;// "ORDER ID";

    $scope.isNewOrder = !orderId;

    $scope.products = [];
    $scope.order = {
        id: newGuid(),
        items: []
    };
            
    if ($scope.isNewOrder) {
        $scope.users = [];
        $http({
                url: 'http://localhost:49834/users/list',
                method: 'GET',
                params: {                   
                    skip: 0,
                    take: 1000
                }
            })
            .then(function (response) {
                $scope.users = response.data;
            },
                function (error) {
                    console.error(error);
                });

        $scope.userChanged = function () {     
            $scope.order.user = $scope.userId 
                ? $scope.users.getById($scope.userId)
                : null;
        }

    } else {
        //TODO: get order from WebAPI and set to $scope.order = response.data
    }

        $http({
                url: 'http://localhost:49834/products/list',
                method: 'GET',
                params: {                   
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

    $scope.addItem = function () {
        if (!$scope.productId) {
            return null;
        }
        var product = $scope.products.getById($scope.productId);
        $scope.order.items.push({
            id: newGuid(),
            product: product,
            price: product.price,
            quantity: 1
        });
        $scope.products.removeById($scope.productId);
        $scope.productId = null;
      }

     $scope.removeItem = function (item) {
         $scope.products.push(item.product);
        $scope.order.items.removeById(item.id);             
    }

    $scope.getItemTotal = function (item) {
        return item.quantity * item.price;
    }

    $scope.getOrderTotal = function () {
        var total = 0;
        //for (var i = 0; i < $scope.order.items.length; i++) {
        //    total += $scope.getItemTotal($scope.order.items[i]);
        //}
        angular.forEach($scope.order.items, function (item) {
            total += $scope.getItemTotal(item);
        })
        return total;
    }
          
    $scope.saveChanges = function () {

        var urlElement = $scope.isNewOrder ? 'create': 'update';
        $http({
                url: 'http://localhost:49834/orders/'+urlElement+'/' + $scope.order.id,
                method: 'POST',
                data: JSON.stringify({
                    user: $scope.order.user.id,
                    items: [
                        {
                            id: newGuid(),
                            product: $scope.product
                        }]
                })
            })
                .then(function (response) {
                    console.log(response);
                 
                },
                    function (error) {
                        console.error(error);
                    });
    } 
});