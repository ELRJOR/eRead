const apiBase = 'https://localhost:7223/api';

let librosGlobal = [];
let autoresGlobal = [];
let libroIdEditando = null;

async function cargarAutores() {
    const res = await axios.get(`${apiBase}/Autor`);
    autoresGlobal = res.data;

    const selectAgregar = document.getElementById('autorLibro');
    const selectFiltro = document.getElementById('filtroAutor');
    const selectEditar = document.getElementById('autorEditar');

    selectAgregar.innerHTML = '<option value="">Selecciona un autor</option>';
    selectFiltro.innerHTML = '<option value="">Filtrar por autor</option>';
    selectEditar.innerHTML = '<option value="">Selecciona un autor</option>';

    autoresGlobal.forEach(a => {
        selectAgregar.innerHTML += `<option value="${a.id_Autor}">${a.nombre}</option>`;
        selectFiltro.innerHTML += `<option value="${a.id_Autor}">${a.nombre}</option>`;
        selectEditar.innerHTML += `<option value="${a.id_Autor}">${a.nombre}</option>`;
    });
}

async function cargarLibros() {
    const res = await axios.get(`${apiBase}/Libro`);
    librosGlobal = res.data;
    mostrarLibros();
}

function mostrarLibros() {
    const tabla = document.querySelector('#tablaLibros tbody');
    tabla.innerHTML = '';

    const categoriaFiltro = document.getElementById('filtroCategoria').value.toLowerCase();
    const autorFiltro = document.getElementById('filtroAutor').value;

    librosGlobal
        .filter(l =>
            (categoriaFiltro === "" || l.categoria.toLowerCase().includes(categoriaFiltro)) &&
            (autorFiltro === "" || l.autor.id_Autor == autorFiltro)
        )
        .forEach(l => {
            tabla.innerHTML += `<tr>
                <td>${l.id_Libro}</td>
                <td>${l.titulo}</td>
                <td>${l.categoria}</td>
                <td>${l.anio}</td>
                <td>${l.autor.nombre}</td>
                <td>
                    <button class="btn btn-sm btn-warning me-2" onclick="abrirModalEditar(${l.id_Libro})">Editar</button>
                    <button class="btn btn-sm btn-danger" onclick="eliminarLibro(${l.id_Libro})">Eliminar</button>
                </td>
            </tr>`;
        });
}

document.getElementById('formAutor').addEventListener('submit', async e => {
    e.preventDefault();
    const nombre = document.getElementById('nombreAutor').value.trim();
    if (!nombre) return alert("Ingresa un nombre válido");

    await axios.post(`${apiBase}/Autor`, { nombre });
    document.getElementById('nombreAutor').value = '';
    await cargarAutores();
});

document.getElementById('formLibro').addEventListener('submit', async e => {
    e.preventDefault();

    const titulo = document.getElementById('tituloLibro').value.trim();
    const categoria = document.getElementById('categoriaLibro').value.trim();
    const anioStr = document.getElementById('anioLibro').value.trim();
    const autorId = parseInt(document.getElementById('autorLibro').value);

    if (!titulo) return alert("Ingresa un título válido");
    if (!categoria) return alert("Ingresa una categoría válida");
    if (!anioStr || isNaN(anioStr)) return alert("Ingresa un año válido");
    if (parseInt(anioStr) <= 0) return alert("El año debe ser mayor a 0");
    if (!autorId) return alert("Selecciona un autor válido");

    const dto = {
        titulo: titulo,
        categoria: categoria,
        anio: parseInt(anioStr),
        autor: autorId 
    };

    try {
        await axios.post(`${apiBase}/Libro`, dto);
        document.getElementById('formLibro').reset();
        await cargarLibros();
    } catch (error) {
        console.error("Error al agregar libro:", error.response?.data || error);
        alert("Error al agregar el libro");
    }
});


function abrirModalEditar(id) {
    const libro = librosGlobal.find(l => l.id_Libro === id);
    if (!libro) return alert("Libro no encontrado");

    libroIdEditando = id;

    document.getElementById('tituloEditar').value = libro.titulo;
    document.getElementById('categoriaEditar').value = libro.categoria;
    document.getElementById('anioEditar').value = libro.anio;
    document.getElementById('autorEditar').value = libro.autor.id_Autor;

    const modal = new bootstrap.Modal(document.getElementById('modalEditar'));
    modal.show();
}

document.getElementById('formModalEditar').addEventListener('submit', async e => {
    e.preventDefault();

    const autorId = parseInt(document.getElementById('autorEditar').value);
    if (!autorId) return alert("Selecciona un autor válido");

    const dto = {
        titulo: document.getElementById('tituloEditar').value.trim(),
        categoria: document.getElementById('categoriaEditar').value.trim(),
        anio: parseInt(document.getElementById('anioEditar').value),
        autor: autorId
    };

    try {
        await axios.put(`${apiBase}/Libro/${libroIdEditando}`, dto);
        libroIdEditando = null;
        bootstrap.Modal.getInstance(document.getElementById('modalEditar')).hide();
        await cargarLibros();
    } catch (error) {
        console.error(error);
        alert("Error al editar el libro");
    }
});

async function eliminarLibro(id) {
    if (!confirm("¿Estás seguro de eliminar este libro?")) return;

    try {
        await axios.delete(`${apiBase}/Libro/${id}`);
        await cargarLibros();
    } catch (error) {
        console.error(error);
        alert("No se pudo eliminar el libro.");
    }
}

document.getElementById('filtroCategoria').addEventListener('input', mostrarLibros);
document.getElementById('filtroAutor').addEventListener('change', mostrarLibros);



cargarAutores().then(() => cargarLibros());
