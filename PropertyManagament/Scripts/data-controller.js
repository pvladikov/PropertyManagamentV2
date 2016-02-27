angular.module('homeApplication') // extending angular module from first part
.controller('data-controller', ['$scope', 'FileUploadService','sharedProperties', function ($scope, FileUploadService, sharedProperties) {
    // Variables
    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = null;
    $scope.FileDescription = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false; 

    $scope.$watch("f1.$valid", function (isValid) {
        $scope.IsFormValid = isValid;
    }); 
  
    $scope.ChechFileValid = function (file) {
        var isValid = false;
        if ($scope.SelectedFileForUpload != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif') && file.size <= (512 * 1024)) {
                $scope.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                $scope.FileInvalidMessage = "Selected file is Invalid. (only file type png, jpeg and gif and 512 kb size allowed)";
            }
        }
        else {
            $scope.FileInvalidMessage = "Image required!";
        }
        $scope.IsFileValid = isValid;
    };
    
 
    $scope.selectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }

    $scope.SaveFile = function (is_property) {
       
        $scope.IsFormSubmitted = true;
        $scope.Message = "";
        $scope.ChechFileValid($scope.SelectedFileForUpload);
        if ($scope.IsFormValid && $scope.IsFileValid) {
            FileUploadService.UploadFile($scope.SelectedFileForUpload, $scope.FileDescription).then(function (d) {
                alert(d.Message);
                if (is_property) {
                    $scope.property = sharedProperties.getProperty();
                    $scope.property.pictureUrl = d.URL;
                }
                else {
                    $scope.owner = sharedProperties.getOwner();
                    $scope.owner.pictureUrl = d.URL;
                }
                ClearForm();
            }, function (e) {
                alert(e);
            });
        }
        else {
            $scope.Message = "Please, select image.";
        }
    };
   
    function ClearForm() {
        $scope.FileDescription = "";

        angular.forEach(angular.element("input[type='file']"), function (inputElem) {
            angular.element(inputElem).val(null);
        });
 
        $scope.f1.$setPristine();
        $scope.IsFormSubmitted = false;
    }
 
}])

.factory('FileUploadService', function ($http, $q) { 
    var fac = {};
    fac.UploadFile = function (file, description) {
        var formData = new FormData();
        formData.append("file", file);       
 
        var defer = $q.defer();
        $http.post("/Data/SaveFiles", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
        .success(function (d) {
            defer.resolve(d);
        })
        .error(function () {
            defer.reject("File Upload Failed!");
        });
 
        return defer.promise;
 
    }
    return fac;
 
});