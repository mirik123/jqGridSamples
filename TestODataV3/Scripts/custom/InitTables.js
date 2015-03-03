
function InitWebApiTable() {
    var colModelDefinition = [
                    {
                        label: 'מספר לקוח', name: 'id', index: 'id', editable: false, searchrules: { integer: true }, sorttype: 'number',
                        formatter: function (cellvalue, options, rowObject) { return LinkFormatter({ href: rowObject.id, text: rowObject.id }, options, rowObject); },
                        unformat: LinkUnformatter,
                        formatoptions: { target: '/Clients/GetEditForm?id=' }
                    },
                    { label: 'שם משפחה', name: 'lastname', index: 'lastname', editable: true },
                    { label: 'שם פרטי', name: 'firstname', index: 'firstname', editable: true },
                    {
                        label: 'סטטוס', name: 'status', index: 'status', editable: true, formatter: SelectFormatter, unformat: SelectUnformatter, edittype: 'select', stype: 'select',
                        editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus' },
                        searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus&empty=true' },
                        searchrules: { integer: true }
                    },
                    {
                        label: 'סוג לקוח', name: 'cltype', index: 'cltype', editable: true, formatter: SelectFormatter, unformat: SelectUnformatter, edittype: 'select', stype: 'select',
                        editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType' },
                        searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType&empty=true' },
                        searchrules: { integer: true }
                    },
                    { label: 'ת"ז\\ח.פ', name: 'ssn', index: 'ssn', editable: true },
                    { label: 'כתובת ישוב', name: 'addr_city', index: 'addr_city', editable: true },
                    { label: 'כתובת רחוב', name: 'addr_street', index: 'addr_street', editable: true },
                    { label: 'מספר בית', name: 'addr_home', index: 'addr_home', editable: true },
                    { label: 'טל. נייד', name: 'phone_cell', index: 'phone_cell', editable: true },
                    {
                        label: 'קישור לעסקאות', name: 'request_exists', index: 'request_exists', editable: false, search: false,
                        formatter: function (cellvalue, options, rowObject) { return LinkFormatter({ href: cellvalue ? rowObject.id : null }, options, rowObject); },
                        formatoptions: { target: '/Requests?strict=true&clid=', text: 'עסקאות' }
                    },
                    {
                        label: 'קישור לצ\'קים', name: 'check_exists', index: 'check_exists', editable: false, search: false,
                        formatter: function (cellvalue, options, rowObject) { return LinkFormatter({ href: cellvalue ? rowObject.id : null }, options, rowObject); },
                        formatoptions: { target: '/BankChecks?strict=true&clid=', text: 'צ\'קים' }
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
        direction: 'rtl',
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
            $('#grid').jqGrid('editRow', id, {
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
            $.jgrid.info_dialog.call(this, $.jgrid.errors.errcap, errstring, $.jgrid.edit.bClose);
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

function InitODataTable() {
    var colModelDefinition = [
                {
                    label: 'Client Id', name: 'id', index: 'id', editable: false, searchrules: { integer: true }, sorttype: 'number',
                    formatter: function (cellvalue, options, rowObject) { return LinkFormatter({ href: rowObject.id, text: rowObject.id }, options, rowObject); },
                    unformat: LinkUnformatter,
                    formatoptions: { target: '/Clients/GetEditForm?id=' }
                },
                { label: 'Last Name', name: 'lastname', index: 'lastname', editable: true },
                { label: 'First Name', name: 'firstname', index: 'firstname', editable: true },
                {
                    label: 'Client Status', name: 'status', index: 'status', editable: true, formatter: SelectFormatter, unformat: SelectUnformatter, edittype: 'select', stype: 'select',
                    editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus' },
                    searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientStatus&empty=true' },
                    searchrules: { integer: true },
                    odata: true,
                    odataunformat: function (searchField, searchString, searchOper) { if (searchString !== '-1') { return 'status/Id'; } }
                },
                {
                    label: 'Client Type', name: 'cltype', index: 'cltype', editable: true, formatter: SelectFormatter, unformat: SelectUnformatter, edittype: 'select', stype: 'select',
                    editoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType' },
                    searchoptions: { dataUrl: '/api/ApiServices/GetSelectData?table=ClientType&empty=true' },
                    searchrules: { integer: true },
                    odata: true,
                    odataunformat: function (searchField, searchString, searchOper) { if (searchString !== '-1') { return 'cltype/Id'; } }
                },
                { label: 'SSN', name: 'ssn', index: 'ssn', editable: true },
                { label: 'City', name: 'addr_city', index: 'addr_city', editable: true },
                { label: 'Street', name: 'addr_street', index: 'addr_street', editable: true },
                { label: 'House', name: 'addr_home', index: 'addr_home', editable: true },
                { label: 'Cellphone', name: 'phone_cell', index: 'phone_cell', editable: true }
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
            $('#grid').jqGrid('editRow', id, {
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
            $.jgrid.info_dialog.call(this, $.jgrid.errors.errcap, errstring, $.jgrid.edit.bClose);
        },
        beforeInitGrid: function () {
            $(this).jqGrid('odataInit', {
                version: 3,
                gencolumns: false,
                odataurl: "http://localhost:59661/odata/ODClient",
                metadataurl: 'http://localhost:59661/odata/$metadata'
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

function loadError(jqXHR, textStatus, errorThrown) {
    var errstring = "<div>HTTP status code: " + jqXHR.status + "</div>";
    var title = textStatus;
    var message = errorThrown;

    if (!jqXHR.responseJSON) {
        try {
            jqXHR.responseJSON = $.parseJSON(jqXHR.responseText);
        }
        catch (ex) { }
    }
    if (jqXHR.responseJSON) {
        var odataerr = jqXHR.responseJSON["@odata.error"] || jqXHR.responseJSON["odata.error"] || jqXHR.responseJSON["error"];
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

    errstring += "<div>Message: " + title + '</div><div>' + message + '</div>';

    return errstring;
}

function SelectFormatter(cellvalue, options, rowObject) {
    if (cellvalue == null) {
        cellvalue = { Id: "-1", color: "transparent", descr: "" };
    }
    else if (!$.isPlainObject(cellvalue)) {
        //var selRowId = $("#grid").jqGrid('getGridParam', 'selrow');
        var cell = $('tr#' + options.rowId + ' select[name=' + options.colModel.name + ']', '#grid');

        if (rowObject.id == undefined)
            cell = cell.find('option[value=' + cellvalue + ']');
        else
            cell = cell.find('option:selected');

        cellvalue = { Id: cell.val(), color: cell.data('bkcolor'), descr: cell.text() };
    }

    return '<span class="cellWithoutBackground" data-id="' + cellvalue.Id + '" style="background-color:' + cellvalue.color + '">' + cellvalue.descr + '</span>';
}

function SelectUnformatter(cellvalue, options, cell) {
    return $('span', cell).data('id');
}

function LinkFormatter(cellvalue, options, rowObject) {
    var target = (options.colModel.formatoptions.target != undefined ? options.colModel.formatoptions.target : '') + cellvalue.href;
    var text = (options.colModel.formatoptions.text != undefined ? options.colModel.formatoptions.text : '') + (cellvalue.text != undefined ? cellvalue.text : '');

    if (cellvalue.href == '' || cellvalue.href == undefined || cellvalue.href == null)
        return '';
    else
        return '<a href="' + target + '" target="_self" data-id="' + cellvalue.href + '">' + text + '</a>';
}

function LinkUnformatter(cellvalue, options, cell) {
    return $('a', cell).data('id');
}
