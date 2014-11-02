/// <reference path="jquery-2.1.1.min.js" />
/// <reference path="jquery-2.1.0-vsdoc.js" />
/// <reference path="jquery-ui-1.11.1.js" />
/// <reference path="jquery.datetimepicker.js" />

$('input[type=date]').datepicker({
    dateFormat: "yy-mm-dd"
});

$('input[type="datetime"]').datetimepicker({
    lang: 'pl',
    format: 'Y/m/d H:i:s'
});
