Array.prototype.getIndexByProperty = function(propertyName, value) {
    for (var i = 0; i < this.length; i++) {
        if (this[i][propertyName] != value) {
            continue;
        }
        return i;
    }
    return -1;
};

Array.prototype.getIndexById = function(id) {
    return this.getIndexByProperty('id', id);
};

Array.prototype.getById = function(id) {
    var index = this.getIndexById(id);
    return index != -1 ? this[index] : null;
};

Array.prototype.getByProperty = function(propertyName, value) {
    var index = this.getIndexByProperty(propertyName, value);
    return index != -1 ? this[index] : null;
};

Array.prototype.getAllByProperty = function(propertyName, value) {
    var result = [];
    for (var i = 0; i < this.length; i++) {
        if (this[i][propertyName] != value) {
            continue;
        }
        result.push(this[i]);
    }
    return result;
};

Array.prototype.getAllByMultipleProperties = function(propertyNamesAndValues) {
    var result = this;
    for (var i = 0; i < propertyNamesAndValues.length - 1; i++) {
        result = result.getAllByProperty(propertyNamesAndValues[i].property, propertyNamesAndValues[i].value);
    }
    return result.getByProperty(propertyNamesAndValues[propertyNamesAndValues.length - 1].property,
        propertyNamesAndValues[propertyNamesAndValues.length - 1].value);
};

function getChangesInCollectionsById(target, source) {
    var added = angular.copy(target);
    var removed = angular.copy(source);

    for (var i = 0; i < added.length; i++) {
        for (var j = 0; j < removed.length; j++) {
            if (added[i].id != removed[j].id) {
                continue;
            }
            added.splice(i, 1);
            removed.splice(j, 1);
            i--;
            break;
        }
    }
    return { added: added, removed: removed };
}

Array.prototype.getCollectionOfProperty = function(propertyName, onlyUnique) {
    var collection = [];
    angular.forEach(this, function (item) {
        var value = item[propertyName];
        if (onlyUnique && collection.indexOf(value)>-1) {
            return;
        } 
        collection.push(value);
    });
    return collection;
};

Array.prototype.getIds = function() {
    return this.getCollectionOfProperty('id');
};

Array.prototype.groupBy = function(propertyName, format) {
    var groupedResult = [];
    if (this.isEmpty()) {
        return groupedResult;
    }
    var propertyNames = propertyName.split('.');
    var firstPropertyName = propertyNames[0];
    var i;
    // moment doesn't supported for complex objects
    if (propertyNames.length === 1 && moment.isMoment(this[0][firstPropertyName])) {
        format = angular.isDefined(format) ? format : 'L';
        var collection = angular.copy(this);
        for (i = 0; i < collection.length; i++) {
            var item = collection[i][firstPropertyName];
            collection[i].dateGroup = {
                key: parseInt(item.format('YYYYMMDD')),
                value: item.format(format)
            }
        }
        return collection.groupBy('dateGroup.key');
    }
    for (i = 0; i < this.length; i++) {
        var data = angular.copy(this[i][firstPropertyName]);
        var index = -1;
        var j;
        if (propertyNames.length === 1) {
            index = groupedResult.getIndexByProperty('key', data);
        } else {
            for (j = 0; j < groupedResult.length; j++) {
                if (groupedResult[j].key[propertyNames[1]] == data[propertyNames[1]]) {
                    index = j;
                    break;
                }
            }
        }
        if (index === -1) {
            groupedResult.push({ key: data, value: [] });
            index = groupedResult.length - 1;
        }
        groupedResult[index].value.push(angular.copy(this[i]));
    }
    return groupedResult;
};

Array.prototype.isEmpty = function() {
    return this.length == 0;
};

Array.prototype.getIndex = function(value) {
    for (var i = 0; i < this.length; i++) {
        if (!angular.isObject(value)) {
            if (this[i] == value) {
                return i;
            }
            continue;
        }
        if (moment.isMoment(value)) {
            if (this[i].isSame(value)) {
                return i;
            }
            continue;
        }
    }
    return -1;
};

Array.prototype.hasValue = function(value) {
    return this.getIndex(value) > -1;
};

Array.prototype.hasValueById = function(id) {
    return this.getIndexById(id) > -1;
};

Array.prototype.removeByIndex = function(index) {
    this.splice(index, 1);
};

Array.prototype.removeValue = function(value) {
    var index = this.getIndex(value);
    if (index != -1) {
        this.removeByIndex(index);
        return true;
    }
    return false;
};

Array.prototype.removeById = function(id) {
    var index = this.getIndexById(id);
    if (index != -1) {
        this.removeByIndex(index);
        return true;
    }
    return false;
};

Array.prototype.removeByProperty = function(propertyName, value) {
    var index = this.getIndexByProperty(propertyName, value);
    if (index != -1) {
        this.splice(index, 1);
        return true;
    }
    return false;
};

Array.prototype.addProperty = function(propertyName, propertyValue) {
    if (this.length > 0 && angular.isDefined(this[0][propertyName])) {
        console.error('[Array.addProperty] Exception: Collection has already defined property "', propertyName, '".');
        return;
    }
    this.setProperty(propertyName, propertyValue);
};

Array.prototype.setProperty = function(propertyName, propertyValue) {
    var length = this.length;
    if (length == 0) {
        return;
    }
    for (var i = 0; i < length; i++) {
        this[i][propertyName] = propertyValue;
    }
};

Array.prototype.copyProperty = function(propertyName, newPropertyName) {
    var length = this.length;
    if (length == 0) {
        return;
    }
    if (angular.isDefined(this[0][newPropertyName])) {
        console.log('[Array.addProperty] Exception: Collection has already defined property "', newPropertyName, '".');
        return;
    }
    for (var i = 0; i < length; i++) {
        this[i][newPropertyName] = this[i][propertyName];
    }
};

Array.prototype.insert = function(index, value) {
    this.splice(index, 0, value);
};