const API_URL = "http://localhost:5054/swagger/v1/swagger.json/Spotfy";

async function listarMusicas() {
    const res = await fetch(API_URL);
    if (!res.ok) { alert("Erro ao listar músicas"); return; }
    const data = await res.json();
    const tbody = document.querySelector("#tabelaMusicas tbody");
    tbody.innerHTML = "";
    data.forEach(m => {
        tbody.innerHTML += `<tr>
            <td>${m.id}</td>
            <td>${m.musica}</td>
            <td>${m.artista}</td>
            <td>${m.album}</td>
            <td>${m.ano}</td>
        </tr>`;
    });
}

async function adicionarMusica() {
    const musica = document.getElementById("musica").value.trim();
    const artista = document.getElementById("artista").value.trim();
    const album = document.getElementById("album").value.trim();
    const ano = parseInt(document.getElementById("ano").value);

    if (!musica || !artista || !album || isNaN(ano)) { alert("Preencha todos os campos"); return; }

    const payload = { Musica: musica, Artista: artista, Album: album, Ano: ano };

    const res = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    if (res.ok) {
        alert("Música adicionada!");
        listarMusicas();
    } else {
        const err = await res.json();
        alert("Erro: " + JSON.stringify(err));
    }
}

document.addEventListener("DOMContentLoaded", listarMusicas);
