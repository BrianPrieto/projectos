//se crea una clase

class Uploadpicture {
    //metodo
    archivo(evt, id) {//evento se usa para cargar lña imagen del escriptorio
        let files = evt.target.files; //files es el object tarlled obtiene todo la informacion
        let f = files[0];

        //condi
        if (f.type.match('image.*')) {//type verifica que tipo de archivo se carga y se dice que los archivos tipo imagen se reciben
            let reader = new FileReader();//file reader = leer un archivo
            reader.onload = ((thefile) => {
                return (e) => {
                    //sele asigna el id que tenemos en eñ register.html en este caso imagenUser y se obtiene el elemnto lo que aremos sera remplazar por la imagen
                    document.getElementById(id).innerHTML = ['<img class="imageUser" src="',
                        e.target.result, '" title="', escape(thefile.name), '"/>'].join('');//join para unir
                }
            })(f);//onload funcion para cargar la iagen 
            reader.readAsDataURL(f);//leer informacion
        } 
    }
}