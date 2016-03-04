$(document).ready(function () {

    $('#myTableFromExcel').DataTable({
        dom: 'Blfrtip',
        buttons: [
           {
               extend: 'pdfHtml5',
               orientation: 'landscape',
               pageSize: 'LEGAL',
               message: 'Internation Marketing Group'
           }, 'excel', 'print'
        ],    
        "sScrollY": "600",
        "sScrollX": "100%",
        "sScrollXInner": "400%",
        "bScrollCollapse": true
    });
});

$(document).ready(function () {

    $('#myTableVisaApplcatnStsReport').DataTable({      
        dom: 'Blfrtip',
        buttons: [
            'excel', 'pdf', 'print'
        ]
    });
});

$(document).ready(function () {
    $('#myTableAgentSupport').DataTable(
    {      
        dom: 'Blfrtip',
        buttons: [
            'excel', 'pdf', 'print'
        ]
    });
});

$(document).ready(function () {
    $('#myAccountingVerification').DataTable(
    {
        dom: 'Blfrtip',
        buttons: [
            'excel', 'pdf', 'print'
        ]
    });
});

$(document).ready(function () {
    $('#example').DataTable({
        dom: 'Bfrtip',
        buttons: [
               {
                   extend: 'pdfHtml5',
                   orientation: 'landscape',
                   pageSize: 'LEGAL',
                   message: 'Internation Marketing Group'
               }
        ],
            "sScrollY": "600",
            "sScrollX": "100%",
            "sScrollXInner": "400%",
            "bScrollCollapse": true
        
    });
});