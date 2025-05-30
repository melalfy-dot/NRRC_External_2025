﻿angular.module('umbraco').controller('Our.Umbraco.MediaRemove.DashboardController',
    ['$scope', 'Our.Umbraco.MediaRemove.Resource', '$timeout', function ($scope, mediaRemoveResource, $timeout) {
        $scope.isLoading = true;
        $scope.RebuildStatus = {
            IsProcessing: true,
            ItemName: '',
            ItemsProcessed: 0
        };
        $scope.isBuiltRelations = false;
        $scope.unusedMedia = {
            IsProcessingMedia: false,
            Data: [],
            TotalCount: 0
        };
        $scope.DeleteStatus = {
            IsProcessingDeleting: false,
            ItemsProcessed: 0,
            ItemsToProcess: 0
        }
        $scope.filteredMedia = [];
        $scope.autoRefresh = true;
        $scope.exceptionListSource = null;
        $scope.exceptionSources = [];
        $scope.toBeDeletedAmount = 0;

        $scope.getRebuildStatus = function () {
            mediaRemoveResource.getRebuildStatus()
                .then(function (result) {
                    $scope.isLoading = false;
                    $scope.RebuildStatus = result.data;

                    if ($scope.RebuildStatus.IsProcessing && $scope.autoRefresh) {
                        $timeout(function () { $scope.getRebuildStatus() }, 5000, true);
                    }
                });
        };

        $scope.rebuild = function () {
            if ($scope.isBuiltRelations) {
                return;
            }
            mediaRemoveResource.rebuild(-1)
                .then(function (result) {
                    $scope.getRebuildStatus();
                    $scope.isBuiltRelations = true;
                });
            $timeout(function () { $scope.getRebuildStatus() }, 500, true);
        };

        $scope.getBuiltStatus = function () {
            mediaRemoveResource.getBuiltStatus().then(function ({ data }) {
                $scope.isBuiltRelations = data.IsBuilt;
            });
        }

        $scope.getUnusedMedia = function () {
            if ($scope.unusedMedia.IsProcessingMedia) {
                return;
            }
            $scope.unusedMedia.IsProcessingMedia = true;
            mediaRemoveResource.getUnusedMedia()
                .then(function () {
                    $timeout(function () {
                        $scope.getUnusedMediaStatus();
                    }, 1000);
                });
        };

        $scope.getUnusedMediaStatus = function () {
            mediaRemoveResource.getUnusedMediaStatus()
                .then(function ({ data }) {
                    let count = 0;
                    data.data.forEach((x) => {
                        x.ToRemove = true;
                        count++;
                        if (x.Source) {
                            try {
                                x.Source = JSON.parse(x.Source).src;
                            } catch (ex) {
                                if(console) console.log(ex);
                            }
                            if ($scope.exceptionSources.indexOf(x.Source) > -1) {
                                x.ToRemove = false;
                                count--;
                            }
                        }
                    });

                    $scope.unusedMedia.Data = data.data;
                    $scope.toBeDeletedAmount = count;
                    $scope.filteredMedia = data.data;
                    $scope.unusedMedia.IsProcessingMedia = false;
                    $scope.unusedMedia.TotalCount = data.totalCount;
                    if ($scope.unusedMedia.IsProcessingMedia) {
                        $timeout(function () { $scope.getUnusedMediaStatus() }, 5000, true);
                    }
                });
        };

        $scope.deleteUnusedMedia = function () {
            if ($scope.filteredMedia.length === 0 && $scope.unusedMedia.IsProcessingMedia) {
                return;
            }
            let forDeleting = [];
            $scope.filteredMedia.forEach((x) => {
                if (x.ToRemove) {
                    forDeleting.push(x);
                }
            });
            let ids = forDeleting.map(x => x.id);
            mediaRemoveResource.deleteUnusedMedia(ids)
                .then(function () {
                    $scope.getDeleteMediaStatus();
                    $scope.filteredMedia = $scope.filteredMedia.filter(m => m.ToRemove === false);
                    $scope.unusedMedia.Data = []; 
                    $scope.toBeDeletedAmount = 0; 
                });
        };

        $scope.getDeleteMediaStatus = function () {
            mediaRemoveResource.getDeleteMediaStatus()
                .then(function ({ data }) {
                    $scope.DeleteStatus = data;
                    if ($scope.DeleteStatus.IsProcessingDeleting) {
                        $timeout(function () { $scope.getDeleteMediaStatus() }, 5000, true);
                    }
                });
        };

        $scope.$watch('autoRefresh', function () {
            if ($scope.autoRefresh === true) {
                $scope.getRebuildStatus();
            }
        }, true);

        $scope.showContent = function (fileContent, fileName) {
            $scope.exceptionListSource = fileName;
            $scope.exceptionSources = fileContent.split(/\r\n|\n/);
            let count = 0;
            $scope.unusedMedia.Data.forEach((x) => {
                if ($scope.exceptionSources.indexOf(x.Source) > -1) {
                    x.ToRemove = false;
                } else {
                    x.ToRemove = true;
                    count++;
                }
            });
            $scope.filteredMedia = $scope.unusedMedia.Data;
            $scope.toBeDeletedAmount = count;
        }

        $scope.toRemoveChanged = function (index) {
            if ($scope.filteredMedia[index].ToRemove) {
                $scope.toBeDeletedAmount++;
            } else {
                $scope.toBeDeletedAmount--;
            }
        }

        $scope.getRebuildStatus();
        $scope.getUnusedMediaStatus();
        $scope.getBuiltStatus();
        $scope.getDeleteMediaStatus();
    }]);