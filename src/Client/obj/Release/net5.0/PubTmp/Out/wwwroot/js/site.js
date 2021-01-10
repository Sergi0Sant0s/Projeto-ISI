function VerifyGames(id) {
    $.post("/Events/VerifyGames",
        {
            idEvent: id
        },
        function (data, status) {
            if (data != null) {
                if (data == true)
                    window.location.href = "/Games/Index/" + id;
                else
                    alert("Não existem equipas suficientes na competição.");
            }
        });
}

function VerifyTeams(id) {
    $.post("/Events/VerifyTeams",
        {
            idEvent: id
        },
        function (data, status) {
            if (data != null) {
                if (data == true)
                    window.location.href = "/Events/Teams/"+id;
                else
                    alert("Não existem equipas para adicionar a competição.");
            }
        });
}

