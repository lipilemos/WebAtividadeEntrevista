
$(document).ready(function () {
    $('#AdicionarBeneficiario').click(function (e) {
        $('#modalAdicionarBeneficiario #IDCliente').val(obj.Id);
        $("#modalAdicionarBeneficiario").modal();
    });
    $('.EditarBeneficiario').click(function (e) {        
        e.preventDefault();        
        $('#formBeneficiario #NomeBeneficiario').val(e.target.name);
        $('#formBeneficiario #CPFBeneficiario').val(e.target.title);
        $('#formBeneficiario #IDCliente').val(obj.Id);
        $('#formBeneficiario #IdBeneficia').val(e.target.id);
        return null
        //$.ajax({
        //    url: urlAlteracaoBeneficiario,
        //    method: "POST",
        //    data: {
        //        "NOME": $(this).find("#NomeBeneficiario").val(),
        //        "CPF": $(this).find("#CPFBeneficiario").val(),
        //        "IDCliente": $(this).find("#IDCliente").val()
        //    },
        //    error:
        //        function (r) {
        //            if (r.status == 400)
        //                ModalDialog("Ocorreu um erro", r.responseJSON);
        //            else if (r.status == 500)
        //                ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
        //        },
        //    success:
        //        function (r) {
        //            ModalDialog("Sucesso!", r)
        //            //$("#formBeneficiario")[0].reset();
        //        }
        //});
        
    });
    $('#formBeneficiario2').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPostBeneficiario,
            method: "POST",
            data: {
                "NOME": $(this).find("#NomeBeneficiario").val(),
                "CPF": $(this).find("#CPFBeneficiario").val(),
                "IDCliente": $(this).find("#IDCliente").val()
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formBeneficiario")[0].reset();
            }
        });
    })
    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
