
class Principal {
    userLink(URLactual) {
        let url = "";
        let cadena = URLactual.split("/");//split devuelve un array lo divide por /

        for (var i = 0; i < cadena.length; i++) {//va hasta la longitud del array cadena
            if (cadena[i] != "Index") {//se compara y si es distinto 
                url += cadena[i];
            }
        }
        switch (url) {
            case "UsersRegistro":// el erro que casi me hace llorar
                document.getElementById('files').addEventListener('change', imageUser, false);/*funcion del archivo site*/
                break;
        }
    }
}   