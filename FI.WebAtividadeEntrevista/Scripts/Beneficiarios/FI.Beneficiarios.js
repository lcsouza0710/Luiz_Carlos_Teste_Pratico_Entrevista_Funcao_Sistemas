class beneficiario {
    constructor(name, cpf) {
        this.name = name;
        this.cpf = cpf;
    }
}

class beneficiarios {
    constructor() {
        this.beneficiarios = [];
    }
}

$(document).ready(function () {
    $(".cpf-mask").mask("000.000.000-00", { reverse: true });


});


$("#btnAddBenef").click()



