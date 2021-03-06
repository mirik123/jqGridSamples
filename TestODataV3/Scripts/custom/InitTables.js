﻿/*jslint continue: true, nomen: true, plusplus: true, unparam: true, todo: true, vars: true, white: true */
/*global jQuery */

$(document).ready(function () {
    function selectFormatter(cellvalue, options, rowObject) {
        if (!cellvalue) {
            return '<span class="cellWithoutBackground" data-id="-1" style="background-color:transparent"></span>';
        }

        if ($('#' + options.gid).getGridParam('datatype') === 'xml') {
            var xmlvalue = $(options.colModel.name, rowObject);
            cellvalue = $.jgrid.odataHelper.convertXmlToJson(xmlvalue[0]);
        }

        if (!$.isPlainObject(cellvalue)) {
            //var selRowId = $("#grid").jqGrid('getGridParam', 'selrow');
            var cell = $('tr#' + options.rowId + ' select[name=' + options.colModel.name + ']', '#grid');

            if (!rowObject.id) {
                cell = cell.find('option[value=' + cellvalue + ']');
            }
            else {
                cell = cell.find('option:selected');
            }

            cellvalue = { Id: cell.val(), color: cell.data('bkcolor'), descr: cell.text() };
        }

        return '<span class="cellWithoutBackground" data-id="' + cellvalue.Id + '" style="background-color:' + cellvalue.color + '">' + cellvalue.descr + '</span>';
    }

    function loadError(jqXHR, textStatus, errorThrown) {
        var errstring = "<div>HTTP status code: " + jqXHR.status + "</div>";
        var title = textStatus;
        var message = errorThrown;

        if (!jqXHR.responseJSON) {
            try {
                jqXHR.responseJSON = $.parseJSON(jqXHR.responseText);
            }
            catch (ignore) { }
        }
        if (jqXHR.responseJSON) {
            var odataerr = jqXHR.responseJSON["@odata.error"] || jqXHR.responseJSON["odata.error"] || jqXHR.responseJSON["error"];
            if (odataerr) {
                if (odataerr.innererror) {
                    if (odataerr.innererror.internalexception) {
                        title = odataerr.innererror.internalexception.message;
                        message = odataerr.innererror.internalexception.stacktrace || '';
                    }
                    else {
                        title = odataerr.innererror.message;
                        message = odataerr.innererror.stacktrace || '';
                    }
                }
                else {
                    title = odataerr.message.value || odataerr.message;
                    message = odataerr.stacktrace || '';
                }
            }
        }

        errstring += "<div>Message: " + title + '</div><div>' + message + '</div>';

        return errstring;
    }

    function initWebApiTable() {
        var colModelDefinition = [
            {
                label: 'Client Id', name: 'id', index: 'id', editable: false,
                searchrules: { integer: true }, sorttype: 'number',
                formatter: function (cellvalue, options, rowObject) { return '<a href="#" target="_self" data-id="' + cellvalue + '">' + cellvalue + '</a>'; },
                unformat: function (cellvalue, options, cell) { return $('a', cell).data('id'); }
            },
            { label: 'Last Name', name: 'lastname', index: 'lastname', editable: true },
            { label: 'First Name', name: 'firstname', index: 'firstname', editable: true },
            {
                label: 'Client Status', name: 'status', index: 'status', editable: true,
                formatter: selectFormatter,
                unformat: function (cellvalue, options, cell) { return $('span', cell).data('id'); },
                editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus' },
                searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus&empty=true' },
                searchrules: { integer: true }, edittype: 'select', stype: 'select'
            },
            {
                label: 'Client Type', name: 'cltype', index: 'cltype', editable: true,
                formatter: selectFormatter,
                unformat: function (cellvalue, options, cell) { return $('span', cell).data('id'); },
                editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType' },
                searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType&empty=true' },
                searchrules: { integer: true }, edittype: 'select', stype: 'select'
            },
            { label: 'Cellphone', name: 'phone_cell', index: 'phone_cell', editable: true },
            { label: 'SSN', name: 'ssn', index: 'ssn', editable: true },
            { label: 'City', name: 'addr_city', index: 'addr_city', editable: true },
            { label: 'Street', name: 'addr_street', index: 'addr_street', editable: true },
            { label: 'House', name: 'addr_home', index: 'addr_home', editable: true }            
        ];

        $("#grid").jqGrid({
            height: '100%',
            width: '100%',
            pager: $('#gridpager'),
            sortname: 'id',
            viewrecords: true,
            sortorder: "asc",
            deepempty: true,
            altRows: true,
            footerrow: false,
            shrinkToFit: true,
            ignoreCase: true,
            gridview: true,
            headertitles: true,
            sortable: true,
            autowidth: true,
            toppager: true,
            rowNum: 25,
            toolbar: [true, 'top'],
            datatype: 'json',
            contentType: "application/json;charset=utf-8",
            mtype: 'GET',
            url: 'http://localhost:59661/api/ApiServices/Get1',
            ondblClickRow: function (id) {
                $(this).jqGrid('editRow', id, {
                    beforeEditRow: function (options, rowid) {
                        return true;
                    }
                });
                $("#grid_ilsave").removeClass('ui-state-disabled');
            },
            multiSort: true,
            iconSet: "jQueryUI",
            colModel: colModelDefinition,
            loadError: function (jqXHR, textStatus, errorThrown) {
                var errstring = loadError(jqXHR, textStatus, errorThrown);
                $.jgrid.info_dialog.call(this, $(this).jqGrid("getGridRes", "errors.errcap"), errstring, $(this).jqGrid("getGridRes", "edit.bClose"));
            },
            jsonReader: {
                root: function (data) {
                    var rows = data.rows.$values || data.rows;
                    rows = $.jgrid.odataHelper.resolveJsonReferences(rows);
                    return rows;
                },
                repeatitems: false
            }
        })
        .jqGrid("navGrid", "#pg_grid_toppager", { add: true, del: true, edit: true, view: true, reload: true, search: false, cloneToTop: true },
            {
                closeAfterEdit: true
            },
            {
                closeAfterAdd: true
            })
        .jqGrid('inlineNav', "#pg_grid_toppager", {
            add: true, edit: false, save: true, cancel: false,
            editParams: {
                keys: true
            }
        })
        .jqGrid('filterToolbar', { searchOnEnter: false, enableClear: false, stringResult: true })
        .jqGrid('searchGrid', { multipleSearch: true, multipleGroup: false, overlay: 0 });
    }

    function initODataTable() {
        var colModelDefinition = [
            {
                label: 'Client Id', name: 'id', index: 'id', editable: false, searchrules: { integer: true },
                formatter: function (cellvalue, options, rowObject) { return '<a href="#" target="_self" data-id="' + cellvalue + '">' + cellvalue + '</a>'; },
                unformat: function (cellvalue, options, cell) { return $('a', cell).data('id'); }
            },         
            {
                label: 'Client Type', name: 'cltype', index: 'cltype', editable: true, //xmlmap: 'cltype > Id',
                formatter: selectFormatter,
                unformat: function (cellvalue, options, cell) { return $('span', cell).data('id'); },
                editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType' },
                searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType&empty=true' },
                searchrules: { integer: true }, edittype: 'select', stype: 'select',
                odataunformat: function (searchField, searchString, searchOper) {
                    if (searchString !== '-1') { return 'cltype/Id'; }
                }
            },
            {
                label: 'Client Status', name: 'status', index: 'status', editable: true, //xmlmap: 'status > Id',
                formatter: selectFormatter,
                unformat: function (cellvalue, options, cell) { return $('span', cell).data('id'); },
                editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus' },
                searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus&empty=true' },
                searchrules: { integer: true }, edittype: 'select', stype: 'select',
                odataunformat: function (searchField, searchString, searchOper) { if (searchString !== '-1') { return 'status/Id'; } }
            }
        ];

        $("#grid").jqGrid({
            height: '100%',
            width: '100%',
            pager: $('#gridpager'),
            sortname: 'id',
            viewrecords: true,
            sortorder: "asc",
            deepempty: true,
            altRows: true,
            footerrow: false,
            shrinkToFit: true,
            ignoreCase: true,
            gridview: true,
            headertitles: true,
            sortable: true,
            autowidth: true,
            toppager: true,
            rowNum: 25,
            toolbar: [true, 'top'],
            url: '',
            ondblClickRow: function (id) {
                $(this).jqGrid('editRow', id, {
                    beforeEditRow: function (options, rowid) {
                        return true;
                    }
                });
                $("#grid_ilsave").removeClass('ui-state-disabled');
            },
            multiSort: true,
            iconSet: "jQueryUI",
            colModel: colModelDefinition,
            loadError: function (jqXHR, textStatus, errorThrown) {
                var errstring = loadError(jqXHR, textStatus, errorThrown);
                $.jgrid.info_dialog.call(this, $(this).jqGrid("getGridRes", "errors.errcap"), errstring, $(this).jqGrid("getGridRes", "edit.bClose"));
            },
            beforeInitGrid: function () {
                $(this).jqGrid('odataInit', {
                    annotations: false,
                    datatype: 'xml',
                    version: 3,
                    gencolumns: true,
                    expandable: 'json',
                    entityType: 'ClientModel',
                    useXmlSerializer: false,
                    odataurl: "http://localhost:59661/odata/ODClient",
                    metadataurl: 'http://localhost:59661/odata/$metadata',
                    errorfunc: function (jqXHR, textStatus, errorThrown) {
                        jqXHR = jqXHR.xhr || jqXHR;
                        var $this = $("#grid");
                        var errstring = loadError(jqXHR, textStatus, errorThrown);
                        $.jgrid.info_dialog.call($this[0], $this.jqGrid("getGridRes", "errors.errcap"), errstring, $this.jqGrid("getGridRes", "edit.bClose"));
                    }
                });
            }
        })
        .jqGrid("navGrid", "#pg_grid_toppager", { add: true, del: true, edit: true, view: true, reload: true, search: false, cloneToTop: true },
            {
                closeAfterEdit: true
            },
            {
                closeAfterAdd: true
            })
        .jqGrid('inlineNav', "#pg_grid_toppager", {
            add: true, edit: false, save: true, cancel: false,
            editParams: {
                keys: true
            }
        })
        .jqGrid('filterToolbar', { searchOnEnter: false, enableClear: false, stringResult: true })
        .jqGrid('searchGrid', { multipleSearch: true, multipleGroup: false, overlay: 0 });
    }

    initODataTable();
    //initWebApiTable();
});
