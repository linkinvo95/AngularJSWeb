var app = angular.module("app", [])
    .controller("productCtrl", function ($scope, $http) {
    
       $scope.products = [];

        $scope.search = function () {
            $http({
                url: 'http://localhost:49834/products/list',
                method: 'GET',
                params: {
                    categoryId: $scope.category,
                    name: $scope.name,
                    quantityFrom: $scope.quantityFrom,
                    quantityTo: $scope.quantityTo,
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
        }
        $scope.resetFilter = function () {
            $scope.category = null;
            $scope.name = null;
            $scope.quantityFrom = null;
            $scope.quantityTo = null;
            $scope.search();
        }
        $scope.resetFilter();

        $http({
                url: 'http://localhost:49834/categories/list',
                method: 'GET',
                params: { skip: 0, take: 1000 }
            })
                .then(function (response) {
                    $scope.categories = response.data;
                },
                    function (error) {
                        console.error(error);
                    });

        $scope.createProduct = function () {
            $http({
                url: 'http://localhost:49834/products/create/' + newGuid(),
                method: 'POST',
                data: JSON.stringify({
                    name: $scope.addProduct.name,
                    category: $scope.addProduct.category,
                    price: $scope.addProduct.price,
                    quantity: $scope.addProduct.quantity
                })
            })
                .then(function (response) {
                    console.log(response);
                    $scope.products.push(response.data);
                    
                },
                    function (error) {
                        console.error(error);
                    });
        }

        $scope.deleteProduct = function (product) {
            $http({
                url: 'http://localhost:49834/products/delete/' + product.id,
                method: 'DELETE'
            })
                .then(function (response) {
                    $scope.products.removeById(product.id);
                }),
                function (error) {
                    console.error(error);
                }
        }

        var updateProductItem = function (product) {
            $http({
                url: 'http://localhost:49834/products/update/' + product.id,
                method: 'POST',
                data: JSON.stringify({ 
                    category: product.category.id,
                    name: product.name,
                    price: product.price,
                    quantity: product.quantity })
            })
                .then(function (response) {
                    var productUpdate = $scope.products.getById(response.data.id);
                    if (productUpdate) {
                        productUpdate.category = response.data.category;
                        productUpdate.name = response.data.name;
                        productUpdate.price = response.data.price;
                        productUpdate.quantity = response.data.quantity;
                    }
                },
                    function (error) {
                        console.error(error);
                    });
        }

        $scope.updateProduct = function () {
            updateProductItem($scope.editProduct, function () {
                $scope.editProduct = null;
            });
        }

        $scope.setProductForEdit = function (product) {
            $scope.editProduct = angular.copy(product);
        }

    });