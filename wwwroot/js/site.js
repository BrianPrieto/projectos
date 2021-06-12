// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var principal = new Principal();

/*CODIGO DE USUARIO */
var user = new UserFoto();
var imageUser = (evt) => {
    user.archivo(evt, "imageUser");//archivo creado en Uploadpicture en donde userfoto ereda de este y site obtiene de userfoto
}
//se ejecuta siempre que se carge la pagina
$().ready(() => {
    let URLactual = window.location.pathname;//se obtienen solo los parametros de la url
    principal.userLink(URLactual);//para que realice la operacion de principal
});
