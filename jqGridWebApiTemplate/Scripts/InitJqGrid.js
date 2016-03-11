/*jslint continue: true, nomen: true, plusplus: true, unparam: true, todo: true, vars: true, white: true */
/*global jQuery */

$(document).ready(function () {
    var colModelDefinition = [
            /*{
                label: 'Id', name: 'id', index: 'id', editable: false,
                searchrules: { integer: true }, sorttype: 'number',
                formatter: function (cellvalue, options, rowObject) { return '<a href="#" target="_self" data-id="' + cellvalue + '">' + cellvalue + '</a>'; },
                unformat: function (cellvalue, options, cell) { return $('a', cell).data('id'); }
            },*/
           { label: 'Name', name: 'name', index: 'name', editable: true }
    ];

    $("#grid").jqGrid({
        height: 'auto',
        width: '800px',
        pager: $('#gridpager'),
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
        toppager: true,
        rowNum: 25,
        toolbar: [true, 'top'],
        datatype: 'json',
        contentType: "application/json;charset=utf-8",
        mtype: 'GET',
        url: 'http://localhost:64056/ApiServices/Get',
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
            var parsedError = $.jgrid.odataHelper.loadError(jqXHR, textStatus, errorThrown);
            $('#errdialog').html(parsedError).dialog('open');
        },
        jsonReader: {
            root: function (data) {
                var rows = data.rows.$values || data.rows;
                rows = $.jgrid.odataHelper.resolveJsonReferences(rows);
                return rows;
            },
            repeatitems: false
        },
        gridComplete: function () {
            $('#new_fbox_grid').css('width', $('#gview_grid').css('width'));
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

    $("#searchmodfbox_grid").hide();
    $('.ui-search-clear').hide();
    $('#fbox_grid_reset').appendTo('#new_fbox_grid td:eq(0)');
    $('#fbox_grid_reset .ui-icon').css('display', 'inline-block');
    $('<br/>').appendTo('#new_fbox_grid td:eq(0)');
    $('#fbox_grid_search').appendTo('#new_fbox_grid td:eq(0)');
    $('#fbox_grid_search .ui-icon').css('display', 'inline-block');
    $('#fbox_grid').appendTo('#new_fbox_grid td:eq(1)');
    $('#gridpager').hide();
    $('#t_grid, .ui-jqgrid-titlebar').hide();
    $('.ui-jqgrid-hbox-rtl').css('padding-left', '0px');
    $('#fbox_grid td.columns select').val('name').trigger('change');
});
