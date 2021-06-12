using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class LPaginador<T>
    {//clase generica osea podra recibir cualquier tipo de clase copn <>
        //cantidad de resultados por pagina
        private int pagi_cuantos = 8;
        //cantidad de enlases que se mostraran como maximo en la barra de navegacion
        private int pagi_nav_num_enlaces = 3;
        private int pagi_actual;

        //definimos que ira en el enlace a la pagina anterior 
        private String pagi_nav_anterior = " &laquo; Anterior";

        //defini,ops que ira ben enlace de la pagina siguiente 
        private String pagi_nav_siguiente = " Siguiente &raquo; ";

        //definimos que ira en el enlace a la pagina siguiente
        private String pagi_nav_primera = " &laquo; Primero ";
        private String pagi_nav_ultima = "Ultimo &raquo; ";
        private String pagi_navegacion = null;

        //se crea un metodo 

        public object[] paginador(List<T> table, int pagina, int registros, String area, String controller, String action, String host)
        {
            //si la comparacion es 0 se le agregara para que quede en 1 : si da falso
            pagi_actual = pagina == 0 ? 1 : pagina;

            //cuantos registros se podran visualisar en cada pagina si sale mayor sele asigna el valor del parametro osea registro 
            pagi_cuantos = registros > 0 ? registros : pagi_cuantos;

            int pagi_totalReg = table.Count;//count obtener un dato tipo entero y obtendremos la cantidad de elementos de table
            double valor1 = Math.Ceiling((double)pagi_totalReg / (double)pagi_cuantos);//ceilling redondea, se dividen los dos datos 
            //convertir ahora valor1 en dato de tipo entero
            int pagi_totalPags = Convert.ToInt16(Math.Ceiling(valor1));

            //crear los botones de navegacion de las paginas 
            //evaluamos la pagina actual
            if (pagi_actual != 1)
            {
                int pagi_url = 1;//sera el numero de pagina al enlasarnos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url +
                    "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_primera + "</a>";

                //si no estamos en la pagina 1. ponemos el enlace "anterior"
                pagi_url = pagi_actual - 1;//sera el numero de pagina a la que enlazamos 
                pagi_navegacion += "a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos +
                    "&area=" + area + "'>" + pagi_nav_anterior + "</a>";
            }

            //proceso de desarrollo de los enlaces numericos  para navegar 

            //si se definio la variable pagi_nav_num_enlaces
            //calculamos el intervalo para restatar y sumar a partir de la pagina actual
            double valor2 = (pagi_nav_num_enlaces / 2);
            int pagi_nav_intervalo = Convert.ToInt16(Math.Round(valor2));

            //calculamos desde que numero se muestra
            int pagi_nav_desde = pagi_actual - pagi_nav_intervalo;

            //calculamos hasta que numero de pagina se mostrara 
            int pagi_nav_hasta = pagi_actual + pagi_nav_intervalo;

            //tenemos inicio y final de los enlaces numericos////////////////////////////////////////////////////////////////

            //si es un numero negativo 
            if (pagi_nav_desde < 1)
            {
                //le sumamos la cantidad sobrante al final para mantener
                //el numero de enlaces que queremos mostrar.
                pagi_nav_hasta -= (pagi_nav_desde - 1);
                //establecemos pagi desde como 1.
                pagi_nav_desde = 1;
            }
            // si pagi_nav_hasta es un numero maor que el total de paginas
            if (pagi_nav_hasta > pagi_totalPags)
            {
                //le restamos la cantidad que exede para mantener
                //el numero de enlaces que deseamos mostar.
                pagi_nav_desde -= (pagi_nav_hasta - pagi_totalPags);
                //establecemos hasta como el total de pagians 
                pagi_nav_hasta = pagi_totalPags;

                //hacemos una ultima validacion para verificar que al cambiar pagi_nav_desde
                //no haya quedado con un valor no valido 
                if (pagi_nav_desde < 1)
                {
                    pagi_nav_desde = 1;//ya que esta no puede tener un valor negativo 
                }
            }
            //ciclo para poder crear los enlaces numericos 
            for (int pagi_i = pagi_nav_desde; pagi_i <= pagi_nav_hasta; pagi_i++)
            {
                //Desde pagina 1 hasta ultima pagina
                if (pagi_i == pagi_actual)//si esto se cumple es por que estamos en la misma pagina donde se navega 
                {
                    // el boton non tendra funcion si no que solo mostrara el numero de la pagina 
                    pagi_navegacion += "<span class='btn btn-default' disabled='disabled'>" + pagi_i + "</span>";
                }
                else
                {
                    //si es cualquier otro. se escribe el enlase a ese numero de pagina
                    pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action +
                        "?id=" + pagi_i + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_i + "</a>";
                }
            }
            //botones para navegar entre pginas boton siguinte 
            if (pagi_actual < pagi_totalPags)
            {
                //si no estamos en la ultima pagina se pondra el enlace siguiente 
                int pagi_url = pagi_actual + 1;//numero de paginas a enlazar 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros" + pagi_cuantos + "&area=" + area
                    + "'>" + pagi_nav_siguiente + "</a>";

                //si no estamos en la ultima pagina pondremos el enlase "ultima"
                pagi_url = pagi_totalPags;//numero de paginas al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagi_url + "&registros" + pagi_cuantos + "&area=" + area
                    + "'>" + pagi_nav_ultima + "</a>";
            }
            /*
             *Obtencion de los registros que se mostraran en la pagina actual.
             *******************************************************************
             *pondremos desde que resgitro mostraremos 
             *se recuerda que siempre empieza desde CERO.
             */
            int pagi_inicial = (pagi_actual - 1) * pagi_cuantos;

            //consulta Sql para devolucion de cantidad de registros e,pezando de pagi_inicial
            //con el ski obmitiremos los registro que se le asignen en este caso lo que contenga pagina inicial.
            var query = table.Skip(pagi_inicial).Take(pagi_cuantos).ToList();
            String pagi_info = "del <b>" + pagi_actual + "</b> al <b>" + pagi_totalPags + "</b> de <b>" +
                pagi_totalReg + "</b> <b>/" + pagi_cuantos + "</b>";
            object[] data = { pagi_info, pagi_navegacion, query };
            return data;
            //paginador completo 
        }
    }
}
