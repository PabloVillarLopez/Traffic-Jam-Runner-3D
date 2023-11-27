<?php
//Nombre del fichero y texto que le pasaremos desde Unity
$arch = fopen($_POST['archivo'], 'w');
$t = $_POST['texto'];

//Proteccion hack
$t = str_replace("/", "", $t); //Quitar las barras
$t = str_replace(chr(92), "", $t); //Quitar las barras invertidas

fwrite($arch, $t); //Se guarda el archivo con los datos
fclose($arch); //Se cierra el archivo
echo 'Guardado OK';
?>
